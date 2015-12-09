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
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleSelenium
{
  public class Loader
  {

    public static List<TestDetail> LoadMacro(string MacroName)
    {
      string macroFileDir = ConfigurationManager.AppSettings["MacroFileDir"].ToString();
      string macroFileExtension = ConfigurationManager.AppSettings["MacroFileExtension"].ToString();
      string macroFilePath = Path.GetFullPath(Path.Combine(macroFileDir, String.Format("{0}.{1}", MacroName, macroFileExtension)));

      return LoadFile(MacroName, macroFilePath);
    }

    public static List<TestDetail> LoadMenu(string MenuName = "")
    {
      List<TestDetail> aspects = new List<TestDetail>();
      string menuFileDir = ConfigurationManager.AppSettings["MenuFileDir"].ToString();
      string menuFileExtension = ConfigurationManager.AppSettings["MenuFileExtension"].ToString();
      string searchName = string.Format("*.{0}", menuFileExtension);
      string[] files = Directory.GetFiles(menuFileDir, searchName, SearchOption.TopDirectoryOnly);

      foreach (string filePath in files)
      {
        foreach (TestDetail aspect in Loader.LoadFile(string.Empty, filePath))
        {
          aspects.Add(aspect);
        }
      }

      return aspects;
    }

    public static List<TestDetail> LoadTest(string TestName)
    {
      string testFileDir = ConfigurationManager.AppSettings["TestFileDir"].ToString();
      string testFileExtension = ConfigurationManager.AppSettings["TestFileExtension"].ToString();
      string testFilePath = Path.GetFullPath(Path.Combine(testFileDir, String.Format("{0}.{1}", TestName, testFileExtension)));

      return LoadFile(TestName, testFilePath);
    }

    public static List<TestDetail> LoadFile(string Category, string FilePath)
    {
      string[] delimiter = new string[] { Char.ConvertFromUtf32(9) };
      List<TestDetail> details = new List<TestDetail>();
      TestDetail detail;
      TestDetail header;

      using (StreamReader file = new StreamReader(FilePath))
      {
        string line = file.ReadLine();
        string[] fields = line.Split(delimiter, StringSplitOptions.None);
        // TODO: use the header version information to determine how to parse the file
        //       this will allow the file layout to change with time without breaking the app
        header = TestDetail.Create(fields[0], fields[1], fields[2], fields[3], fields[4], fields[5], fields[6]);

        while ((line = file.ReadLine()) != null)
        {
          fields = line.Split(delimiter, StringSplitOptions.None);
          string category = (Category == string.Empty) ? fields[1] : Category;
          detail = TestDetail.Create(fields[0], category, fields[2], fields[3], fields[4], fields[5], fields[6]);
          details.Add(detail);
        }
      }

      return details;
    }

  }
}
