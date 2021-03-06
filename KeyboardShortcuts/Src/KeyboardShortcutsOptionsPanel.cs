﻿using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;
using System.Data;
using System.Drawing;

namespace ClarionAddins.KeyboardShortcuts
{
    public partial class KeyboardShortcutsOptionsPanel : AbstractOptionPanel
    {
        private KeysConverter _KeysConverter = new KeysConverter();
        private bool _editingMenu;

        public KeyboardShortcutsOptionsPanel()
        {
            InitializeComponent();
            DataTable dt = Helper.GetToolItemTable();
            SetupGrid(dt);
        }

        private void SetupGrid(DataTable dt)
        {
            
            dataGridView.RowTemplate.Height = dataGridView.RowTemplate.Height - 2;
            dataGridView.DataSource = dt;
            dataGridView.RowHeadersVisible = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToResizeColumns = true;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.ReadOnly = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
            dataGridViewCellStyle.BackColor = SystemColors.ButtonFace;
            dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
            if (PropertyService.Get<bool>("ClarionAddins.IdeDebug.cbExtraDebugInfo", false) == false)
                dataGridView.Columns[Helper.ColumnName.Name.ToString()].Visible = false;

            foreach (DataGridViewColumn col in dataGridView.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dataGridView.Columns[Helper.ColumnName.Image.ToString()].HeaderText = "";
            dataGridView.Columns[Helper.ColumnName.Image.ToString()].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[Helper.ColumnName.Label.ToString()].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        public override void LoadPanelContents()
        {
            // not used yet, obviously...
        }

        public override bool StorePanelContents()
        {
            // save any changes to the "new" column into the properties file
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[Helper.ColumnName.Name.ToString()].Value.ToString().Contains("*TL*"))
                {
                    string menuName = (string)row.Cells[Helper.ColumnName.Name.ToString()].Value;
                    ICSharpCode.Core.Properties newMenuProps = Helper.Properties.Get(menuName, new ICSharpCode.Core.Properties());
                    newMenuProps.Set("newLabel", (string)row.Cells[Helper.ColumnName.Label.ToString()].Value);
                    Helper.Properties.Set(menuName, newMenuProps);
                }
                else
                {
                    string codonID = (string)row.Cells[Helper.ColumnName.Name.ToString()].Value;
                    ICSharpCode.Core.Properties newKeyProps = Helper.Properties.Get(codonID, new ICSharpCode.Core.Properties());

                    if (row.Cells[Helper.ColumnName.New.ToString()].Value == System.DBNull.Value)
                        newKeyProps.Set(Helper.ColumnName.New.ToString(), String.Empty);
                    else
                    {
                        Keys newKey = (Keys)row.Cells[Helper.ColumnName.New.ToString()].Value;
                        newKeyProps.Set(Helper.ColumnName.New.ToString(), _KeysConverter.ConvertToString(newKey));
                    }
                    Helper.Properties.Set(codonID, newKeyProps);
                }
            }
            Helper.Properties.Save(Helper.PropertiesFileName);
            Helper.ApplyShortcutKeys();
            return true;
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.SelectedRows.Count > 0)
            {
                if (dgv.SelectedRows[0].Cells[Helper.ColumnName.Name.ToString()].Value.ToString().Contains("*TL*"))
                {
                    _editingMenu = true;
                    labelCurrentSelection.Text = "Edit Menu Label";
                    labelEntryPrompt.Text = "Menu Label:";
                    buttonNone.Enabled = false;
                    buttonRemove.Enabled = false;
                    labelUsedBy.Visible = false;
                    tbNewShortcut.Text = dgv.SelectedRows[0].Cells[Helper.ColumnName.Label.ToString()].Value.ToString();
                }
                else
                {
                    _editingMenu = false;
                    labelCurrentSelection.Text = "Re-Assign Shortcut: " + dgv.SelectedRows[0].Cells[Helper.ColumnName.Label.ToString()].Value.ToString();
                    labelEntryPrompt.Text = "New shortcut key:";
                    buttonNone.Enabled = true;
                    buttonRemove.Enabled = true;
                    labelUsedBy.Visible = true;
                    tbNewShortcut.Text = "";
                }
            }

        }

        private void buttonAssign_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                if (_editingMenu == false)
                {
                    if (tbNewShortcut.Text == "")
                    {
                        dataGridView.SelectedRows[0].Cells[Helper.ColumnName.New.ToString()].Value = System.DBNull.Value;
                    }
                    else
                    {
                        dataGridView.SelectedRows[0].Cells[Helper.ColumnName.New.ToString()].Value =
                            _KeysConverter.ConvertFromString(tbNewShortcut.Text);
                    }
                }
                else
                {
                    if (tbNewShortcut.Text != "")
                        dataGridView.SelectedRows[0].Cells[Helper.ColumnName.Label.ToString()].Value = tbNewShortcut.Text;
                }
                dataGridView.Refresh();
            }
        }

        private void tbNewShortcut_KeyDown(object sender, KeyEventArgs e)
        {
            if (_editingMenu == false)
                e.Handled = true;
        }

        private void tbNewShortcut_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (_editingMenu == false)
                tbNewShortcut.Text = _KeysConverter.ConvertToString(e.KeyData);
        }

        private void tbNewShortcut_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_editingMenu == false)
                e.Handled = true;
        }

        private void tbNewShortcut_TextChanged(object sender, EventArgs e)
        {
            if (_editingMenu == false && dataGridView.Rows.Count > 0)
            {
                string label = MatchColumnCell(Helper.ColumnName.New, tbNewShortcut.Text);
                if (label == String.Empty)
                    label = MatchColumnCell(Helper.ColumnName.Original, tbNewShortcut.Text);

                if (label != String.Empty)
                    labelUsedBy.Text = "Shortcut currently used by: " + label;
                else
                    labelUsedBy.Text = "Shortcut currently not used.";
            }
        }

        private string MatchColumnCell(Helper.ColumnName columnName, string findString)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[columnName.ToString()].Value == System.DBNull.Value)
                    continue;

                Keys cellKey = (Keys)row.Cells[columnName.ToString()].Value;

                if (_KeysConverter.ConvertToString(cellKey) == findString)
                {
                    return row.Cells[Helper.ColumnName.Label.ToString()].Value.ToString();
                }
            }

            return String.Empty;
        }

        private void buttonNone_Click(object sender, EventArgs e)
        {
            tbNewShortcut.Text = _KeysConverter.ConvertToString(Keys.None);
            buttonAssign.PerformClick();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            tbNewShortcut.Text = "";
            buttonAssign.PerformClick();
        }

    }

}