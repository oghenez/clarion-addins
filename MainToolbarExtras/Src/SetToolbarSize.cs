using System.Windows.Forms;
using ICSharpCode.Core;

namespace ClarionEdge.MainToolbarExtras
{
    public partial class SetToolbarSize : Form
    {
        public SetToolbarSize()
        {
            InitializeComponent();
            spinIconSize.Value = PropertyService.Get("ClarionEdge.MainToolbarExtras.iconSize", 24);
            spinHeight.Value = PropertyService.Get("ClarionEdge.MainToolbarExtras.toolbarHeight", 42);
        }

        private void buttonOK_Click(object sender, System.EventArgs e)
        {
            PropertyService.Set("ClarionEdge.MainToolbarExtras.iconSize", spinIconSize.Value.ToString());
            PropertyService.Set("ClarionEdge.MainToolbarExtras.toolbarHeight", spinHeight.Value.ToString());
        }

        private void buttonHelp_Click(object sender, System.EventArgs e)
        {
            MessageService.ShowMessage("Original dimensions are: \n\n" +
                "(Clarion 7)\n" + 
                "Icon Size = 16\n" +
                "ToolBar Height = 25\n\n" +
                "(Clarion 8)\n" + 
                "Icon Size = 24\n" + 
                "ToolBar Height = 42");
        }

        private void buttonSetSizeC7_Click(object sender, System.EventArgs e)
        {
            spinIconSize.Value = 16;
            spinHeight.Value = 24;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            spinIconSize.Value = 24;
            spinHeight.Value = 42;
        }
    }
}
