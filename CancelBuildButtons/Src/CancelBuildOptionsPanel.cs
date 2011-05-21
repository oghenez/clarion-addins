using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;

namespace ClarionEdge.CancelBuildButtons
{
    public partial class CancelBuildOptionsPanel : AbstractOptionPanel
    {

        public CancelBuildOptionsPanel()
        {
            InitializeComponent();
        }

        public override void LoadPanelContents()
        {
            hideButtonLabels.Checked = PropertyService.Get("CancelBuildButtons.Options.HideButtonLabels", false);
            
        }

        public override bool StorePanelContents()
        {
            PropertyService.Set("CancelBuildButtons.Options.HideButtonLabels", hideButtonLabels.Checked);
            return true;
        }

    }
}
