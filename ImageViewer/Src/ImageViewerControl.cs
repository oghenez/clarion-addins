using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ClarionAddins.ImageViewer
{
    public partial class ImageViewerControl : UserControl
    {
        public ImageViewerControl()
        {
            InitializeComponent();
        }

        private void buttonSpecHeaderGroup1_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        public string FileName
        {
            get
            {
                return roundedPanel1.GroupText;
            }
            set
            {
                roundedPanel1.GroupText = value;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void exButton1_Click(object sender, EventArgs e)
        {
            Process.Start(this.FileName);
        }
    }
}
