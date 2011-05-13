using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.DefaultEditor.Gui.Editor;
using ICSharpCode.SharpDevelop.Gui;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

namespace ClarionEdge.ClarionEditorContextHelp
{
    public class ShowClarionEditorContextHelp : AbstractMenuCommand
    {
        public override void Run()
        {
            FileInfo info;
            string fullHelpFileName = "ClarionHelp.chm";
            string keyword = GetContextKeyword(WorkbenchSingleton.Workbench);

            info = new FileInfo(Assembly.GetEntryAssembly().Location);
            string path = Path.Combine(info.DirectoryName, fullHelpFileName);

            if (File.Exists(path))
            {
                // If the keyword is null or String.Empty on the very FIRST time this is called then
                // any subsequent call with a keyword that triggers "Topics Found" popup to appear
                // will cause the hh.exe to HANG. Because it has WorkbenchSingleton.helpHost as a parent it will 
                // also hang the IDE!
                if (keyword == "" || keyword == String.Empty)
                {
                    WorkbenchSingleton.SafeThreadAsyncCall(
                        Help.ShowHelp, WorkbenchSingleton.helpHost, path, HelpNavigator.KeywordIndex, "Special Characters");
                }
                else
                {
                    WorkbenchSingleton.SafeThreadAsyncCall(
                        Help.ShowHelp, WorkbenchSingleton.helpHost, path, HelpNavigator.KeywordIndex, keyword);
                }
            }
            else
            {
                MessageService.ShowWarning("${res:MainWindow.Windows.HtmlHelp.NotFound} " + path);
            }
        }

        private string GetContextKeyword(IWorkbench iWorkbench)
        {

            ITextEditorControlProvider tecp = WorkbenchSingleton.Workbench.ActiveContent as ITextEditorControlProvider;
            if (tecp == null)
            {
                return String.Empty;
            }

            TextArea textArea = tecp.TextEditorControl.ActiveTextAreaControl.TextArea;
            if (textArea.SelectionManager.HasSomethingSelected == true)
            {
                return textArea.SelectionManager.SelectedText;
            }
            else
            {
                TextWord word = textArea.Document.GetLineSegment(textArea.Caret.Line).GetWord(textArea.Caret.Column);
                if (word != null)
                {
                    return word.Word;
                }
            }
            
            return String.Empty;
        }
    }
}
