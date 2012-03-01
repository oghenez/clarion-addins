using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;
using ICSharpCode.SharpDevelop.DefaultEditor.Gui.Editor;
using ICSharpCode.TextEditor;

namespace ClarionEdge.MainToolbarExtras.EditorFontSize
{
    class Reset : AbstractMenuCommand
    {
        public override void Run()
        {
            IWorkbenchWindow window = WorkbenchSingleton.Workbench.ActiveWorkbenchWindow;

            if (window == null || !(window.ViewContent is ITextEditorControlProvider))
            {
                return;
            }

            TextEditorControl textArea = ((ITextEditorControlProvider)window.ViewContent).TextEditorControl;
            float defaultSize = PropertyService.Get<float>("ClarionEdge.MainToolbarExtras.StartUpFontSize", 9.0f);
            textArea.TextEditorProperties.Font = new System.Drawing.Font(textArea.TextEditorProperties.Font.Name, defaultSize);
            textArea.Refresh();
        }
    }

    class Increase : AbstractMenuCommand
    {
        public override void Run()
        {
            IWorkbenchWindow window = WorkbenchSingleton.Workbench.ActiveWorkbenchWindow;

            if (window == null || !(window.ViewContent is ITextEditorControlProvider))
            {
                return;
            }
            TextEditorControl textArea = ((ITextEditorControlProvider)window.ViewContent).TextEditorControl;
            textArea.TextEditorProperties.Font = new System.Drawing.Font(textArea.TextEditorProperties.Font.Name, textArea.TextEditorProperties.Font.Size+1.0f);
            textArea.Refresh();
        }
    }

    class Decrease : AbstractMenuCommand
    {
        public override void Run()
        {
            IWorkbenchWindow window = WorkbenchSingleton.Workbench.ActiveWorkbenchWindow;

            if (window == null || !(window.ViewContent is ITextEditorControlProvider))
            {
                return;
            }

            TextEditorControl textArea = ((ITextEditorControlProvider)window.ViewContent).TextEditorControl;
            textArea.TextEditorProperties.Font = new System.Drawing.Font(textArea.TextEditorProperties.Font.Name, textArea.TextEditorProperties.Font.Size - 1.0f);
            textArea.Refresh();
        }
    }
}
