using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;
using ICSharpCode.SharpDevelop;
using SearchAndReplace;
using System.Windows.Forms;

namespace ClarionEdge.SearchPadExtras
{
    class StartupCode : AbstractCommand
    {
        public override void Run()
        {
            //WorkbenchSingleton.WorkbenchCreated += new EventHandler(WorkbenchSingleton_WorkbenchCreated);
            PadDescriptor searchResultPanel = WorkbenchSingleton.Workbench.GetPad(typeof(SearchResultPanel));
            if (searchResultPanel != null)
            {
                LoggingService.Debug("searchResultPanel");
                searchResultPanel.BringPadToFront();
                foreach (Control c in SearchResultPanel.Instance.Control.Controls)
                {
                    LoggingService.Debug("Search Panel instance");
                    if (c is ExtTreeView)
                    {
                        LoggingService.Debug("In TreeView");
                        ExtTreeView tv = (ExtTreeView)c;
                        tv.Font = ResourceService.LoadFont("Consolas", 22, System.Drawing.FontStyle.Bold);
                        foreach (SearchRootNode rnode in tv.Nodes)
                        {
                            LoggingService.Debug("In SearchRootNode ");
                            rnode.NodeFont = ResourceService.LoadFont("Consolas", 22, System.Drawing.FontStyle.Bold);
                            foreach (SearchResultNode srnode in rnode.Nodes)
                            {
                                LoggingService.Debug("In SearchResultNode ");
                                srnode.DrawDefault = true;
                                srnode.Text = "Fred!";
                                srnode.ToolTipText = "FRED";
                                srnode.NodeFont = ResourceService.LoadFont("Consolas", 22, System.Drawing.FontStyle.Bold);
                                foreach (TreeNode tn in srnode.Nodes)
                                {
                                    LoggingService.Debug("In TreeNode ");
                                    tn.NodeFont = ResourceService.LoadFont("Consolas", 22, System.Drawing.FontStyle.Bold);
                                }
                            }
                        } 
                        /*
                        foreach (TreeNode node in tv.Nodes)
                        {
                            LoggingService.Debug("In TreeView Node");
                            node.NodeFont = ResourceService.LoadFont("Consolas", 22, System.Drawing.FontStyle.Bold);
                            foreach (TreeNode childNode in node.Nodes)
                            {
                                childNode.NodeFont = ResourceService.LoadFont("Consolas", 22, System.Drawing.FontStyle.Bold);
                            }
                        }
                        */
                    }
                }
            }

        }

        void WorkbenchSingleton_WorkbenchCreated(object sender, EventArgs e)
        {
            WorkbenchSingleton.Workbench.ViewOpened += new ViewContentEventHandler(Workbench_ViewOpened);
        }

        void Workbench_ViewOpened(object sender, ViewContentEventArgs e)
        {
            PadDescriptor searchResultPanel = WorkbenchSingleton.Workbench.GetPad(typeof(SearchResultPanel));
            if (searchResultPanel != null)
            {
                LoggingService.Debug("searchResultPanel");
                searchResultPanel.BringPadToFront();
                foreach (Control c in SearchResultPanel.Instance.Control.Controls)
                {
                    LoggingService.Debug("Search Panel instance");
                    if (c is ExtTreeView)
                    {
                        LoggingService.Debug("In TreeView");
                        ExtTreeView tv = (ExtTreeView)c;
                        tv.Font = ResourceService.LoadFont("Consolas", 22, System.Drawing.FontStyle.Bold);
                        foreach (TreeNode node in tv.Nodes)
                        {
                            LoggingService.Debug("In TreeView Node");
                            node.NodeFont = ResourceService.LoadFont("Consolas", 22, System.Drawing.FontStyle.Bold);
                            foreach (TreeNode childNode in node.Nodes)
                            {
                                childNode.NodeFont = ResourceService.LoadFont("Consolas", 22, System.Drawing.FontStyle.Bold);
                            }
                        }
                    }
                }
            }
        }
    }
}
