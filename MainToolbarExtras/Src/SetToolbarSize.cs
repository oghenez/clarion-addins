using System.Windows.Forms;
using ICSharpCode.Core;

namespace ClarionEdge.MainToolbarExtras
{
    public partial class SetToolbarSize : Form
    {
        public SetToolbarSize()
        {
            InitializeComponent();
            tbIconSize.Text = PropertyService.Get("ClarionEdge.MainToolbarExtras.iconSize", 24).ToString();
            tbToolbarHeight.Text = PropertyService.Get("ClarionEdge.MainToolbarExtras.toolbarHeight", 42).ToString();
        }

        private void buttonOK_Click(object sender, System.EventArgs e)
        {
            PropertyService.Set("ClarionEdge.MainToolbarExtras.iconSize", tbIconSize.Text);
            PropertyService.Set("ClarionEdge.MainToolbarExtras.toolbarHeight", tbToolbarHeight.Text);
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
    }
}
