using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;

namespace ClarionEdge.OptionsAutoSave
{
    class OptionsCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            using (TreeViewOptions optionsDialog = new TreeViewOptions((Properties)PropertyService.Get("ICSharpCode.TextEditor.Document.Document.DefaultDocumentAggregatorProperties", new Properties()),
            AddInTree.GetTreeNode("/SharpDevelop/Dialogs/OptionsDialog")))
            {
                optionsDialog.FormBorderStyle = FormBorderStyle.FixedDialog;

                optionsDialog.Owner = (Form)WorkbenchSingleton.Workbench;
                optionsDialog.ShowDialog(ICSharpCode.SharpDevelop.Gui.WorkbenchSingleton.MainForm);
                if (optionsDialog.DialogResult == DialogResult.OK)
                {
                    PropertyService.Save();
                }
            }
        }
    }
}
