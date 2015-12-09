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

namespace SimpleSelenium
{
  public sealed class Cache
  {
    private static readonly Cache instance = new Cache();
    private static Dictionary<string, TestViewer> _viewerCache = new Dictionary<string, TestViewer>();

    static Cache()
    {
    }

    private Cache()
    {
    }

    public static Cache GetInstance()
    {
      return instance;
    }

    public List<TestViewer> Viewers
    {
      get
      {
        return _viewerCache.Values.ToList<TestViewer>();
      }
    }

    public List<string> ViewerNames
    {
      get
      {
        return _viewerCache.Keys.ToList<string>();
      }
    }

    public void AddViewer(TestViewer Viewer)
    {
      _viewerCache.Add(Viewer.testPortName, Viewer);
    }

    public TestViewer GetViewer(string ViewerName)
    {
      return _viewerCache[ViewerName];
    }

    public bool HasViewer(string ViewerName)
    {
      return _viewerCache.Keys.ToList<string>().Contains(ViewerName);
    }

    public bool RemoveViewer(string ViewerName)
    {
      return _viewerCache.Remove(ViewerName);
    }

    //public TestDriver GetDriver(string CacheName)
    //{
    //  string driverName = TestDriver.DecomposeDriverName(CacheName);
    //  DriverType driverType = TestDriver.DecomposeDriverType(CacheName);
    //  return GetDriver(driverName, driverType);
    //}

    //public TestDriver GetDriver(string DriverName, DriverType DriverType)
    //{
    //  TestDriver driver;
    //  string cacheName = TestDriver.ComposeDriverName(DriverName, DriverType);

    //  if (HasDriver(cacheName))
    //  {
    //    // get the cached driver
    //    driver = _driverCache[cacheName];
    //    Console.WriteLine(string.Format("Found driver: {0}", cacheName)); //TODO: remove
    //  }
    //  else
    //  {
    //    Console.WriteLine(string.Format("Created driver: {0}", cacheName)); //TODO: remove
    //    // create the requested driver and add it to the cache
    //    driver = new TestDriver(DriverName, DriverType);
    //    _driverCache.Add(driver.cacheName, driver);
    //  }

    //  return driver;
    //}

    //public bool RemoveDriver(string CacheName)
    //{
    //  return _driverCache.Remove(CacheName);
    //}

  }
}
