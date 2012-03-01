using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Xml;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Commands;
using ICSharpCode.SharpDevelop.Gui;
using VisualHint.SmartPropertyGrid;
using System.Windows.Forms;
using System.ComponentModel;
using SoftVelocity.ClarionNet;
using System.Drawing.Design;
using ClarionEdge.PropertyGridExtras.Src;


namespace ClarionEdge.PropertyGridExtras
{
    class PropertyGridStateHandler
    {
        private const int INT_AFTER_EXPAND_ALL_WAIT_PERIOD = 500;
        private Stopwatch _expandAllStopWatch;
        private bool _skipExpandAll = false;
        private Int32 _extraDesignerSkipCount = 0;
        private bool _eventHandlersRegistered = false;
        private PropertyGridState _selectedGridState;
        private PropertyGridToolbar _toolbar;

        public PropertyGridStateHandler()
        {
            // Make sure the temp path exists for the property store!
            String path = Path.Combine(PropertyService.ConfigDirectory, "temp");
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            PropertyGridHelper.Log(path);
            WorkbenchSingleton.WorkbenchCreated += new EventHandler(WorkbenchSingleton_WorkbenchCreated);
        }

        void WorkbenchSingleton_WorkbenchCreated(object sender, EventArgs e)
        {
            WorkbenchSingleton.Workbench.ActiveWorkbenchWindowChanged += new EventHandler(Workbench_ActiveWorkbenchWindowChanged);
        }

        internal void Workbench_ActiveWorkbenchWindowChanged(object sender, System.EventArgs e)
        {
            PropertyGridHelper.Log("");
            if (_toolbar == null && PropertyPad.Grid != null)
                _toolbar = new PropertyGridToolbar(PropertyPad.Grid);

            if (Enabled() == false)
            {
                PropertyGridHelper.Log("not enabled");
                return;
            }

            if (_eventHandlersRegistered == false)
            {
                RegisterEventHandlers(PropertyPad.Grid);
                _eventHandlersRegistered = true;
            }

            SetDesignerGridOverrides();
            grid_SelectedObjectChanged(null, null);
        }

        private bool Enabled()
        {
            PropertyGridHelper.Log("");
            if (PropertyPad.Instance == null || PropertyPad.Grid == null || PropertyPad.Grid.SelectedObject == null)
                return false;

            if (PropertyPad.Grid.DisplayMode != VisualHint.SmartPropertyGrid.PropertyGrid.DisplayModes.Categorized)
                return false;
            // TODO: Maybe it would be a good idea to detect a change of DisplayMode and reactivate the RegisterEventHandlers?
            PropertyEnumerator propEnum = PropertyPad.Grid.SelectedPropertyEnumerator;

            if (propEnum == null)
                return false;

            if (propEnum.Property != null && propEnum.Property.DisplayName != null && propEnum.Property.DisplayName != "")
                PropertyGridHelper.Log("DisplayName=" + propEnum.Property.DisplayName);

            //if (propEnum != propEnum.RightBound)
            //if (PropertyPad.Grid.ContainsFocus == true)
            //    return false;
            return Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.RememberExpandedState", "true"));
        }

        internal void RegisterEventHandlers(PropertyGridSV grid)
        {
            PropertyGridHelper.Log("");
            grid.PropertyExpanded += new VisualHint.SmartPropertyGrid.PropertyGrid.PropertyExpandedEventHandler(grid_PropertyExpanded);
            grid.PropertyExpanding += new VisualHint.SmartPropertyGrid.PropertyGrid.PropertyExpandingEventHandler(grid_PropertyExpanding);
            grid.SelectedObjectChanged += new VisualHint.SmartPropertyGrid.PropertyGrid.SelectedObjectChangedEventHandler(grid_SelectedObjectChanged);
            grid.Invalidated += new System.Windows.Forms.InvalidateEventHandler(grid_Invalidated);
        }

