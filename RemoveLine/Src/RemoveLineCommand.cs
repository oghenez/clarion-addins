using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.DefaultEditor.Gui.Editor;
using ICSharpCode.SharpDevelop.Gui;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using SoftVelocity.Generator.Editor;

namespace ClarionEdge.RemoveLine
{
    public class RemoveLineCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            IWorkbenchWindow window = WorkbenchSingleton.Workbench.ActiveWorkbenchWindow;

            if (window == null || !(window.ViewContent is ITextEditorControlProvider))
            {
                return;
            }

            TextEditorControl textArea = ((ITextEditorControlProvider)window.ViewContent).TextEditorControl;
            PweeLineManager customLineManager = textArea.Document.CustomLineManager as PweeLineManager;
            if (customLineManager != null)
            {
                RemovePweeLine(textArea, customLineManager);
            }
            else
            {
                new ICSharpCode.TextEditor.Actions.DeleteLine().Execute(textArea.ActiveTextAreaControl.TextArea);
            }
        }

        private void RemovePweeLine(TextEditorControl textArea, PweeLineManager customLineManager)
        {
            int lineNumber = textArea.Document.GetLineNumberForOffset(textArea.ActiveTextAreaControl.Caret.Offset);
            if (customLineManager.IsReadOnly(lineNumber, false) == false)
            {
                //new ICSharpCode.TextEditor.Actions.DeleteToLineEnd().Execute(textArea.ActiveTextAreaControl.TextArea);
                TextAreaControl textControl = textArea.ActiveTextAreaControl;
                Caret caret = textControl.Caret;
                IDocument doc = textControl.Document;
                int line = caret.Line;
                LineSegment lineSegment = doc.GetLineSegment(line);

                textArea.BeginUpdate();
                doc.UndoStack.StartUndoGroup();

                // Remove the whole line where the caret currently is
                doc.Remove(lineSegment.Offset, lineSegment.Length);

                // move the caret to the beginning of the line
                caret.Position = doc.OffsetToPosition(lineSegment.Offset);
                doc.UndoStack.EndUndoGroup();
                textArea.EndUpdate();

                // Now try to remove the line itself but be careful of the read-only embed sections!
                if (customLineManager.IsReadOnly(lineNumber + 1, false) == true && customLineManager.IsReadOnly(lineNumber - 1, false) == false)
                {
                    // The line below this one is read only.and the line above isn't
                    // That means we have just deleted the last line of an embed point
                    new ICSharpCode.TextEditor.Actions.Backspace().Execute(textArea.ActiveTextAreaControl.TextArea);
                    new ICSharpCode.TextEditor.Actions.Home().Execute(textArea.ActiveTextAreaControl.TextArea);
                }
                else if (customLineManager.IsReadOnly(lineNumber + 1, false) == false)
                {
                    // The line below isn't read only
                    new ICSharpCode.TextEditor.Actions.Delete().Execute(textArea.ActiveTextAreaControl.TextArea);
                }

                //doc.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.WholeTextArea));
                //doc.CommitUpdate();
                textArea.Refresh();
            }
        }

    }
}
