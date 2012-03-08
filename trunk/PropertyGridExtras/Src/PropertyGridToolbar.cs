using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using VisualHint.SmartPropertyGrid;

namespace ClarionEdge.PropertyGridExtras.Src
{
    class PropertyGridToolbar
    {
        private VisualHint.SmartPropertyGrid.PropertyGridSV _grid;
        private PropertyGridStateHandler _stateHandler;
        private System.Windows.Forms.ToolStripButton _buttonOptions;
        private System.Windows.Forms.ToolStripButton _buttonContract;
        private System.Windows.Forms.ToolStripButton _buttonExpand;

        public PropertyGridToolbar(VisualHint.SmartPropertyGrid.PropertyGridSV propertyGridSV, PropertyGridStateHandler propertyGridStateHandler)
        {
            PGHelper.Log("");
            _grid = propertyGridSV;
            _stateHandler = propertyGridStateHandler;
            SetToolbarButtons();
        }

        private void SetToolbarButtons()
        {
            PGHelper.Log("");
            _buttonExpand = new System.Windows.Forms.ToolStripButton();
            _buttonExpand.Image = (Image)ClarionEdge.PropertyGridExtras.Properties.Resources.dvAddGreen.ToBitmap();
            _buttonExpand.ToolTipText = "Expand All";
            _buttonExpand.Click += new System.EventHandler(buttonExpand_Click);

            _buttonContract = new System.Windows.Forms.ToolStripButton();
            _buttonContract.Image = ClarionEdge.PropertyGridExtras.Properties.Resources.dvDeleteGreen.ToBitmap();
            _buttonContract.ToolTipText = "Contract All";
            _buttonContract.Click += new System.EventHandler(buttonContract_Click);

            _buttonOptions = new System.Windows.Forms.ToolStripButton();
            _buttonOptions.Image = ClarionEdge.PropertyGridExtras.Properties.Resources.dvTool.ToBitmap();
            _buttonOptions.ToolTipText = "Options";
            _buttonOptions.Click += new System.EventHandler(buttonSettings_Click);

            //System.Windows.Forms.ToolStripButton buttonTest = new System.Windows.Forms.ToolStripButton("test");
            //buttonTest.Image = ClarionEdge.PropertyGridExtras.Properties.Resources.dvTool.ToBitmap();
            //buttonTest.Click += new EventHandler(buttonTest_Click);

            _grid.Toolbar.Items.Add(_buttonExpand);
            _grid.Toolbar.Items.Add(_buttonContract);
            _grid.Toolbar.Items.Add(_buttonOptions);
            //_grid.Toolbar.Items.Add(buttonTest);
            _grid.Toolbar.RenderMode = ToolStripRenderMode.ManagerRenderMode;
        }

        void buttonContract_Click(object sender, System.EventArgs e)
        {
            _stateHandler.AllowExpand = true;
            _grid.ExpandAllProperties(false);
            _stateHandler.AllowExpand = false;
        }

        void buttonExpand_Click(object sender, System.EventArgs e)
        {
            _stateHandler.AllowExpand = true;
            _grid.ExpandAllProperties(true);
            _stateHandler.AllowExpand = false;
        }

        void buttonSettings_Click(object sender, System.EventArgs e)
        {
            PGHelper.ShowOptionsDialog();
        }

        void buttonTest_Click(object sender, EventArgs e)
        {
            _grid.InternalGrid.Focus();
            PropertyEnumerator selectedPropEnum = _grid.SelectedPropertyEnumerator;
            _grid.SelectProperty(_grid.RightBound);
            if (selectedPropEnum == null || selectedPropEnum.Property == null)
                _grid.SelectProperty(_grid.FirstProperty);

            if (selectedPropEnum == null)
                PGHelper.Log("selectedPropEnum=null");

            PropertyVisibleDeepEnumerator propEnum = _grid.FirstProperty.GetVisibleDeepEnumerator();
            if (propEnum == null)
                PGHelper.Log("propEnum=null");
            
            while (propEnum != propEnum.RightBound)
            {
                if (selectedPropEnum.Property.Id == propEnum.Property.Id)
                {
                    propEnum.MoveNext();
                    _grid.SelectProperty(propEnum);
                    break;
                }
                propEnum.MoveNext();
            }
        }
    }
}
