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
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Interactions.Internal;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace SimpleSelenium
{
    struct TestWindow
    {
        public TestWindow(string Name, string Handle, int Ordinal)
        {
            name = Name;
            title = String.Empty;
            handle = Handle;
            ordinal = Ordinal;
        }
        public string name;
        public string title;
        public string handle;
        public int ordinal;
    }

  public enum DriverType { IE=1, Firefox=2, Chrome=4 }

    public class TestDriver
    {
      Dictionary<string, TestWindow> _windows = new Dictionary<string, TestWindow>();
      string _currentWindowName = String.Empty;
      IWebDriver _driver;
      IJavaScriptExecutor _js;
      string _driverName;
      int _clickCount = 0;
      int _windowOrdinal = 0;

      public TestDriver(string Name, DriverType DriverType)
      {
        //_cacheName = ComposeDriverName(Name, DriverType);
        _driverName = Name;
        _driver = CreateDriver(DriverType);
        _js = _driver as IJavaScriptExecutor;
      }

      //public static string ComposeDriverName(string Name, DriverType DriverType)
      //{
      //  return string.Format("{0}::{1}", Name, DriverType);
      //}

      //public static string DecomposeDriverName(string CacheName)
      //{
      //  string[] parseList = CacheName.Split(new string[] { "::" }, StringSplitOptions.None);
      //  return parseList[0];
      //}

      //public static DriverType DecomposeDriverType(string CacheName)
      //{
      //  string[] parseList = CacheName.Split(new string[] { "::" }, StringSplitOptions.None);
      //  return (DriverType)Enum.Parse(typeof(DriverType), parseList[1]);
      //}

      // TODO: add a way to set options on the driver
      private static IWebDriver CreateDriver(DriverType DriverType)
      {
        IWebDriver driver = null;

        switch (DriverType)
        {
          case DriverType.Chrome:
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("test-type");
            options.AddArgument("ignore-certificate-errors");
            options.AddArgument("--window-size=1024,768");
            driver = new ChromeDriver(options);
            break;
          case DriverType.Firefox:
            driver = new FirefoxDriver();
            break;
          default:
            InternetExplorerOptions option = new InternetExplorerOptions();
            //option.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
            driver = new InternetExplorerDriver(option);
            break;
        }

        driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
        return driver;
      }

      public IJavaScriptExecutor JSExec { get { return _js; } }

      public int clickCount { get { return _clickCount; } }

      public string driverName { get { return _driverName;  } }

      public int windowCount { get { return _driver.WindowHandles.Count;  } }

      public string windowName { get { return _currentWindowName; } }

        #region Public Methods

        public void BrowseTo(string Url)
        {
            _driver.Navigate().GoToUrl(Url);
            SyncWindows();
        }

        public void Cleanup()
        {
            _driver.Quit();
        }

        public void Click(By FindBy)
        {
            IWebElement elem = FindElement(FindBy);
            new Actions(_driver).MoveToElement(elem).Click().Perform();
            _clickCount += 1;
            SyncWindows();
        }

        public void DblClick(IWebElement Element)
        {
            new Actions(_driver).MoveToElement(Element).DoubleClick().Perform();
            _clickCount += 1;
            SyncWindows();
        }

        public void CountClick()
        {
            _clickCount += 1;
        }

        public void ExecuteScript(string Script)
        {
            _js.ExecuteScript(Script);
        }

        public void ExitFrame()
        {
            _driver.SwitchTo().DefaultContent();
        }

        public IWebElement FindElement(By FindBy)
        {
            WaitForElement(FindBy);
            return _driver.FindElement(FindBy);
        }

        public void FindFrame(string FrameName)
        {
            _driver.SwitchTo().DefaultContent();
            _driver.SwitchTo().Frame(FrameName);
            WaitForFrameLoad(FrameName);
        }

        public void FindFrame(int Index)
        {
            _driver.SwitchTo().Frame(Index);
            // TODO: implement wait
            //WaitForFrameLoad();
        }

        public void FindWindow(string WindowName, int VerifyCount)
        {
            string currentHandle = _driver.CurrentWindowHandle;
            WaitForWindowCount(VerifyCount);
            SyncWindows(new List<string>());

            foreach (KeyValuePair<string, TestWindow> kvp in _windows)
            {
                _driver.SwitchTo().Window(kvp.Key);
                if (_driver.Title.Contains(WindowName) && kvp.Key != currentHandle) break;
            }

            WaitForPageLoad();
        }

        public void FindWindow(int Index, int VerifyCount)
        {
            WaitForWindowCount(VerifyCount);
            SyncWindows(new List<string>());
            int index = (Index < 0) ? _windows.Count - 1 : Index;
            _driver.SwitchTo().Window(_windows.ElementAt(index).Key);
            WaitForPageLoad();
        }

        public string GetAttribute(By FindBy, string AttributeName)
        {
            IWebElement elem = FindElement(FindBy);
            return elem.GetAttribute(AttributeName);
        }

        public bool GetGridRowCount(string Command, int RowNo)
        {
          int rowCount; 
          string result = _js.ExecuteScript(Command).ToString();
          if (!int.TryParse(result, out rowCount)) rowCount = 0;
          return (rowCount >= RowNo);
        }

        public void GridDblClick(TestElement Element, int RowNo)
        {
            string command = String.Format("{0}_LfDblClick(1, {1}, 1, 1, 0)", Element.itemName, RowNo);
            WaitForGridLoad(Element.itemName, RowNo);
            _js.ExecuteScript(command);
        }

        public void GridDblClick(TestElement Element, string SearchColumns, string SearchValues)
        {
            string action = string.Format("{0}_LfDblClick(1, rowIndex, 1, 1, 0)", Element.itemName);
            string command = JSFindGridRow(Element.itemName, SearchColumns, SearchValues, action);
            WaitForGridLoad(Element.itemName, 0);
            _js.ExecuteScript(command);  
        }

        public void GridRtClick(TestElement Element, int RowNo)
        {
            int colPos = (RowNo < 0) ? RowNo : 1;
            string command = String.Format("{0}_RtClick(1, {1}, {2}, {2}, 0)", Element.itemName, RowNo, colPos);
            if(RowNo >= 0) WaitForGridLoad(Element.itemName, RowNo);
            _js.ExecuteScript(command);
        }

        public void GridRtClick(TestElement Element, string SearchColumns, string SearchValues)
        {
            string action = String.Format("var colPos = (rowIndex < 0) ? rowIndex : 1; {0}_RtClick(1, rowIndex, colPos, colPos, 0)", Element.itemName);
            string command = JSFindGridRow(Element.itemName, SearchColumns, SearchValues, action);
            WaitForGridLoad(Element.itemName, 0);
            _js.ExecuteScript(command);
        }

        public INavigation Navigate()
        {
            return _driver.Navigate();
        }

        public void NavigateMenu(List<By> MenuItems)
        {
            for (int i = 0; i < MenuItems.Count - 1; i++)
            {

                if (!FindElement(MenuItems[i + 1]).Displayed)
                {
                    FindElement(MenuItems[i]).Click();
                }
            }

            FindElement(MenuItems[MenuItems.Count - 1]).Click();
            SyncWindows();
        }

        public void SelectItem(By FindBy, string SelectThis, bool UseValue)
        {
            IWebElement elem = FindElement(FindBy);
            SelectElement select = new SelectElement(elem);
            if (UseValue)
            {
                select.SelectByValue(SelectThis);
            }
            else
            {
                select.SelectByText(SelectThis);
            }
        }

        public void SendKeys(By FindBy, string KeysToSend, bool ClearField = true, int VerifyCount = -1)
        {
            IWebElement elem = FindElement(FindBy);
            if (ClearField) elem.Clear();
            elem.SendKeys(KeysToSend);
            if (VerifyCount >= 0) SyncWindows();
        }

        public void TabOut(By FindBy, string KeysToSend = "", bool ClearField = true, int VerifyCount = -1)
        {
            IWebElement elem = FindElement(FindBy);
            if (ClearField) elem.Clear();
            Actions actions = new Actions(_driver).MoveToElement(elem);

            if (KeysToSend != "") actions = actions.SendKeys(KeysToSend);
            actions.SendKeys(Keys.Tab).Perform();

            if (VerifyCount >= 0) SyncWindows();
        }

        public void WaitForPageLoad()
        {
            WebDriverWait wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 0, 60));
            wait.Until(driver1 => _js.ExecuteScript("return document.readyState").Equals("complete"));
        }

        #endregion

      public void CloseWindow()
      {
        _driver.Close();
      }

      public void SyncWindows(List<string> WindowNames=null)
      {
          string windowName;
          WebDriverWait wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 0, 60));
          Dictionary<string, TestWindow> windows = new Dictionary<string, TestWindow>(_windows);

          if (WindowNames == null) WindowNames = new List<string>();

          // remove window referencs for windows that are no longer open
          foreach (KeyValuePair<string, TestWindow> kvp in windows)
          {
              if (!_driver.WindowHandles.Contains(kvp.Value.handle)) _windows.Remove(kvp.Key);
          }

          // process each window handle the driver is currently holding
          if (_driver.WindowHandles.LastOrDefault() != null) _driver.SwitchTo().Window(_driver.WindowHandles.LastOrDefault());
          string currentHandle = _driver.CurrentWindowHandle;
          List<string> driverHandles = _driver.WindowHandles.ToList<string>();

          foreach (string handle in driverHandles)
          {
              // add window references
              if (!_windows.Values.Any(v => v.handle == handle))
              {
                  if (WindowNames.Count > 0)
                  {
                      windowName = WindowNames[0];
                      WindowNames.RemoveAt(0);
                  }
                  else
                  {
                      windowName = handle;
                  }

                  _windows.Add(windowName, new TestWindow(windowName, handle, _windowOrdinal));
                  _windowOrdinal += 1;
              }

              KeyValuePair<string, TestWindow> kvp = _windows.Where(w => w.Value.handle == handle).FirstOrDefault();
              string key = kvp.Key;
              TestWindow window = kvp.Value;

              // update windows that are missing a user-defined title or name
              if (window.handle == window.name || window.title == String.Empty)
              {
                  // switch to the window if it is still open and not the current window
                  string thisHandle = _driver.CurrentWindowHandle;
                  if (_driver.CurrentWindowHandle != handle)
                  {
                      if (!_driver.WindowHandles.Contains(handle)) continue;
                      wait.Until(d => WaitForWindow(handle));
                      //_driver.SwitchTo().Window(handle);
                  }

                  if (_driver.CurrentWindowHandle == handle)
                  {
                      window.title = _driver.Title;
                      if (window.handle == window.name) window.name = window.title;
                      _windows[key] = window;
                  }
              }
          }

          if (_driver.CurrentWindowHandle != currentHandle) _driver.SwitchTo().Window(currentHandle);
      }

      protected static string JSFindGridRow(string GridName, string SearchColumns, string SearchValues, string ActionScript)
      {
          string[] script = new string[]{
              string.Format("var colArg = '{0}';", SearchColumns),
              string.Format("var datArg = '{0}';", SearchValues),
              "",
              "// parse column and data args",
              "var colNames = colArg.split('|');",
              "var data = datArg.split('|');",
              "var colIndexes = [];",
              "var rowIndex = -1;",
              "",
              "// gets table and its columns",
              "var tbl = document.getElementById('DataTable1');",
              "var col = tbl.ColumnSet;",
              "",
              "// gets the indexes of the columns to search",
              "for(var i=0; i<col.Count; i++){",
              "  if(colNames.indexOf(col(i).Name) >= 0) colIndexes.push(i);",
              "}",
              "",
              "// check each row for matching data and return the row index of the first match",
              "for(rowIndex=0; rowIndex<tbl.RowSet.Count; rowIndex++){",
              "  var result = true;",
              "",
              "  // check each search column for the corresponding data",
              "  for(var i=0; i<colIndexes.length; i++){",
              "    var colIndex = colIndexes[i];",
              "    var cell = tbl.CellSet.Item(rowIndex, colIndex).Text;",
              "    var compareVal = data[i].toString();",
              "    result = (result && (cell == compareVal));",
              "  }",
              "",
              "  if(result) break;",
              "}",
              "",
              "if(!result) rowIndex = -1;",
              ActionScript,
          };

          return string.Join("\r\n", script);
      }

      protected void WaitForElement(By FindBy)
      {
          WebDriverWait wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 60));
          wait.Until(ExpectedConditions.ElementExists(FindBy));
          // TODO: removed temporarily for troubleshooting
          //wait.Until(ExpectedConditions.ElementIsVisible(FindBy));
          wait.Until(d => d.FindElement(FindBy).Enabled);
      }

        public void WaitForFrameLoad(string FrameName)
        {
            //// TODO: implement this
            //string script = String.Format("var elem = document.getElementById('{0}'); return elem.readyState", FrameName);
            //WebDriverWait wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 0, 60));
            //wait.Until(d => _js.ExecuteScript(script).Equals("complete"));
        }

        protected void WaitForGridLoad(string DataTableName, int RowNo)
        {
          WebDriverWait wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 0, 60));
          string command = String.Format("return {0}.ColumnSet.Count;", DataTableName);
          wait.Until(d => { return GetGridRowCount(command, RowNo); });
        }

        protected bool WaitForWindow(string handle)
        {
            bool result = true;
            //if (_driver.CurrentWindowHandle == handle) return true;

            try
            {
                _driver.SwitchTo().Window(handle);
            }
            catch (NoSuchWindowException e)
            {
                result = false;
            }

            return result;
        }

        protected void WaitForWindowCount(int VerifyCount)
        {
            if (VerifyCount < 0) return;
            string joinedHandles = String.Join(";", _driver.WindowHandles.ToArray<string>());
            int matchCount = 0;
            Thread.Sleep(300);
            WebDriverWait wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 0, 60));
            wait.Until(d => { matchCount = (joinedHandles == String.Join(";", _driver.WindowHandles.ToArray<string>())) ? matchCount + 1 : 0; return (matchCount == 3); });
            wait.Until(d => { return (d.WindowHandles.Count == VerifyCount); });
        }

        public string WindowDump()
        {
            StringBuilder sb = new StringBuilder();
            string currentHandle = _driver.CurrentWindowHandle;

            foreach (string handle in _driver.WindowHandles)
            {
                try
                {
                    _driver.SwitchTo().Window(handle);
                }
                catch (Exception e) { }

                sb.AppendFormat("Handle: {0};  Title: {1}; Current: {2}; ", handle, _driver.Title, (handle == currentHandle));
            }

            return sb.ToString();
        }

        //protected string WindowHandlesMD5()
        //{
        //    byte[] bytes;
        //    MD5 md5 = new MD5CryptoServiceProvider();
        //    StringBuilder sb = new StringBuilder();
        //    string windowHandlesMD5 = string.Empty;

        //    foreach (string handle in _driver.WindowHandles.ToList<string>())
        //    {
        //        sb.Append(handle);
        //    }

        //    if (sb.Length > 0)
        //    {
        //        bytes = md5.ComputeHash(Encoding.ASCII.GetBytes(sb.ToString()));
        //        windowHandlesMD5 = bytes.ToString();
        //    }

        //    return windowHandlesMD5;
        //}

    }
}