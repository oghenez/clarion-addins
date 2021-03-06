﻿namespace ClarionEdge.SetTheme
{
    partial class SetThemeOptionsPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            UserControls.Krypton.PaletteSelectors.CustomPalette customPalette1 = new UserControls.Krypton.PaletteSelectors.CustomPalette();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetThemeOptionsPanel));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.disableAddin = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.kcmDropDown = new UserControls.Krypton.PaletteSelectors.KryptonPaletteContextMenu();
            this.dbSelectTheme = new UserControls.Krypton.PaletteSelectors.KryptonPaletteDropButton();
            this.dgvCustomThemes = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.themeName = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnImport = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.btnDelete = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBox1 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomThemes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1)).BeginInit();
            this.kryptonGroup1.Panel.SuspendLayout();
            this.kryptonGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // disableAddin
            // 
            this.disableAddin.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.disableAddin.Location = new System.Drawing.Point(11, 11);
            this.disableAddin.Name = "disableAddin";
            this.disableAddin.Size = new System.Drawing.Size(214, 20);
            this.disableAddin.TabIndex = 1;
            this.disableAddin.Values.Text = "Disable this addin (requires restart)";
            this.disableAddin.CheckedChanged += new System.EventHandler(this.disableAddin_CheckedChanged);
            // 
            // kcmDropDown
            // 
            customPalette1.DisplayName = "test";
            customPalette1.PaletteXml = "asdasd";
            this.kcmDropDown.CustomPalettes.Add(customPalette1);
            this.kcmDropDown.StandardPalettes = ((System.Collections.ObjectModel.Collection<ComponentFactory.Krypton.Toolkit.PaletteModeManager>)(resources.GetObject("kcmDropDown.StandardPalettes")));
            this.kcmDropDown.PaletteSelected += new System.EventHandler<UserControls.Krypton.PaletteSelectors.PaletteSelectedEventArgs>(this.kryptonPaletteContextMenu1_PaletteSelected);
            // 
            // dbSelectTheme
            // 
            this.dbSelectTheme.ButtonText = "Select Theme";
            this.dbSelectTheme.KryptonContextMenu = this.kcmDropDown;
            this.dbSelectTheme.Location = new System.Drawing.Point(11, 37);
            this.dbSelectTheme.Name = "dbSelectTheme";
            this.dbSelectTheme.OverrideFocus.Content.DrawFocus = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.dbSelectTheme.Size = new System.Drawing.Size(240, 25);
            this.dbSelectTheme.Splitter = false;
            this.dbSelectTheme.TabIndex = 3;
            this.dbSelectTheme.Text = "Select Theme";
            this.dbSelectTheme.Values.ExtraText = "(current selection)";
            this.dbSelectTheme.Values.Image = ((System.Drawing.Image)(resources.GetObject("dbSelectTheme.Values.Image")));
            this.dbSelectTheme.Values.Text = "Select Theme";
            // 
            // dgvCustomThemes
            // 
            this.dgvCustomThemes.AllowUserToAddRows = false;
            this.dgvCustomThemes.AllowUserToDeleteRows = false;
            this.dgvCustomThemes.AllowUserToResizeColumns = false;
            this.dgvCustomThemes.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvCustomThemes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCustomThemes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.themeName});
            this.dgvCustomThemes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCustomThemes.Location = new System.Drawing.Point(0, 0);
            this.dgvCustomThemes.MultiSelect = false;
            this.dgvCustomThemes.Name = "dgvCustomThemes";
            this.dgvCustomThemes.RowHeadersVisible = false;
            this.dgvCustomThemes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustomThemes.ShowEditingIcon = false;
            this.dgvCustomThemes.Size = new System.Drawing.Size(437, 262);
            this.dgvCustomThemes.StateCommon.Background.Color1 = System.Drawing.SystemColors.Window;
            this.dgvCustomThemes.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
            this.dgvCustomThemes.TabIndex = 4;
            this.dgvCustomThemes.SelectionChanged += new System.EventHandler(this.dgvCustomThemes_SelectionChanged);
            // 
            // themeName
            // 
            this.themeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.themeName.DataPropertyName = "themeName";
            this.themeName.HeaderText = "Theme Name";
            this.themeName.Name = "themeName";
            this.themeName.ReadOnly = true;
            this.themeName.Width = 436;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.TitlePanel;
            this.kryptonLabel1.Location = new System.Drawing.Point(11, 68);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(227, 29);
            this.kryptonLabel1.TabIndex = 5;
            this.kryptonLabel1.Values.Text = "Manage Custom Themes";
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImport.Location = new System.Drawing.Point(11, 373);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(90, 25);
            this.btnImport.TabIndex = 6;
            this.btnImport.Values.Text = "Import";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // kryptonGroup1
            // 
            this.kryptonGroup1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonGroup1.Location = new System.Drawing.Point(11, 103);
            this.kryptonGroup1.Name = "kryptonGroup1";
            // 
            // kryptonGroup1.Panel
            // 
            this.kryptonGroup1.Panel.Controls.Add(this.dgvCustomThemes);
            this.kryptonGroup1.Size = new System.Drawing.Size(439, 264);
            this.kryptonGroup1.TabIndex = 7;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(107, 373);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 25);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Values.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonButton1.Location = new System.Drawing.Point(330, 373);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(119, 25);
            this.kryptonButton1.TabIndex = 9;
            this.kryptonButton1.Values.Text = "Palette Designer";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroupBox1.Location = new System.Drawing.Point(5, 5);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.disableAddin);
            this.kryptonGroupBox1.Panel.Controls.Add(this.dbSelectTheme);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonGroup1);
            this.kryptonGroupBox1.Panel.Controls.Add(this.btnImport);
            this.kryptonGroupBox1.Panel.Controls.Add(this.btnDelete);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonButton1);
            this.kryptonGroupBox1.Panel.Padding = new System.Windows.Forms.Padding(8);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(465, 415);
            this.kryptonGroupBox1.TabIndex = 10;
            this.kryptonGroupBox1.Values.Heading = "";
            // 
            // SetThemeOptionsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonGroupBox1);
            this.Name = "SetThemeOptionsPanel";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(475, 425);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomThemes)).EndInit();
            this.kryptonGroup1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1)).EndInit();
            this.kryptonGroup1.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox disableAddin;
        private UserControls.Krypton.PaletteSelectors.KryptonPaletteContextMenu kcmDropDown;
        private UserControls.Krypton.PaletteSelectors.KryptonPaletteDropButton dbSelectTheme;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvCustomThemes;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnImport;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDelete;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn themeName;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
    }
}
