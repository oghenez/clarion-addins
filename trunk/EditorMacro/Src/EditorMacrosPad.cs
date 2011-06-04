using System.Windows.Forms;
using ICSharpCode.SharpDevelop.Gui;

namespace ClarionEdge.EditorMacros
{
    class EditorMacrosPad : AbstractPadContent
    {
        EditorMacrosPadControl macroPad;

        public EditorMacrosPad()
        {
            macroPad = new EditorMacrosPadControl();
        }
        public override Control Control
        {
            get { return macroPad; }
        }
    }
}
