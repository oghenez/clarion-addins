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
            this.label3 = new System.Windows.Forms.Label();
            this.dgvXLog = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.cbExtraDebugInfo = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbQuietMode = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbOutputDebugStringAppender = new System.Windows.Forms.CheckBox();
            this.tbDebugViewPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvXLog)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbDebugViewPath);
            this.groupBox1.Controls.Add(this.cbQuietMode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbExtraDebugInfo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbOutputDebugStringAppender);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dgvXLog);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 363);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 253);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(190, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "View XLOG files (Double click to open)";
            // 
            // dgvXLog
            // 
            this.dgvXLog.AllowUserToAddRows = false;
            this.dgvXLog.AllowUserToDeleteRows = false;
            this.dgvXLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvXLog.Location = new System.Drawing.Point(6, 276);
            this.dgvXLog.Name = "dgvXLog";
            this.dgvXLog.ReadOnly = true;
            this.dgvXLog.Size = new System.Drawing.Size(388, 80);
            this.dgvXLog.TabIndex = 4;
            this.dgvXLog.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvXLog_CellDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Location = new System.Drawing.Point(3, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(369, 39);
            this.label2.TabIndex = 3;
            this.label2.Text = "The IdeDebug Addin will output debug strings for useful events.\r\nThis can assist " +
    "in diagnosing problems. \r\nNOTE: Requires a restart of the IDE.";
            // 
            // cbExtraDebugInfo
            // 
            this.cbExtraDebugInfo.AutoSize = true;
            this.cbExtraDebugInfo.Location = new System.Drawing.Point(6, 70);
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
            this.label1.Location = new System.Drawing.Point(3, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(371, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Turning OFF Quiet Mode will cause all exceptions to be shown.\r\nThis can be helpfu" +
    "l when trying to debug problems with the IDE.";
            // 
            // cbQuietMode
            // 
            this.cbQuietMode.AutoSize = true;
            this.cbQuietMode.Location = new System.Drawing.Point(6, 14);
            this.cbQuietMode.Name = "cbQuietMode";
            this.cbQuietMode.Size = new System.Drawing.Size(81, 17);
            this.cbQuietMode.TabIndex = 0;
            this.cbQuietMode.Text = "Quiet Mode";
            this.cbQuietMode.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label4.Location = new System.Drawing.Point(6, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(297, 26);
            this.label4.TabIndex = 7;
            this.label4.Text = "Send IDE debug information to OutputDebugString.\r\nNOTE: Requires a restart of the" +
    " IDE.";
            // 
            // cbOutputDebugStringAppender
            // 
            this.cbOutputDebugStringAppender.AutoSize = true;
            this.cbOutputDebugStringAppender.Location = new System.Drawing.Point(9, 139);
            this.cbOutputDebugStringAppender.Name = "cbOutputDebugStringAppender";
            this.cbOutputDebugStringAppender.Size = new System.Drawing.Size(202, 17);
            this.cbOutputDebugStringAppender.TabIndex = 6;
            this.cbOutputDebugStringAppender.Text = "Enable OutputDebugString Appender";
            this.cbOutputDebugStringAppender.UseVisualStyleBackColor = true;
            // 
            // tbDebugViewPath
            // 
            this.tbDebugViewPath.Location = new System.Drawing.Point(9, 208);
            this.tbDebugViewPath.Name = "tbDebugViewPath";
            this.tbDebugViewPath.Size = new System.Drawing.Size(350, 20);
            this.tbDebugViewPath.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 192);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "DebugView Path:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(365, 205);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 26);
            this.button1.TabIndex = 10;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label6.Location = new System.Drawing.Point(6, 234);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(380, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "DbgView.exe or in fact any program you want to embed in the DebugView Pad!";
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvXLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbQuietMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbExtraDebugInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvXLog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbOutputDebugStringAppender;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDebugViewPath;
    }
}
