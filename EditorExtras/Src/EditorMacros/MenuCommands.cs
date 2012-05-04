using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Gui;

namespace ClarionAddins.EditorMacros
{
    public class PlayCurrentMacro : AbstractMenuCommand
    {
        public override void Run()
        {
            PadDescriptor index = WorkbenchSingleton.Workbench.GetPad(typeof(EditorMacrosPad));
            if (index != null)
            {
                index.BringPadToFront();
                ((EditorMacrosPad)index.PadContent).PlayCurrentMacro();
            }

        }
    }
}
