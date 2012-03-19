using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ICSharpCode.SharpDevelop.Gui;
using System.Windows.Forms;
using ICSharpCode.Core;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Xml;

namespace ClarionAddins.KeyboardShortcuts
{
    public static class Helper
    {
        public enum ColumnName
        {
            Image,
            Label,
            Name,
            Original,
            New
        }
        private static KeysConverter _KeysConverter = new KeysConverter();
        private static ICSharpCode.Core.Properties _properties;
        public static ICSharpCode.Core.Properties Properties
        {
            get
            {
                if (_properties == null)
                {
                    _properties = LoadProperties(PropertiesFileName);
                }
                return _properties;
            }
        }

        private static string _propertiesFileName = string.Empty;
        public static string PropertiesFileName
        {
            get
            {
                if (_propertiesFileName == String.Empty)
                {
                    _propertiesFileName = Path.Combine(PropertyService.ConfigDirectory, "KeyBindings.xml");
                }

                return _propertiesFileName;
            }
        }

        internal static DataTable GetToolItemTable()
        {
            DataTable dt = new DataTable("ToolItems");
            dt.Columns.Add(ColumnName.Image.ToString(), typeof(Image));
            dt.Columns.Add(ColumnName.Label.ToString(), typeof(String));
            dt.Columns.Add(ColumnName.Name.ToString(), typeof(String));
            dt.Columns.Add(ColumnName.Original.ToString(), typeof(Keys));
            dt.Columns.Add(ColumnName.New.ToString(), typeof(Keys));

            DefaultWorkbench wbForm;
            wbForm = (DefaultWorkbench)WorkbenchSingleton.Workbench;
            foreach (object o in wbForm.TopMenu.Items)
            {
                if (o is ICSharpCode.Core.Menu)
                {
                    ToolStripMenuItem menuItem = (ToolStripMenuItem)o;
                    SetTopMenuTableRow(menuItem, dt);

                    foreach (ToolStripItem tsi in menuItem.DropDownItems)
                    {
                        if (tsi is MenuCommand)
                        {
                            SetTableRow((MenuCommand)tsi, dt);
                        }
                    }
                }
            }
            /*
            DataRow row = dt.NewRow();
            row[ColumnName.Image.ToString()] = new Bitmap(16, 16);
            row[ColumnName.Label.ToString()] = "          ============ Toolbar ============";
            row[ColumnName.Name.ToString()] = "";
            row[ColumnName.Original.ToString()] = System.DBNull.Value;
            row[ColumnName.New.ToString()] = System.DBNull.Value;
            dt.Rows.Add(row);

            foreach (ToolStrip toolBar in wbForm.ToolBars)
            {
                foreach (ToolStripItem tsi in toolBar.Items)
                {
                    if (tsi is ToolBarCommand)
                    {
                        SetTableRow((ToolBarCommand)tsi, dt);
                    }
                }
            }
            */
            Properties.Save(Helper.PropertiesFileName);
            return dt;
        }

        private static void SetTopMenuTableRow(ToolStripMenuItem menuItem, DataTable dt)
        {

            DataRow row = dt.NewRow();
            row[ColumnName.Image.ToString()] = new Bitmap(16, 16);
            row[ColumnName.Label.ToString()] = "          ============ Menu ============";
            row[ColumnName.Name.ToString()] = "";
            row[ColumnName.Original.ToString()] = System.DBNull.Value;
            row[ColumnName.New.ToString()] = System.DBNull.Value;
            dt.Rows.Add(row);

            string menuName = "*TL*" + menuItem.Text;
            ICSharpCode.Core.Properties newMenuProps = Properties.Get(menuName, new ICSharpCode.Core.Properties());
            string newMenuLabel = newMenuProps.Get("newLabel", menuItem.Text);
            if (newMenuLabel != String.Empty && _ApplyKeys == true)
            {
                menuItem.Text = newMenuLabel;
            }
            row = dt.NewRow();
            row[ColumnName.Image.ToString()] = new Bitmap(16, 16);
            row[ColumnName.Label.ToString()] = menuItem.Text;
            row[ColumnName.Name.ToString()] = menuName;
            row[ColumnName.Original.ToString()] = System.DBNull.Value;
            row[ColumnName.New.ToString()] = System.DBNull.Value;
            dt.Rows.Add(row);
        }

