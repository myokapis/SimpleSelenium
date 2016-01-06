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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using SimpleSelenium;

namespace TestComposer
{

    public partial class Composer : Form
    {
      #region "Class Variables"

      List<TestDetail> _commandDetails = new List<TestDetail>();
      List<TestDetail> _dataDetails = new List<TestDetail>();
      List<TestDetail> _elementDetails = new List<TestDetail>();
      List<TestDetail> _menuDetails = new List<TestDetail>();
      string _filePath = String.Empty;
      string[] _noData = new string[]{String.Empty, String.Empty, string.Empty, string.Empty};
      string mode = "Ready";
      int? _selectedCommandIndex = null;
      int? _selectedElementIndex = null;
      bool _isDirty = false;
      int _columnCount = 7;
      bool _loading = false;

      Dictionary<string, string> _configuration = new Dictionary<string, string>();

      Dictionary<string, string> _options = new Dictionary<string, string>(){
          { "SendKeys", "TrueFalse" }, { "TabOut", "TrueFalse" }, { "GridDblClick", "SearchBy" }, { "GridRtClick", "SearchBy" }
      };

      Dictionary<string, Dictionary<string, string>> _hierarchy = new Dictionary<string, Dictionary<string,string>>(){
        {"BrowseTo", new Dictionary<string, string>() {{"Desc", "Browse To"}, {"Panel", "pnlBigText"}, {"0", "Label"}}},
        {"Click", new Dictionary<string, string>() {{"Desc", "Click"}, {"Panel", "pnlMainEditor"}, {"0", "Element to Click"}}},
        {"DblClick", new Dictionary<string, string>() {{"Desc", "Click"}, {"Panel", "pnlMainEditor"}, {"0", "Element to Double Click"}}},
        {"ExitFrame", new Dictionary<string, string>() {{"Desc", "Exit IFrame"}, {"Panel", "pnlMainEditor"}}},
        {"ExecuteScript", new Dictionary<string, string>() {{"Desc", "Execute Script"}, {"Panel", "pnlBigText"}, {"0", "Label"}}},
        {"FindFrame", new Dictionary<string, string>() {{"Desc", "Find IFrame"}, {"Panel", "pnlMainEditor"}, {"0", "IFrame to Find"}}},
        {"FindWindow", new Dictionary<string, string>() {{"Desc", "Find Window"}, {"Panel", "pnlWindow"}, {"0", "Window to Find"}, {"1", "Expected Window Count"}}},
        {"GridDblClick", new Dictionary<string, string>() {{"Desc", "Grid Double Click"}, {"Panel", "pnlMainEditor"}, {"0", "Grid Name"}, {"1", "Row Number/Search Criteria"}, {"2", "Search By"}}},
        {"GridRtClick", new Dictionary<string, string>() {{"Desc", "Grid Right Click"}, {"Panel", "pnlMainEditor"}, {"0", "Grid Name"}, {"1", "Row Number/Search Criteria"}, {"2", "Search By"}}},
        {"NavigateMenu", new Dictionary<string, string>() {{"Desc", "Navigate Menu"}, {"Panel", "pnlNavigation"}, {"0", "Menu Option 1"}}},
        {"RunMacro", new Dictionary<string, string>() {{"Desc", "Run Macro"}, {"Panel", "pnlMacro"}, {"0", "Macro Name"}}},
        {"SendKeys", new Dictionary<string, string>() {{"Desc", "Send Keys"}, {"Panel", "pnlMainEditor"}, {"0", "Receiving Element"}, {"1", "Keys to Send"}, {"2", "Append Keys"}}},
        {"Sleep", new Dictionary<string, string>() {{"Desc", "Sleep"}, {"Panel", "pnlMainEditor"}, {"1", "Sleep Time (sec)"}}},
        {"TabOut", new Dictionary<string, string>() {{"Desc", "Send Keys & Tab Out"}, {"Panel", "pnlMainEditor"}, {"0", "Receiving Element"}, {"1", "Keys to Send"}, {"2", "Append Keys"}}}
      };

      Dictionary<string, List<string>> _panelLabels = new Dictionary<string, List<string>>()
      {
        {"pnlMainEditor", new List<string>{"lblFinder1", "lblArg1", "lblArg2"}},
        {"pnlBigText", new List<string>{"lblBigText"}},
        {"pnlNavigation", new List<string>{"lblMenu1", "lblMenu2", "lblMenu3", "lblMenu4"}},
        {"pnlMacro", new List<string>{"lblMacro"}},
        {"pnlWindow", new List<string>{"lblWindow", "lblWindowArg1"}},
        {"pnlElement", new List<string>{ "lblElementName", "lblElementId" }}
      };

