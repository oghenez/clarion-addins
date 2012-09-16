using System;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;
using ICSharpCode.SharpDevelop.Project;
using ICSharpCode.SharpDevelop.DefaultEditor.Gui.Editor;
using System.Drawing;
using ICSharpCode.SharpDevelop;

namespace ClarionEdge.MainToolbarExtras
{
    public class AutoStart : AbstractCommand
    {

        public override void Run()
        {
            WorkbenchSingleton.WorkbenchCreated += new System.EventHandler(WorkbenchSingleton_WorkbenchCreated);
        }

        void WorkbenchSingleton_WorkbenchCreated(object sender, System.EventArgs e)
        {
            // Setup other services when the IDE loads
            ToolbarHelper.AttachContextMenu();
            ToolbarHelper.UpdateMainToolbarSize();
            Font defaultFont = SharpDevelopTextEditorProperties.Instance.FontContainer.DefaultFont;
            PropertyService.Set<float>("ClarionEdge.MainToolbarExtras.StartUpFontSize", defaultFont.Size);

            // Create the event handlers we need
            WorkbenchSingleton.Workbench.ActiveWorkbenchWindowChanged += new EventHandler(Workbench_ActiveWorkbenchWindowChanged);
            ProjectService.SolutionClosed += new EventHandler(ProjectService_SolutionClosed);
            ProjectService.SolutionLoaded += new EventHandler<SolutionEventArgs>(ProjectService_SolutionLoaded);
            WorkbenchSingleton.Workbench.ViewClosed += new ViewContentEventHandler(Workbench_ViewClosed);
            WorkbenchSingleton.Workbench.ViewOpened += new ViewContentEventHandler(Workbench_ViewOpened);
        }

        void Workbench_ViewOpened(object sender, ViewContentEventArgs e)
        {
            StartPageCommands.Refresh();
        }

        void Workbench_ViewClosed(object sender, ViewContentEventArgs e)
        {
            StartPageCommands.Refresh();
        }

        void ProjectService_SolutionLoaded(object sender, SolutionEventArgs e)
        {
            StartPageCommands.Refresh();
        }

        void ProjectService_SolutionClosed(object sender, EventArgs e)
        {
            StartPageCommands.ReOpen();
        }

        void Workbench_ActiveWorkbenchWindowChanged(object sender, EventArgs e)
        {
            ToolbarHelper.UpdateOtherToolbars();
        }

    }
}