using System;
using ComponentFactory.Krypton.Toolkit;

namespace UserControls.Krypton.PaletteSelectors
{
	public class PaletteSelectedEventArgs : EventArgs
	{

		public PaletteSelectedEventArgs(CustomPalette customPalette)
		{
			Name = customPalette.DisplayName;
			PaletteMode = PaletteModeManager.Custom;
			CustomPalette = customPalette;
		}

		public PaletteSelectedEventArgs(string name, PaletteModeManager mode)
		{
			Name = name;
			PaletteMode = mode;
			CustomPalette = null;
		}
        
		public PaletteModeManager PaletteMode;
		public CustomPalette CustomPalette;
		public string Name;
	}
}
