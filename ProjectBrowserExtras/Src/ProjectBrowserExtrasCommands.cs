using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Project;

namespace ClarionEdge.ProjectBrowserExtras
{
    public class CollapseTree : AbstractMenuCommand
    {
        public override void Run()
        {
            TreeNode node = ProjectBrowserPad.Instance.ProjectBrowserControl.RootNode;
            if (node != null)
            {
                foreach (TreeNode n in node.Nodes)
                {
                    node.Collapse(false);
                }
                node.Nodes[0].Expand();
            }
        }
    }

    public class ExpandTree : AbstractMenuCommand
    {
        public override void Run()
        {
            TreeNode node = ProjectBrowserPad.Instance.ProjectBrowserControl.RootNode;
            if (node != null)
            {
                foreach (TreeNode n in node.Nodes)
                {
                    node.Expand();
                }
                node.Nodes[0].Expand();
            }
        }
    }
}
