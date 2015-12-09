/* ***************************************************************************************************
 * Copyright (c) 2015 Gene Graves
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
 * associated documentation files (the "Software"), to deal in the Software without restriction, 
 * including without limitation the rights to use, copy, modify, merge, publish, distribute, 
 * sublicense, and/or sell copies of the Software, and to permit persons to whom the Software 
 * is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial 
 * portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT 
 * NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 ************************************************************************************************** */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Interactions.Internal;
using OpenQA.Selenium.Internal;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using NUnit.Framework;

namespace SimpleSelenium
{
  // TODO: consolidate this with the list in TestDrivers
    public enum WebDrivers : int { IE=1, Firefox=2, Chrome=4 };

    public delegate void TestRunner();

    public class Core
    {
        protected const int LAST_WINDOW = -1;
        protected const int MAIN_WINDOW = 0;
        protected const int NO_VAL = -1;
        Dictionary<string, TestInstance> _testInstances = new Dictionary<string, TestInstance>();
        protected string _currentTestInstanceName = String.Empty;
        protected string[] _defaultWindows = new string[0];
        protected static Dictionary<string, string> _supportedCommands = new Dictionary<string, string>() { {"BrowseTo", "Browse To"}, 
            {"Click", "Click"}, {"DblClick", "Double Click"}, {"ExitFrame", "Exit IFrame"}, {"ExecuteScript", "Execute Script"},
            {"FindFrame", "Find Frame"}, {"FindWindow", "Find Window"}, {"GridDblClick", "Grid Double Click"},
            {"GridRtClick", "Grid Right Click"}, {"NavigateMenu", "Navigate Menu"}, {"RunMacro", "Run Macro"}, {"SendKeys", "Send Keys"},
            {"Sleep", "Sleep"}, {"TabOut", "Send Keys & Tab Out"}};

        public Core()
        {
        }

        #region Properties

        public static Dictionary<string, string> SupportedCommands { get { return _supportedCommands; } }

        public IJavaScriptExecutor JSExec { get { return testDriver.JSExec; }}

        public TestDriver testDriver { get { return testPort.testDriver; } }

        public string testDriverName { get { return testPort.testDriverName; } }

        public TestInstance testInstance { get { return _testInstances[_currentTestInstanceName]; } }

        public TestViewer testPort { get { return _testInstances[_currentTestInstanceName].testView; } }

        public string testPortName { get { return _testInstances[_currentTestInstanceName].testViewName; } }

        //public string window { get { return testDriver.window;  } }

        public string windowName { get { return testDriver.windowName; } }

        #endregion

        #region Setup and Teardown Methods

        public void ExecuteLogin([CallerMemberName] string TestName = "")
        {
          try
          {
            TestInstance testInstance = new TestInstance(TestName);
            Log(string.Format("TestName2: {0}", TestName));
            _testInstances.Add(TestName, testInstance);
            _currentTestInstanceName = TestName;
            RunTests();
          }
          catch (Exception e)
          {
            // TODO: revamp this later
            //if (_testInstances != null)
            //{
            //  TestInstance testInstance = null;
            //  if (_testInstances.ContainsKey(_currentTestInstanceName)) testInstance = _testInstances[_currentTestInstanceName];

            //  if (testInstance != null)
            //  {
            //    if (testInstance.testView != null)
            //    {
            //      if (testInstance.testView.testDriver != null)
            //      {
            //        TestDriver driver = testInstance.testView.testDriver;
            //        Log(driver.WindowDump());
            //      }
            //    }

            //    testInstance.Cleanup();
            //  }
            //}

            Log(String.Format("Test, {0}, failed with an error: {1}", TestName, e.Message));
            Assert.Fail();
          }
        }

        public void ExecuteTest(Action Assertions, [CallerMemberName] string TestName = "")
        {
            try
            {
                TestInstance testInstance = new TestInstance(TestName);
                _testInstances.Add(TestName, testInstance);
                _currentTestInstanceName = TestName;

                RunTests();
                Assertions();
            }
            catch (Exception e)
            {
                if (_testInstances != null)
                {
                    TestInstance testInstance = _testInstances[_currentTestInstanceName];
                    if (testInstance.testView != null)
                    {
                        if (testInstance.testView.testDriver != null)
                        {
                            TestDriver driver = testInstance.testView.testDriver;
                            Log(driver.WindowDump());
                        }
                    }
                }
                Log(String.Format("Test, {0}, failed with an error: {1}", TestName, e.Message));
                Assert.Fail();
            }
          // TODO: add a command for cleaning up drivers and windows
            //finally
            //{
            //    CleanupTest(TestName);
            //}
        }

        //public void Cleanup()
        //{
        //  foreach (TestInstance testInstance in _testInstances.Values)
        //  {
        //    testInstance.Cleanup();
        //  }
        //}

