using System;
using System.IO;
using System.Windows.Forms;

namespace UserControls.Krypton.PaletteSelectors
{
	public partial class CustomPaletteXmlDialog : Form
	{
		public CustomPaletteXmlDialog()
		{
			InitializeComponent();
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}
		
		private void loadPallete_Click(object sender, EventArgs e)
		{
			if (paletteFileDialog.ShowDialog() != DialogResult.OK) return;
			var path = paletteFileDialog.FileName;
			paletteXml.Text = File.ReadAllText(path);
		}

		public CustomPaletteXmlDialog(string value)
		{
			if (!string.IsNullOrEmpty(value)) paletteXml.Text = value;
		}



	}
}