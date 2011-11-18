using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;

namespace ClarionAddins.KeyboardShortcuts
{
    class AutoStartCommand : AbstractCommand
    {
        public override void Run()
        {
            WorkbenchSingleton.WorkbenchCreated += new EventHandler(WorkbenchSingleton_WorkbenchCreated);
        }

        void WorkbenchSingleton_WorkbenchCreated(object sender, EventArgs e)
        {
            LoggingService.Debug("ClarionAddins.KeyboardShortcuts.AutoStart, StoreOriginalKeys");
            Helper.StoreOriginalKeys();
            Helper.ApplyShortcutKeys();
        }
    }
}
