using System.Windows.Forms;
using ICSharpCode.SharpDevelop.Gui;

namespace ClarionEdge.InsertClarionColor
{
    class ColorDialogPad : AbstractPadContent
    {
        ColorDialogPadControl colorPad;

        public ColorDialogPad()
        {
            colorPad = new ColorDialogPadControl();
        }

        public override Control Control
        {
            get { return colorPad; }
        }
    }
}
