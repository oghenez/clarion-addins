using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ClarionEdge.PropertyGridExtras.Src
{
    class PropertyGridToolbar
    {
        private VisualHint.SmartPropertyGrid.PropertyGridSV _grid;

        public PropertyGridToolbar(VisualHint.SmartPropertyGrid.PropertyGridSV grid)
        {
            PropertyGridHelper.Log("");
            _grid = grid;
            SetToolbarButtons();
        }

        private void SetToolbarButtons()
        {
            PropertyGridHelper.Log("");
            System.Windows.Forms.ToolStripButton buttonExpand = new System.Windows.Forms.ToolStripButton();
            buttonExpand.Image = (Image)ClarionEdge.PropertyGridExtras.Properties.Resources.dvAddGreen.ToBitmap();
            buttonExpand.ToolTipText = "Expand All";
            buttonExpand.Click += new System.EventHandler(buttonExpand_Click);

            System.Windows.Forms.ToolStripButton buttonContract = new System.Windows.Forms.ToolStripButton();
            buttonContract.Image = ClarionEdge.PropertyGridExtras.Properties.Resources.dvDeleteGreen.ToBitmap();
            buttonContract.ToolTipText = "Contract All";
            buttonContract.Click += new System.EventHandler(buttonContract_Click);

            System.Windows.Forms.ToolStripButton buttonOptions = new System.Windows.Forms.ToolStripButton();
            buttonOptions.Image = ClarionEdge.PropertyGridExtras.Properties.Resources.dvTool.ToBitmap();
            buttonOptions.ToolTipText = "Options";
            buttonOptions.Click += new System.EventHandler(buttonSettings_Click);

            System.Windows.Forms.ToolStripButton buttonTest = new System.Windows.Forms.ToolStripButton("test");
            //buttonTest.Image = ClarionEdge.PropertyGridExtras.Properties.Resources.dvTool.ToBitmap();
            buttonTest.Click += new EventHandler(buttonTest_Click);

            _grid.Toolbar.Items.Add(buttonExpand);
            _grid.Toolbar.Items.Add(buttonContract);
            _grid.Toolbar.Items.Add(buttonOptions);
            //_grid.Toolbar.Items.Add(buttonTest);
            _grid.Toolbar.RenderMode = ToolStripRenderMode.ManagerRenderMode;
        }

        void buttonContract_Click(object sender, System.EventArgs e)
        {
            _grid.ExpandAllProperties(false);
        }

        void buttonExpand_Click(object sender, System.EventArgs e)
        {
            _grid.ExpandAllProperties(true);
        }

        void buttonSettings_Click(object sender, System.EventArgs e)
        {
            PropertyGridHelper.ShowOptionsDialog();
        }

        void buttonTest_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Display Mode=" + _grid.DisplayMode.ToString());
        }
    }
}
