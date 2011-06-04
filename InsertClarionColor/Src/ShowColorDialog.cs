using System.IO;
using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.DefaultEditor.Gui.Editor;
using ICSharpCode.SharpDevelop.Gui;
using ICSharpCode.TextEditor;
using ZetaColorEditor.Runtime;

namespace ClarionEdge.InsertClarionColor
{
    public class ShowColorDialog : AbstractMenuCommand
    {
        public override void Run()
        {
            IWorkbenchWindow window = WorkbenchSingleton.Workbench.ActiveWorkbenchWindow;

            if (window == null || !(window.ViewContent is ITextEditorControlProvider))
            {
                return;
            }
            TextEditorControl textarea = ((ITextEditorControlProvider)window.ViewContent).TextEditorControl;

            using (ColorEditorForm cd = new ColorEditorForm())
            {
                if (cd.ShowDialog(ICSharpCode.SharpDevelop.Gui.WorkbenchSingleton.MainForm) == DialogResult.OK)
                {                 
                    string ext = Path.GetExtension(textarea.FileName).ToLowerInvariant();
                    string colorstr;
                    colorstr = string.Format("00{0:X2}{1:X2}{2:X2}h", cd.SelectedColor.B, cd.SelectedColor.G, cd.SelectedColor.R);

                    textarea.Document.Insert(textarea.ActiveTextAreaControl.Caret.Offset, colorstr);
                    int lineNumber = textarea.Document.GetLineNumberForOffset(textarea.ActiveTextAreaControl.Caret.Offset);
                    textarea.ActiveTextAreaControl.Caret.Column += colorstr.Length;
                    textarea.Document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.SingleLine, new TextLocation(0, lineNumber)));
                    textarea.Document.CommitUpdate();
                }
            }
        }
    }

}
