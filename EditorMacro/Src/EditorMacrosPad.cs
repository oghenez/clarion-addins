using System.Windows.Forms;
using ICSharpCode.SharpDevelop.Gui;
using ICSharpCode.Core;

namespace ClarionEdge.EditorMacros
{
    class EditorMacrosPad : AbstractPadContent
    {
        EditorMacrosPadControl macroPad;

        public EditorMacrosPad()
        {
            macroPad = new EditorMacrosPadControl();
            WorkbenchSingleton.Workbench.ActiveWorkbenchWindowChanged += new System.EventHandler(Workbench_ActiveWorkbenchWindowChanged);
        }

        void Workbench_ActiveWorkbenchWindowChanged(object sender, System.EventArgs e)
        {
            if (WorkbenchSingleton.Workbench.ActiveContent != null &&
                WorkbenchSingleton.Workbench.ActiveContent is EditorMacrosPad)
                macroPad.GainFocus();
        }
        public override Control Control
        {
            get { return macroPad; }
        }
    }
}