      Dictionary<string, string[]> _panelControls = new Dictionary<string, string[]>()
      {
        {"pnlMainEditor", new string[]{"cboElement", "txtArg1", "cboArg2"}},
        {"pnlBigText", new string[]{"txtBigText"}},
        {"pnlNavigation", new string[]{"cboMenu1", "cboMenu2", "cboMenu3", "cboMenu4"}},
        {"pnlMacro", new string[]{"cboMacro"}},
        {"pnlWindow", new string[]{"cboWindow", "txtWindowArg1"}},
        {"pnlElement", new string[]{"txtElementName", "txtElementId"}}
      };

      List<string> _commandPanels = new List<string>() { "pnlMainEditor", "pnlBigText", "pnlNavigation", "pnlMacro", "pnlWindow" };
      List<string> _dataPanels = new List<string>() { };
      List<string> _elementPanels = new List<string>() { "pnlElement" };

      #endregion

      public Composer()
      {
          InitializeComponent();
      }

      #region "Events"

      private void btnCancelCommand_Click(object sender, EventArgs e)
      {
          ListView lvCurrent = null;

          switch (tabControl1.SelectedTab.Name)
          {
              case "tabCommands":
                  lvCurrent = lvCommands;
                  if (mode == "Add" && _selectedCommandIndex.HasValue) lvCurrent.Items.RemoveAt(_selectedCommandIndex.Value);
                  ClearCommandEditor();
                  break;
              case "tabData":
                  lvCurrent = lvData;
                  break;
              case "tabElements":
                  lvCurrent = lvElements;
                  if (mode == "Add" && _selectedElementIndex.HasValue) lvCurrent.Items.RemoveAt(_selectedElementIndex.Value);
                  ClearElementEditor();
                  break;
              default:
                  break;
          }

          mode = "Ready";
          lvCurrent.Enabled = true;
          btnDelete.Visible = false;
          lvCurrent.Focus();
      }

