using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using ICSharpCode.Core;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Gui;
using System.Diagnostics;

namespace ClarionEdge.CancelBuildButtons
{
    public partial class CancelBuildOptionsPanel : AbstractOptionPanel
    {

        public CancelBuildOptionsPanel()
        {
            InitializeComponent();
            //baseOptionsPanel.Image = CommonResources.Properties.famfamfam_silk.famfamfam_silk_picture_empty;
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
