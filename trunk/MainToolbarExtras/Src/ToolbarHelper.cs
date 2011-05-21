using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Gui;

namespace ClarionEdge.MainToolbarExtras
{
    public static class ToolbarHelper
    {
        public static void UpdateMainToolbarSize()
        {
            DefaultWorkbench wbForm;
            wbForm = (DefaultWorkbench)WorkbenchSingleton.Workbench;
            foreach (ToolStrip toolBar in wbForm.ToolBars)
            {
                Size tbImageScaling = toolBar.ImageScalingSize;
                int iconSize = PropertyService.Get("ClarionEdge.MainToolbarExtras.iconSize", tbImageScaling.Height);
                int toolbarHeight = PropertyService.Get("ClarionEdge.MainToolbarExtras.toolbarHeight", toolBar.Height);
                toolBar.ImageScalingSize = new Size(iconSize, iconSize);
                toolBar.Height = toolbarHeight;
                toolBar.ContextMenuStrip = MenuService.CreateContextMenu(toolBar, "/ClarionEdge/MainToolBarExtras/ContextMenu");
            }
        }

        public static void UpdateOtherToolbars()
        {
            if (PropertyService.Get("ClarionEdge.MainToolbarExtras.updateOtherToolbars", false) == false)
                return;

            foreach (IViewContent view in WorkbenchSingleton.Workbench.ViewContentCollection)
            {
                List<ToolStrip> toolBars = new List<ToolStrip>();
                FindAllToolbars(view.Control.Controls, ref toolBars);
                foreach (ISecondaryViewContent sv in view.SecondaryViewContents)
                {
                    FindAllToolbars(sv.Control.Controls, ref toolBars);
                }
                foreach (PadDescriptor pd in WorkbenchSingleton.Workbench.PadContentCollection)
                {
                    FindAllToolbars(pd.PadContent.Control.Controls, ref toolBars);
                }

                foreach (ToolStrip ts in toolBars)
                {
                    Size tbImageScaling = ts.ImageScalingSize;
                    int iconSize = PropertyService.Get("ClarionEdge.MainToolbarExtras.iconSize", tbImageScaling.Height);
                    int toolbarHeight = PropertyService.Get("ClarionEdge.MainToolbarExtras.toolbarHeight", ts.Height);
                    ts.ImageScalingSize = new Size(iconSize, iconSize);
                    ts.Height = toolbarHeight;
                }
            }
        }

        private static void FindAllToolbars(Control.ControlCollection controlCollection, ref List<ToolStrip> toolBars)
        {
            foreach (Control c in controlCollection)
            {
                if (c is ToolStrip)
                {
                    toolBars.Add((ToolStrip)c);
                }
                else
                {
                    FindToolbarsRecursive(c, ref toolBars);
                }
            }
        }

        private static void FindToolbarsRecursive(Control root, ref List<ToolStrip> toolBars)
        {
            if (root.Controls.Count != 0)
            {
                foreach (Control c in root.Controls)
                {
                    if (c is ToolStrip)
                    {
                        toolBars.Add((ToolStrip)c);
                    }
                    else if (c.HasChildren == true)
                    {
                        FindToolbarsRecursive(c, ref toolBars);
                    }
                }
            }
        }
    }
}
