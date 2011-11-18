using System;
using System.Threading;
using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.BrowserDisplayBinding;
using ICSharpCode.SharpDevelop.Gui;
using ICSharpCode.SharpDevelop.Project;
using MouseKeyboardLibrary;
using SoftVelocity.Generator.UI;

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
            ProjectService.SolutionClosed += new EventHandler(ProjectService_SolutionClosed);
            ProjectService.SolutionLoaded += new EventHandler<SolutionEventArgs>(ProjectService_SolutionLoaded);
            
        }

        void ProjectService_SolutionLoaded(object sender, SolutionEventArgs e)
        {
            if (PropertyService.Get<bool>("SharpDevelop.CloseStartPageOnSolutionOpening", true) == false &&
                PropertyService.Get<bool>("ClarionEdge.MainToolbarExtras.AutoRefreshStartPage", true) == true)
            {
                foreach (IViewContent current in WorkbenchSingleton.Workbench.ViewContentCollection)
                {
                    BrowserPane browserPane = current as BrowserPane;
                    if (browserPane != null && browserPane.Url.Scheme == "startpage")
                    {
                        browserPane.Load("startpage://project/");
                        LoggingService.Debug("attempting to refresh startpage!");
                    }
                }
            }
        }

        void ProjectService_SolutionClosed(object sender, EventArgs e)
        {
            LoggingService.Debug("ClarionEdge.MainToolbarExtras, ProjectService_SolutionClosed");

            if (WorkbenchSingleton.Workbench.ViewContentCollection.Count == 0)
            {
                // For some reason there was an exception if I tried to combine these two if statements with &&
                // go figure, this works ok for now it seems!
                if (Convert.ToBoolean(PropertyService.Get("ClarionEdge.MainToolbarExtras.ReOpenStartPage", "true")) == true)
                {
                    ToolbarHelper.RunAbstractCommand("/SharpDevelop/Workbench/ToolBar/Standard", "StartPage");
                }
            }
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
    
    public class OpenAppGenSourceCommand : AbstractCommand
    {

        public override void Run()
        {
            if (WorkbenchSingleton.Workbench.ActiveWorkbenchWindow != null &&
                WorkbenchSingleton.Workbench.ActiveWorkbenchWindow.ActiveViewContent != null &&
                WorkbenchSingleton.Workbench.ActiveWorkbenchWindow.ViewContent.GetType() == typeof(ApplicationMainWindowControl_ViewContent))
            {
                Thread.Sleep(100);
                //KeyboardSimulator.KeyUp(Keys.Alt);
                KeyboardSimulator.KeyDown(Keys.Shift);
                KeyboardSimulator.KeyPress(Keys.F10);
                KeyboardSimulator.KeyUp(Keys.Shift);
                KeyboardSimulator.KeyPress(Keys.S);
            }
        }
    }

    class ReOpenStartPage : AbstractCheckableMenuCommand
    {
        public override bool IsChecked
        {
            get
            {
                return Convert.ToBoolean(PropertyService.Get("ClarionEdge.MainToolbarExtras.ReOpenStartPage", "true"));
            }
            set
            {
                PropertyService.Set("ClarionEdge.MainToolbarExtras.ReOpenStartPage", Convert.ToString(value));
            }
        }
    }

    class AutoRefreshStartPage : AbstractCheckableMenuCommand
    {
        public override bool IsChecked
        {
            get
            {
                return Convert.ToBoolean(PropertyService.Get("ClarionEdge.MainToolbarExtras.AutoRefreshStartPage", "true"));
            }
            set
            {
                PropertyService.Set("ClarionEdge.MainToolbarExtras.AutoRefreshStartPage", Convert.ToString(value));
            }
        }
    }

}
