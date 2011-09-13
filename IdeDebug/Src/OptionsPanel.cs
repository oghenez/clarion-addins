using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.SharpDevelop.Gui;
using ICSharpCode.Core;

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
