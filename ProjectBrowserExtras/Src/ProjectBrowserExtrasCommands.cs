using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Project;
using System;

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

    public class FindNodeStartsWith : AbstractTextBoxCommand
    {
        enum SearchMode
        {
            StartsWith,
            Contains
        };

        private bool isEnabled = true;
        public override bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                this.isEnabled = value;
            }
        }

        public override void Run()
        {
            string searchTerm = ((ToolBarTextBox)this.Owner).TextBox.Text;
            TreeNode fileNode = FindNode(searchTerm, ProjectBrowserPad.Instance.ProjectBrowserControl.RootNode, SearchMode.StartsWith);
            if (fileNode == null)
            { 
                fileNode = FindNode(searchTerm, ProjectBrowserPad.Instance.ProjectBrowserControl.RootNode, SearchMode.Contains); 
            }

            if (fileNode != null)
            {
                ProjectBrowserPad.Instance.ProjectBrowserControl.TreeView.SelectedNode = fileNode;
                ProjectBrowserPad.Instance.ProjectBrowserControl.TreeView.Select();
            }
        }

        private TreeNode FindNode(string searchTerm, TreeNode node, SearchMode searchMode)
        {
            if (node == null)
                return null;

            if (searchMode == SearchMode.StartsWith)
            {
                if (node.Text.StartsWith(searchTerm, StringComparison.CurrentCultureIgnoreCase))
                    return node;
            }
            else if(searchMode == SearchMode.Contains)
            {
                if (node.Text.ToLower().Contains(searchTerm.ToLower()))
                    return node;
            }

            foreach (TreeNode n in node.Nodes)
            {
                TreeNode fileNode = FindNode(searchTerm, n, searchMode);
                if (fileNode != null)
                    return fileNode;
            }
            return null;
        }

    }

    public class ExpandTree : AbstractMenuCommand
    {
        public override void Run()
        {
            ExpandRecursive(ProjectBrowserPad.Instance.ProjectBrowserControl.RootNode);
        }

        private void ExpandRecursive(TreeNode node)
        {
            if (node == null)
                return;

            node.Expand();
            foreach (TreeNode n in node.Nodes)
            {
                ExpandRecursive(n);
            }
        }
    }
}
