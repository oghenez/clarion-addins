using System;
using System.ComponentModel.Design;

namespace UserControls.Krypton.PaletteSelectors
{
	public class CustomPaletteCollectionEditor : CollectionEditor
	{
		public CustomPaletteCollectionEditor(Type type)
			: base(type)
		{}

		protected override bool CanSelectMultipleInstances()
		{
			return false;
		}

		protected override Type CreateCollectionItemType()
		{
			return typeof(CustomPalette);
		}

		protected override string GetDisplayText(object value)
		{
			CustomPalette palette = (CustomPalette)value;
			return base.GetDisplayText(palette.DisplayName);
		}
	}
}