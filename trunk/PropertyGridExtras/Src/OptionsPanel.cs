using System;
using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;

namespace ClarionEdge.PropertyGridExtras
{
    public partial class OptionsPanel : AbstractOptionPanel
    {
        FontSelectionPanel fontSelectionPanel;
        public OptionsPanel()
        {
            InitializeComponent();
        }

        public override void LoadPanelContents()
        {
            fontSelectionPanel = new FontSelectionPanel();
            fontSelectionPanel.Dock = DockStyle.Fill;
            fontSelectionPanel.CurrentFont = PropertyPad.Grid.Font;
            FontGroupBox.Controls.Add(fontSelectionPanel);
            
            checkBoxAutoAdjustLabelColumn.Text = ResourceService.GetString(checkBoxAutoAdjustLabelColumn.Text);
            checkBoxAutoAdjustLabelColumn.Checked = Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.AutoAdjustLabelColumn", "false"));
            
            checkBoxShowAdditionalIndentation.Text = ResourceService.GetString(checkBoxShowAdditionalIndentation.Text);
            checkBoxShowAdditionalIndentation.Checked = Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.ShowAdditionalIndentation", "false"));

            checkBoxRememberExpandedState.Text = ResourceService.GetString(checkBoxRememberExpandedState.Text);
            checkBoxRememberExpandedState.Checked = Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.RememberExpandedState", "true"));

            checkBoxEnableLogging.Checked = Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.EnableLogging", "false"));

            dropDownTheme.Items.Add(ResourceService.GetString("ClarionEdge.PropertyGridExtras.UseOriginalDrawManager"));
            dropDownTheme.Items.Add(ResourceService.GetString("ClarionEdge.PropertyGridExtras.UseDefaultDrawManager"));
            dropDownTheme.Items.Add(ResourceService.GetString("ClarionEdge.PropertyGridExtras.UseLightDrawManager"));
            dropDownTheme.Items.Add(ResourceService.GetString("ClarionEdge.PropertyGridExtras.UseCustomDrawManager"));
            dropDownTheme.SelectedIndex = GetSelectedTheme();
        }

        private int GetSelectedTheme()
        {
            if (Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.UseLightDrawManager", "false")) == true)
            {
                return 2;
            }
            if (Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.UseDefaultDrawManager", "false")) == true)
            {
                return 1;
            }
            if (Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.UseCustomDrawManager", "false")) == true)
            {
                return 3;
            }
            return 0;
        }

        public override bool StorePanelContents()
        {
            try
            {
                PropertyPad.Grid.Font = fontSelectionPanel.CurrentFont;
                PropertyService.Set("ClarionEdge.PropertyGridExtras.Font", fontSelectionPanel.CurrentFont.ToString());
            }
            catch (Exception e)
            {
                LoggingService.Debug("ClarionEdge.PropertyGridExtras, save font exception: " + e.Message);
            }

            PropertyService.Set("ClarionEdge.PropertyGridExtras.AutoAdjustLabelColumn", checkBoxAutoAdjustLabelColumn.Checked);
            PropertyService.Set("ClarionEdge.PropertyGridExtras.ShowAdditionalIndentation", checkBoxShowAdditionalIndentation.Checked);
            PropertyService.Set("ClarionEdge.PropertyGridExtras.RememberExpandedState", checkBoxRememberExpandedState.Checked);
            PropertyService.Set("ClarionEdge.PropertyGridExtras.EnableLogging", checkBoxEnableLogging.Checked);

            SetSelectedTheme(dropDownTheme.SelectedIndex);
            PropertyGridHelper.SetDrawManager();
            return true;
        }

        private void SetSelectedTheme(int p)
        {
            PropertyService.Set("ClarionEdge.PropertyGridExtras.UseDefaultDrawManager", "false");
            PropertyService.Set("ClarionEdge.PropertyGridExtras.UseLightDrawManager", "false");
            PropertyService.Set("ClarionEdge.PropertyGridExtras.UseCustomDrawManager", "false");
            if (p == 1)
                PropertyService.Set("ClarionEdge.PropertyGridExtras.UseDefaultDrawManager", "true");
            if (p == 2)
                PropertyService.Set("ClarionEdge.PropertyGridExtras.UseLightDrawManager", "true");
            if (p == 3)
                PropertyService.Set("ClarionEdge.PropertyGridExtras.UseCustomDrawManager", "true");
        }

    }

}
