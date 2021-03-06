﻿using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Gui;
using System;

namespace ClarionEdge.MainToolbarExtras
{
    public static class ToolbarHelper
    {
        public static void UpdateMainToolbarSize()
        {
            if (Convert.ToBoolean(PropertyService.Get("ClarionEdge.MainToolbarExtras.DisableSetToolbarSize", "false")) == true)
                return;

            LoggingService.Debug("About to resize main toolbar");
            ToolStrip toolBar = GetToolBar();
            if (toolBar != null)
            {
                Size tbImageScaling = toolBar.ImageScalingSize;
                int iconSize = PropertyService.Get("ClarionEdge.MainToolbarExtras.iconSize", tbImageScaling.Height);
                int toolbarHeight = PropertyService.Get("ClarionEdge.MainToolbarExtras.toolbarHeight", toolBar.Height);
                toolBar.ImageScalingSize = new Size(iconSize, iconSize);
                toolBar.Height = toolbarHeight;
                UpdateOtherToolbars();
            }
        }

        private static ToolStrip GetToolBar()
        {
            DefaultWorkbench wbForm;
            wbForm = (DefaultWorkbench)WorkbenchSingleton.Workbench;
            foreach (ToolStrip toolBar in wbForm.ToolBars)
            {
                // At the moment there is only one so just return it
                return toolBar;
            }
            return null;
        }

        public static void UpdateOtherToolbars()
        {
            if (PropertyService.Get("ClarionEdge.MainToolbarExtras.updateOtherToolbars", false) == false ||
                Convert.ToBoolean(PropertyService.Get("ClarionEdge.MainToolbarExtras.DisableSetToolbarSize", "false")) == true)
                return;

            List<ToolStrip> toolBars = new List<ToolStrip>();

            foreach (PadDescriptor pad in WorkbenchSingleton.Workbench.PadContentCollection)
            {
                if (pad.PadContent != null)
                {
                    FindAllToolbars(pad.PadContent.Control.Controls, ref toolBars);
                }
            }

            foreach (IViewContent view in WorkbenchSingleton.Workbench.ViewContentCollection)
            {
                if (view.Control != null && view.Control.Controls != null)
                    FindAllToolbars(view.Control.Controls, ref toolBars);
                
                foreach (ISecondaryViewContent sv in view.SecondaryViewContents)
                {
                    if (sv.Control != null && sv.Control.Controls != null)
                    FindAllToolbars(sv.Control.Controls, ref toolBars);
                }
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

        internal static void AttachContextMenu()
        {
            LoggingService.Debug("About to attach context menu to main toolbar");
            ToolStrip toolBar = GetToolBar();
            if (toolBar != null)
                toolBar.ContextMenuStrip = MenuService.CreateContextMenu(toolBar, "/ClarionEdge/MainToolBarExtras/ContextMenu");

        }

        internal static void RunAbstractCommand(string path, string id)
        {
            AddInTreeNode treeNode = AddInTree.GetTreeNode(path, false);
            if (treeNode != null)
            {
                AbstractCommand result;
                foreach (Codon current in treeNode.Codons)
                {
                    if (current.Id == id)
                    {
                        LoggingService.Debug("Found " + id + ", class=" + current.Properties["class"]);

                        result = current.AddIn.CreateObject(current.Properties["class"]) as AbstractCommand;
                        if (result == null)
                            LoggingService.Debug("result is null.");
                        else
                        {
                            try
                            {
                                result.Run();
                            }
                            catch (Exception e)
                            {
                                LoggingService.Debug("Problem trying to run command e=" + e.ToString());
                            }
                        }
                        break;
                    }
                }
            }
        }
    }
}
