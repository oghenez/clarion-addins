using System;
using System.Collections.Generic;
using ZetaColorEditor.Runtime;

namespace ClarionEdge.InsertClarionColor
{
    class ClarionColorProvider : IExternalColorEditorInformationProvider
    {
        public ZetaColorEditor.Runtime.ColorSchemes.IColorScheme[] ColorSchemes
        {
            get { throw new NotImplementedException(); }
        }

        public bool AllowNoColorSelectable
        {
            get { throw new NotImplementedException(); }
        }

        public void FormatDisplayText(System.Drawing.Color color, ref string displayText)
        {
            throw new NotImplementedException();
        }

        public void AdjustColorSettingLookupOrder(IList<ColorLookupElement> lookupOrder)
        {
            throw new NotImplementedException();
        }

        public void SavePerUserPerWorkstationValue(string name, string value)
        {
            throw new NotImplementedException();
        }

        public string RestorePerUserPerWorkstationValue(string name, string fallBackTo)
        {
            throw new NotImplementedException();
        }
    }
}
