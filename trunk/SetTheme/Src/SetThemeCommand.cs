using System;
using System.IO;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;
using UserControls.Krypton.PaletteSelectors;
using WeifenLuo.WinFormsUI;

namespace ClarionEdge.SetTheme
{
    class SetThemeCommand : AbstractCommand
    {
        private KryptonManager kryptonManager;
        private SetTheme setTheme = new SetTheme();
        private string selectedThemeName = String.Empty;

        public override void Run()
        {

            if ((bool)PropertyService.Get("SetTheme.Options.disableAddin", false) == true)
            {
                return;
            }

            WorkbenchSingleton.WorkbenchCreated += WorkbenchCreated;
        }

        void WorkbenchCreated(object source, EventArgs e)
        {
            kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager();
            if ((PaletteModeManager)PropertyService.Get("SetTheme.Options.GlobalPaletteMode", (int)PaletteModeManager.ProfessionalSystem) == PaletteModeManager.Custom)
            {
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Custom;
            }
            else
            {
                kryptonManager.GlobalPaletteMode = (PaletteModeManager)PropertyService.Get("SetTheme.Options.GlobalPaletteMode", (int)PaletteModeManager.ProfessionalSystem);
            }
            LoggingService.Debug("kryptonManager.GlobalPaletteMode=" + (int)kryptonManager.GlobalPaletteMode);
            selectedThemeName = PropertyService.Get("SetTheme.Options.selectedThemeName", String.Empty);
            LoggingService.Debug("Attempting to load custom theme " + selectedThemeName);
            if ((PaletteModeManager)PropertyService.Get("SetTheme.Options.GlobalPaletteMode", (int)PaletteModeManager.ProfessionalSystem) == PaletteModeManager.Custom && selectedThemeName != String.Empty)
            {
                LoggingService.Debug("Finding custom theme " + selectedThemeName);
                LoggingService.Debug("Theme Path: " + setTheme.ThemesPath);
                DirectoryInfo directoryInfo = new DirectoryInfo(setTheme.ThemesPath);
                foreach (FileSystemInfo fileSystemInfo in directoryInfo.GetFileSystemInfos())
                {
                    LoggingService.Debug(fileSystemInfo.Name);
                    if (fileSystemInfo.Name == selectedThemeName)
                    {
                        CustomPalette customPalette = new CustomPalette();
                        customPalette.DisplayName = fileSystemInfo.Name;
                        TextReader tr = new StreamReader(fileSystemInfo.FullName);
                        customPalette.PaletteXml = tr.ReadToEnd();
                        kryptonManager.GlobalPalette = customPalette.Palette;
                        LoggingService.Debug("done!");
                        break;
                    }
                }
            }

            SetupDockPanel();
        }

        private void SetupDockPanel()
        {
            LoggingService.Debug("**** SetupDockPanel ***");
            IWorkbenchLayout wl = WorkbenchSingleton.Workbench.WorkbenchLayout;
            foreach (Control c in WorkbenchSingleton.MainForm.Controls)
            {
                if (c is ToolStripContainer)
                {
                    ToolStripContainer tsc = (ToolStripContainer)c;
                    foreach (Control innerC in tsc.ContentPanel.Controls)
                    {
                        if (innerC is Panel)
                        {
                            Panel helperPanel = (Panel)innerC;
                            foreach (Control panelC in helperPanel.Controls)
                            {
                                if (panelC is DockPanel)
                                {
                                    LoggingService.Debug("Found it!");
                                }
                            }
                        }
                        LoggingService.Debug("Panel control type: " + innerC.GetType().ToString());
                    }
                }
            }
        }
    }
}
