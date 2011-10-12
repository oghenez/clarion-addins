using System;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;

namespace ClarionEdge.PropertyGridExtras
{
    public delegate void ChangedEventHandler(object sender, EventArgs e);

    class StartupCode : AbstractCommand
    {

        bool _startupDone = false;
        PropertyGridStateHandler gridState = new PropertyGridStateHandler();

        public override void Run()
        {
            WorkbenchSingleton.WorkbenchCreated += new System.EventHandler(WorkbenchSingleton_WorkbenchCreated);
        }

        void WorkbenchSingleton_WorkbenchCreated(object sender, System.EventArgs e)
        {

            WorkbenchSingleton.Workbench.ActiveWorkbenchWindowChanged += new System.EventHandler(Workbench_ActiveWorkbenchWindowChanged);
        }

        void Workbench_ActiveWorkbenchWindowChanged(object sender, System.EventArgs e)
        {
            gridState.ActiveWorkbenchWindowChanged();

            if (_startupDone == true || PropertyPad.Grid == null)
                return;

            // This event is handy for doing things when focus or pad instance changes
            PropertyPad.SelectedObjectChanged += new System.EventHandler(PropertyPad_SelectedObjectChanged);

            // Init all the additional properties
            PropertyGridHelper.AutoAdjustLabelColumn();
            PropertyGridHelper.ShowAdditionalIndentation();
            PropertyGridHelper.SetDrawManager();
            PropertyGridHelper.SetFonts();

            gridState.RegisterEventHandlers(PropertyPad.Grid);
            _startupDone = true;
        }

        void PropertyPad_SelectedObjectChanged(object sender, System.EventArgs e)
        {
            //LoggingService.Debug("PropertyPad_SelectedObjectChanged");

            gridState.SelectedObjectChanged(PropertyPad.Grid.SelectedObject);
            
            //if (PropertyPad.Grid.Site != null)
            //    LoggingService.Debug("PropertyPad.Grid.Site=" + PropertyPad.Grid.Site.Name);

            PropertyGridHelper.AutoAdjustLabelColumn();
        }

    }
}
