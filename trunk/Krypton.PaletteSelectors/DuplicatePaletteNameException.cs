using System;

namespace UserControls.Krypton.PaletteSelectors
{
	[Serializable]
	public class DuplicatePaletteNameException : Exception
	{
		public DuplicatePaletteNameException() { }
		public DuplicatePaletteNameException(string s) : base(s) { }
	}
}