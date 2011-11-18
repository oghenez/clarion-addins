namespace ClarionAddins.KeyboardShortcuts
{
    partial class KeyboardShortcutsOptionsPanel
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelUsedBy = new System.Windows.Forms.Label();
            this.labelCurrentSelection = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbNewShortcut = new System.Windows.Forms.TextBox();
            this.buttonAssign = new System.Windows.Forms.Button();
            this.buttonNone = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(423, 453);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // dataGridView
            // 
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(3, 19);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(417, 327);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonRemove);
            this.panel1.Controls.Add(this.buttonNone);
            this.panel1.Controls.Add(this.labelUsedBy);
            this.panel1.Controls.Add(this.labelCurrentSelection);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbNewShortcut);
            this.panel1.Controls.Add(this.buttonAssign);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 352);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(417, 98);
            this.panel1.TabIndex = 6;
            // 
            // labelUsedBy
            // 
            this.labelUsedBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUsedBy.AutoSize = true;
            this.labelUsedBy.BackColor = System.Drawing.SystemColors.Control;
            this.labelUsedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUsedBy.Location = new System.Drawing.Point(3, 69);
            this.labelUsedBy.Name = "labelUsedBy";
            this.labelUsedBy.Size = new System.Drawing.Size(117, 13);
            this.labelUsedBy.TabIndex = 6;
            this.labelUsedBy.Text = "Currently used by...";
            this.labelUsedBy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCurrentSelection
            // 
            this.labelCurrentSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCurrentSelection.AutoSize = true;
            this.labelCurrentSelection.BackColor = System.Drawing.SystemColors.Control;
            this.labelCurrentSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurrentSelection.Location = new System.Drawing.Point(3, 11);
            this.labelCurrentSelection.Name = "labelCurrentSelection";
            this.labelCurrentSelection.Size = new System.Drawing.Size(140, 13);
            this.labelCurrentSelection.TabIndex = 5;
            this.labelCurrentSelection.Text = "Current Selection Label";
            this.labelCurrentSelection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "New shortcut key:";
            // 
            // tbNewShortcut
            // 
            this.tbNewShortcut.Location = new System.Drawing.Point(102, 37);
            this.tbNewShortcut.Name = "tbNewShortcut";
            this.tbNewShortcut.Size = new System.Drawing.Size(114, 20);
            this.tbNewShortcut.TabIndex = 3;
            this.tbNewShortcut.TextChanged += new System.EventHandler(this.tbNewShortcut_TextChanged);
            this.tbNewShortcut.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbNewShortcut_KeyDown);
            this.tbNewShortcut.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbNewShortcut_KeyPress);
            this.tbNewShortcut.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tbNewShortcut_PreviewKeyDown);
            // 
            // buttonAssign
            // 
            this.buttonAssign.Location = new System.Drawing.Point(222, 35);
            this.buttonAssign.Name = "buttonAssign";
            this.buttonAssign.Size = new System.Drawing.Size(75, 23);
            this.buttonAssign.TabIndex = 4;
            this.buttonAssign.Text = "Assign";
            this.buttonAssign.UseVisualStyleBackColor = true;
            this.buttonAssign.Click += new System.EventHandler(this.buttonAssign_Click);
            // 
            // buttonNone
            // 
            this.buttonNone.Location = new System.Drawing.Point(303, 35);
            this.buttonNone.Name = "buttonNone";
            this.buttonNone.Size = new System.Drawing.Size(75, 23);
            this.buttonNone.TabIndex = 7;
            this.buttonNone.Text = "None";
            this.buttonNone.UseVisualStyleBackColor = true;
            this.buttonNone.Click += new System.EventHandler(this.buttonNone_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Image = global::ClarionAddins.KeyboardShortcuts.Properties.Resources.dvCancelRed;
            this.buttonRemove.Location = new System.Drawing.Point(384, 35);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(23, 23);
            this.buttonRemove.TabIndex = 8;
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // KeyboardShortcutsOptionsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "KeyboardShortcutsOptionsPanel";
            this.Size = new System.Drawing.Size(423, 453);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonAssign;
        private System.Windows.Forms.TextBox tbNewShortcut;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelCurrentSelection;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelUsedBy;
        private System.Windows.Forms.Button buttonNone;
        private System.Windows.Forms.Button buttonRemove;
    }
}
