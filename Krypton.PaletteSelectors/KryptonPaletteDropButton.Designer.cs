using ComponentFactory.Krypton.Toolkit;

namespace UserControls.Krypton.PaletteSelectors
{
	public partial class KryptonPaletteDropButton
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			_components = new System.ComponentModel.Container();
			this.OverrideFocus.Content.DrawFocus = InheritBool.False;
			this.Values.Image = Properties.Resources.palette;
			this.Values.Text = "Palette";
		}

		#endregion
	}
}
