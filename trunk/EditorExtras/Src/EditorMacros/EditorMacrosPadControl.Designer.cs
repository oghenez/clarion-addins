namespace ClarionAddins.EditorMacros
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
            this.components = new System.ComponentModel.Container();
            this.groupBoxAll = new System.Windows.Forms.GroupBox();
            this.listBoxDebug = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonPlayBack = new System.Windows.Forms.Button();
            this.buttonStartRecording = new System.Windows.Forms.Button();
            this.cbDisableCCWhileRecording = new System.Windows.Forms.CheckBox();
            this.groupBoxAll.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxAll
            // 
            this.groupBoxAll.Controls.Add(this.cbDisableCCWhileRecording);
            this.groupBoxAll.Controls.Add(this.listBoxDebug);
            this.groupBoxAll.Controls.Add(this.buttonPlayBack);
            this.groupBoxAll.Controls.Add(this.buttonStartRecording);
            this.groupBoxAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxAll.Location = new System.Drawing.Point(0, 0);
            this.groupBoxAll.Name = "groupBoxAll";
            this.groupBoxAll.Size = new System.Drawing.Size(419, 497);
            this.groupBoxAll.TabIndex = 3;
            this.groupBoxAll.TabStop = false;
            // 
            // listBoxDebug
            // 
            this.listBoxDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxDebug.ContextMenuStrip = this.contextMenuStrip1;
            this.listBoxDebug.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxDebug.FormattingEnabled = true;
            this.listBoxDebug.ItemHeight = 14;
            this.listBoxDebug.Location = new System.Drawing.Point(6, 81);
            this.listBoxDebug.Margin = new System.Windows.Forms.Padding(10);
            this.listBoxDebug.Name = "listBoxDebug";
            this.listBoxDebug.ScrollAlwaysVisible = true;
            this.listBoxDebug.Size = new System.Drawing.Size(407, 396);
            this.listBoxDebug.TabIndex = 4;
            this.listBoxDebug.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxDebug_MouseDown);
            this.listBoxDebug.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxDebug_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::ClarionAddins.Properties.Resources.delete;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "Remove";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // buttonPlayBack
            // 
            this.buttonPlayBack.Image = global::ClarionAddins.Properties.Resources.play;
            this.buttonPlayBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonPlayBack.Location = new System.Drawing.Point(94, 19);
            this.buttonPlayBack.Name = "buttonPlayBack";
            this.buttonPlayBack.Size = new System.Drawing.Size(80, 25);
            this.buttonPlayBack.TabIndex = 3;
            this.buttonPlayBack.Text = "Playback";
            this.buttonPlayBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonPlayBack.UseVisualStyleBackColor = true;
            this.buttonPlayBack.Click += new System.EventHandler(this.buttonStartPlayback_Click);
            // 
            // buttonStartRecording
            // 
            this.buttonStartRecording.Image = global::ClarionAddins.Properties.Resources.record;
            this.buttonStartRecording.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonStartRecording.Location = new System.Drawing.Point(8, 19);
            this.buttonStartRecording.Name = "buttonStartRecording";
            this.buttonStartRecording.Size = new System.Drawing.Size(80, 25);
            this.buttonStartRecording.TabIndex = 1;
            this.buttonStartRecording.Text = "Start";
            this.buttonStartRecording.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonStartRecording.UseVisualStyleBackColor = true;
            this.buttonStartRecording.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // cbDisableCCWhileRecording
            // 
            this.cbDisableCCWhileRecording.AutoSize = true;
            this.cbDisableCCWhileRecording.Location = new System.Drawing.Point(6, 50);
            this.cbDisableCCWhileRecording.Name = "cbDisableCCWhileRecording";
            this.cbDisableCCWhileRecording.Size = new System.Drawing.Size(218, 17);
            this.cbDisableCCWhileRecording.TabIndex = 5;
            this.cbDisableCCWhileRecording.Text = "Disable Code Completion while recording";
            this.cbDisableCCWhileRecording.UseVisualStyleBackColor = true;
            this.cbDisableCCWhileRecording.CheckedChanged += new System.EventHandler(this.cbDisableCCWhileRecording_CheckedChanged);
            // 
            // EditorMacrosPadControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxAll);
            this.Name = "EditorMacrosPadControl";
            this.Size = new System.Drawing.Size(419, 497);
            this.groupBoxAll.ResumeLayout(false);
            this.groupBoxAll.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxAll;
        private System.Windows.Forms.Button buttonPlayBack;
        private System.Windows.Forms.Button buttonStartRecording;
        private System.Windows.Forms.ListBox listBoxDebug;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.CheckBox cbDisableCCWhileRecording;



    }
}
