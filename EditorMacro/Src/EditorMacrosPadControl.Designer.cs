namespace ClarionEdge.EditorMacros
{
    partial class EditorMacrosPadControl
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
            this.groupBoxAll = new System.Windows.Forms.GroupBox();
            this.buttonPlayBack = new System.Windows.Forms.Button();
            this.buttonStopRecording = new System.Windows.Forms.Button();
            this.buttonStartRecording = new System.Windows.Forms.Button();
            this.listBoxDebug = new System.Windows.Forms.ListBox();
            this.groupBoxAll.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxAll
            // 
            this.groupBoxAll.Controls.Add(this.listBoxDebug);
            this.groupBoxAll.Controls.Add(this.buttonPlayBack);
            this.groupBoxAll.Controls.Add(this.buttonStopRecording);
            this.groupBoxAll.Controls.Add(this.buttonStartRecording);
            this.groupBoxAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxAll.Location = new System.Drawing.Point(0, 0);
            this.groupBoxAll.Name = "groupBoxAll";
            this.groupBoxAll.Size = new System.Drawing.Size(419, 497);
            this.groupBoxAll.TabIndex = 3;
            this.groupBoxAll.TabStop = false;
            // 
            // buttonPlayBack
            // 
            this.buttonPlayBack.Image = global::ClarionEdge.EditorMacros.Properties.Resources.play;
            this.buttonPlayBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonPlayBack.Location = new System.Drawing.Point(200, 19);
            this.buttonPlayBack.Name = "buttonPlayBack";
            this.buttonPlayBack.Size = new System.Drawing.Size(90, 25);
            this.buttonPlayBack.TabIndex = 3;
            this.buttonPlayBack.Text = "Play Back";
            this.buttonPlayBack.UseVisualStyleBackColor = true;
            this.buttonPlayBack.Click += new System.EventHandler(this.buttonStartPlayback_Click);
            // 
            // buttonStopRecording
            // 
            this.buttonStopRecording.Image = global::ClarionEdge.EditorMacros.Properties.Resources.stop;
            this.buttonStopRecording.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonStopRecording.Location = new System.Drawing.Point(104, 19);
            this.buttonStopRecording.Name = "buttonStopRecording";
            this.buttonStopRecording.Size = new System.Drawing.Size(90, 25);
            this.buttonStopRecording.TabIndex = 2;
            this.buttonStopRecording.Text = "Stop Rec";
            this.buttonStopRecording.UseVisualStyleBackColor = true;
            this.buttonStopRecording.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonStartRecording
            // 
            this.buttonStartRecording.Image = global::ClarionEdge.EditorMacros.Properties.Resources.record;
            this.buttonStartRecording.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonStartRecording.Location = new System.Drawing.Point(8, 19);
            this.buttonStartRecording.Name = "buttonStartRecording";
            this.buttonStartRecording.Size = new System.Drawing.Size(90, 25);
            this.buttonStartRecording.TabIndex = 1;
            this.buttonStartRecording.Text = "Start Rec";
            this.buttonStartRecording.UseVisualStyleBackColor = true;
            this.buttonStartRecording.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // listBoxDebug
            // 
            this.listBoxDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxDebug.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxDebug.FormattingEnabled = true;
            this.listBoxDebug.ItemHeight = 14;
            this.listBoxDebug.Location = new System.Drawing.Point(6, 53);
            this.listBoxDebug.Margin = new System.Windows.Forms.Padding(10);
            this.listBoxDebug.Name = "listBoxDebug";
            this.listBoxDebug.ScrollAlwaysVisible = true;
            this.listBoxDebug.Size = new System.Drawing.Size(407, 424);
            this.listBoxDebug.TabIndex = 4;
            // 
            // EditorMacrosPadControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxAll);
            this.Name = "EditorMacrosPadControl";
            this.Size = new System.Drawing.Size(419, 497);
            this.groupBoxAll.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxAll;
        private System.Windows.Forms.Button buttonPlayBack;
        private System.Windows.Forms.Button buttonStopRecording;
        private System.Windows.Forms.Button buttonStartRecording;
        private System.Windows.Forms.ListBox listBoxDebug;



    }
}
