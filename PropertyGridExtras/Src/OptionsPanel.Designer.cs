namespace ClarionEdge.PropertyGridExtras
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
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.FontGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxEnableLogging = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoAdjustLabelColumn = new System.Windows.Forms.CheckBox();
            this.checkBoxShowAdditionalIndentation = new System.Windows.Forms.CheckBox();
            this.checkBoxRememberExpandedState = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dropDownTheme = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxRememberSelectedProperty = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(57, 3);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.label1.Size = new System.Drawing.Size(268, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "General options for the property grid.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ClarionEdge.PropertyGridExtras.Properties.Resources._1317629949_document_properties;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // FontGroupBox
            // 
            this.FontGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FontGroupBox.Location = new System.Drawing.Point(3, 57);
            this.FontGroupBox.Name = "FontGroupBox";
            this.FontGroupBox.Size = new System.Drawing.Size(409, 148);
            this.FontGroupBox.TabIndex = 3;
            this.FontGroupBox.TabStop = false;
            this.FontGroupBox.Text = "Font";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBoxRememberSelectedProperty);
            this.groupBox1.Controls.Add(this.checkBoxEnableLogging);
            this.groupBox1.Controls.Add(this.checkBoxAutoAdjustLabelColumn);
            this.groupBox1.Controls.Add(this.checkBoxShowAdditionalIndentation);
            this.groupBox1.Controls.Add(this.checkBoxRememberExpandedState);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dropDownTheme);
            this.groupBox1.Location = new System.Drawing.Point(3, 211);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(409, 192);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Additional Settings";
            // 
            // checkBoxEnableLogging
            // 
            this.checkBoxEnableLogging.AutoSize = true;
            this.checkBoxEnableLogging.Location = new System.Drawing.Point(58, 143);
            this.checkBoxEnableLogging.Name = "checkBoxEnableLogging";
            this.checkBoxEnableLogging.Size = new System.Drawing.Size(100, 17);
            this.checkBoxEnableLogging.TabIndex = 5;
            this.checkBoxEnableLogging.Text = "Enable Logging";
            this.checkBoxEnableLogging.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoAdjustLabelColumn
            // 
            this.checkBoxAutoAdjustLabelColumn.AutoSize = true;
            this.checkBoxAutoAdjustLabelColumn.Location = new System.Drawing.Point(60, 42);
            this.checkBoxAutoAdjustLabelColumn.Name = "checkBoxAutoAdjustLabelColumn";
            this.checkBoxAutoAdjustLabelColumn.Size = new System.Drawing.Size(288, 17);
            this.checkBoxAutoAdjustLabelColumn.TabIndex = 1;
            this.checkBoxAutoAdjustLabelColumn.Text = "ClarionEdge.PropertyGridExtras.AutoAdjustLabelColumn";
            this.checkBoxAutoAdjustLabelColumn.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowAdditionalIndentation
            // 
            this.checkBoxShowAdditionalIndentation.AutoSize = true;
            this.checkBoxShowAdditionalIndentation.Location = new System.Drawing.Point(60, 19);
            this.checkBoxShowAdditionalIndentation.Name = "checkBoxShowAdditionalIndentation";
            this.checkBoxShowAdditionalIndentation.Size = new System.Drawing.Size(302, 17);
            this.checkBoxShowAdditionalIndentation.TabIndex = 0;
            this.checkBoxShowAdditionalIndentation.Text = "ClarionEdge.PropertyGridExtras.ShowAdditionalIndentation";
            this.checkBoxShowAdditionalIndentation.UseVisualStyleBackColor = true;
            // 
            // checkBoxRememberExpandedState
            // 
            this.checkBoxRememberExpandedState.AutoSize = true;
            this.checkBoxRememberExpandedState.Location = new System.Drawing.Point(60, 65);
            this.checkBoxRememberExpandedState.Name = "checkBoxRememberExpandedState";
            this.checkBoxRememberExpandedState.Size = new System.Drawing.Size(300, 17);
            this.checkBoxRememberExpandedState.TabIndex = 4;
            this.checkBoxRememberExpandedState.Text = "ClarionEdge.PropertyGridExtras.RememberExpandedState";
            this.checkBoxRememberExpandedState.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Theme";
            // 
            // dropDownTheme
            // 
            this.dropDownTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropDownTheme.FormattingEnabled = true;
            this.dropDownTheme.Location = new System.Drawing.Point(58, 116);
            this.dropDownTheme.Name = "dropDownTheme";
            this.dropDownTheme.Size = new System.Drawing.Size(163, 21);
            this.dropDownTheme.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.FontGroupBox);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(415, 406);
            this.panel1.TabIndex = 0;
            // 
            // checkBoxRememberSelectedProperty
            // 
            this.checkBoxRememberSelectedProperty.AutoSize = true;
            this.checkBoxRememberSelectedProperty.Location = new System.Drawing.Point(60, 88);
            this.checkBoxRememberSelectedProperty.Name = "checkBoxRememberSelectedProperty";
            this.checkBoxRememberSelectedProperty.Size = new System.Drawing.Size(308, 17);
            this.checkBoxRememberSelectedProperty.TabIndex = 6;
            this.checkBoxRememberSelectedProperty.Text = "ClarionEdge.PropertyGridExtras.RememberSelectedProperty";
            this.checkBoxRememberSelectedProperty.UseVisualStyleBackColor = true;
            // 
            // OptionsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "OptionsPanel";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(425, 416);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox FontGroupBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxShowAdditionalIndentation;
        private System.Windows.Forms.CheckBox checkBoxAutoAdjustLabelColumn;
        private System.Windows.Forms.ComboBox dropDownTheme;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBoxRememberExpandedState;
        private System.Windows.Forms.CheckBox checkBoxEnableLogging;
        private System.Windows.Forms.CheckBox checkBoxRememberSelectedProperty;
    }
}
