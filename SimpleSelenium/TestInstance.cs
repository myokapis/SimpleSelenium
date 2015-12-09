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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace SimpleSelenium
{
    public class TestInstance
    {
      string _testInstanceName;
      string _currentContext;
      Dictionary<string, TestViewer> _testBrowsers = new Dictionary<string, TestViewer>();
      string _currentTestViewName = String.Empty;
      List<TestDetail> _testDetails;
      List<TestDetail> _menuDetails;
      Dictionary<string, List<TestDetail>> _macros = new Dictionary<string, List<TestDetail>>();
      List<TestDetail> _currentDetails;
      Cache _cache = Cache.GetInstance();
      Regex _nameRegex = new Regex("^\x7B.+\x7D$");

        public TestInstance(string TestName)
        {
          _testInstanceName = TestName;
          _currentContext = TestName;
          _menuDetails = Loader.LoadMenu(string.Empty);
          _testDetails = Loader.LoadTest(TestName);
          _currentDetails = _testDetails;
          Console.WriteLine("got here 1");
          CreateViews();
          Console.WriteLine("got here 2");
          _currentTestViewName = _testBrowsers.FirstOrDefault().Key;
          Console.WriteLine("got here 3");
        }

        public string testInstanceName { get { return _testInstanceName; } }

        public TestViewer testView { get { return _testBrowsers[_currentTestViewName]; } }

        public string testViewName { get { return _currentTestViewName; } }

        public void Cleanup()
        {
          foreach (TestViewer view in _testBrowsers.Values)
            {
                view.Cleanup();
            }
        }

        public List<TestDetail> Commands()
        {
            // TODO: replace literals with a constant or enum
            return _currentDetails.Where(a => a.type == "command" && a.category == _currentContext).ToList<TestDetail>();
        }

        public TestElement Find(string FindId)
        {
          string alias;
          string id;
          string name;

          if(_nameRegex.Match(FindId).Success)
          {
            id = "";
            alias = FindId.Substring(1, FindId.Length - 2);
            name = alias;
          }
          else
          {
            alias = FindId;
            id = FindId;
            name = "";
          }

          TestDetail testDetail = _testDetails.Where(a => a.category == _testInstanceName && a.type == "element" && a.alias == alias).FirstOrDefault<TestDetail>();

          if (testDetail == null || testDetail.element == null)
          {
              testDetail = TestDetail.Create("Element", _testInstanceName, alias, id, name);
              _testDetails.Add(testDetail.element);
          }

          return testDetail.element;
        }

        public TestMenu FindMenu(string Owner, string FindId)
        {
            TestMenu menu = null;
            TestDetail detail = _menuDetails.Where(a => a.category == Owner && a.type == "menu" && a.alias == FindId).FirstOrDefault<TestDetail>(); 
            if (detail != null) menu = detail.menu;

            if (menu == null)
            {
              menu = TestDetail.Create("Menu", Owner, FindId, FindId).menu;
              _testDetails.Add(menu);
            }

            return menu;
        }

        public void RunMacro(string MacroName, TestRunner FuncPointer)
        {
            _currentContext = MacroName;
            if (!_macros.ContainsKey(MacroName)) _macros.Add(MacroName, Loader.LoadMacro(MacroName));
            _currentDetails = _macros[MacroName];

            FuncPointer();

            _currentContext = _testInstanceName;
            _currentDetails = _testDetails;
        }

        protected void CreateViews()
        {
          TestViewer browser;
          List<TestDetail> details = _testDetails.Where(a => a.type == "view" && a.category == _testInstanceName).ToList<TestDetail>();

          Console.WriteLine(string.Format("detail count: {0}", details.Count));

          foreach (TestDetail detail in details)
          {
            if (_cache.HasViewer(detail.alias))
            {
              browser = _cache.GetViewer(detail.alias);
            }
            else
            {
              browser = new TestViewer(detail.alias, detail.view.driverBitMask);
              _cache.AddViewer(browser);
            }

            _testBrowsers.Add(detail.alias, browser);
          }
        }


    }
}
