namespace ClarionAddins.IdeDebug
{
    partial class OptionsPanel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbExtraDebugInfo = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbQuietMode = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbExtraDebugInfo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbQuietMode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 363);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Location = new System.Drawing.Point(3, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(369, 39);
            this.label2.TabIndex = 3;
            this.label2.Text = "The IdeDebug Addin will output debug strings for useful events.\r\nThis can assist " +
    "in diagnosing problems. \r\nNOTE: Requires a restart of the IDE.";
            // 
            // cbExtraDebugInfo
            // 
            this.cbExtraDebugInfo.AutoSize = true;
            this.cbExtraDebugInfo.Location = new System.Drawing.Point(6, 78);
            this.cbExtraDebugInfo.Name = "cbExtraDebugInfo";
            this.cbExtraDebugInfo.Size = new System.Drawing.Size(187, 17);
            this.cbExtraDebugInfo.TabIndex = 2;
            this.cbExtraDebugInfo.Text = "Generate Extra Debug Information";
            this.cbExtraDebugInfo.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(3, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(371, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Turning OFF Quiet Mode will cause all exceptions to be shown.\r\nThis can be helpfu" +
    "l when trying to debug problems with the IDE.";
            // 
            // cbQuietMode
            // 
            this.cbQuietMode.AutoSize = true;
            this.cbQuietMode.Location = new System.Drawing.Point(6, 19);
            this.cbQuietMode.Name = "cbQuietMode";
            this.cbQuietMode.Size = new System.Drawing.Size(81, 17);
            this.cbQuietMode.TabIndex = 0;
            this.cbQuietMode.Text = "Quiet Mode";
            this.cbQuietMode.UseVisualStyleBackColor = true;
            // 
            // OptionsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "OptionsPanel";
            this.Size = new System.Drawing.Size(400, 363);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbQuietMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbExtraDebugInfo;
    }
}
