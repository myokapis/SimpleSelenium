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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Interactions.Internal;
using OpenQA.Selenium.Internal;

// TODO: add a way to cleanup all drivers and browsers (be sure to wrap for errors)
namespace SimpleSelenium
{
    public class TestViewer
    {
      Dictionary<string, TestDriver> _testDrivers = new Dictionary<string, TestDriver>();
      string _currentTestDriverName = String.Empty;
      string _testPortName;
      Cache _cache = Cache.GetInstance();
      List<int> _initBrowsers;
      List<int> _initDrivers;
      List<int> _currBrowsers;
      List<int> _currDrivers;
      private static List<string> _browserProcessNames = new List<string>() { "iexplore", "firefox", "chrome" };
      // TODO: enhance this for firefox and chrome
      private static List<string> _driverProcessNames = new List<string>() { "IEDriverServer" };

      public TestViewer(string Name, int DriversBitMask)
      {
        _testPortName = Name;
        _initBrowsers = GetBrowserProcesses();
        _initDrivers = GetDriverProcesses();
        CreateDrivers(DriversBitMask);
        _currentTestDriverName = _testDrivers.OrderBy(d => d.Key).Select(s => s.Key).First();
        _currBrowsers = GetBrowserProcesses();
        _currDrivers = GetDriverProcesses();
      }

      public TestDriver testDriver{ get{ return _testDrivers[_currentTestDriverName]; } }

      public string testDriverName { get { return _currentTestDriverName; } }

      public string testPortName { get { return _testPortName; } }

      public static List<int> GetBrowserProcesses()
      {
        Process[] processes = Process.GetProcesses();
        return processes.Join(_browserProcessNames, p => p.ProcessName, name => name, (p, name) => p).Select(p => p.Id).ToList<int>();
      }

      public static List<int> GetDriverProcesses()
      {
        Process[] processes = Process.GetProcesses();
        return processes.Join(_driverProcessNames, p => p.ProcessName, name => name, (p, name) => p).Select(p => p.Id).ToList<int>();
      }
      
      public void Cleanup()
      {

        foreach (KeyValuePair<string, TestDriver> kvp in _testDrivers)
        {
          kvp.Value.Cleanup();
        }

        _testDrivers.Clear();

        List<int> nowBrowsers = GetBrowserProcesses();
        List<int> nowDrivers = GetDriverProcesses();
        List<int> browsers = nowBrowsers.Intersect(_currBrowsers).Except(_initBrowsers).ToList<int>();
        List<int> drivers = nowDrivers.Intersect(_currDrivers).Except(_initDrivers).ToList<int>();
        List<int> processes = browsers.Union(drivers).ToList<int>();

        foreach (int pid in processes)
        {
          try
          {
            Process process = Process.GetProcessById(pid);
            if (process != null) process.Kill();
          }
          catch (Exception e)
          {
            // ignore the error
          }
        }

      }

      protected void CreateDrivers(int Drivers)
      {
        DriverType driverType;
        TestDriver driver;
          
        if ((Drivers & 2) == 2)
        {
          driverType = DriverType.Firefox;
          driver = new TestDriver(_testPortName, driverType);
          _testDrivers.Add(driver.driverName, driver);
        }

        if ((Drivers & 4) == 4)
        {
          driverType = DriverType.Chrome;
          driver = new TestDriver(_testPortName, driverType);
          _testDrivers.Add(driver.driverName, driver);
        }

        if (((Drivers & 1) == 1) | (_testDrivers.Count == 0))
        {
          driverType = DriverType.IE;
          driver = new TestDriver(_testPortName, driverType);
          _testDrivers.Add(driver.driverName, driver);
        }
      }

    }
}
