using System;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Project;
using System.Windows.Forms;
using ICSharpCode.SharpDevelop.Gui;

namespace ClarionEdge.MainToolbarExtras
{

    public class RunStartUpProjectInDebugger : AbstractMenuCommand
    {

        public override void Run()
        {
            DoRun doRun = new DoRun();
            doRun.UseDebug = true;
            doRun.Run(ProjectService.OpenSolution.StartupProject);
        }

    }
    public class RunStartUpProjectNonElevated : AbstractMenuCommand
    {

        public override void Run()
        {
            if (Environment.OSVersion.Version.Major <= 5)
            {
                // This is XP or less!
                // User the built in "run startup" button
                ToolbarHelper.RunAbstractCommand("/SharpDevelop/Workbench/ToolBar/Standard", "RunStartUpProject");
            }
            else
            {
                DoRun doRun = new DoRun();
                doRun.UseDebug = false;
                doRun.NonElevated = true;
                doRun.Run(ProjectService.OpenSolution.StartupProject);
            }
        }
    }
}
