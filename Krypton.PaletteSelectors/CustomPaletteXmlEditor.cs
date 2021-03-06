﻿using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace UserControls.Krypton.PaletteSelectors
{
	class CustomPaletteXmlEditor : UITypeEditor
	{
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			IWindowsFormsEditorService svc = (IWindowsFormsEditorService)
			                                 provider.GetService(typeof(IWindowsFormsEditorService));
			if (svc != null)
			{
				var paletteDialog = new CustomPaletteXmlDialog();
				if (value != null && typeof(string) == value.GetType())
				{
					paletteDialog.paletteXml.Text = (string) value;
				}
				svc.ShowDialog(paletteDialog);
				if (paletteDialog.DialogResult == DialogResult.OK) value = paletteDialog.paletteXml.Text;
			}
			return value;
		}

	}
}