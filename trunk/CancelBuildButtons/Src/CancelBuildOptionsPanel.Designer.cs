namespace ClarionEdge.CancelBuildButtons
{
    partial class CancelBuildOptionsPanel
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
            this.hideButtonLabels = new System.Windows.Forms.CheckBox();
            this.roundedPanel1 = new CG.Controls.Misc.RoundedPanel(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.roundedPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // hideButtonLabels
            // 
            this.hideButtonLabels.AutoSize = true;
            this.hideButtonLabels.ForeColor = System.Drawing.Color.White;
            this.hideButtonLabels.Location = new System.Drawing.Point(3, 3);
            this.hideButtonLabels.Name = "hideButtonLabels";
            this.hideButtonLabels.Size = new System.Drawing.Size(194, 17);
            this.hideButtonLabels.TabIndex = 0;
            this.hideButtonLabels.Text = "Hide Button Labels (requires restart)";
            this.hideButtonLabels.UseVisualStyleBackColor = true;
            // 
            // roundedPanel1
            // 
            this.roundedPanel1.BackColor = System.Drawing.Color.Transparent;
            this.roundedPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.roundedPanel1.BorderWidth = 4;
            this.roundedPanel1.ColorPreset = CG.Controls.Common.ColorPresetType.Gray;
            this.roundedPanel1.Controls.Add(this.panel1);
            this.roundedPanel1.CornerRadiusLeftBottom = 0;
            this.roundedPanel1.CornerRadiusLeftTop = 20;
            this.roundedPanel1.CornerRadiusRightBottom = 0;
            this.roundedPanel1.CornerRadiusRightTop = 20;
            this.roundedPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundedPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roundedPanel1.GradientAngle = 90F;
            this.roundedPanel1.GradientColorFrom = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(56)))), ((int)(((byte)(69)))));
            this.roundedPanel1.GradientColorTo = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(126)))), ((int)(((byte)(137)))));
            this.roundedPanel1.GroupText = "";
            this.roundedPanel1.GroupTextOffSetX = 5;
            this.roundedPanel1.GroupTextOffSetY = 0;
            this.roundedPanel1.Location = new System.Drawing.Point(0, 5);
            this.roundedPanel1.Name = "roundedPanel1";
            this.roundedPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.roundedPanel1.RepaintOnResize = false;
            this.roundedPanel1.ShowGroupTextBorder = true;
            this.roundedPanel1.Size = new System.Drawing.Size(475, 420);
            this.roundedPanel1.TabIndex = 0;
            this.roundedPanel1.TextBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(126)))), ((int)(((byte)(137)))));
            this.roundedPanel1.TextColor = System.Drawing.Color.White;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.hideButtonLabels);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(455, 400);
            this.panel1.TabIndex = 0;
            // 
            // CancelBuildOptionsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.roundedPanel1);
            this.Name = "CancelBuildOptionsPanel";
            this.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.Size = new System.Drawing.Size(475, 425);
            this.roundedPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.CheckBox hideButtonLabels;
        private CG.Controls.Misc.RoundedPanel roundedPanel1;
        private System.Windows.Forms.Panel panel1;
    }
}
