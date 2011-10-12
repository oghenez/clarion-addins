using System;
using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;

namespace ClarionEdge.MainToolbarExtras
{
    public class StartupCode : AbstractCommand
    {
        public override void Run()
        {
            WorkbenchSingleton.WorkbenchCreated += new System.EventHandler(WorkbenchSingleton_WorkbenchCreated);
            /*
             * Some example code I was playing with on how to pull messages out of addin configurations
             * 
            foreach (AddIn a in AddInTree.AddIns)
            {
                if (a.Manifest.PrimaryIdentity == "ClarionEdge.MainToolbarExtras")
                {
                    MessageService.ShowMessage(a.Properties["someProperty"]);
                }
            }
             */
        }

        void WorkbenchSingleton_WorkbenchCreated(object sender, System.EventArgs e)
        {
            WorkbenchSingleton.Workbench.ActiveWorkbenchWindowChanged += new EventHandler(Workbench_ActiveWorkbenchWindowChanged);
            ToolbarHelper.AttachContextMenu();
            ToolbarHelper.UpdateMainToolbarSize();
        }

        void Workbench_ActiveWorkbenchWindowChanged(object sender, EventArgs e)
        {
            ToolbarHelper.UpdateOtherToolbars();
        }

    }

    public class SetToolbarSizeCommand : AbstractCommand
    {
        public override void Run()
        {
            Form setToolbarSize = new SetToolbarSize();
            if (setToolbarSize.ShowDialog(WorkbenchSingleton.MainForm) == DialogResult.OK)
            {
                ToolbarHelper.UpdateMainToolbarSize();
            }
            setToolbarSize.Dispose();
        }
    }

    class UpdateOtherToolbars : AbstractCheckableMenuCommand
    {
        public override bool IsChecked
        {
            get
            {
                return Convert.ToBoolean(PropertyService.Get("ClarionEdge.MainToolbarExtras.updateOtherToolbars", "false"));
            }
            set
            {
                PropertyService.Set("ClarionEdge.MainToolbarExtras.updateOtherToolbars", Convert.ToString(value));
                ToolbarHelper.UpdateOtherToolbars();
            }
        }
    }

    class ShowRunStartUpProjectInDebugger : AbstractCheckableMenuCommand
    {
        public override bool IsChecked
        {
            get
            {
                return Convert.ToBoolean(PropertyService.Get("ClarionEdge.MainToolbarExtras.ShowRunStartUpProjectInDebugger", "false"));
            }
            set
            {
                PropertyService.Set("ClarionEdge.MainToolbarExtras.ShowRunStartUpProjectInDebugger", Convert.ToString(value));
            }
        }
    }

    class ShowRunExeNonElevated : AbstractCheckableMenuCommand
    {
        public override bool IsChecked
        {
            get
            {
                return Convert.ToBoolean(PropertyService.Get("ClarionEdge.MainToolbarExtras.ShowRunExeNonElevated", "true"));
            }
            set
            {
                PropertyService.Set("ClarionEdge.MainToolbarExtras.ShowRunExeNonElevated", Convert.ToString(value));
            }
        }
    }
}
