namespace ClarionEdge.InsertClarionColor
{
    partial class ColorDialogPadControl
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
            this.colorEditorControl = new ZetaColorEditor.Runtime.ColorEditorUserControl();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.colorEditorControl);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 464);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // colorEditorControl
            // 
            this.colorEditorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorEditorControl.ExternalColorEditorInformationProvider = null;
            this.colorEditorControl.Location = new System.Drawing.Point(3, 16);
            this.colorEditorControl.MinimumSize = new System.Drawing.Size(372, 398);
            this.colorEditorControl.Name = "colorEditorControl";
            this.colorEditorControl.SelectedColor = System.Drawing.Color.Empty;
            this.colorEditorControl.Size = new System.Drawing.Size(372, 445);
            this.colorEditorControl.SystemColorsTabPageSelected = false;
            this.colorEditorControl.TabIndex = 1;
            // 
            // ColorDialogPadControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ColorDialogPadControl";
            this.Size = new System.Drawing.Size(375, 464);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ZetaColorEditor.Runtime.ColorEditorUserControl colorEditorControl;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
