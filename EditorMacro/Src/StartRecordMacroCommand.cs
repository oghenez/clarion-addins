using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.DefaultEditor.Gui.Editor;
using ICSharpCode.SharpDevelop.Gui;
using ICSharpCode.TextEditor;
using MouseKeyboardLibrary;

namespace ClarionEdge.EditorMacros
{

    public class StartRecordMacroCommand : AbstractMenuCommand
    {

        List<MacroEvent> events = new List<MacroEvent>();
        int lastTimeRecorded = 0;
        KeyboardHook keyboardHook = new KeyboardHook();

        ICSharpCode.Core.Properties macroProperty = PropertyService.Get("ClarionEdge.EditorMacros.macroProperty", new ICSharpCode.Core.Properties());
        //private int count = 0;

        public override void Run()
        {
          
            ITextEditorControlProvider tecp = WorkbenchSingleton.Workbench.ActiveContent as ITextEditorControlProvider;
            if (tecp == null) return;
            
            TextArea textArea = tecp.TextEditorControl.ActiveTextAreaControl.TextArea;
            textArea.KeyDown += new System.Windows.Forms.KeyEventHandler(HandleKeyDown);
            textArea.KeyUp += new System.Windows.Forms.KeyEventHandler(HandleKeyUp);

        }

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            events.Add(
                new MacroEvent(
                    MacroEventType.KeyDown,
                    e,
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;

        }

        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            events.Add(
                new MacroEvent(
                    MacroEventType.KeyUp,
                    e,
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;
        }

    }

    public class PlayMacroCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            ICSharpCode.Core.Properties macroProperty = PropertyService.Get("ClarionEdge.EditorMacros.macroProperty", new ICSharpCode.Core.Properties());

            IWorkbenchWindow window = WorkbenchSingleton.Workbench.ActiveWorkbenchWindow;

            if (window == null || !(window.ViewContent is ITextEditorControlProvider))
            {
                return;
            }
            TextEditorControl textArea = ((ITextEditorControlProvider)window.ViewContent).TextEditorControl;

            int i = 1;

            while (true)
            {
                char ch = macroProperty.Get(i.ToString(), "")[0];
                if (ch.ToString() == "")
                {
                    textArea.ActiveTextAreaControl.Caret.Column += i;
                    break;
                }
                LoggingService.Debug("ch: " + ch);
                if (ch == (char)13)
                {
                    textArea.Document.Insert(textArea.ActiveTextAreaControl.Caret.Offset + (i - 1), "\n");
                    textArea.ActiveTextAreaControl.Caret.Line += 1;
                    LoggingService.Debug("char=13");
                }
                else if (ch == (char)10)
                {
                    textArea.Document.Insert(textArea.ActiveTextAreaControl.Caret.Offset + (i - 1), "\n");
                    textArea.ActiveTextAreaControl.Caret.Line += 1;
                    LoggingService.Debug("char=10");
                }
                else
                {
                    textArea.Document.Insert(textArea.ActiveTextAreaControl.Caret.Offset + (i - 1), ch.ToString());
                }
                i++;
                

            }

        }
    }
}
