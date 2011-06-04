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
            this.headerGroup = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonLinkLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLinkLabel();
            this.listBoxDebug = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.kryptonGroupBox2 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.buttonStartPlayback = new ComponentFactory.Krypton.Toolkit.VisualTaskDialog.MessageButton();
            this.kryptonGroupBox1 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.buttonStop = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.buttonStart = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup)).BeginInit();
            this.headerGroup.Panel.SuspendLayout();
            this.headerGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
            this.kryptonGroupBox2.Panel.SuspendLayout();
            this.kryptonGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerGroup
            // 
            this.headerGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerGroup.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.headerGroup.HeaderVisibleSecondary = false;
            this.headerGroup.Location = new System.Drawing.Point(0, 0);
            this.headerGroup.Name = "headerGroup";
            // 
            // headerGroup.Panel
            // 
            this.headerGroup.Panel.Controls.Add(this.kryptonPanel1);
            this.headerGroup.Panel.Controls.Add(this.listBoxDebug);
            this.headerGroup.Panel.Controls.Add(this.kryptonGroupBox2);
            this.headerGroup.Panel.Controls.Add(this.kryptonGroupBox1);
            this.headerGroup.Size = new System.Drawing.Size(419, 497);
            this.headerGroup.TabIndex = 0;
            this.headerGroup.ValuesPrimary.Heading = "Editor Macros";
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonLinkLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 435);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(417, 39);
            this.kryptonPanel1.TabIndex = 3;
            // 
            // kryptonLinkLabel1
            // 
            this.kryptonLinkLabel1.Location = new System.Drawing.Point(4, 16);
            this.kryptonLinkLabel1.Name = "kryptonLinkLabel1";
            this.kryptonLinkLabel1.Size = new System.Drawing.Size(206, 20);
            this.kryptonLinkLabel1.TabIndex = 0;
            this.kryptonLinkLabel1.Values.Text = "http://www.clarionedge.com/addins";
            this.kryptonLinkLabel1.LinkClicked += new System.EventHandler(this.kryptonLinkLabel1_LinkClicked);
            // 
            // listBoxDebug
            // 
            this.listBoxDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxDebug.Location = new System.Drawing.Point(4, 189);
            this.listBoxDebug.Name = "listBoxDebug";
            this.listBoxDebug.Size = new System.Drawing.Size(411, 240);
            this.listBoxDebug.TabIndex = 2;
            // 
            // kryptonGroupBox2
            // 
            this.kryptonGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonGroupBox2.Location = new System.Drawing.Point(4, 96);
            this.kryptonGroupBox2.Name = "kryptonGroupBox2";
            // 
            // kryptonGroupBox2.Panel
            // 
            this.kryptonGroupBox2.Panel.Controls.Add(this.buttonStartPlayback);
            this.kryptonGroupBox2.Size = new System.Drawing.Size(411, 87);
            this.kryptonGroupBox2.TabIndex = 1;
            this.kryptonGroupBox2.Values.Heading = "Playback";
            // 
            // buttonStartPlayback
            // 
            this.buttonStartPlayback.IgnoreAltF4 = false;
            this.buttonStartPlayback.Location = new System.Drawing.Point(20, 20);
            this.buttonStartPlayback.Name = "buttonStartPlayback";
            this.buttonStartPlayback.Size = new System.Drawing.Size(90, 25);
            this.buttonStartPlayback.TabIndex = 0;
            this.buttonStartPlayback.Values.Text = "Start";
            this.buttonStartPlayback.Click += new System.EventHandler(this.buttonStartPlayback_Click);
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.buttonStop);
            this.kryptonGroupBox1.Panel.Controls.Add(this.buttonStart);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(411, 87);
            this.kryptonGroupBox1.TabIndex = 0;
            this.kryptonGroupBox1.Values.Heading = "Record Macro";
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(117, 19);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(90, 25);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Values.Text = "Stop";
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(21, 19);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(90, 25);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Values.Text = "Start";
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // EditorMacrosPadControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.headerGroup);
            this.Name = "EditorMacrosPadControl";
            this.Size = new System.Drawing.Size(419, 497);
            this.headerGroup.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup)).EndInit();
            this.headerGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.kryptonGroupBox2.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).EndInit();
            this.kryptonGroupBox2.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup headerGroup;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox listBoxDebug;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton buttonStop;
        private ComponentFactory.Krypton.Toolkit.KryptonButton buttonStart;
        private ComponentFactory.Krypton.Toolkit.VisualTaskDialog.MessageButton buttonStartPlayback;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLinkLabel kryptonLinkLabel1;



    }
}
