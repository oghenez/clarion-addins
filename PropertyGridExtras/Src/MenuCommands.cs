using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Commands;

namespace ClarionEdge.PropertyGridExtras
{
    class ShowOptions : AbstractMenuCommand
    {
        public override void Run()
        {
            PropertyGridHelper.ShowOptionsDialog();
        }
    }
}
