using System.Drawing;
using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;
using VisualHint.SmartPropertyGrid;

namespace ClarionEdge.PropertyGridExtras
{
    class StartupCode : AbstractCommand
    {

        bool _startupDone = false;
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
            if (_startupDone == true || PropertyPad.Grid == null)
                return;

            // This event is handy for doing things when focus or pad instance changes
            PropertyPad.SelectedObjectChanged += new System.EventHandler(PropertyPad_SelectedObjectChanged);
            PropertyPad.SelectedGridItemChanged += new VisualHint.SmartPropertyGrid.PropertyGrid.PropertySelectedEventHandler(PropertyPad_SelectedGridItemChanged);

            // Init all the additional properties
            PropertyGridHelper.AutoAdjustLabelColumn();
            PropertyGridHelper.ShowAdditionalIndentation();
            PropertyGridHelper.SetDrawManager();

            PropertyGridSV grid = PropertyPad.Grid;
            grid.PropertyExpanded += new VisualHint.SmartPropertyGrid.PropertyGrid.PropertyExpandedEventHandler(grid_PropertyExpanded);
            Form mf = WorkbenchSingleton.MainForm;
            if (PropertyService.Get("ClarionEdge.PropertyGridExtras.FontSize", "") != "")
            {
                float newSize = float.Parse(PropertyService.Get("ClarionEdge.PropertyGridExtras.FontSize", ""));
                if (PropertyService.Get("ClarionEdge.PropertyGridExtras.ResetDone", "false") == "true")
                {
                    grid.Font = new Font(grid.Font.Name, newSize, grid.Font.Style, mf.Font.Unit);
                }
                else
                {
                    grid.Font = new Font(grid.Font.Name, newSize, grid.Font.Style, grid.Font.Unit);
                }
                grid.RefreshProperties();
            }
            _startupDone = true;
        }

        void PropertyPad_SelectedGridItemChanged(object sender, PropertySelectedEventArgs e)
        {
            LoggingService.Debug("PropertyGridExtras PropertyPad_SelectedGridItemChanged");
            if (PropertyPad.Grid.Site != null)
                LoggingService.Debug("PropertyPad.Grid.Site=" + PropertyPad.Grid.Site.Name);
        }

        void grid_PropertyExpanded(object sender, PropertyExpandedEventArgs e)
        {
            LoggingService.Debug("property (" + e.PropertyEnum.Property.Id + ") " + e.PropertyEnum.Property.DisplayName + " state=" + e.Expanded.ToString());
        }

        void PropertyPad_SelectedObjectChanged(object sender, System.EventArgs e)
        {
            SelectedObjectChangedEventArgs ea = (SelectedObjectChangedEventArgs)e;
            if (PropertyPad.Grid.SelectedObject != null)
            {
                LoggingService.Debug("PropertyPad.Grid.SelectedObject: " + PropertyPad.Grid.SelectedObject.ToString());
            }
            
            if (PropertyPad.Grid.Site != null)
                LoggingService.Debug("PropertyPad.Grid.Site=" + PropertyPad.Grid.Site.Name);

            PropertyGridHelper.AutoAdjustLabelColumn();
        }
    }
}
