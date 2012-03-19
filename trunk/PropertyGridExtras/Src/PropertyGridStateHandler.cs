using System;
using System.IO;
using ClarionEdge.PropertyGridExtras.Src;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Gui;
using VisualHint.SmartPropertyGrid;
using CWBinding.ClarionEditor;
using System.Diagnostics;
using SoftVelocity.Generator.Editor;
using System.Threading;


namespace ClarionEdge.PropertyGridExtras
{
    class PropertyGridStateHandler
    {
        private bool _eventHandlersRegistered = false;
        private PropertyGridState _selectedGridState;
        private PropertyGridToolbar _toolbar;
        private bool _allowExpand;
        private Stopwatch _designerSW = new Stopwatch();

        public bool AllowExpand
        {
            get { return _allowExpand; }
            set { _allowExpand = value; }
        }
        private bool _allowSelection;

        public bool AllowSelection
        {
            get { return _allowSelection; }
            set { _allowSelection = value; }
        }

        public PropertyGridStateHandler()
        {
            // Make sure the temp path exists for the property store!
            String path = Path.Combine(PropertyService.ConfigDirectory, "temp");
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            PGHelper.Log(path);
            WorkbenchSingleton.WorkbenchCreated += new EventHandler(WorkbenchSingleton_WorkbenchCreated);
        }

        void WorkbenchSingleton_WorkbenchCreated(object sender, EventArgs e)
        {
            PGHelper.Log("");
            PropertyPad.SelectedObjectChanged += new EventHandler(PropertyPad_SelectedObjectChanged);

            if (Enabled() == false)
            {
                PGHelper.Log("Not enabled, returning");
                return;
            }
        }

        void PropertyPad_SelectedObjectChanged(object sender, EventArgs e)
        {

            if (_toolbar == null && PropertyPad.Grid != null)
            {
                PGHelper.Log("Setup Toolbar");
                _toolbar = new PropertyGridToolbar(PropertyPad.Grid, this);
            }

            if (_eventHandlersRegistered == false && PropertyPad.Grid != null)
            {
                RegisterEventHandlers(PropertyPad.Grid);
                _eventHandlersRegistered = true;
            }
        }
        
        private bool Enabled()
        {
            PGHelper.Log("");
            if (PropertyPad.Instance == null || PropertyPad.Grid == null || PropertyPad.Grid.SelectedObject == null)
                return false;

            if (PropertyPad.Grid.DisplayMode != VisualHint.SmartPropertyGrid.PropertyGrid.DisplayModes.Categorized)
            {
                return false;
                // TODO: Maybe it would be a good idea to detect a change of DisplayMode and reactivate the RegisterEventHandlers?
            }

            return Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.RememberExpandedState", "true"));
        }

        internal void RegisterEventHandlers(PropertyGridSV grid)
        {
            PGHelper.Log("");
            grid.PropertyExpanded += new VisualHint.SmartPropertyGrid.PropertyGrid.PropertyExpandedEventHandler(grid_PropertyExpanded);
            grid.PropertyExpanding += new VisualHint.SmartPropertyGrid.PropertyGrid.PropertyExpandingEventHandler(grid_PropertyExpanding);
            grid.SelectedObjectChanged += new VisualHint.SmartPropertyGrid.PropertyGrid.SelectedObjectChangedEventHandler(grid_SelectedObjectChanged);
            grid.PropertySelected += new VisualHint.SmartPropertyGrid.PropertyGrid.PropertySelectedEventHandler(grid_PropertySelected);
            grid.PropertySelecting += new VisualHint.SmartPropertyGrid.PropertyGrid.PropertySelectingEventHandler(grid_PropertySelecting);
            grid.PropertyRestored += new PropertyGrid.PropertyRestoredEventHandler(grid_PropertyRestored);
        }

        void grid_PropertyRestored(object sender, PropertyRestoredEventArgs e)
        {
            if (e.PropertyEnum == PropertyPad.Grid.LastProperty)
            {
                // For some reason the built in "restore" is really nasty. 
                // Each time the complete grid is restored automatically we are going to _force_ it to be restored our way.
                PGHelper.Log("");
                grid_SelectedObjectChanged(null, null);
            }
        }

        void grid_PropertySelecting(object sender, PropertySelectingEventArgs e)
        {

            PGHelper.Log("");
            if (PropertyPad.Grid.InternalGrid.Focused == false && _allowSelection == false)
            {
                e.Handled = true;
                return;
            }
        }

        void grid_PropertySelected(object sender, PropertySelectedEventArgs e)
        {
            PGHelper.Log("");
            if (_selectedGridState != null && PropertyPad.Grid.InternalGrid.Focused == true)
                _selectedGridState.SaveSelected(e.PropertyEnum);
        }

        void grid_PropertyExpanding(object sender, PropertyExpandingEventArgs e)
        {
            if (Enabled() == false)
                return;

            PGHelper.Log("ClarionEdge.PropertyGridExtras.spinDelay=" + PropertyService.Get<int>("ClarionEdge.PropertyGridExtras.spinDelay", 1000).ToString());
            PGHelper.Log("_designerSW.IsRunning=" + _designerSW.IsRunning);
            PGHelper.Log("_designerSW.ElapsedMilliseconds=" + _designerSW.ElapsedMilliseconds);

            if (_designerSW.IsRunning == true && 
                _designerSW.ElapsedMilliseconds < PropertyService.Get<int>("ClarionEdge.PropertyGridExtras.spinDelay", 1000)
                && _allowExpand == false)
            {
                PGHelper.Log("skip because of _designerSW");
                Thread.Sleep(10);
                _designerSW.Reset();
                _designerSW.Start();
                e.Handled = true;
                return;
            }

            _designerSW.Reset();

            PGHelper.Log("");
            if (PropertyPad.Grid.InternalGrid.Focused == false && _allowExpand == false) 
            {
                e.Handled = true;
                return;
            }


        }

        void grid_PropertyExpanded(object sender, PropertyExpandedEventArgs e)
        {
            if (Enabled() == false)
                return;

            PGHelper.Log("");
            if (_selectedGridState != null)
                _selectedGridState.SaveProperty(e);
        }

        void grid_SelectedObjectChanged(object sender, SelectedObjectChangedEventArgs e)
        {
            PGHelper.Log("");
            _selectedGridState = new PropertyGridState();

            if (Enabled() == false)
                return;

            // What is being shown in the PropertyPad has changed. Lets reload the state from disc
            _selectedGridState.ReloadState(PropertyPad.Grid, this);
            //PropertyPad.Grid.s

            PGHelper.Log("WorkbenchSingleton.Workbench.ActiveContent=" + WorkbenchSingleton.Workbench.ActiveContent.ToString());
            PGHelper.Log("PropertyPad.Grid.SelectedObject=" + PropertyPad.Grid.SelectedObject.ToString());
            if (WorkbenchSingleton.Workbench.ActiveContent == null ||
                WorkbenchSingleton.Workbench.ActiveContent is ClarionDesignerView)
            {
                _designerSW.Reset();
                _designerSW.Start();
            }

            if (sender == null ||
                PropertyPad.Grid.SelectedObject is SoftVelocity.ClarionNet.WindowDesigner.Window ||
                PropertyPad.Grid.SelectedObject is SoftVelocity.ClarionNet.Designer.SectionControls.BaseDesignerControl)
            {
                _designerSW.Reset();
                _designerSW.Start();
            }


        }

    }

}
