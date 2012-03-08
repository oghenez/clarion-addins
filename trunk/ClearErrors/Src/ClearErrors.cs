using ICSharpCode.Core;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Gui;

namespace ClarionEdge.ClearErrors
{
    public class ClearErrors : AbstractMenuCommand
    {
        public override void Run()
        {
            TaskService.Clear();
        }
    }

    public class SelectAllErrors : AbstractMenuCommand
    {

        public override void Run()
        {
            PadDescriptor index = WorkbenchSingleton.Workbench.GetPad(typeof(ErrorListPad));
            if (index != null)
            {
                index.BringPadToFront();
                ((ErrorListPad)index.PadContent).SelectAll();
            }

        }
    }
    public class CopyAllErrors : AbstractMenuCommand
    {

        public override void Run()
        {
            PadDescriptor index = WorkbenchSingleton.Workbench.GetPad(typeof(ErrorListPad));
            if (index != null)
            {
                index.BringPadToFront();
                ((ErrorListPad)index.PadContent).SelectAll();
                ((ErrorListPad)index.PadContent).Copy();
            }

        }
    }

    public class CutAllErrors : AbstractMenuCommand
    {

        public override void Run()
        {
            PadDescriptor index = WorkbenchSingleton.Workbench.GetPad(typeof(ErrorListPad));
            if (index != null)
            {
                index.BringPadToFront();
                ((ErrorListPad)index.PadContent).SelectAll();
                ((ErrorListPad)index.PadContent).Copy();
                TaskService.Clear();
            }

        }
    }
}
