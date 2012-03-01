using System;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Actions;
using ICSharpCode.TextEditor.Document;

namespace ClarionEdge.MainToolbarExtras.Folding
{
    public class CollapseAllAction : AbstractEditAction
    {
        public override void Execute(TextArea textArea)
        {
            foreach (FoldMarker current in textArea.Document.FoldingManager.FoldMarker)
            {
                current.IsFolded = true;
            }
            textArea.Document.FoldingManager.NotifyFoldingsChanged(EventArgs.Empty);
        }
    }

    public class ExpandAllAction : AbstractEditAction
    {
        public override void Execute(TextArea textArea)
        {
            foreach (FoldMarker current in textArea.Document.FoldingManager.FoldMarker)
            {
                current.IsFolded = false;
            }
            textArea.Document.FoldingManager.NotifyFoldingsChanged(EventArgs.Empty);
        }
    }
}
