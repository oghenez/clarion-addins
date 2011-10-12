using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Project;

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
            DoRun doRun = new DoRun();
            doRun.UseDebug = false;
            doRun.NonElevated = true;
            doRun.Run(ProjectService.OpenSolution.StartupProject);
        }

    }

}