        private static void SetTableRow(object command, DataTable dt)
        {
            DataRow row = dt.NewRow();
            ToolStripMenuItem tsi = (ToolStripMenuItem)command;
            if (tsi.Text != null && tsi.Text != String.Empty)
                row[ColumnName.Label.ToString()] = tsi.Text.Replace("&", "");
            else
                row[ColumnName.Label.ToString()] = tsi.ToolTipText;

            string codonID;
            try
            {
                if (command is MenuCommand)
                    codonID = ((MenuCommand)command).CodonId;
                else if (command is ToolBarCommand)
                    codonID = ((ToolBarCommand)command).CodonId;
                else
                    codonID = String.Empty;
            }
            catch
            {
                codonID = row[ColumnName.Label.ToString()].ToString();
            }

            row[ColumnName.Name.ToString()] = codonID;
            // Don't do this for menu items, they are ALL hidden until dropped down :)
            if (command is ToolBarCommand && tsi.Visible == false)
                row[ColumnName.Label.ToString()] += " (Hidden)";

            object originalKey = GetOriginalKeys(codonID, tsi);
            if (originalKey == System.DBNull.Value)
            {
                // Skip this item for now.
                return;
            }
            else
            {
                row[ColumnName.Original.ToString()] = (Keys)originalKey;

            }

            if (tsi.Image != null)
            {
                Bitmap newImage = new Bitmap(16, 16);
                using (Graphics gr = Graphics.FromImage(newImage))
                {
                    gr.SmoothingMode = SmoothingMode.AntiAlias;
                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    gr.DrawImage(tsi.Image, new Rectangle(0, 0, 16, 16));
                }
                row[ColumnName.Image.ToString()] = newImage;
            }
            else
            {
                row[ColumnName.Image.ToString()] = new Bitmap(16, 16);
            }

            GetNewShortcutKeys(row, tsi, codonID);

            dt.Rows.Add(row);
        }

        private static object GetOriginalKeys(string codonID, ToolStripMenuItem tsi)
        {
            ICSharpCode.Core.Properties newKeyProps = Properties.Get(codonID, new ICSharpCode.Core.Properties());
            string savedKeysStr = newKeyProps.Get(ColumnName.Original.ToString(), String.Empty);

            if (_StoreOriginalKeys == true) 
            {
                newKeyProps.Set(ColumnName.Original.ToString(), _KeysConverter.ConvertToString(tsi.ShortcutKeys));
                Properties.Set(codonID, newKeyProps);
                return tsi.ShortcutKeys;
            }
            else if (savedKeysStr == "")
            {
                // This happens if the current menu/toolbar item was NOT included during start up (_StoreOriginalKeys == true_)
                // At the moment we will NOT display or handle those items
                return System.DBNull.Value;
            }
            
            string newKeysStr = newKeyProps.Get(ColumnName.Original.ToString(), _KeysConverter.ConvertToString(Keys.None));
            return (Keys)_KeysConverter.ConvertFromString(newKeysStr);

        }

        internal static void GetNewShortcutKeys(DataRow row, ToolStripMenuItem tsi, String codonID)
        {
            ICSharpCode.Core.Properties newKeyProps = Properties.Get(codonID, new ICSharpCode.Core.Properties());
            string newKeysStr = newKeyProps.Get(ColumnName.New.ToString(), String.Empty);
            try
            {
                if (newKeysStr != String.Empty)
                {
                    Keys newKeys = (Keys)_KeysConverter.ConvertFromString(newKeysStr);
                    row[ColumnName.New.ToString()] = newKeys;
                    if (_ApplyKeys == true)
                    {
                        tsi.ShortcutKeys = newKeys;
                    }
                }
                else
                {
                    row[ColumnName.New.ToString()] = System.DBNull.Value;
                    if (_ApplyKeys == true)
                    {
                        tsi.ShortcutKeys = (Keys)row[ColumnName.Original.ToString()];
                    }
                }
            }
            catch (Exception e)
            {
                LoggingService.Debug("Exception trying to apply shortcut key! e=" + e.ToString());
            }
        }

        private static ICSharpCode.Core.Properties LoadProperties(string fileName)
        {
            if (!File.Exists(fileName))
            {
                ICSharpCode.Core.Properties properties = new ICSharpCode.Core.Properties();
                return properties;
            }
            using (XmlTextReader reader = new XmlTextReader(fileName))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.LocalName)
                        {
                            case "Properties":
                                ICSharpCode.Core.Properties properties = new ICSharpCode.Core.Properties();
                                properties.ReadProperties(reader, "Properties");
                                return properties;
                        }
                    }
                }
            }
            return null;
        }

        private static bool _ApplyKeys { get; set; }
        public static void ApplyShortcutKeys()
        {
            _ApplyKeys = true;
            DataTable dt = Helper.GetToolItemTable();
            _ApplyKeys = false;
        }

        private static bool _StoreOriginalKeys { get; set; }
        public static void StoreOriginalKeys()
        {
            _StoreOriginalKeys = true;
            DataTable dt = Helper.GetToolItemTable();
            _StoreOriginalKeys = false;
        }
    }
}