        //public void CleanupTest([CallerMemberName] string TestName = "")
        //{
        //    if (_testInstances.ContainsKey(TestName)) _testInstances[TestName].Cleanup();
        //}
        
        public TestInstance InitializeTest([CallerMemberName] string TestName="")
        {
            
            TestInstance testInstance = new TestInstance(TestName);
            _testInstances.Add(TestName, testInstance);
            _currentTestInstanceName = TestName;
            return testInstance;
        }

        public static void Teardown()
        {
          Cache cache = Cache.GetInstance();

          foreach (TestViewer viewer in cache.Viewers)
          {
            viewer.Cleanup();
            cache.RemoveViewer(viewer.testPortName);
          }
        }

        #endregion

        # region Convenience Methods

        public void BrowseTo(string Url)
        {
          Log(String.Format("Browse to: {0}", Url));
          testDriver.BrowseTo(Url);
        }

        public void Click(string FindId)
        {
          Log(String.Format("Click element: {0}", FindId));
          TestElement element = Find(FindId);
          testDriver.Click(element.findBy);
        }

        public void CloseWindow(string WindowName)
        {
          Log(String.Format("Closing window: {0}", WindowName));
          testDriver.CloseWindow();
        }

        public void DblClick(string FindId)
        {
            Log(String.Format("Click element: {0}", FindId));
            TestElement element = Find(FindId);
            IWebElement webElement = FindWebElement(element.findBy);
            testDriver.DblClick(webElement);
        }

        public void ExecuteScript(string Script)
        {
            Log(String.Format("Execute script: {0}", Script));
            testDriver.ExecuteScript(Script);
        }

        public void ExitFrame()
        {
            Log("Exit iframe");
            testDriver.ExitFrame();
        }

        public void FindFrame(string FrameName)
        {
            Log(String.Format("Switch to frame by name: {0}", FrameName));
            // TODO: change this to use FindId
            testDriver.FindFrame(FrameName);
        }

        public void FindFrame(int Index)
        {
            Log(String.Format("Switch to frame by index: {0}", Index));
            testDriver.FindFrame(Index);
        }

        public void FindWindow(string WindowName, int VerifyCount = -1)
        {
            Log(String.Format("Find window by name: {0}", WindowName));
            // TODO: change to use FindId
            testDriver.FindWindow(WindowName, VerifyCount);
        }

        public void FindWindow(int Index, int VerifyCount = -1)
        {
            Log(String.Format("Find window by index: {0}", Index));
            // TODO: change to use FindId
            testDriver.FindWindow(Index, VerifyCount);
        }

        public string GetAttribute(string FindId, string AttributeName)
        {
            Log(String.Format("Get attribute: {0}; Element: {1}", FindId, AttributeName));
            TestElement element = Find(FindId);
            return testDriver.GetAttribute(element.findBy, AttributeName);
        }

        public void GridDblClick(string FindId, int RowNo)
        {
            Log(String.Format("Dbl click grid row: {1}; Element: {0}", FindId, RowNo));
            TestElement element = Find(FindId);
            testDriver.GridDblClick(element, RowNo);
        }

        public void GridDblClick(string FindId, string SearchColumns, string SearchValues)
        {
            Log(String.Format("Dbl click grid row: Search columns: {1}; Search values: {2}; Element: {0}", FindId, SearchColumns, SearchValues));
            TestElement element = Find(FindId);
            testDriver.GridDblClick(element, SearchColumns, SearchValues);
        }

        public void GridRtClick(string FindId, int RowNo)
        {
            Log(String.Format("Right click grid row: {1}; Element: {0}", FindId, RowNo));
            TestElement element = Find(FindId);
            testDriver.GridRtClick(element, RowNo);
        }

        public void GridRtClick(string FindId, string SearchColumns, string SearchValues)
        {
            Log(String.Format("Right click grid row: Search columns: {1}; Search values: {2}; Element: {0}", FindId, SearchColumns, SearchValues));
            TestElement element = Find(FindId);
            testDriver.GridRtClick(element, SearchColumns, SearchValues);
        }

        public void NavigateMenu(string MenuSequence)
        {
            Log(String.Format("Navigate menu: {0}", MenuSequence));
            List<By> menuFindBy = new List<By>();
            List<string> menuOwner = new List<string>();

            foreach (string seq in GetParams(MenuSequence))
            {
                List<string> owner = new List<string>(menuOwner);
                owner.Insert(0, "Menu");
                TestMenu menu = FindMenu(string.Join("|", owner), seq);
                menuFindBy.Add(menu.findBy);
                menuOwner.Add(seq);
            }
            
            testDriver.NavigateMenu(menuFindBy);
        }

        public void RunMacro(string MacroName)
        {
            TestRunner testRunner = new TestRunner(RunTests);
            testInstance.RunMacro(MacroName, testRunner);
        }

