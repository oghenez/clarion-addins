using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.SharpDevelop.DefaultEditor.Commands;

namespace ClarionEdge.MainToolbarExtras.Folding
{
    public class CollapseAll : AbstractEditActionMenuCommand
    {
        public override ICSharpCode.TextEditor.Actions.IEditAction EditAction
        {
            get { return new CollapseAllAction(); }
        }
    }
    public class ExpandAll : AbstractEditActionMenuCommand
    {
        public override ICSharpCode.TextEditor.Actions.IEditAction EditAction
        {
            get { return new ExpandAllAction(); }
        }
    }
}
