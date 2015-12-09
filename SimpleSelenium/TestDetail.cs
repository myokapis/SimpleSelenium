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
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SimpleSelenium
{
  public class TestDetail
  {
    protected string _type;
    protected string _category;
    protected string _alias;
    protected string _val1;
    protected string _val2;
    protected string _val3;
    protected string _val4;
    protected string _val5;
    protected string _val6;

    public TestDetail(string type, string category, string alias, string val1, string val2 = "",
                       string val3 = "", string val4 = "", string val5 = "", string val6 = "")
    {
      _type = type.ToLower();
      _category = category;
      _alias = alias;
      _val1 = val1;
      _val2 = val2;
      _val3 = val3;
      _val4 = val4;
      _val5 = val5;
      _val6 = val6;
    }

    public static TestDetail Create(string type, string category, string alias, string val1, string val2 = "",
                                    string val3 = "", string val4 = "", string val5 = "", string val6 = "")
    {
      switch(type.ToLower())
      {
        case "command":
          return new TestCommand(type, category, alias, val1, val2, val3, val4, val5, val6);
        case "data":
          return new TestData(type, category, alias, val1, val2, val3, val4, val5, val6);
        case "element":
          return new TestElement(type, category, alias, val1, val2, val3, val4, val5, val6);
        case "header":
          return new TestHeader(type, category, alias, val1, val2, val3, val4, val5, val6);
        case "menu":
          return new TestMenu(type, category, alias, val1, val2, val3, val4, val5, val6);
        case "view":
          return new TestView(type, category, alias, val1, val2, val3, val4, val5, val6);
        default:
          return new TestDetail(type, category, alias, val1, val2, val3, val4, val5, val6);
      }
    }
    
    public string alias
    {
      get
      {
        return _alias;
      }
    }

    public string category
    {
      get { return _category; }
    }

    public string type
    {
      get { return _type; }
    }

    public TestCommand command
    {
      get
      {
        return (type == "command") ? (TestCommand)this : null;
      }
    }

    public TestData data
    {
      get
      {
        return (type == "data") ? (TestData)this : null;
      }
    }

    public TestElement element
    {
      get
      {
        return (type == "element") ? (TestElement)this : null;
      }
    }

    public TestMenu menu
    {
      get
      {
        return (type == "menu") ? (TestMenu)this : null;
      }
    }

    public TestView view
    {
      get
      {
        return (type == "view") ? (TestView)this : null;
      }
    }

  }

  public class TestCommand : TestDetail
  {
    public TestCommand(string type, string category, string alias, string val1, string val2 = "",
                       string val3 = "", string val4 = "", string val5 = "", string val6 = "")
      : base(type, category, alias, val1, val2, val3, val4, val5, val6) { }

    public string arg1
    {
      get
      {
        return _val3;
      }
    }

    public string arg2
    {
      get
      {
        return _val4;
      }
    }
    
    public string name
    {
      get
      {
        return _val1;
      }
    }
    public string target
    {
      get
      {
        return _val2;
      }
    }
  }

  public class TestData : TestDetail
  {
    public TestData(string type, string category, string alias, string val1, string val2 = "",
                     string val3 = "", string val4 = "", string val5 = "", string val6 = "")
      : base(type, category, alias, val1, val2, val3, val4, val5, val6) { }
  }
  
  public class TestElement : TestDetail
  {
    public TestElement(string type, string category, string alias, string val1, string val2 = "",
                       string val3 = "", string val4 = "", string val5 = "", string val6 = "")
      : base(type, category, alias, val1, val2, val3, val4, val5, val6) { }

    public string id { get { return _val1; } } // was val2
    public string name { get { return _val2; } } // was val1
    public string arg1 { get { return _val3; } }
    public string arg2 { get { return _val4; } }

    public By findBy
    {
      get
      {
        if (id != null && id != String.Empty)
        {
          return By.Id(id);
        }
        else if (name != null && name != String.Empty)
        {
          return By.Name(name);
        }
        else
        {
          return null;
        }
      }
    }

    public By findById
    {
      get
      {
        if (id != null && id != String.Empty) return By.Id(id);
        return null;
      }
    }

    public By findByName
    {
      get
      {
        if (id != null && id != String.Empty) return By.Name(name);
        return null;
      }
    }

    public string itemName
    {
        get
        {
          return (id == null || id == String.Empty) ? name : id;
        }
    }
  }

  public class TestHeader : TestDetail
  {
    public TestHeader(string type, string category, string alias, string val1, string val2 = "",
                       string val3 = "", string val4 = "", string val5 = "", string val6 = "")
      : base(type, category, alias, val1, val2, val3, val4, val5, val6) { }

    public string lastSaved
    {
      get
      {
        return (_val2.IsDate()) ? DateTime.Parse(_val2).ToShortDateString() : string.Empty;
      }
    }
    
    public string version
    {
      get
      {
        return _val1;
      }
    }
  }

  public class TestMenu : TestDetail
  {
    public TestMenu(string type, string category, string alias, string val1, string val2 = "",
                       string val3 = "", string val4 = "", string val5 = "", string val6 = "")
      : base(type, category, alias, val1, val2, val3, val4, val5, val6) { }

    public string id { get { return _val2; } }
    public string name { get { return _val1; } }
    public string arg1 { get { return _val3; } }
    public string arg2 { get { return _val4; } }

    public By findBy
    {
      get
      {
        if (id != null && id != String.Empty)
        {
          return By.Id(id);
        }
        else if (name != null && name != String.Empty)
        {
          return By.Name(name);
        }
        else
        {
          return null;
        }
      }
    }
  }

  public class TestView : TestDetail
  {
    public TestView(string type, string category, string alias, string val1, string val2 = "",
                       string val3 = "", string val4 = "", string val5 = "", string val6 = "")
      : base(type, category, alias, val1, val2, val3, val4, val5, val6) { }

    public int driverBitMask
    {
      get
      {
        int value;
        int.TryParse(_val1, out value);
        return value;
      }
    }
  }
}
