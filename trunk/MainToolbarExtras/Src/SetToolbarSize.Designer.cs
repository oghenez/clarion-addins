namespace ClarionEdge.MainToolbarExtras
{
    partial class SetToolbarSize
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.spinIconSize = new System.Windows.Forms.NumericUpDown();
            this.spinHeight = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonSetSizeC7 = new System.Windows.Forms.Button();
            this.buttonHelp = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.spinIconSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Toolbar Icon Size";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Toolbar Height";
            // 
            // spinIconSize
            // 
            this.spinIconSize.Location = new System.Drawing.Point(109, 13);
            this.spinIconSize.Name = "spinIconSize";
            this.spinIconSize.Size = new System.Drawing.Size(73, 20);
            this.spinIconSize.TabIndex = 7;
            // 
            // spinHeight
            // 
            this.spinHeight.Location = new System.Drawing.Point(109, 39);
            this.spinHeight.Name = "spinHeight";
            this.spinHeight.Size = new System.Drawing.Size(73, 20);
            this.spinHeight.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Use Default Size";
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Image = global::ClarionEdge.MainToolbarExtras.Properties.Resources.famfamfam_silk_accept;
            this.buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOK.Location = new System.Drawing.Point(91, 106);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "&OK";
            this.buttonOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Image = global::ClarionEdge.MainToolbarExtras.Properties.Resources.famfamfam_silk_cancel;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(172, 106);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Image = global::ClarionEdge.MainToolbarExtras.Properties.Resources.Clarion8_16x16;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(163, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(48, 24);
            this.button1.TabIndex = 11;
            this.button1.Text = "C8";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonSetSizeC7
            // 
            this.buttonSetSizeC7.Image = global::ClarionEdge.MainToolbarExtras.Properties.Resources.Clarion7_16x16;
            this.buttonSetSizeC7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSetSizeC7.Location = new System.Drawing.Point(109, 65);
            this.buttonSetSizeC7.Name = "buttonSetSizeC7";
            this.buttonSetSizeC7.Size = new System.Drawing.Size(48, 24);
            this.buttonSetSizeC7.TabIndex = 9;
            this.buttonSetSizeC7.Text = "C7";
            this.buttonSetSizeC7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSetSizeC7.UseVisualStyleBackColor = true;
            this.buttonSetSizeC7.Click += new System.EventHandler(this.buttonSetSizeC7_Click);
            // 
            // buttonHelp
            // 
            this.buttonHelp.Image = global::ClarionEdge.MainToolbarExtras.Properties.Resources.famfamfam_silk_help;
            this.buttonHelp.Location = new System.Drawing.Point(16, 105);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(24, 24);
            this.buttonHelp.TabIndex = 4;
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // SetToolbarSize
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(259, 141);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonSetSizeC7);
            this.Controls.Add(this.spinHeight);
            this.Controls.Add(this.spinIconSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Name = "SetToolbarSize";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Set Toolbar Size";
            ((System.ComponentModel.ISupportInitialize)(this.spinIconSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.NumericUpDown spinIconSize;
        private System.Windows.Forms.NumericUpDown spinHeight;
        private System.Windows.Forms.Button buttonSetSizeC7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}