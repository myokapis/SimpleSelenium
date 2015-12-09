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

namespace TestComposer
{
    partial class Composer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
      System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Tests");
      System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Macros");
      System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Menus");
      System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Project", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.newTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabProject = new System.Windows.Forms.TabPage();
      this.tvwProject = new System.Windows.Forms.TreeView();
      this.tabCommands = new System.Windows.Forms.TabPage();
      this.lvCommands = new System.Windows.Forms.ListView();
      this.tabElements = new System.Windows.Forms.TabPage();
      this.lvElements = new System.Windows.Forms.ListView();
      this.tabData = new System.Windows.Forms.TabPage();
      this.lvData = new System.Windows.Forms.ListView();
      this.pnlBody = new System.Windows.Forms.Panel();
      this.btnDelete = new System.Windows.Forms.Button();
      this.pnlElement = new System.Windows.Forms.Panel();
      this.txtElementId = new System.Windows.Forms.TextBox();
      this.lblElementId = new System.Windows.Forms.Label();
      this.txtElementName = new System.Windows.Forms.TextBox();
      this.lblElementName = new System.Windows.Forms.Label();
      this.pnlWindow = new System.Windows.Forms.Panel();
      this.txtWindowArg1 = new System.Windows.Forms.TextBox();
      this.lblWindowArg1 = new System.Windows.Forms.Label();
      this.lblWindow = new System.Windows.Forms.Label();
      this.cboWindow = new System.Windows.Forms.ComboBox();
      this.pnlMacro = new System.Windows.Forms.Panel();
      this.lblMacro = new System.Windows.Forms.Label();
      this.cboMacro = new System.Windows.Forms.ComboBox();
      this.pnlNavigation = new System.Windows.Forms.Panel();
      this.lblMenu4 = new System.Windows.Forms.Label();
      this.lblMenu3 = new System.Windows.Forms.Label();
      this.lblMenu2 = new System.Windows.Forms.Label();
      this.lblMenu1 = new System.Windows.Forms.Label();
      this.cboMenu4 = new System.Windows.Forms.ComboBox();
      this.cboMenu3 = new System.Windows.Forms.ComboBox();
      this.cboMenu2 = new System.Windows.Forms.ComboBox();
      this.cboMenu1 = new System.Windows.Forms.ComboBox();
      this.pnlBigText = new System.Windows.Forms.Panel();
      this.lblBigText = new System.Windows.Forms.Label();
      this.txtBigText = new System.Windows.Forms.TextBox();
      this.btnCancelCommand = new System.Windows.Forms.Button();
      this.btnSaveCommand = new System.Windows.Forms.Button();
      this.pnlMainEditor = new System.Windows.Forms.Panel();
      this.lblArg2 = new System.Windows.Forms.Label();
      this.cboArg2 = new System.Windows.Forms.ComboBox();
      this.chkClear = new System.Windows.Forms.CheckBox();
      this.txtArg1 = new System.Windows.Forms.TextBox();
      this.lblArg1 = new System.Windows.Forms.Label();
      this.cboElement = new System.Windows.Forms.ComboBox();
      this.lblFinder1 = new System.Windows.Forms.Label();
      this.pnlSelector = new System.Windows.Forms.Panel();
      this.cboCommand = new System.Windows.Forms.ComboBox();
      this.lblCommand = new System.Windows.Forms.Label();
      this.txtAlias = new System.Windows.Forms.TextBox();
      this.lblAlias = new System.Windows.Forms.Label();
      this.panelFileName = new System.Windows.Forms.Panel();
      this.txtFilePath = new System.Windows.Forms.TextBox();
      this.lblFilePath = new System.Windows.Forms.Label();
      this.menuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabProject.SuspendLayout();
      this.tabCommands.SuspendLayout();
      this.tabElements.SuspendLayout();
      this.tabData.SuspendLayout();
      this.pnlBody.SuspendLayout();
      this.pnlElement.SuspendLayout();
      this.pnlWindow.SuspendLayout();
      this.pnlMacro.SuspendLayout();
      this.pnlNavigation.SuspendLayout();
      this.pnlBigText.SuspendLayout();
      this.pnlMainEditor.SuspendLayout();
      this.pnlSelector.SuspendLayout();
      this.panelFileName.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(1354, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTestToolStripMenuItem,
            this.openTestToolStripMenuItem,
            this.saveMenuItem,
            this.saveAsMenuItem,
            this.closeTestToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // newTestToolStripMenuItem
      // 
      this.newTestToolStripMenuItem.Name = "newTestToolStripMenuItem";
      this.newTestToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
      this.newTestToolStripMenuItem.Text = "New Test";
      this.newTestToolStripMenuItem.Click += new System.EventHandler(this.newTest_Click);
      // 
      // openTestToolStripMenuItem
      // 
      this.openTestToolStripMenuItem.Name = "openTestToolStripMenuItem";
      this.openTestToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
      this.openTestToolStripMenuItem.Text = "Open Test";
      this.openTestToolStripMenuItem.Click += new System.EventHandler(this.OpenTest_Click);
      // 
      // saveMenuItem
      // 
      this.saveMenuItem.Name = "saveMenuItem";
      this.saveMenuItem.Size = new System.Drawing.Size(128, 22);
      this.saveMenuItem.Text = "Save";
      this.saveMenuItem.Click += new System.EventHandler(this.saveMenuItem_Click);
      // 
      // saveAsMenuItem
      // 
      this.saveAsMenuItem.Name = "saveAsMenuItem";
      this.saveAsMenuItem.Size = new System.Drawing.Size(128, 22);
      this.saveAsMenuItem.Text = "Save As";
      this.saveAsMenuItem.Click += new System.EventHandler(this.saveAs_Click);
      // 
      // closeTestToolStripMenuItem
      // 
      this.closeTestToolStripMenuItem.Name = "closeTestToolStripMenuItem";
      this.closeTestToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
      this.closeTestToolStripMenuItem.Text = "Close Test";
      this.closeTestToolStripMenuItem.Click += new System.EventHandler(this.closeTest_Click);
      // 
      // toolsToolStripMenuItem
      // 
      this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
      this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
      this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
      this.toolsToolStripMenuItem.Text = "Tools";
      // 
      // optionsToolStripMenuItem
      // 
      this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
      this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
      this.optionsToolStripMenuItem.Text = "Settings";
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.viewHelpToolStripMenuItem});
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.helpToolStripMenuItem.Text = "Help";
      // 
      // aboutToolStripMenuItem
      // 
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      this.aboutToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
      this.aboutToolStripMenuItem.Text = "About";
      this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutDialog_Click);
      // 
      // viewHelpToolStripMenuItem
      // 
      this.viewHelpToolStripMenuItem.Name = "viewHelpToolStripMenuItem";
      this.viewHelpToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
      this.viewHelpToolStripMenuItem.Text = "View Help";
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 24);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.pnlBody);
      this.splitContainer1.Panel2.Controls.Add(this.pnlSelector);
      this.splitContainer1.Panel2.Controls.Add(this.panelFileName);
      this.splitContainer1.Size = new System.Drawing.Size(1354, 709);
      this.splitContainer1.SplitterDistance = 736;
      this.splitContainer1.TabIndex = 1;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabProject);
      this.tabControl1.Controls.Add(this.tabCommands);
      this.tabControl1.Controls.Add(this.tabElements);
      this.tabControl1.Controls.Add(this.tabData);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(736, 709);
      this.tabControl1.TabIndex = 0;
      this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
      this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
      this.tabControl1.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Deselecting);
      // 
      // tabProject
      // 
      this.tabProject.Controls.Add(this.tvwProject);
      this.tabProject.Location = new System.Drawing.Point(4, 22);
      this.tabProject.Name = "tabProject";
      this.tabProject.Padding = new System.Windows.Forms.Padding(3);
      this.tabProject.Size = new System.Drawing.Size(728, 683);
      this.tabProject.TabIndex = 3;
      this.tabProject.Text = "Project";
      this.tabProject.UseVisualStyleBackColor = true;
      // 
      // tvwProject
      // 
      this.tvwProject.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvwProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tvwProject.Location = new System.Drawing.Point(3, 3);
      this.tvwProject.Name = "tvwProject";
      treeNode1.Name = "nodTests";
      treeNode1.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      treeNode1.Text = "Tests";
      treeNode2.Name = "nodMacros";
      treeNode2.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      treeNode2.Text = "Macros";
      treeNode3.Name = "nodMenus";
      treeNode3.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      treeNode3.Text = "Menus";
      treeNode4.Name = "nodProject";
      treeNode4.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      treeNode4.Text = "Project";
      this.tvwProject.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
      this.tvwProject.ShowPlusMinus = false;
      this.tvwProject.ShowRootLines = false;
      this.tvwProject.Size = new System.Drawing.Size(722, 677);
      this.tvwProject.TabIndex = 0;
      this.tvwProject.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tvwProject_KeyUp);
      // 
      // tabCommands
      // 
      this.tabCommands.Controls.Add(this.lvCommands);
      this.tabCommands.Location = new System.Drawing.Point(4, 22);
      this.tabCommands.Name = "tabCommands";
      this.tabCommands.Padding = new System.Windows.Forms.Padding(3);
      this.tabCommands.Size = new System.Drawing.Size(728, 683);
      this.tabCommands.TabIndex = 1;
      this.tabCommands.Text = "Commands";
      this.tabCommands.UseVisualStyleBackColor = true;
      // 
      // lvCommands
      // 
      this.lvCommands.AllowDrop = true;
      this.lvCommands.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvCommands.FullRowSelect = true;
      this.lvCommands.GridLines = true;
      this.lvCommands.Location = new System.Drawing.Point(3, 3);
      this.lvCommands.MultiSelect = false;
      this.lvCommands.Name = "lvCommands";
      this.lvCommands.Size = new System.Drawing.Size(722, 677);
      this.lvCommands.TabIndex = 0;
      this.lvCommands.UseCompatibleStateImageBehavior = false;
      this.lvCommands.View = System.Windows.Forms.View.Details;
      this.lvCommands.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvCommands_ItemDrag);
      this.lvCommands.Click += new System.EventHandler(this.lvCommands_Click);
      this.lvCommands.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvCommands_DragDrop);
      this.lvCommands.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvCommands_DragEnter);
      this.lvCommands.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvCommands_KeyUp);
      // 
      // tabElements
      // 
      this.tabElements.Controls.Add(this.lvElements);
      this.tabElements.Location = new System.Drawing.Point(4, 22);
      this.tabElements.Name = "tabElements";
      this.tabElements.Padding = new System.Windows.Forms.Padding(3);
      this.tabElements.Size = new System.Drawing.Size(728, 683);
      this.tabElements.TabIndex = 0;
      this.tabElements.Text = "Elements";
      this.tabElements.UseVisualStyleBackColor = true;
      // 
      // lvElements
      // 
      this.lvElements.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvElements.FullRowSelect = true;
      this.lvElements.GridLines = true;
      this.lvElements.Location = new System.Drawing.Point(3, 3);
      this.lvElements.MultiSelect = false;
      this.lvElements.Name = "lvElements";
      this.lvElements.Size = new System.Drawing.Size(722, 677);
      this.lvElements.TabIndex = 0;
      this.lvElements.UseCompatibleStateImageBehavior = false;
      this.lvElements.View = System.Windows.Forms.View.Details;
      this.lvElements.Click += new System.EventHandler(this.lvElements_Click);
      this.lvElements.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvElements_KeyUp);
      // 
      // tabData
      // 
      this.tabData.Controls.Add(this.lvData);
      this.tabData.Location = new System.Drawing.Point(4, 22);
      this.tabData.Name = "tabData";
      this.tabData.Size = new System.Drawing.Size(728, 683);
      this.tabData.TabIndex = 2;
      this.tabData.Text = "Data";
      this.tabData.UseVisualStyleBackColor = true;
      // 
      // lvData
      // 
      this.lvData.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvData.FullRowSelect = true;
      this.lvData.GridLines = true;
      this.lvData.Location = new System.Drawing.Point(0, 0);
      this.lvData.MultiSelect = false;
      this.lvData.Name = "lvData";
      this.lvData.Size = new System.Drawing.Size(728, 683);
      this.lvData.TabIndex = 0;
      this.lvData.UseCompatibleStateImageBehavior = false;
      this.lvData.View = System.Windows.Forms.View.Details;
      // 
      // pnlBody
      // 
      this.pnlBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pnlBody.Controls.Add(this.btnDelete);
      this.pnlBody.Controls.Add(this.pnlElement);
      this.pnlBody.Controls.Add(this.pnlWindow);
      this.pnlBody.Controls.Add(this.pnlMacro);
      this.pnlBody.Controls.Add(this.pnlNavigation);
      this.pnlBody.Controls.Add(this.pnlBigText);
      this.pnlBody.Controls.Add(this.btnCancelCommand);
      this.pnlBody.Controls.Add(this.btnSaveCommand);
      this.pnlBody.Controls.Add(this.pnlMainEditor);
      this.pnlBody.Location = new System.Drawing.Point(0, 134);
      this.pnlBody.Name = "pnlBody";
      this.pnlBody.Size = new System.Drawing.Size(611, 575);
      this.pnlBody.TabIndex = 2;
      // 
      // btnDelete
      // 
      this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.btnDelete.Location = new System.Drawing.Point(239, 157);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(75, 23);
      this.btnDelete.TabIndex = 12;
      this.btnDelete.Text = "Delete";
      this.btnDelete.UseVisualStyleBackColor = true;
      this.btnDelete.Visible = false;
      this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
      // 
      // pnlElement
      // 
      this.pnlElement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pnlElement.Controls.Add(this.txtElementId);
      this.pnlElement.Controls.Add(this.lblElementId);
      this.pnlElement.Controls.Add(this.txtElementName);
      this.pnlElement.Controls.Add(this.lblElementName);
      this.pnlElement.Location = new System.Drawing.Point(0, 211);
      this.pnlElement.Name = "pnlElement";
      this.pnlElement.Size = new System.Drawing.Size(611, 100);
      this.pnlElement.TabIndex = 3;
      this.pnlElement.TabStop = true;
      this.pnlElement.Visible = false;
      // 
      // txtElementId
      // 
      this.txtElementId.Location = new System.Drawing.Point(239, 45);
      this.txtElementId.Name = "txtElementId";
      this.txtElementId.Size = new System.Drawing.Size(190, 20);
      this.txtElementId.TabIndex = 3;
      // 
      // lblElementId
      // 
      this.lblElementId.AutoSize = true;
      this.lblElementId.Location = new System.Drawing.Point(242, 28);
      this.lblElementId.Name = "lblElementId";
      this.lblElementId.Size = new System.Drawing.Size(57, 13);
      this.lblElementId.TabIndex = 2;
      this.lblElementId.Text = "Element Id";
      // 
      // txtElementName
      // 
      this.txtElementName.Location = new System.Drawing.Point(13, 45);
      this.txtElementName.Name = "txtElementName";
      this.txtElementName.Size = new System.Drawing.Size(189, 20);
      this.txtElementName.TabIndex = 1;
      // 
      // lblElementName
      // 
      this.lblElementName.AutoSize = true;
      this.lblElementName.Location = new System.Drawing.Point(13, 29);
      this.lblElementName.Name = "lblElementName";
      this.lblElementName.Size = new System.Drawing.Size(76, 13);
      this.lblElementName.TabIndex = 0;
      this.lblElementName.Text = "Element Name";
      // 
      // pnlWindow
      // 
      this.pnlWindow.Controls.Add(this.txtWindowArg1);
      this.pnlWindow.Controls.Add(this.lblWindowArg1);
      this.pnlWindow.Controls.Add(this.lblWindow);
      this.pnlWindow.Controls.Add(this.cboWindow);
      this.pnlWindow.Location = new System.Drawing.Point(0, 500);
      this.pnlWindow.Name = "pnlWindow";
      this.pnlWindow.Size = new System.Drawing.Size(652, 100);
      this.pnlWindow.TabIndex = 6;
      this.pnlWindow.TabStop = true;
      this.pnlWindow.Visible = false;
      // 
      // txtWindowArg1
      // 
      this.txtWindowArg1.Location = new System.Drawing.Point(239, 48);
      this.txtWindowArg1.Name = "txtWindowArg1";
      this.txtWindowArg1.Size = new System.Drawing.Size(190, 20);
      this.txtWindowArg1.TabIndex = 3;
      // 
      // lblWindowArg1
      // 
      this.lblWindowArg1.AutoSize = true;
      this.lblWindowArg1.Location = new System.Drawing.Point(239, 32);
      this.lblWindowArg1.Name = "lblWindowArg1";
      this.lblWindowArg1.Size = new System.Drawing.Size(125, 13);
      this.lblWindowArg1.TabIndex = 2;
      this.lblWindowArg1.Text = "Expected Window Count";
      // 
      // lblWindow
      // 
      this.lblWindow.AutoSize = true;
      this.lblWindow.Location = new System.Drawing.Point(13, 32);
      this.lblWindow.Name = "lblWindow";
      this.lblWindow.Size = new System.Drawing.Size(81, 13);
      this.lblWindow.TabIndex = 1;
      this.lblWindow.Text = "Window to Find";
      // 
      // cboWindow
      // 
      this.cboWindow.FormattingEnabled = true;
      this.cboWindow.Location = new System.Drawing.Point(13, 47);
      this.cboWindow.Name = "cboWindow";
      this.cboWindow.Size = new System.Drawing.Size(189, 21);
      this.cboWindow.TabIndex = 0;
      // 
      // pnlMacro
      // 
      this.pnlMacro.Controls.Add(this.lblMacro);
      this.pnlMacro.Controls.Add(this.cboMacro);
      this.pnlMacro.Location = new System.Drawing.Point(0, 350);
      this.pnlMacro.Name = "pnlMacro";
      this.pnlMacro.Size = new System.Drawing.Size(652, 100);
      this.pnlMacro.TabIndex = 4;
      this.pnlMacro.TabStop = true;
      this.pnlMacro.Visible = false;
      // 
      // lblMacro
      // 
      this.lblMacro.AutoSize = true;
      this.lblMacro.Location = new System.Drawing.Point(16, 30);
      this.lblMacro.Name = "lblMacro";
      this.lblMacro.Size = new System.Drawing.Size(37, 13);
      this.lblMacro.TabIndex = 1;
      this.lblMacro.Text = "Macro";
      // 
      // cboMacro
      // 
      this.cboMacro.FormattingEnabled = true;
      this.cboMacro.Location = new System.Drawing.Point(16, 49);
      this.cboMacro.Name = "cboMacro";
      this.cboMacro.Size = new System.Drawing.Size(190, 21);
      this.cboMacro.TabIndex = 0;
      // 
      // pnlNavigation
      // 
      this.pnlNavigation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pnlNavigation.Controls.Add(this.lblMenu4);
      this.pnlNavigation.Controls.Add(this.lblMenu3);
      this.pnlNavigation.Controls.Add(this.lblMenu2);
      this.pnlNavigation.Controls.Add(this.lblMenu1);
      this.pnlNavigation.Controls.Add(this.cboMenu4);
      this.pnlNavigation.Controls.Add(this.cboMenu3);
      this.pnlNavigation.Controls.Add(this.cboMenu2);
      this.pnlNavigation.Controls.Add(this.cboMenu1);
      this.pnlNavigation.Location = new System.Drawing.Point(0, 460);
      this.pnlNavigation.Name = "pnlNavigation";
      this.pnlNavigation.Size = new System.Drawing.Size(611, 100);
      this.pnlNavigation.TabIndex = 5;
      this.pnlNavigation.TabStop = true;
      this.pnlNavigation.Visible = false;
      // 
      // lblMenu4
      // 
      this.lblMenu4.AutoSize = true;
      this.lblMenu4.Location = new System.Drawing.Point(484, 26);
      this.lblMenu4.Name = "lblMenu4";
      this.lblMenu4.Size = new System.Drawing.Size(77, 13);
      this.lblMenu4.TabIndex = 7;
      this.lblMenu4.Text = "Menu Option 4";
      // 
      // lblMenu3
      // 
      this.lblMenu3.AutoSize = true;
      this.lblMenu3.Location = new System.Drawing.Point(327, 26);
      this.lblMenu3.Name = "lblMenu3";
      this.lblMenu3.Size = new System.Drawing.Size(77, 13);
      this.lblMenu3.TabIndex = 6;
      this.lblMenu3.Text = "Menu Option 3";
      // 
      // lblMenu2
      // 
      this.lblMenu2.AutoSize = true;
      this.lblMenu2.Location = new System.Drawing.Point(170, 26);
      this.lblMenu2.Name = "lblMenu2";
      this.lblMenu2.Size = new System.Drawing.Size(77, 13);
      this.lblMenu2.TabIndex = 5;
      this.lblMenu2.Text = "Menu Option 2";
      // 
      // lblMenu1
      // 
      this.lblMenu1.AutoSize = true;
      this.lblMenu1.Location = new System.Drawing.Point(13, 26);
      this.lblMenu1.Name = "lblMenu1";
      this.lblMenu1.Size = new System.Drawing.Size(77, 13);
      this.lblMenu1.TabIndex = 4;
      this.lblMenu1.Text = "Menu Option 1";
      // 
      // cboMenu4
      // 
      this.cboMenu4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboMenu4.FormattingEnabled = true;
      this.cboMenu4.Location = new System.Drawing.Point(484, 45);
      this.cboMenu4.Name = "cboMenu4";
      this.cboMenu4.Size = new System.Drawing.Size(150, 21);
      this.cboMenu4.TabIndex = 3;
      // 
      // cboMenu3
      // 
      this.cboMenu3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboMenu3.FormattingEnabled = true;
      this.cboMenu3.Location = new System.Drawing.Point(327, 45);
      this.cboMenu3.Name = "cboMenu3";
      this.cboMenu3.Size = new System.Drawing.Size(150, 21);
      this.cboMenu3.TabIndex = 2;
      this.cboMenu3.SelectedIndexChanged += new System.EventHandler(this.cboMenu3_SelectedIndexChanged);
      // 
      // cboMenu2
      // 
      this.cboMenu2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboMenu2.FormattingEnabled = true;
      this.cboMenu2.Location = new System.Drawing.Point(170, 45);
      this.cboMenu2.Name = "cboMenu2";
      this.cboMenu2.Size = new System.Drawing.Size(150, 21);
      this.cboMenu2.TabIndex = 1;
      this.cboMenu2.SelectedIndexChanged += new System.EventHandler(this.cboMenu2_SelectedIndexChanged);
      // 
      // cboMenu1
      // 
      this.cboMenu1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboMenu1.FormattingEnabled = true;
      this.cboMenu1.Location = new System.Drawing.Point(13, 45);
      this.cboMenu1.Name = "cboMenu1";
      this.cboMenu1.Size = new System.Drawing.Size(150, 21);
      this.cboMenu1.TabIndex = 0;
      this.cboMenu1.SelectedIndexChanged += new System.EventHandler(this.cboMenu1_SelectedIndexChanged);
      // 
      // pnlBigText
      // 
      this.pnlBigText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pnlBigText.Controls.Add(this.lblBigText);
      this.pnlBigText.Controls.Add(this.txtBigText);
      this.pnlBigText.Location = new System.Drawing.Point(0, 450);
      this.pnlBigText.Name = "pnlBigText";
      this.pnlBigText.Size = new System.Drawing.Size(611, 100);
      this.pnlBigText.TabIndex = 3;
      this.pnlBigText.Visible = false;
      // 
      // lblBigText
      // 
      this.lblBigText.AutoSize = true;
      this.lblBigText.Location = new System.Drawing.Point(13, 22);
      this.lblBigText.Name = "lblBigText";
      this.lblBigText.Size = new System.Drawing.Size(71, 13);
      this.lblBigText.TabIndex = 1;
      this.lblBigText.Text = "Web Address";
      // 
      // txtBigText
      // 
      this.txtBigText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtBigText.Location = new System.Drawing.Point(13, 41);
      this.txtBigText.Multiline = true;
      this.txtBigText.Name = "txtBigText";
      this.txtBigText.Size = new System.Drawing.Size(580, 43);
      this.txtBigText.TabIndex = 0;
      // 
      // btnCancelCommand
      // 
      this.btnCancelCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancelCommand.Location = new System.Drawing.Point(354, 157);
      this.btnCancelCommand.Name = "btnCancelCommand";
      this.btnCancelCommand.Size = new System.Drawing.Size(75, 23);
      this.btnCancelCommand.TabIndex = 11;
      this.btnCancelCommand.Text = "Cancel";
      this.btnCancelCommand.UseVisualStyleBackColor = true;
      this.btnCancelCommand.Click += new System.EventHandler(this.btnCancelCommand_Click);
      // 
      // btnSaveCommand
      // 
      this.btnSaveCommand.Location = new System.Drawing.Point(127, 157);
      this.btnSaveCommand.Name = "btnSaveCommand";
      this.btnSaveCommand.Size = new System.Drawing.Size(75, 23);
      this.btnSaveCommand.TabIndex = 10;
      this.btnSaveCommand.Text = "Save";
      this.btnSaveCommand.UseVisualStyleBackColor = true;
      this.btnSaveCommand.Click += new System.EventHandler(this.btnSaveCommand_Click);
      // 
      // pnlMainEditor
      // 
      this.pnlMainEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pnlMainEditor.Controls.Add(this.lblArg2);
      this.pnlMainEditor.Controls.Add(this.cboArg2);
      this.pnlMainEditor.Controls.Add(this.chkClear);
      this.pnlMainEditor.Controls.Add(this.txtArg1);
      this.pnlMainEditor.Controls.Add(this.lblArg1);
      this.pnlMainEditor.Controls.Add(this.cboElement);
      this.pnlMainEditor.Controls.Add(this.lblFinder1);
      this.pnlMainEditor.Location = new System.Drawing.Point(0, 3);
      this.pnlMainEditor.Name = "pnlMainEditor";
      this.pnlMainEditor.Size = new System.Drawing.Size(611, 100);
      this.pnlMainEditor.TabIndex = 2;
      this.pnlMainEditor.TabStop = true;
      // 
      // lblArg2
      // 
      this.lblArg2.AutoSize = true;
      this.lblArg2.Location = new System.Drawing.Point(466, 24);
      this.lblArg2.Name = "lblArg2";
      this.lblArg2.Size = new System.Drawing.Size(43, 13);
      this.lblArg2.TabIndex = 6;
      this.lblArg2.Text = "Options";
      // 
      // cboArg2
      // 
      this.cboArg2.FormattingEnabled = true;
      this.cboArg2.Location = new System.Drawing.Point(466, 39);
      this.cboArg2.Name = "cboArg2";
      this.cboArg2.Size = new System.Drawing.Size(121, 21);
      this.cboArg2.TabIndex = 4;
      // 
      // chkClear
      // 
      this.chkClear.AutoSize = true;
      this.chkClear.Enabled = false;
      this.chkClear.Location = new System.Drawing.Point(357, 65);
      this.chkClear.Name = "chkClear";
      this.chkClear.Size = new System.Drawing.Size(75, 17);
      this.chkClear.TabIndex = 5;
      this.chkClear.Text = "Clear Field";
      this.chkClear.UseVisualStyleBackColor = true;
      // 
      // txtArg1
      // 
      this.txtArg1.Enabled = false;
      this.txtArg1.Location = new System.Drawing.Point(239, 39);
      this.txtArg1.Name = "txtArg1";
      this.txtArg1.Size = new System.Drawing.Size(190, 20);
      this.txtArg1.TabIndex = 3;
      // 
      // lblArg1
      // 
      this.lblArg1.Location = new System.Drawing.Point(239, 24);
      this.lblArg1.Name = "lblArg1";
      this.lblArg1.Size = new System.Drawing.Size(190, 21);
      this.lblArg1.TabIndex = 2;
      this.lblArg1.Text = "Expected Window Count";
      // 
      // cboElement
      // 
      this.cboElement.Enabled = false;
      this.cboElement.FormattingEnabled = true;
      this.cboElement.Location = new System.Drawing.Point(12, 39);
      this.cboElement.Name = "cboElement";
      this.cboElement.Size = new System.Drawing.Size(190, 21);
      this.cboElement.Sorted = true;
      this.cboElement.TabIndex = 1;
      // 
      // lblFinder1
      // 
      this.lblFinder1.Location = new System.Drawing.Point(12, 24);
      this.lblFinder1.Name = "lblFinder1";
      this.lblFinder1.Size = new System.Drawing.Size(192, 21);
      this.lblFinder1.TabIndex = 0;
      this.lblFinder1.Text = "Element to Find";
      // 
      // pnlSelector
      // 
      this.pnlSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pnlSelector.Controls.Add(this.cboCommand);
      this.pnlSelector.Controls.Add(this.lblCommand);
      this.pnlSelector.Controls.Add(this.txtAlias);
      this.pnlSelector.Controls.Add(this.lblAlias);
      this.pnlSelector.Location = new System.Drawing.Point(0, 22);
      this.pnlSelector.Name = "pnlSelector";
      this.pnlSelector.Size = new System.Drawing.Size(611, 100);
      this.pnlSelector.TabIndex = 1;
      this.pnlSelector.TabStop = true;
      // 
      // cboCommand
      // 
      this.cboCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboCommand.FormattingEnabled = true;
      this.cboCommand.Location = new System.Drawing.Point(242, 38);
      this.cboCommand.Name = "cboCommand";
      this.cboCommand.Size = new System.Drawing.Size(190, 21);
      this.cboCommand.TabIndex = 3;
      this.cboCommand.SelectedIndexChanged += new System.EventHandler(this.cboCommand_SelectedIndexChanged);
      // 
      // lblCommand
      // 
      this.lblCommand.AutoSize = true;
      this.lblCommand.Location = new System.Drawing.Point(239, 22);
      this.lblCommand.Name = "lblCommand";
      this.lblCommand.Size = new System.Drawing.Size(54, 13);
      this.lblCommand.TabIndex = 2;
      this.lblCommand.Text = "Command";
      // 
      // txtAlias
      // 
      this.txtAlias.Location = new System.Drawing.Point(13, 38);
      this.txtAlias.Name = "txtAlias";
      this.txtAlias.Size = new System.Drawing.Size(190, 20);
      this.txtAlias.TabIndex = 1;
      // 
      // lblAlias
      // 
      this.lblAlias.AutoSize = true;
      this.lblAlias.Location = new System.Drawing.Point(13, 22);
      this.lblAlias.Name = "lblAlias";
      this.lblAlias.Size = new System.Drawing.Size(29, 13);
      this.lblAlias.TabIndex = 0;
      this.lblAlias.Text = "Alias";
      // 
      // panelFileName
      // 
      this.panelFileName.Controls.Add(this.txtFilePath);
      this.panelFileName.Controls.Add(this.lblFilePath);
      this.panelFileName.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelFileName.Location = new System.Drawing.Point(0, 0);
      this.panelFileName.Name = "panelFileName";
      this.panelFileName.Size = new System.Drawing.Size(614, 21);
      this.panelFileName.TabIndex = 0;
      // 
      // txtFilePath
      // 
      this.txtFilePath.BackColor = System.Drawing.SystemColors.Menu;
      this.txtFilePath.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtFilePath.Enabled = false;
      this.txtFilePath.Location = new System.Drawing.Point(77, 4);
      this.txtFilePath.Name = "txtFilePath";
      this.txtFilePath.Size = new System.Drawing.Size(797, 13);
      this.txtFilePath.TabIndex = 999;
      this.txtFilePath.TabStop = false;
      // 
      // lblFilePath
      // 
      this.lblFilePath.AutoSize = true;
      this.lblFilePath.Location = new System.Drawing.Point(-1, 4);
      this.lblFilePath.Name = "lblFilePath";
      this.lblFilePath.Size = new System.Drawing.Size(71, 13);
      this.lblFilePath.TabIndex = 999;
      this.lblFilePath.Text = "Selected File:";
      // 
      // Composer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1354, 733);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.menuStrip1);
      this.Name = "Composer";
      this.Text = "Simple Selenium Test Composer";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Closing);
      this.Load += new System.EventHandler(this.Form_OnLoad);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabProject.ResumeLayout(false);
      this.tabCommands.ResumeLayout(false);
      this.tabElements.ResumeLayout(false);
      this.tabData.ResumeLayout(false);
      this.pnlBody.ResumeLayout(false);
      this.pnlElement.ResumeLayout(false);
      this.pnlElement.PerformLayout();
      this.pnlWindow.ResumeLayout(false);
      this.pnlWindow.PerformLayout();
      this.pnlMacro.ResumeLayout(false);
      this.pnlMacro.PerformLayout();
      this.pnlNavigation.ResumeLayout(false);
      this.pnlNavigation.PerformLayout();
      this.pnlBigText.ResumeLayout(false);
      this.pnlBigText.PerformLayout();
      this.pnlMainEditor.ResumeLayout(false);
      this.pnlMainEditor.PerformLayout();
      this.pnlSelector.ResumeLayout(false);
      this.pnlSelector.PerformLayout();
      this.panelFileName.ResumeLayout(false);
      this.panelFileName.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabElements;
        private System.Windows.Forms.TabPage tabCommands;
        private System.Windows.Forms.TabPage tabData;
        private System.Windows.Forms.ListView lvCommands;
        private System.Windows.Forms.Panel panelFileName;
        private System.Windows.Forms.ListView lvElements;
        private System.Windows.Forms.ListView lvData;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Panel pnlSelector;
        private System.Windows.Forms.ComboBox cboCommand;
        private System.Windows.Forms.Label lblCommand;
        private System.Windows.Forms.TextBox txtAlias;
        private System.Windows.Forms.Label lblAlias;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.ToolStripMenuItem viewHelpToolStripMenuItem;
        private System.Windows.Forms.Panel pnlMainEditor;
        private System.Windows.Forms.CheckBox chkClear;
        private System.Windows.Forms.TextBox txtArg1;
        private System.Windows.Forms.Label lblArg1;
        private System.Windows.Forms.ComboBox cboElement;
        private System.Windows.Forms.Label lblFinder1;
        private System.Windows.Forms.Button btnCancelCommand;
        private System.Windows.Forms.Button btnSaveCommand;
        private System.Windows.Forms.Panel pnlBigText;
        private System.Windows.Forms.Label lblBigText;
        private System.Windows.Forms.TextBox txtBigText;
        private System.Windows.Forms.Panel pnlNavigation;
        private System.Windows.Forms.Label lblMenu4;
        private System.Windows.Forms.Label lblMenu3;
        private System.Windows.Forms.Label lblMenu2;
        private System.Windows.Forms.Label lblMenu1;
        private System.Windows.Forms.ComboBox cboMenu4;
        private System.Windows.Forms.ComboBox cboMenu3;
        private System.Windows.Forms.ComboBox cboMenu2;
        private System.Windows.Forms.ComboBox cboMenu1;
        private System.Windows.Forms.Panel pnlMacro;
        private System.Windows.Forms.Label lblMacro;
        private System.Windows.Forms.ComboBox cboMacro;
        private System.Windows.Forms.Panel pnlWindow;
        private System.Windows.Forms.Label lblWindow;
        private System.Windows.Forms.ComboBox cboWindow;
        private System.Windows.Forms.TextBox txtWindowArg1;
        private System.Windows.Forms.Label lblWindowArg1;
        private System.Windows.Forms.Panel pnlElement;
        private System.Windows.Forms.TextBox txtElementId;
        private System.Windows.Forms.Label lblElementId;
        private System.Windows.Forms.TextBox txtElementName;
        private System.Windows.Forms.Label lblElementName;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ToolStripMenuItem closeTestToolStripMenuItem;
        private System.Windows.Forms.Label lblArg2;
        private System.Windows.Forms.ComboBox cboArg2;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.TabPage tabProject;
        private System.Windows.Forms.TreeView tvwProject;


    }
}

