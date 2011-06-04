using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;
using UserControls.Krypton.PaletteSelectors;

namespace ClarionEdge.SetTheme
{
    public partial class SetThemeOptionsPanel : AbstractOptionPanel
    {
        private SetTheme setTheme = new SetTheme();
        private KryptonManager kryptonManager;
        DataTable dtCustomThemes;
        DataSet dsCustomThemes;
        private string customThemePath;
        private string selectedThemeName = String.Empty;

        public SetThemeOptionsPanel()
        {
            InitializeComponent();

            //foreach (string s in SplashScreenForm.GetParameterList())
            //{
            //    LoggingService.Debug("s: " + s);
            //}
            //foreach (string arg in SharpDevelopMain.CommandLineArgs)
            //{
            //    LoggingService.Debug("command: " + arg);
            //}

        }

        private void RefreshCustomThemes()
        {
            dtCustomThemes = new DataTable("CustomThemes");
            dsCustomThemes = new DataSet();
            dtCustomThemes.Columns.Add("themeName");
            kcmDropDown = new UserControls.Krypton.PaletteSelectors.KryptonPaletteContextMenu();
            DirectoryInfo directoryInfo = new DirectoryInfo(customThemePath);
            foreach (FileSystemInfo fileSystemInfo in directoryInfo.GetFileSystemInfos())
            {
                dtCustomThemes.Rows.Add(fileSystemInfo.Name);

                CustomPalette customPalette = new CustomPalette();
                customPalette.DisplayName = fileSystemInfo.Name;
                TextReader tr = new StreamReader(fileSystemInfo.FullName);
                customPalette.PaletteXml = tr.ReadToEnd();
                kcmDropDown.CustomPalettes.Add(customPalette);

            }
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetThemeOptionsPanel));
            this.kcmDropDown.StandardPalettes = ((Collection<PaletteModeManager>)(resources.GetObject("kcmDropDown.StandardPalettes")));
            var modes = new Collection<PaletteModeManager>();
            foreach (var mode in Enum.GetValues(typeof(PaletteModeManager)))
            {
                LoggingService.Debug("mode: " + mode);
                if ((PaletteModeManager)mode != PaletteModeManager.Custom)
                    modes.Add((PaletteModeManager)mode);
            }
            kcmDropDown.StandardPalettes = modes;
            kcmDropDown.PaletteSelected += new System.EventHandler<UserControls.Krypton.PaletteSelectors.PaletteSelectedEventArgs>(this.kryptonPaletteContextMenu1_PaletteSelected);

            dbSelectTheme.KryptonContextMenu = kcmDropDown;

            dsCustomThemes.Tables.Add(dtCustomThemes);
            dgvCustomThemes.DataSource = dsCustomThemes;
            dgvCustomThemes.DataMember = "CustomThemes";

        }

        private void disableAddin_CheckedChanged(object sender, EventArgs e)
        {
            if (disableAddin.Checked == true)
            {
                dbSelectTheme.Enabled = false;
            } else {
                dbSelectTheme.Enabled = true;
            }
        }

        public override void LoadPanelContents()
        {
            customThemePath = setTheme.ThemesPath;
            disableAddin.Checked = (bool)PropertyService.Get("SetTheme.Options.disableAddin", false);         
            kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager();

            RefreshCustomThemes();

            kryptonManager.GlobalPaletteMode = (PaletteModeManager)PropertyService.Get("SetTheme.Options.GlobalPaletteMode", PaletteModeManager.ProfessionalSystem);
            selectedThemeName = PropertyService.Get("SetTheme.Options.selectedThemeName", String.Empty);
            if (kryptonManager.GlobalPaletteMode == PaletteModeManager.Custom && selectedThemeName != String.Empty)
            {
                foreach (CustomPalette cp in kcmDropDown.CustomPalettes)
                {
                    if (cp.DisplayName == selectedThemeName)
                    {
                        kryptonManager.GlobalPalette = cp.Palette;
                        break;
                    }
                }
            }
            SetDropButtonText(selectedThemeName);
        }

        private void SetDropButtonText(string p)
        {
            dbSelectTheme.Values.ExtraText = "(" + p + ")";
            
            //kryptonManager.GlobalAllowFormChrome = true;
            //base.BackColor = System.Drawing.ColorTranslator.FromHtml("#5D564F");
            //WorkbenchSingleton.MainForm.BackColor = System.Drawing.ColorTranslator.FromHtml("#5D564F");
            //WorkbenchSingleton.MainForm.Font = new Font("Segoe UI", 7);
            //foreach (Form f in WorkbenchSingleton.MainForm.OwnedForms)
            //{
            //    f.BackColor = System.Drawing.ColorTranslator.FromHtml("#5D564F");
            //    f.Font = new Font("Segoe UI", 7);
            //}
            ////WorkbenchSingleton.MainForm.ForeColor = System.Drawing.Color.HotPink;
            //foreach (Form f in WorkbenchSingleton.MainForm.OwnedForms)
            //{
            //    f.BackColor = System.Drawing.ColorTranslator.FromHtml("#5D564F");
            //}
            //foreach (PadDescriptor pad in WorkbenchSingleton.Workbench.PadContentCollection)
            //{
            //    pad.PadContent.Control.BackColor = System.Drawing.ColorTranslator.FromHtml("#5D564F");             
            //    foreach (Control c in pad.PadContent.Control.Controls)
            //    {
            //        c.BackColor = System.Drawing.ColorTranslator.FromHtml("#5D564F");
            //        //c.ForeColor = System.Drawing.Color.Gray;
            //        c.Font = new Font("Segoe UI", 7);
            //    }
            //}
            
        }

        public override bool StorePanelContents()
        {
            PropertyService.Set("SetTheme.Options.disableAddin", disableAddin.Checked);
            PropertyService.Set("SetTheme.Options.GlobalPaletteMode", (int)kryptonManager.GlobalPaletteMode);
            PropertyService.Set("SetTheme.Options.selectedThemeName", selectedThemeName);
            PropertyService.Save();
            return true;
        }

        private void kryptonPaletteContextMenu1_PaletteSelected(object sender, UserControls.Krypton.PaletteSelectors.PaletteSelectedEventArgs e)
        {
            kryptonManager.GlobalPaletteMode = e.PaletteMode;
            selectedThemeName = e.Name;
            if (e.CustomPalette != null)
            {
                kryptonManager.GlobalPalette = e.CustomPalette.Palette;
            }
            SetDropButtonText(e.Name);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {          
			using (OpenFileDialog dlg = new OpenFileDialog())
            {
				dlg.Filter = "Theme files|*.xml|All files|*.*";
                dlg.RestoreDirectory = true;
                if (dlg.ShowDialog() == DialogResult.OK) 
                {
                    File.Copy(dlg.FileName, Path.Combine(customThemePath, Path.GetFileName(dlg.FileName)), true);
                    RefreshCustomThemes();
				}
			}
        }

        private void dgvCustomThemes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomThemes.SelectedRows.Count > 0)
            {
                btnDelete.Enabled = true;
            }
            else
            {
                btnDelete.Enabled = false;                
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            File.Delete(Path.Combine(customThemePath, dgvCustomThemes.Rows[dgvCustomThemes.CurrentRow.Index].Cells["themeName"].Value.ToString()));
            RefreshCustomThemes();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(setTheme.AddinPath, "PaletteDesigner.exe"));
        }
    }
}