      private void btnDelete_Click(object sender, EventArgs e)
      {
          ListView lvCurrent = null;
          ListViewItem lvi = null;
          string message = string.Empty;

          if (MessageBox.Show("Delete the current record?", "Delete", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

          switch (tabControl1.SelectedTab.Name)
          {
              case "tabCommands":
                  if (!_selectedCommandIndex.HasValue) return;
                  lvCurrent = lvCommands;
                  lvi = lvCommands.Items[_selectedCommandIndex.Value];
                  lvi.Remove();
                  ClearCommandEditor();
                  break;
              case "tabData":
                  lvCurrent = lvData;
                  break;
              case "tabElements":
                  if (!_selectedElementIndex.HasValue) return;
                  lvCurrent = lvElements;
                  lvi = lvElements.Items[_selectedElementIndex.Value];
                  lvi.Remove();
                  ClearElementEditor();
                  break;
              default:
                  break;
          }

          lvi.BackColor = lvCommands.BackColor;
          mode = "Ready";
          lvCurrent.Enabled = true;
          btnDelete.Visible = false;
          lvCurrent.Focus();
      }

      private void btnSaveCommand_Click(object sender, EventArgs e)
      {
          ListView lvCurrent = null;
          ListViewItem lvi = null;
          string message = string.Empty;

          switch (tabControl1.SelectedTab.Name)
          {
              case "tabCommands":
                  if (!_selectedCommandIndex.HasValue) return;
                  lvCurrent = lvCommands;
                  lvi = lvCommands.Items[_selectedCommandIndex.Value];

                  if (!ValidateCommand(ref message))
                  {
                      MessageBox.Show(message);
                      return;
                  }

                  CopyCommandData(ref lvi);
                  ClearCommandEditor();
                  break;
              case "tabData":
                  lvCurrent = lvData;
                  break;
              case "tabElements":
                  if (!_selectedElementIndex.HasValue) return;
                  lvCurrent = lvElements;
                  lvi = lvElements.Items[_selectedElementIndex.Value];

                  if (!ValidateElement(ref message))
                  {
                      MessageBox.Show(message);
                      return;
                  }

                  CopyElementData(ref lvi);
                  ClearElementEditor();
                  break;
              default:
                  break;
          }

          lvi.BackColor = lvCommands.BackColor;
          mode = "Ready";
          lvCurrent.Enabled = true;
          btnDelete.Visible = false;
          lvCurrent.Focus();
      }

      private void closeTest_Click(object sender, EventArgs e)
      {
          // let the user save the form if it is dirty
          if (_isDirty)
          {
              if (MessageBox.Show("Save changes?", "Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
              {
                  if (!SaveAll()) return;
              }
          }

          ClearAll();
      }

      private void newTest_Click(object sender, EventArgs e)
      {
        if (mode != "Ready") return;

        if (_isDirty)
        {
          if (MessageBox.Show("Save changes?", "Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
          {
            if (!SaveAll()) return;
          }
        }

        ClearAll();
      }

      private void Form_Closing(object sender, FormClosingEventArgs e)
      {
          if (!_isDirty) return;
          if (MessageBox.Show("Changes have not been saved. Close anyway?", "Close", MessageBoxButtons.YesNo) == DialogResult.No)
          {
              e.Cancel = true;
          }
      }

      private void Form_OnLoad(object sender, EventArgs e)
      {
        ReadAppConfig();
        SetupListHeaders();
        PopulateCommands();
        //// move these to the project open event
        //LoadMacros();
        //LoadMenus();
        //LoadWindows();
        PopulateMenu(0);
        splitContainer1.Panel2.Enabled = false;
        tabCommands.Enabled = false;
        tabData.Enabled = false;
        tabElements.Enabled = false;
        tvwProject.Enabled = true;
        tvwProject.Select();
      }

      private void OpenTest_Click(object sender, EventArgs e)
      {
          OpenFileDialog dialog = new OpenFileDialog();
          string lastDir = (_filePath == string.Empty) ? _configuration["TestFileDir"] : Path.GetDirectoryName(_filePath);
          dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
          dialog.InitialDirectory = lastDir;
          dialog.Title = "Select a test or macro";

          if (dialog.ShowDialog() == DialogResult.OK) _filePath = dialog.FileName;
          if (_filePath == string.Empty) return;

          string owner = Path.GetFileNameWithoutExtension(_filePath);
          List<TestDetail> details = Loader.LoadFile(owner, _filePath);
          _commandDetails = details.Where(a => a.type == "command").ToList<TestDetail>();
          _dataDetails = details.Where(a => a.type == "data").ToList<TestDetail>();
          _elementDetails = details.Where(a => a.type == "element").ToList<TestDetail>();

          txtFilePath.Text = _filePath;
          RefreshCommandList();
          RefreshElementList();
          RefreshDataList();
          LoadElements();
      }

      private void saveMenuItem_Click(object sender, EventArgs e)
      {
          if (SaveAll())
          {
              _isDirty = false;
              txtFilePath.Text = _filePath;
          }
      }

      #region "Command Events"

      private void cboCommand_SelectedIndexChanged(object sender, EventArgs e)
      {
          int index = cboCommand.SelectedIndex;
          if (mode == "Loading" || index < 0) return;
          string command = cboCommand.Items[index].ToString();

          ShowCommandEditor(command, _noData);
      }

      private void cboMenu1_SelectedIndexChanged(object sender, EventArgs e)
      {
          if (_loading) return;
          _loading = true;
          PopulateMenu(1);
          cboMenu2.Enabled = (cboMenu2.Items.Count > 1 & cboMenu1.SelectedItem != null);
          _loading = false;
      }

      private void cboMenu2_SelectedIndexChanged(object sender, EventArgs e)
      {
          if (_loading) return;
          _loading = true;
          PopulateMenu(2);
          cboMenu3.Enabled = (cboMenu3.Items.Count > 1 & cboMenu2.SelectedItem != null & cboMenu2.SelectedItem.ToString() != string.Empty);
          _loading = false;
      }

      private void cboMenu3_SelectedIndexChanged(object sender, EventArgs e)
      {
          if (_loading) return;
          _loading = true;
          PopulateMenu(3);
          cboMenu4.Enabled = (cboMenu4.Items.Count > 1 & cboMenu3.SelectedItem != null & cboMenu3.SelectedItem.ToString() != string.Empty);
          _loading = false;
      }

      private void lvCommands_Click(object sender, EventArgs e)
      {
          if (lvCommands.SelectedIndices.Count == 0 || mode != "Ready") return;
          mode = "Loading";
          _selectedCommandIndex = lvCommands.SelectedIndices[0];
          var subItems = lvCommands.SelectedItems[0].SubItems;
          string alias = subItems[0].Text;
          string command = subItems[1].Text;
          string[] args = new string[] { subItems[2].Text, subItems[3].Text, subItems[4].Text };

          txtAlias.Text = alias;
          cboCommand.Text = command;
          ShowCommandEditor(command, args);
          mode = "Update";
          btnDelete.Visible = true;
          splitContainer1.Panel2.Enabled = true;
          txtAlias.Focus();
          lvCommands.Enabled = false;
      }

      private void lvCommands_DragDrop(object sender, DragEventArgs e)
      {
          if (e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
          {
              ListViewItem lviDrop = ((ListView.SelectedListViewItemCollection)e.Data.GetData(typeof(ListView.SelectedListViewItemCollection)))[0];
              Point point = lvCommands.PointToClient(new Point(e.X, e.Y));
              ListViewItem lviDest = lvCommands.GetItemAt(point.X, point.Y);

              if ((lviDrop != lviDest) && (lviDest != null))
              {
                  lvCommands.Items.Remove(lviDrop);
                  lvCommands.Items.Insert(lviDest.Index + 1, lviDrop);
              }
          }
      }

      private void lvCommands_DragEnter(object sender, DragEventArgs e)
      {
          if(e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection))) e.Effect = DragDropEffects.Move;
      }

      private void lvCommands_ItemDrag(object sender, ItemDragEventArgs e)
      {
          lvCommands.DoDragDrop(lvCommands.SelectedItems, DragDropEffects.Move);
      }

      private void lvCommands_KeyUp(object sender, KeyEventArgs e)
      {
          switch (e.KeyCode)
          {
              case Keys.Insert:
                  if (mode == "Ready" && tabControl1.SelectedTab.Name == "tabCommands") AddCommand();
                  e.Handled = true;
                  break;
              default:
                  e.Handled = false;
                  break;
          }
      }

#endregion

      #region "Element Events"

      private void lvElements_Click(object sender, EventArgs e)
      {
        if (lvElements.SelectedIndices.Count == 0 || mode != "Ready") return;
        mode = "Loading";
        _selectedElementIndex = lvElements.SelectedIndices[0];
        var subItems = lvElements.SelectedItems[0].SubItems;
        ClearElementEditor();
        txtAlias.Text = subItems[0].Text;
        txtElementName.Text = subItems[1].Text;
        txtElementId.Text = subItems[2].Text;
        mode = "Update";
        btnDelete.Visible = true;
        splitContainer1.Panel2.Enabled = true;
        txtAlias.Focus();
        lvElements.Enabled = false;
      }

      private void lvElements_KeyUp(object sender, KeyEventArgs e)
      {
          switch (e.KeyCode)
          {
              case Keys.Insert:
                  if (mode == "Ready" && tabControl1.SelectedTab.Name == "tabElements") AddElement();
                  e.Handled = true;
                  break;
              default:
                  e.Handled = false;
                  break;
          }
      }

      private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
      {
        string panelName = "";
        Control ctlCurrent = null;

        switch (tabControl1.SelectedTab.Name)
        {
          case "tabProject":
            ctlCurrent = tvwProject;
            panelName = "pnlProject";
            cboCommand.Enabled = false;
            break;
          case "tabCommands":
            ctlCurrent = lvCommands;
            panelName = "pnlMainEditor";
            cboCommand.Enabled = true;
            LoadElements();
            break;
          case "tabElements":
            ctlCurrent = lvElements;
            panelName = "pnlElement";
            cboCommand.Enabled = false;
            break;
          case "tabData":
            ctlCurrent = lvData;
            panelName = "";
            cboCommand.Enabled = false;
            break;
          default:
            break;
        }

        // hide all but the default panel for each tab
        foreach (string ctlName in _commandPanels.Union(_dataPanels).Union(_elementPanels))
        {
          Control ctl = this.Controls.Find(ctlName, true)[0];
          if(ctlName == panelName) ctl.Location = new Point(0, 0);
          ctl.Visible = (ctlName == panelName);
        }

        ctlCurrent.Select();
      }

      private void tabControl1_Deselecting(object sender, TabControlCancelEventArgs e)
      {
          if (mode != "Ready") e.Cancel = true;
      }

      #endregion
      #endregion

      #region "Private Methods"
      #region "Private Common Methods"

      private string BuildRecord(ListViewItem Item, string Category, string Type)
      {
        List<string> data = new List<string>() { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
          Dictionary<int, int> map = new Dictionary<int, int>() { {0, 2}, {1, 3}, {2, 4}, {3, 5}, {4, 6}, {5, 7}, {6, 8} };
          data[0] = Type;
          data[1] = Category;

          for (int i = 0; i < _columnCount; i++)
          {
              if(map.ContainsKey(i))
              {
                  data[map[i]] = (i < Item.SubItems.Count) ? Item.SubItems[i].Text : string.Empty;
              }
          }

          return string.Join("\t", data.ToArray<string>());
      }

      private void ClearAll()
      {
        LoadWindows();
        PopulateMenu(0);
        mode = "Ready";
        _commandDetails.Clear();
        _dataDetails.Clear();
        _elementDetails.Clear();
        _filePath = String.Empty;
        _selectedCommandIndex = null;
        _selectedElementIndex = null;
        _isDirty = false;
        _loading = false;
        splitContainer1.Panel2.Enabled = false;
        lvCommands.Enabled = true;
        lvCommands.Select();
      }

      private void LoadElements()
      {
          cboElement.Items.Clear();

          foreach (ListViewItem lvi in lvElements.Items)
          {
              cboElement.Items.Add(new ComboboxItem() { Text = lvi.Text, Value = lvi.Text });
          }
      }

      private void LoadMacros()
      {
        cboMacro.Items.Clear();
        string directory = _configuration["MacroFileDir"];
        if (!Directory.Exists(directory)) return;

        foreach (string filePath in Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories))
        {
          string fileName = Path.GetFileNameWithoutExtension(filePath);
          ComboboxItem item = new ComboboxItem() { Text = fileName, Value = fileName};
          cboMacro.Items.Add(item);
        }
      }

      private void LoadMenus()
      {
        string directory = _configuration["MenuFileDir"];
        if (!Directory.Exists(directory)) return;

        foreach (string filePath in Directory.GetFiles(directory, "*.*", SearchOption.TopDirectoryOnly))
        {
          foreach (TestDetail detail in Loader.LoadFile(string.Empty, filePath))
          {
              _menuDetails.Add(detail);
          }
        }
      }

      private void LoadWindows()
      {
          cboWindow.Items.Clear();
          cboWindow.Items.Add(new ComboboxItem() { Text = "LAST_WINDOW", Value = "LAST_WINDOW" });
          cboWindow.Items.Add(new ComboboxItem() { Text = "MAIN_WINDOW", Value = "MAIN_WINDOW" });
      }

      private void ReadAppConfig()
      {
          _configuration.Add("MacroFileDir", ConfigurationManager.AppSettings["MacroFileDir"]);
          _configuration.Add("MacroFileExtension", ConfigurationManager.AppSettings["MacroFileExtension"]);
          _configuration.Add("TestFileDir", ConfigurationManager.AppSettings["TestFileDir"]);
          _configuration.Add("TestFileExtension", ConfigurationManager.AppSettings["TestFileExtension"]);
          _configuration.Add("MenuFileDir", ConfigurationManager.AppSettings["MenuFileDir"]);
          _configuration.Add("MenuFileExtension", ConfigurationManager.AppSettings["MenuFileExtension"]);
      }

    private bool SaveAll(bool ChooseFile = false)
    {
      List<string> lines = new List<string>(); // { "Type\tCategory\tAlias\tVal1\tVal2\tVal3\tVal4\tVal5\tVal6\tEOR" };
      string lastDir = (_filePath == string.Empty) ? _configuration["TestFileDir"] : Path.GetDirectoryName(_filePath);
      string filePath = _filePath;
      string fileName = (_filePath == string.Empty || ChooseFile) ? string.Empty : Path.GetFileName(_filePath);

      if (ChooseFile || fileName == string.Empty)
      {
        SaveFileDialog dialog = new SaveFileDialog();
        dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
        dialog.InitialDirectory = lastDir;
        dialog.FileName = fileName;
        dialog.Title = "Save Test";

        if (dialog.ShowDialog() != DialogResult.OK) return false;
        filePath = dialog.FileName;
      }

      string owner = Path.GetFileNameWithoutExtension(filePath);
      string[] header = { "header", Application.ProductName, string.Empty,
      Application.ProductVersion, DateTime.Now.ToLongDateString(), string.Empty,
      string.Empty, string.Empty, string.Empty};
     
      lines.Add(string.Join("\t", header));

      foreach (ListViewItem lvi in lvElements.Items)
      {
        lines.Add(BuildRecord(lvi, owner, "Element"));
      }

      foreach (ListViewItem lvi in lvCommands.Items)
      {
        lines.Add(BuildRecord(lvi, owner, "Command"));
      }

      foreach (ListViewItem lvi in lvData.Items)
      {
        lines.Add(BuildRecord(lvi, owner, "Data"));
      }

      File.WriteAllLines(filePath, lines.ToArray<string>());
      _filePath = filePath;
      return true;
    }

      private void SetupListHeaders()
      {
        lvCommands.Columns.Add("Alias", 100);
        lvCommands.Columns.Add("Command", 100);
        lvCommands.Columns.Add("Argument 1", 100);
        lvCommands.Columns.Add("Argument 2", 100);
        lvCommands.Columns.Add("Argument 3", 100);

        lvElements.Items.Clear();
        lvElements.Columns.Add("Alias", 150);
        lvElements.Columns.Add("Element Name", 150);
        lvElements.Columns.Add("Element Id", 150);
        lvElements.Columns.Add("Argument 1", 150);
        lvElements.Columns.Add("Argument 2", 100);

        tvwProject.ExpandAll();
      }

      #endregion

      #region "Private Command Methods"

      private void AddCommand()
      {
          ListViewItem lvi = new ListViewItem("");
          lvi.SubItems.Add("");
          lvi.SubItems.Add("");
          lvi.SubItems.Add("");
          lvi.SubItems.Add("");
          lvi.BackColor = Color.Beige;
          lvCommands.Items.Add(lvi);
          _selectedCommandIndex = lvi.Index;
          ClearCommandEditor();
          mode = "Add";
          splitContainer1.Panel2.Enabled = true;
          lvCommands.Enabled = false;
      }

      private void AddElement()
      {
          ListViewItem lvi = new ListViewItem("");
          lvi.SubItems.Add("");
          lvi.SubItems.Add("");
          lvi.SubItems.Add("");
          lvi.SubItems.Add("");
          lvi.BackColor = Color.Beige;
          lvElements.Items.Add(lvi);
          _selectedElementIndex = lvi.Index;
          ClearElementEditor();
          mode = "Add";
          splitContainer1.Panel2.Enabled = true;
          lvElements.Enabled = false;
      }

      private void ClearCommandEditor()
      {
          cboCommand.SelectedIndex = -1;
          txtAlias.Text = string.Empty;

          // clear and disable all of the input controls
          foreach (KeyValuePair<string, string[]> kvp in _panelControls)
          {
              foreach (string ctlName in kvp.Value)
              {
                  Control ctl = this.Controls.Find(ctlName, true)[0];
                  ctl.Enabled = false;
                  SetControl(ref ctl, String.Empty);
              }
          }
          splitContainer1.Panel2.Enabled = false;
      }

      private void ClearElementEditor()
      {
          cboCommand.SelectedIndex = -1;
          txtAlias.Text = string.Empty;

          // clear and disable all of the input controls
          foreach (KeyValuePair<string, string[]> kvp in _panelControls)
          {
              foreach (string ctlName in kvp.Value)
              {
                  Control ctl = this.Controls.Find(ctlName, true)[0];
                  ctl.Enabled = true;
                  SetControl(ref ctl, String.Empty);
              }
          }
          splitContainer1.Panel2.Enabled = false;
      }

      private void CopyCommandData(ref ListViewItem Record)
      {
          string command = cboCommand.Text;
          Dictionary<string, string> controls = _hierarchy[command];
          string panelName = controls["Panel"];
          List<string> pnlControls = _panelControls.Where(c => c.Key == panelName).SelectMany(i => i.Value).ToList<string>();
          List<string> values = new List<string>();

          // get the values from each enabled control
          foreach (string ctlName in pnlControls)
          {
              Control ctl = this.Controls.Find(ctlName, true)[0];
              string textValue = (ctl is CheckBox) ? ((CheckBox)ctl).Checked.ToString() : ctl.Text;
              if (ctl.Enabled) values.Add(textValue);
          }

          // join navigation data into a pipe delimited string
          if (panelName == "pnlNavigation")
          {
              string navPath = string.Join<string>("|", values);
              values.Clear();
              values.Add(navPath);
          }

          // update the list
          Record.SubItems[0].Text = txtAlias.Text;
          Record.SubItems[1].Text = command;

          for (int i = 2; i < Record.SubItems.Count; i++)
          {
              Record.SubItems[i].Text = (i - 2 < values.Count) ? values[i - 2] : string.Empty;
          }
      }

      private void CopyElementData(ref ListViewItem Record)
      {
          string panelName = "pnlElement";
          List<string> pnlControls = _panelControls.Where(c => c.Key == panelName).SelectMany(i => i.Value).ToList<string>();
          List<string> values = new List<string>();

          // get the values from each enabled control
          foreach (string ctlName in pnlControls)
          {
              Control ctl = this.Controls.Find(ctlName, true)[0];
              string textValue = (ctl is CheckBox) ? ((CheckBox)ctl).Checked.ToString() : ctl.Text;
              if (ctl.Enabled) values.Add(textValue);
          }

          // update the list
          Record.SubItems[0].Text = txtAlias.Text;
          Record.SubItems[1].Text = string.Empty;

          for (int i = 2; i < Record.SubItems.Count; i++)
          {
              Record.SubItems[i].Text = (i - 2 < values.Count) ? values[i - 2] : string.Empty;
          }
      }

      private void DeleteCommand()
      {
          if (MessageBox.Show("Delete the selected command?", "Delete Command", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
          {
              if (lvCommands.SelectedItems.Count > 0)
              {
                  lvCommands.Items.Remove(lvCommands.SelectedItems[0]);
                  ClearCommandEditor();
              }
          }
      }

      private void DeleteElement()
      {
          if (MessageBox.Show("Delete the selected element?", "Delete Element", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
          {
              if (lvCommands.SelectedItems.Count > 0)
              {
                  lvCommands.Items.Remove(lvElements.SelectedItems[0]);
                  ClearElementEditor();
              }
          }
      }

      private bool HasData(string TextToValidate, string Message, ref List<string> Messages)
      {
          if (TextToValidate.Trim() == string.Empty)
          {
              Messages.Add(Message);
              return false;
          }
          return true;
      }

      private bool IsNumber(string TextToValidate, bool AllowBlank, string Message, ref List<string> Messages)
      {
          int result;
          if (!int.TryParse(TextToValidate.Trim(), out result))
          {
              Messages.Add(Message);
              return false;
          }
          return true;
      }

      private bool IsValidIdentifier(string ElementName, string Message, ref List<string> Messages)
      {
          if (ElementName.Trim() == string.Empty || ElementName.Trim().Contains(" "))
          {
              Messages.Add(Message);
              return false;
          }
          return true;
      }

      private void PopulateCommands()
      {
          foreach (KeyValuePair<string, string> kvp in Core.SupportedCommands.OrderBy(c => c.Value))
          {
              ComboboxItem item = new ComboboxItem() { Text = kvp.Value, Value = kvp.Key };
              cboCommand.Items.Add(item);
          }
      }

      private void PopulateMenu(int Index)
      {
          ComboBox[] controls = new ComboBox[] { cboMenu1, cboMenu2, cboMenu3, cboMenu4 };
          string owner = (Index == 0) ? "Menu" : ((ComboboxItem)(controls[Index - 1].SelectedItem)).Text;
          ComboBox ctl = controls[Index];
          for (int i = Index; i < 4; i++) { controls[i].Items.Clear(); controls[i].Enabled = (i == Index); }
          ComboboxItem blank = new ComboboxItem() { Value = string.Empty, Text = string.Empty};
          cboMenu2.Items.Add(blank);
          cboMenu3.Items.Add(blank);
          cboMenu4.Items.Add(blank);

          foreach (TestDetail detail in _menuDetails.Where(d => d.category == owner))
          {
              ctl.Items.Add(new ComboboxItem() { Text = string.Format("{0}|{1}", owner, detail.alias), Value = detail.alias });
          }
      }

      private void PopulateOptions(string OptionSetName)
      {
          if (_loading) return;
          _loading = true;
          cboArg2.Items.Clear();

          switch (OptionSetName)
          {
              case "TrueFalse":
                  cboArg2.Items.Add(new ComboboxItem() { Text = "False", Value = "False" });
                  cboArg2.Items.Add(new ComboboxItem() { Text = "True", Value = "True" });
                  cboArg2.SelectedIndex = 0;
                  break;
              case "SearchBy":
                  cboArg2.Items.Add(new ComboboxItem() { Text = "Index", Value = "Index" });
                  cboArg2.Items.Add(new ComboboxItem() { Text = "Text", Value = "Text" });
                  cboArg2.Items.Add(new ComboboxItem() { Text = "Value", Value = "Value" });
                  cboArg2.SelectedIndex = 1;
                  break;
              default:
                  break;
          }

          _loading = false;
      }

      private void RefreshCommandList()
      {
          lvCommands.Items.Clear();

          foreach (TestDetail detail in _commandDetails)
          {
            TestCommand command = detail.command;
            string[] columns = new string[] { command.alias, command.name, command.target, command.arg1, command.arg2, String.Empty };
              lvCommands.Items.Add(new ListViewItem(columns));
          }
      }

      private void SetControl(ref Control control, string Value)
      {
          if (control is CheckBox)
          {
              bool isChecked = !(Value == String.Empty);
              ((CheckBox)control).Checked = isChecked;
          }
          else
          {
              control.Text = Value;
          }
      }

      private void ShowCommandEditor(string Command, string[] Data)
      {
          if (!_hierarchy.ContainsKey(Command)) return;
          Dictionary<string, string> controls = _hierarchy[Command];
          string panelName = controls["Panel"];
            
          // set panel visibility, location, and availability
          foreach (string cmdPanel in _commandPanels)
          {
              Panel panel = (Panel)this.Controls.Find(cmdPanel, true)[0];
              bool isSelection = (panel.Name == panelName);
              panel.Enabled = isSelection;
              panel.Visible = isSelection;
              panel.Location = new Point(0, 0);
          }

          pnlMainEditor.Visible = (panelName == "pnlMainEditor");
          pnlBigText.Visible = (panelName == "pnlBigText");
          pnlNavigation.Visible = (panelName == "pnlNavigation");
          pnlMacro.Visible = (panelName == "pnlMacro");
          pnlWindow.Visible = (panelName == "pnlWindow");

          // get the labels and controls
          List<int> indexes = controls.Keys.Where(c => c.IsNumeric()).Select(i => int.Parse(i)).ToList<int>();
          List<string> pnlLabels = _panelLabels.Where(c => c.Key == panelName).SelectMany(i => i.Value).ToList<string>();
          List<string> pnlControls = _panelControls.Where(c => c.Key == panelName).SelectMany(i => i.Value).ToList<string>();

          // populate options
          string optionSet = (_options.ContainsKey(Command)) ? _options[Command] : string.Empty;
          PopulateOptions(optionSet);

          // massage data for the navigation panel
          if (panelName == "pnlNavigation") Data = Data[0].Split(new string[] {"|"}, StringSplitOptions.None);

          // set label text - set control initial values and availability
          for (int i = 0; i < pnlControls.Count; i++)
          {
              string controlName = pnlControls[i];
              string labelName = pnlLabels[i];
              Control ctl = this.Controls.Find(controlName, true)[0];
              Control lbl = this.Controls.Find(labelName, true)[0];
              bool hasCtl = (indexes.Contains(i));
              // TODO: decide if this should be kept or just handled in validation
              bool navOverride = true; // !((panelName == "pnlNavigation") && !(ctl.Name == "lblMenu1" || ctl.Name == "cboMenu1"));
              if (hasCtl) lbl.Text = controls[i.ToString()];
              ctl.Enabled = (hasCtl && navOverride);
              lbl.Enabled = (hasCtl && navOverride);
              SetControl(ref ctl, (Data.Length > i) ? Data[i] : string.Empty);
          }

      }

      private bool ValidateCommand(ref string Message)
      {
          bool result = true;
          Message = String.Empty;
          List<string> messages = new List<string>();

          string command = cboCommand.SelectedItem.ToString();

          switch (command)
          {
              case "BrowseTo":
                  result &= HasData(txtBigText.Text, "URL is required", ref messages);
                  break;
              case "ExecuteScript":
                  result &= HasData(txtBigText.Text, "Script is required", ref messages);
                  break;
              case "Click":
              case "DblClick":
              case "FindFrame":
                  result &= IsValidIdentifier(cboElement.Text, "Element identifier is required", ref messages);
                  break;
              case "ExitFrame":
                  break;
              case "FindWindow":
                  result &= HasData(cboWindow.Text, "Window identifier is required", ref messages);
                  result &= (txtWindowArg1.Text == string.Empty || IsNumber(txtWindowArg1.Text, true, "Expected window count must be a number", ref messages));
                  break;
              case "GetAttribute":
                  result &= IsValidIdentifier(cboElement.Text, "Element identifier is required", ref messages);
                  result &= IsValidIdentifier(txtArg1.Text, "Attribute name is required", ref messages);
                  break;
              case "GridDblClick":
              case "GridRtClick":
                  result &= IsValidIdentifier(cboElement.Text, "Element identifier is required", ref messages);
                  result &= IsNumber(txtArg1.Text, false, "Row number is required", ref messages);
                  break;
              case "NavigateMenu":
                  // TOOD: add validation here
                  //       valid identifier in 1 is required
                  //       2,3,4 can only be set if the prior item was set
                  //       if set, identifier must be valid
                  break;
              case "RunMacro":
                  result &= IsValidIdentifier(cboMacro.Text, "Macro identifier is required", ref messages);
                  break;
              case "SendKeys":
              case "TabOut":
                  result &= IsValidIdentifier(cboElement.Text, "Element identifier is required", ref messages);
                  result &= HasData(txtArg1.Text, "Text to send to control is required", ref messages);
                  break;
              case "Sleep":
                  // TODO: add validation here
                  //       may need to move sleep command to a different panel

                  break;
              default:
                  Message = "Invalid Command";
                  result = false;
                  break;
          }

          Message = string.Join("\r\n", messages.ToArray<string>());
          return result;
      }

      private bool ValidateElement(ref string Message)
      {
          bool result = true;
          Message = String.Empty;
          List<string> messages = new List<string>();
          bool hasName = IsValidIdentifier(txtElementName.Text, "", ref messages);
          bool hasId = IsValidIdentifier(txtElementId.Text, "", ref messages);
          messages.Clear();

          if (!HasData(txtAlias.Text, "An alias for the element is required.", ref messages)) result = false;

          if (hasName && hasId)
          {
              messages.Add("Only an element name or an element id may be specified.");
              result = false;
          }
          else if(!(hasName || hasId))
          {
              messages.Add("Either an element name or an element id must be provided.");
              result = false;
          }

          Message = string.Join("\r\n", messages.ToArray<string>());
          return result;
      }

      #endregion

      #region "Private Data Methods"

      private void RefreshDataList()
      {
          lvData.Items.Clear();

          foreach (TestDetail detail in _dataDetails)
          {
            TestData data = detail.data;
            // TODO: figure out what the heck needs to be written here
            string[] columns = new string[] { data.alias, data.alias, data.alias, data.alias, data.alias };
            lvData.Items.Add(new ListViewItem(columns));
          }
      }

      #endregion

      #region "Private Element Methods"

      private void RefreshElementList()
      {
          foreach (TestDetail detail in _elementDetails)
          {
            TestElement element = detail.element;
            string[] columns = new string[] { element.alias, element.id, element.name, element.arg1 };
            lvElements.Items.Add(new ListViewItem(columns));
          }
      }

      #endregion

      private void saveAs_Click(object sender, EventArgs e)
      {
        if (SaveAll(true))
        {
          _isDirty = false;
          txtFilePath.Text = _filePath;
        }
      }

      #endregion

      private void aboutDialog_Click(object sender, EventArgs e)
      {
          AboutBox1 about = new AboutBox1();
          about.ShowDialog(this);
      }

      private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
      {
        e.Cancel = !e.TabPage.Enabled;
      }

      private void tvwProject_KeyUp(object sender, KeyEventArgs e)
      {
        switch (e.KeyCode)
        {
          case Keys.Insert:
            if (mode == "Ready" && tabControl1.SelectedTab.Name == "tabProject") AddProjectItem();
            e.Handled = true;
            break;
          case Keys.Delete:
            //if (mode == "Ready" && tabControl1.SelectedTab.Name == "tabProject") RemoveProjectItem();
            e.Handled = true;
            break;
          default:
            e.Handled = false;
            break;
        }
      }

      private void AddProjectItem()
      {

      }

    }
}
