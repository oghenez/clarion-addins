using System.Windows.Forms;
using ICSharpCode.SharpDevelop.Gui;

namespace ClarionAddins.IdeDebug
{
    class DebugViewPad : AbstractPadContent
    {
        DebugViewPadControl debugViewPadControl;

        public DebugViewPad()
        {
            debugViewPadControl = new DebugViewPadControl();
        }

        public override Control Control
        {
            get { return debugViewPadControl; }
        }
    }
}
