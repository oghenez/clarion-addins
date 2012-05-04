namespace ClarionAddins.ImageViewer
{
    partial class ImageViewerControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.roundedPanel1 = new CG.Controls.Misc.RoundedPanel(this.components);
            this.roundedPanel2 = new CG.Controls.Misc.RoundedPanel(this.components);
            this.exHyperlink1 = new CG.Controls.Grid.Buttons.ExHyperlink();
            this.btnClose = new CG.Controls.Grid.Buttons.ExButton();
            this.exButton1 = new CG.Controls.Grid.Buttons.ExButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.roundedPanel1.SuspendLayout();
            this.roundedPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10, 40, 10, 40);
            this.panel1.Size = new System.Drawing.Size(512, 383);
            this.panel1.TabIndex = 1;
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.Location = new System.Drawing.Point(10, 40);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(492, 303);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // roundedPanel1
            // 
            this.roundedPanel1.BackColor = System.Drawing.Color.Transparent;
            this.roundedPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.roundedPanel1.BorderWidth = 2;
            this.roundedPanel1.ColorPreset = CG.Controls.Common.ColorPresetType.Gray;
            this.roundedPanel1.Controls.Add(this.roundedPanel2);
            this.roundedPanel1.Controls.Add(this.panel1);
            this.roundedPanel1.CornerRadiusLeftBottom = 0;
            this.roundedPanel1.CornerRadiusLeftTop = 20;
            this.roundedPanel1.CornerRadiusRightBottom = 0;
            this.roundedPanel1.CornerRadiusRightTop = 20;
            this.roundedPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundedPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roundedPanel1.GradientAngle = 90F;
            this.roundedPanel1.GradientColorFrom = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.roundedPanel1.GradientColorTo = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(222)))), ((int)(((byte)(222)))));
            this.roundedPanel1.GroupText = "Image information";
            this.roundedPanel1.GroupTextOffSetX = 5;
            this.roundedPanel1.GroupTextOffSetY = 0;
            this.roundedPanel1.Location = new System.Drawing.Point(5, 5);
            this.roundedPanel1.Name = "roundedPanel1";
            this.roundedPanel1.RepaintOnResize = false;
            this.roundedPanel1.ShowGroupTextBorder = true;
            this.roundedPanel1.Size = new System.Drawing.Size(512, 383);
            this.roundedPanel1.TabIndex = 1;
            this.roundedPanel1.TextBackColor = System.Drawing.Color.White;
            this.roundedPanel1.TextColor = System.Drawing.Color.DimGray;
            // 
            // roundedPanel2
            // 
            this.roundedPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roundedPanel2.BackColor = System.Drawing.Color.Transparent;
            this.roundedPanel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.roundedPanel2.BorderWidth = 1;
            this.roundedPanel2.ColorPreset = CG.Controls.Common.ColorPresetType.Gray;
            this.roundedPanel2.Controls.Add(this.exButton1);
            this.roundedPanel2.Controls.Add(this.exHyperlink1);
            this.roundedPanel2.Controls.Add(this.btnClose);
            this.roundedPanel2.CornerRadiusLeftBottom = 0;
            this.roundedPanel2.CornerRadiusLeftTop = 0;
            this.roundedPanel2.CornerRadiusRightBottom = 0;
            this.roundedPanel2.CornerRadiusRightTop = 0;
            this.roundedPanel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roundedPanel2.GradientAngle = 360F;
            this.roundedPanel2.GradientColorFrom = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.roundedPanel2.GradientColorTo = System.Drawing.Color.DimGray;
            this.roundedPanel2.GroupText = "";
            this.roundedPanel2.GroupTextOffSetX = 5;
            this.roundedPanel2.GroupTextOffSetY = 0;
            this.roundedPanel2.Location = new System.Drawing.Point(0, 351);
            this.roundedPanel2.Name = "roundedPanel2";
            this.roundedPanel2.RepaintOnResize = false;
            this.roundedPanel2.ShowGroupTextBorder = false;
            this.roundedPanel2.Size = new System.Drawing.Size(512, 32);
            this.roundedPanel2.TabIndex = 1;
            this.roundedPanel2.TextBackColor = System.Drawing.SystemColors.Control;
            this.roundedPanel2.TextColor = System.Drawing.Color.Black;
            // 
            // exHyperlink1
            // 
            this.exHyperlink1.BackColor = System.Drawing.Color.Transparent;
            this.exHyperlink1.DrawFocusRect = true;
            this.exHyperlink1.Image = global::ImageViewer.Properties.Resources.logo_16x16;
            this.exHyperlink1.ImageOffset = new System.Drawing.Point(4, 0);
            this.exHyperlink1.ImageSize = new System.Drawing.Size(16, 16);
            this.exHyperlink1.LinkColor = System.Drawing.Color.DimGray;
            this.exHyperlink1.LinkColorHover = System.Drawing.Color.Black;
            this.exHyperlink1.LinkColorVisited = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(102)))), ((int)(((byte)(128)))));
            this.exHyperlink1.LinkUnderline = CG.Controls.Grid.Buttons.EnLinkUnderline.OnHover;
            this.exHyperlink1.LinkVisited = false;
            this.exHyperlink1.Location = new System.Drawing.Point(3, 3);
            this.exHyperlink1.Name = "exHyperlink1";
            this.exHyperlink1.NavigateToUrl = false;
            this.exHyperlink1.PaddingText = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.exHyperlink1.Size = new System.Drawing.Size(171, 23);
            this.exHyperlink1.TabIndex = 1;
            this.exHyperlink1.Text = "http://clarionaddins.com";
            this.exHyperlink1.Url = "http://clarionaddins.com/?ImageViewer";
            this.exHyperlink1.WordWrap = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.CornerRadius = 3;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.DrawFocusRect = true;
            this.btnClose.Image = global::ImageViewer.Properties.Resources.dvCancelRed;
            this.btnClose.ImageOffset = new System.Drawing.Point(4, 0);
            this.btnClose.ImageSize = new System.Drawing.Size(16, 16);
            this.btnClose.IsSplitButton = false;
            this.btnClose.Location = new System.Drawing.Point(416, 5);
            this.btnClose.MetroColorScheme = CG.Controls.Grid.Render.MetroScheme.Orange;
            this.btnClose.Name = "btnClose";
            this.btnClose.PaddingText = new System.Windows.Forms.Padding(4);
            this.btnClose.RenderType = CG.Controls.Common.EnRenderType.Metro;
            this.btnClose.Size = new System.Drawing.Size(84, 23);
            this.btnClose.SplitButtonWidth = 20;
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // exButton1
            // 
            this.exButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exButton1.BackColor = System.Drawing.Color.Transparent;
            this.exButton1.CornerRadius = 3;
            this.exButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.exButton1.DrawFocusRect = true;
            this.exButton1.Image = global::ImageViewer.Properties.Resources.dvPicture1;
            this.exButton1.ImageOffset = new System.Drawing.Point(4, 0);
            this.exButton1.ImageSize = new System.Drawing.Size(16, 16);
            this.exButton1.IsSplitButton = false;
            this.exButton1.Location = new System.Drawing.Point(326, 5);
            this.exButton1.MetroColorScheme = CG.Controls.Grid.Render.MetroScheme.Orange;
            this.exButton1.Name = "exButton1";
            this.exButton1.PaddingText = new System.Windows.Forms.Padding(4);
            this.exButton1.RenderType = CG.Controls.Common.EnRenderType.Metro;
            this.exButton1.Size = new System.Drawing.Size(84, 23);
            this.exButton1.SplitButtonWidth = 20;
            this.exButton1.TabIndex = 2;
            this.exButton1.Text = "Open";
            this.toolTip1.SetToolTip(this.exButton1, "Open image using associated program...");
            this.exButton1.Click += new System.EventHandler(this.exButton1_Click);
            // 
            // ImageViewerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.roundedPanel1);
            this.Name = "ImageViewerControl";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(522, 393);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.roundedPanel1.ResumeLayout(false);
            this.roundedPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private CG.Controls.Misc.RoundedPanel roundedPanel1;
        private CG.Controls.Misc.RoundedPanel roundedPanel2;
        private CG.Controls.Grid.Buttons.ExButton btnClose;
        private CG.Controls.Grid.Buttons.ExHyperlink exHyperlink1;
        private CG.Controls.Grid.Buttons.ExButton exButton1;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}
