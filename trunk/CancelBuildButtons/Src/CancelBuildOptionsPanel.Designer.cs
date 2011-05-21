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
            this.hideButtonLabels = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // hideButtonLabels
            // 
            this.hideButtonLabels.AutoSize = true;
            this.hideButtonLabels.Location = new System.Drawing.Point(19, 33);
            this.hideButtonLabels.Name = "hideButtonLabels";
            this.hideButtonLabels.Size = new System.Drawing.Size(194, 17);
            this.hideButtonLabels.TabIndex = 0;
            this.hideButtonLabels.Text = "Hide Button Labels (requires restart)";
            this.hideButtonLabels.UseVisualStyleBackColor = true;
            // 
            // CancelBuildOptionsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hideButtonLabels);
            this.Name = "CancelBuildOptionsPanel";
            this.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.Size = new System.Drawing.Size(492, 441);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.CheckBox hideButtonLabels;
    }
}
