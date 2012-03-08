using System;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;

namespace ClarionEdge.PropertyGridExtras
{
    public delegate void ChangedEventHandler(object sender, EventArgs e);

    class StartupCode : AbstractCommand
    {

        private bool _startupDone = false;
        private PropertyGridStateHandler _gridState;

        public override void Run()
        {
             _gridState = new PropertyGridStateHandler();
            WorkbenchSingleton.WorkbenchCreated += new System.EventHandler(WorkbenchSingleton_WorkbenchCreated);
        }

        void WorkbenchSingleton_WorkbenchCreated(object sender, System.EventArgs e)
        {
           WorkbenchSingleton.Workbench.ActiveWorkbenchWindowChanged += new System.EventHandler(Workbench_ActiveWorkbenchWindowChanged);
        }

        void Workbench_ActiveWorkbenchWindowChanged(object sender, System.EventArgs e)
        {

            if (PropertyPad.Instance == null || PropertyPad.Grid == null)
                return;

            // This other stuff only happens the first time
            if (_startupDone == true)
                return;

            // This event is handy for doing things when focus or pad instance changes
            PropertyPad.SelectedObjectChanged += new System.EventHandler(PropertyPad_SelectedObjectChanged);

            // Init all the additional properties
            PGHelper.AutoAdjustLabelColumn();
            PGHelper.ShowAdditionalIndentation();
            PGHelper.SetDrawManager();
            PGHelper.SetFonts();

            _startupDone = true;
        }

        void PropertyPad_SelectedObjectChanged(object sender, System.EventArgs e)
        {
            
            //if (PropertyPad.Grid.Site != null)
            //    LoggingService.Debug("PropertyPad.Grid.Site=" + PropertyPad.Grid.Site.Name);

            PGHelper.AutoAdjustLabelColumn();
        }

    }
}
