using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;

namespace ClarionAddins.IdeDebug
{
    public partial class OptionsPanel : AbstractOptionPanel
    {
        public OptionsPanel()
        {
            InitializeComponent();
        }

        public override void LoadPanelContents()
        {
            cbQuietMode.Checked = MessageService.QuietMode;
            cbExtraDebugInfo.Checked = PropertyService.Get<bool>("ClarionAddins.IdeDebug.cbExtraDebugInfo", false);
        }

        public override bool StorePanelContents()
        {
            MessageService.QuietMode = cbQuietMode.Checked;
            PropertyService.Set("CoreProperties.QuietMode", MessageService.QuietMode);
            PropertyService.Set("ClarionAddins.IdeDebug.cbExtraDebugInfo", cbExtraDebugInfo.Checked);
            return true;
        }
    }
}
