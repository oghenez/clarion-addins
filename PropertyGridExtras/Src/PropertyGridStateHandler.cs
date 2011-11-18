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


namespace ClarionEdge.PropertyGridExtras
{
    class PropertyGridStateHandler
    {
        private const int INT_AFTER_EXPAND_ALL_WAIT_PERIOD = 500;
        Stopwatch wbChanged;
        bool skipExpandAll = false;
        ICSharpCode.Core.Properties properties;
        string propertiesFileName = string.Empty;
        Int32 extraDesignerSkipCount = 0;

        internal void ActiveWorkbenchWindowChanged()
        {
            if (Enabled() == false)
                return;

            if (DesignerGridSelected() == true)
            {
                Log("Setting skipExpandAll in Workbench_ActiveWorkbenchWindowChanged");
                wbChanged = Stopwatch.StartNew();
                skipExpandAll = true;
            }
            else
            {
                Log("DesignerGridSelected ELSE in Workbench_ActiveWorkbenchWindowChanged");
            }
        }

        private void Log(string p)
        {
            if (Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.EnableLogging", "false")) == true)
                LoggingService.Debug(p);
        }

        private bool Enabled()
        {
            return Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.RememberExpandedState", "true"));
        }

        internal void RegisterEventHandlers(PropertyGridSV grid)
        {
            Log("PropertyGridExtras, RegisterEventHandlers");
            grid.PropertyExpanded += new VisualHint.SmartPropertyGrid.PropertyGrid.PropertyExpandedEventHandler(grid_PropertyExpanded);
            grid.PropertyExpanding += new VisualHint.SmartPropertyGrid.PropertyGrid.PropertyExpandingEventHandler(grid_PropertyExpanding);
            grid.SelectedObjectChanged += new VisualHint.SmartPropertyGrid.PropertyGrid.SelectedObjectChangedEventHandler(grid_SelectedObjectChanged);
            grid.Invalidated += new System.Windows.Forms.InvalidateEventHandler(grid_Invalidated);

            SetToolbarButtons(grid);
        }

        void grid_Invalidated(object sender, System.Windows.Forms.InvalidateEventArgs e)
        {
            if (Enabled() == false)
                return;

            if (skipExpandAll == true)
            {
                // Restart the stopwatch because we are still inside skipExpandAll=true
                // Only when there is an expand event AFTER the actual skipExpandAll has completed will the Stopwatch elapsed time be relevant. 
                // So this means that we are detecting the elapsed time since the last event of an "Expand All".
                // Probably we can decrease the INT_AFTER_EXPAND_ALL_WAIT_PERIOD, I will see...
                // The stop watch is started in the ActiveWorkbenchWindowChanged event but I found that on reports there was too big a gap after that event 
                // Resetting it here seems to be good enough. Who knows what will happen on different systems. I wish I could find a better way than this stopwatch!
                Log("grid_Invalidated, wbChanged restarted");
                wbChanged = Stopwatch.StartNew();
            }
        }

        void grid_PropertyExpanding(object sender, PropertyExpandingEventArgs e)
        {
            if (Enabled() == false)
                return;

            Log("grid_PropertyExpanding id=" + e.PropertyEnum.Property.Id);
            Log("grid_PropertyExpanding, skipExpandAll=" + skipExpandAll);
            if (skipExpandAll == true)
            {
                if (wbChanged.ElapsedMilliseconds > INT_AFTER_EXPAND_ALL_WAIT_PERIOD)
                {
                    Log("grid_PropertyExpanding, wbChanged.ElapsedMilliseconds=" + wbChanged.ElapsedMilliseconds);
                    skipExpandAll = false;
                    return;
                }
                e.Handled = true;

                if (e.PropertyEnum == PropertyPad.Grid.LastProperty)
                {
                    Log("grid_PropertyExpanding, Expand all has finished, restore our selections again please!");
                    if (extraDesignerSkipCount > 0)
                    {
                        extraDesignerSkipCount -= 1;
                    }
                    else
                    {
                        skipExpandAll = false;
                    }
                    properties = LoadProperties(propertiesFileName);
                }
            }

        }

        void grid_PropertyExpanded(object sender, PropertyExpandedEventArgs e)
        {
            if (Enabled() == false)
                return;

            Log("grid_PropertyExpanded, property (" + e.PropertyEnum.Property.Id + ") " + e.PropertyEnum.Property.DisplayName + " state=" + e.Expanded.ToString());
            properties.Set<bool>("id" + e.PropertyEnum.Property.Id, e.Expanded);
            properties.Save(propertiesFileName);
        }

        void grid_SelectedObjectChanged(object sender, SelectedObjectChangedEventArgs e)
        {
            if (Enabled() == false)
                return;

            Log("grid_SelectedObjectChanged");
            skipExpandAll = false;
            ReloadState();
            if (DesignerGridSelected() == true)
            {
                Log("Setting skipExpandAll in grid_SelectedObjectChanged");
                wbChanged = Stopwatch.StartNew();
                skipExpandAll = true;
            }
        }

        void buttonContract_Click(object sender, System.EventArgs e)
        {
            PropertyPad.Grid.ExpandAllProperties(false);
        }

        void buttonExpand_Click(object sender, System.EventArgs e)
        {
            PropertyPad.Grid.ExpandAllProperties(true);
        }

        void buttonSettings_Click(object sender, System.EventArgs e)
        {
            OptionsCommand.ShowTabbedOptions("Property Grid Options", AddInTree.GetTreeNode("/SharpDevelop/Dialogs/PropertyGridExtras"));
            PropertyGridHelper.SetFonts();
            PropertyGridHelper.ShowAdditionalIndentation();
            ReloadState();
        }

        private void SetToolbarButtons(PropertyGridSV grid)
        {
            System.Windows.Forms.ToolStripButton buttonExpand = new System.Windows.Forms.ToolStripButton();
            buttonExpand.Image = (Image)ClarionEdge.PropertyGridExtras.Properties.Resources.dvAddGreen.ToBitmap();
            buttonExpand.ToolTipText = "Expand All";
            buttonExpand.Click += new System.EventHandler(buttonExpand_Click);
            System.Windows.Forms.ToolStripButton buttonContract = new System.Windows.Forms.ToolStripButton();
            buttonContract.Image = ClarionEdge.PropertyGridExtras.Properties.Resources.dvDeleteGreen.ToBitmap();
            buttonContract.ToolTipText = "Contract All";
            buttonContract.Click += new System.EventHandler(buttonContract_Click);
            System.Windows.Forms.ToolStripButton buttonOptions = new System.Windows.Forms.ToolStripButton();
            buttonOptions.Image = ClarionEdge.PropertyGridExtras.Properties.Resources.dvTool.ToBitmap();
            buttonOptions.ToolTipText = "Options";
            buttonOptions.Click += new System.EventHandler(buttonSettings_Click);

            grid.Toolbar.Items.Add(buttonExpand);
            grid.Toolbar.Items.Add(buttonContract);
            grid.Toolbar.Items.Add(buttonOptions);
            grid.Toolbar.RenderMode = ToolStripRenderMode.ManagerRenderMode;
        }

        internal void SelectedObjectChanged(object p)
        {
            Log("PropertyGridExtras, SelectedObjectChanged");
            if (p != null)
            {
                String path = Path.Combine(PropertyService.ConfigDirectory, "temp");
                if (Directory.Exists(path) == false)
                    Directory.CreateDirectory(path);

                propertiesFileName = Path.Combine(Path.Combine(PropertyService.ConfigDirectory, "temp"),
                    "propertygridextras." + p.GetType().ToString() + ".xml");
                properties = LoadProperties(propertiesFileName);
                Log("PropertyPad.Grid.SelectedObject: " + p.GetType().ToString());

                // It seems that the designers issue a few extra "expand all" commands.
                // Skipping 2 of them seems to do the trick...
                if (p.GetType().ToString().Contains("SoftVelocity.ClarionNet.WindowDesigner.Window") ||  
                    p.GetType().ToString().Contains("SoftVelocity.ClarionNet.Designer.SectionControls.BaseDesignerControl"))
                {
                    // Reset this counter
                    extraDesignerSkipCount = 2;
                }
            }
        }

        private bool DesignerGridSelected()
        {
            if (PropertyPad.Grid == null)
                return false;

            if (PropertyPad.Grid.SelectedObject == null)
                return false;

            if (PropertyPad.Grid.SelectedObject.GetType().ToString().Contains("SoftVelocity.ClarionNet.Window") == true || 
                PropertyPad.Grid.SelectedObject.GetType().ToString().Contains("SoftVelocity.ClarionNet.Reports") == true ||
                PropertyPad.Grid.SelectedObject.GetType().ToString().Contains("SoftVelocity.ClarionNet.Designer") == true)
                return true;

            return false;
        }

        private void ReloadState()
        {
            if (Enabled() == false)
                return;

            Log("PropertyGridExtras, ReloadState");
            PropertyEnumerator propEnum = PropertyPad.Grid.FirstProperty;
            while (propEnum != propEnum.RightBound)
            {
                if (properties.Get("id" + propEnum.Property.Id) != null)
                {
                    // currently by default all categories are expanded. So w only perform an action on those that need to be contracted
                    Log("ReloadState: Restoring property (" + propEnum.Property.DisplayName + ") id=" + propEnum.Property.Id);
                    PropertyPad.Grid.ExpandProperty(propEnum, (bool)properties.Get("id" + propEnum.Property.Id, true));
                }
                else {
                    Log("ReloadState: Property (" + propEnum.Property.DisplayName + ") not found id=" + propEnum.Property.Id);
                }

                propEnum.MoveNext();
            }
        }
        

        private ICSharpCode.Core.Properties LoadProperties(string fileName)
        {
            Log("PropertyGridExtras, LoadProperties");
            if (!File.Exists(fileName))
            {
                Log("properties File not found");
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
                                Log("returning previous properties contents");
                                return properties;
                        }
                    }
                }
            }
            Log("returning null!");
            return null;

        }

    }
}
