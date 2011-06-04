namespace UserControls.Krypton.PaletteSelectors
{
	partial class CustomPaletteXmlDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer _components;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (_components != null))
			{
				_components.Dispose();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomPaletteXmlDialog));
			this.paletteXml = new System.Windows.Forms.TextBox();
			this.loadPallete = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.paletteFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.cancelButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// paletteXml
			// 
			this.paletteXml.Location = new System.Drawing.Point(13, 13);
			this.paletteXml.Multiline = true;
			this.paletteXml.Name = "paletteXml";
			this.paletteXml.Size = new System.Drawing.Size(538, 283);
			this.paletteXml.TabIndex = 0;
			// 
			// loadPallete
			// 
			this.loadPallete.Location = new System.Drawing.Point(13, 302);
			this.loadPallete.Name = "loadPallete";
			this.loadPallete.Size = new System.Drawing.Size(63, 23);
			this.loadPallete.TabIndex = 1;
			this.loadPallete.Text = "Load";
			this.loadPallete.UseVisualStyleBackColor = true;
			this.loadPallete.Click += new System.EventHandler(this.loadPallete_Click);
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(445, 302);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(50, 23);
			this.okButton.TabIndex = 2;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// paletteFileDialog
			// 
			this.paletteFileDialog.Filter = "XML files |*.xml";
			this.paletteFileDialog.Title = "Please select a palette to import.";
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(501, 302);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(50, 23);
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// CustomPaletteXmlDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(563, 339);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.loadPallete);
			this.Controls.Add(this.paletteXml);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CustomPaletteXmlDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Palette Editor";
			this.ResumeLayout(false);
			this.PerformLayout();

			_components = new System.ComponentModel.Container();
		}

		#endregion

// ReSharper disable InconsistentNaming
		private System.Windows.Forms.Button loadPallete;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.OpenFileDialog paletteFileDialog;
		private System.Windows.Forms.Button cancelButton;
		public System.Windows.Forms.TextBox paletteXml;
// ReSharper restore InconsistentNaming
	}
}