        void grid_Invalidated(object sender, System.Windows.Forms.InvalidateEventArgs e)
        {
            if (Enabled() == false)
                return;

            PropertyGridHelper.Log("");
            if (_skipExpandAll == true)
            {
                // Restart the stopwatch because we are still inside skipExpandAll=true
                // Only when there is an expand event AFTER the actual skipExpandAll has completed will the Stopwatch elapsed time be relevant. 
                // So this means that we are detecting the elapsed time since the last event of an "Expand All".
                // Probably we can decrease the INT_AFTER_EXPAND_ALL_WAIT_PERIOD, I will see...
                // The stop watch is started in the ActiveWorkbenchWindowChanged event but I found that on reports there was too big a gap after that event 
                // Resetting it here seems to be good enough. Who knows what will happen on different systems. I wish I could find a better way than this stopwatch!
                PropertyGridHelper.Log("Stopwatch restarted");
                _expandAllStopWatch = Stopwatch.StartNew();
            }
        }

        void grid_PropertyExpanding(object sender, PropertyExpandingEventArgs e)
        {
            if (Enabled() == false)
                return;

            if (PropertyPad.Grid.ContainsFocus == true && IsBaseDeignerControl(PropertyPad.Grid.SelectedObject) == false)
            {
                PropertyGridHelper.Log("Skipping restore while in focus");
                return;
            }


            PropertyGridHelper.Log("id=" + e.PropertyEnum.Property.Id);
            PropertyGridHelper.Log("skipExpandAll=" + _skipExpandAll);
            if (_skipExpandAll == true)
            {
                if (_expandAllStopWatch.ElapsedMilliseconds > INT_AFTER_EXPAND_ALL_WAIT_PERIOD)
                {
                    PropertyGridHelper.Log("ElapsedMilliseconds=" + _expandAllStopWatch.ElapsedMilliseconds);
                    _skipExpandAll = false;
                    return;
                }
                e.Handled = true;

                if (e.PropertyEnum == PropertyPad.Grid.LastProperty)
                {
                    PropertyGridHelper.Log("Expand all has finished, restore our selections again please!");
                    if (_extraDesignerSkipCount > 0)
                    {
                        _extraDesignerSkipCount -= 1;
                    }
                    else
                    {
                        _skipExpandAll = false;
                    }
                }
            }

        }

        void grid_PropertyExpanded(object sender, PropertyExpandedEventArgs e)
        {
            if (Enabled() == false)
                return;

             _selectedGridState.SaveProperty(e);
        }

        void grid_SelectedObjectChanged(object sender, SelectedObjectChangedEventArgs e)
        {
            PropertyGridHelper.Log("");
            _selectedGridState = new PropertyGridState();

            if (Enabled() == false)
                return;

            // What is being shown in the PropertyPad has changed. Lets reload the state from disc
            _selectedGridState.SetFileName();

            // It seems that the designers issue a few extra "expand all" commands.
            // Skipping 2 of them seems to do the trick...
            object p = PropertyPad.Grid.SelectedObject;
            if (p != null)
            {
                if (IsBaseDeignerControl(p))
                {
                    // Reset this counter
                    _extraDesignerSkipCount = 2;
                }
            }

            _skipExpandAll = false;
            ReloadState();
            SetDesignerGridOverrides();
        }

        private static bool IsBaseDeignerControl(object p)
        {
            return p.GetType().ToString().Contains("SoftVelocity.ClarionNet.WindowDesigner.Window") ||
                                p.GetType().ToString().Contains("SoftVelocity.ClarionNet.Designer.SectionControls.BaseDesignerControl");
        }

        private void SetDesignerGridOverrides()
        {
            PropertyGridHelper.Log("");
            if (PropertyPad.Grid.SelectedObject.GetType().ToString().Contains("SoftVelocity.ClarionNet.Window") == true ||
                PropertyPad.Grid.SelectedObject.GetType().ToString().Contains("SoftVelocity.ClarionNet.Reports") == true ||
                PropertyPad.Grid.SelectedObject.GetType().ToString().Contains("SoftVelocity.ClarionNet.Designer") == true)
            {
                PropertyGridHelper.Log("Setting skipExpandAll in SetDesignerGridOverrides");
                _expandAllStopWatch = Stopwatch.StartNew();
                _skipExpandAll = true;
            }
        }

        private void ReloadState()
        {
            if (Enabled() == false || _selectedGridState == null)
                return;

            PropertyGridHelper.Log("");
            PropertyEnumerator propEnum = PropertyPad.Grid.FirstProperty;
            while (propEnum != propEnum.RightBound)
            {
                PropertyPad.Grid.ExpandProperty(propEnum, _selectedGridState.GetPropertyState(propEnum));
                propEnum.MoveNext();
            }
        }
        
    }
}
