﻿using System.Collections;

namespace UserControls.Krypton.PaletteSelectors
{
	public class CustomPaletteCollection: CollectionBase
	{
		public CustomPalette this[int index]
		{
			get { return (CustomPalette)List[index]; }
		}

		public void Add(CustomPalette palette)
		{
			List.Add(palette);
		}

		public void Remove(CustomPalette palette)
		{
			List.Remove(palette);
		}
	}
}