        public void RunTests()
        {
            List<TestDetail> commands = testInstance.Commands();
            bool argBool = true;
            int argInt;
            int argInt2;

            foreach (TestCommand command in commands)
            {
                switch (command.name)
                {
                    case "BrowseTo":
                        BrowseTo(command.target);
                        break;

                    case "Click":
                        Click(command.target);
                        break;

                  case "CloseWindow":
                    argInt = (command.arg1 == String.Empty) ? -1 : int.Parse(command.arg1);
                        
                    if (command.target == "LAST_WINDOW")
                    {
                      FindWindow(LAST_WINDOW, argInt);
                      CloseWindow(command.target);
                    }
                    else if (command.target == "MAIN_WINDOW")
                    {
                      FindWindow(MAIN_WINDOW, argInt);
                      CloseWindow(command.target);
                    }
                    else if (int.TryParse(command.target, out argInt2))
                    {
                      FindWindow(argInt2, argInt);
                      CloseWindow(command.target);
                    }
                    else
                    {
                      FindWindow(command.target, argInt);
                      CloseWindow(command.target);
                    }
                    break;

                    case "DblClick":
                        DblClick(command.target);
                        break;

                    case "ExecuteScript":
                        ExecuteScript(command.target);
                        break;

                    case "ExitFrame":
                        ExitFrame();
                        break;

                    case "FindFrame":
                        FindFrame(command.target);
                        break;

                    case "FindWindow":
                        argInt = (command.arg1 == String.Empty) ? -1 : int.Parse(command.arg1);
                        
                        if (command.target == "LAST_WINDOW")
                        {
                            FindWindow(LAST_WINDOW, argInt);
                        }
                        else if (command.target == "MAIN_WINDOW")
                        {
                            FindWindow(MAIN_WINDOW, argInt);
                        }
                        else if (int.TryParse(command.target, out argInt2))
                        {
                            FindWindow(argInt2, argInt);
                        }
                        else
                        {
                            FindWindow(command.target, argInt);
                        }
                        break;

                    case "GridDblClick":
                        if (command.arg2 == string.Empty)
                        {
                            argInt = (command.arg1 == String.Empty) ? 1 : int.Parse(command.arg1);
                            GridDblClick(command.target, argInt);
                        }
                        else
                        {
                            GridDblClick(command.target, command.arg1, command.arg2);
                        }
                        break;

                    case "GridRtClick":
                        if (command.arg2 == string.Empty)
                        {
                            argInt = (command.arg1 == String.Empty) ? 1 : int.Parse(command.arg1);
                            GridRtClick(command.target, argInt);
                        }
                        else
                        {
                            GridRtClick(command.target, command.arg1, command.arg2);
                        }
                        break;

                    case "NavigateMenu":
                        NavigateMenu(command.target);
                        break;

                    case "RunMacro":
                        RunMacro(command.target);
                        break;

                    case "SendKeys":
                        argBool = (command.arg2 == string.Empty) ? true : bool.Parse(command.arg2);                       
                        SendKeys(command.target, command.arg1, argBool);
                        break;

                    case "Sleep":
                        argInt = int.Parse(command.target);
                        Thread.Sleep(argInt);
                        break;

                    case "TabOut":
                        argBool = (command.arg2 == string.Empty) ? true : bool.Parse(command.arg2);
                        TabOut(command.target, command.arg1, argBool);
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public void SelectItem(string FindId, string SelectThis, bool UseValue)
        {
            Log(String.Format("Select this: {0}; Element: {1}", FindId, SelectThis));
            TestElement element = Find(FindId);
            testDriver.SelectItem(element.findBy, SelectThis, UseValue);
        }

        public void SendKeys(string FindId, string Keys, bool ClearField = true, int VerifyCount = -1)
        {
            Log(String.Format("Send keys: {1}; Element: {0}", FindId, Keys));
            TestElement element = Find(FindId);
            testDriver.SendKeys(element.findBy, Keys, ClearField, VerifyCount);
        }

        public void TabOut(string FindId, string Keys = "", bool ClearField = true, int VerifyCount = -1)
        {
            Log(String.Format("Send keys and tab out: {1}; Element: {0}", FindId, Keys));
            TestElement element = Find(FindId);
            testDriver.TabOut(element.findBy, Keys, ClearField, VerifyCount);
        }

        public void WaitForPageLoad()
        {
            testDriver.WaitForPageLoad();
        }

        # endregion

        #region Protected Methods

        protected TestElement Find(string FindId)
        {
            return testInstance.Find(FindId);
        }

        protected TestMenu FindMenu(string Owner, string FindId)
        {
            return testInstance.FindMenu(Owner, FindId);
        }

        protected IWebElement FindWebElement(By FindBy)
        {
            return testDriver.FindElement(FindBy);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }

        protected List<string> GetParams(string ParamString)
        {
            string[] delimiters = { "|" };
            string[] names = ParamString.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            return names.ToList<string>();
        }

        protected void Log(string Message)
        {
            Console.WriteLine(Message);
        }

        #endregion
    }
}