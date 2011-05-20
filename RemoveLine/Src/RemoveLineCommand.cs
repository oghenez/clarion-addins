using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.DefaultEditor.Gui.Editor;
using ICSharpCode.SharpDevelop.Gui;
using ICSharpCode.TextEditor;
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

            TextEditorControl textarea = ((ITextEditorControlProvider)window.ViewContent).TextEditorControl;
            PweeLineManager customLineManager = textarea.Document.CustomLineManager as PweeLineManager;
            int lineNumber = textarea.Document.GetLineNumberForOffset(textarea.ActiveTextAreaControl.Caret.Offset);
            if (customLineManager.IsReadOnly(lineNumber, false) == false)
            {
                new ICSharpCode.TextEditor.Actions.DeleteLine().Execute(textarea.ActiveTextAreaControl.TextArea);
                textarea.Refresh();
            }
        }
    }
}
