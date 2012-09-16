using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.DefaultEditor.Gui.Editor;
using ICSharpCode.SharpDevelop.Gui;
using ICSharpCode.TextEditor;
using SearchAndReplace;
using SoftVelocity.Generator.Editor;
using System.Drawing;

namespace ClarionEdge.SearchPadExtras
{
    public static class SearchModeOptions
    {
        public const string STR_StandardSearch = "ClarionEdge.SearchPadExtras.StandardSearch";
        public const string STR_EmbedSearch = "ClarionEdge.SearchPadExtras.EmbedSearch";
        public const string STR_GeneratedSearch = "ClarionEdge.SearchPadExtras.GeneratedSearch";

        public static bool GetSearchOption(string searchName)
        {
            if (searchName==STR_StandardSearch)
                return PropertyService.Get<bool>(searchName, true);
            else
                return PropertyService.Get<bool>(searchName, false);
        }

        public static void SetSearchOptions(string searchName, bool value)
        {
            if (value == false && GetSearchOption(searchName) == true)
            {
                // don't do anything', we are doing a fake "option" control not actual check boxes
                return;
            }

            // Clear the existing values
            PropertyService.Set<bool>(STR_StandardSearch, false);
            PropertyService.Set<bool>(STR_EmbedSearch, false);
            PropertyService.Set<bool>(STR_GeneratedSearch, false);
            // And set the new one
            PropertyService.Set<bool>(searchName, value);
        }

        public static void MyFindNext()
        {
            if (GetSearchOption(STR_StandardSearch))
            {
                SearchReplaceManager.FindNext(null);
                return;
            }

            // Try one of the special methods
            // 1. Find current position
            // 2. Loop around and see if we get a hit
            //  * if the position goes back past the original then stop!

            int originalOffset = GetCurrentOffset();
            if (originalOffset == -1)
                return;

            int firstMatchOffset = -1;
            while (true)
            {
                SearchReplaceManager.FindNext(null);
                if (originalOffset == GetCurrentOffset())
                {
                    // very likely nothing was found as the caret did not move OR we cycled around to the top!
                    if (firstMatchOffset != -1)
                        NotFoundMessage();

                    return; 
                }
                // if an area of text is selected then a match has been found AND meets our special search mode criteria then we are done!
                if (SelectedText().Equals(SearchOptions.FindPattern, StringComparison.CurrentCultureIgnoreCase) && SearchModeMatches() == true)
                    return;

                // if it doesn't then try to search again but make sure not to go into endless loop
                if (firstMatchOffset != -1 && firstMatchOffset == GetCurrentOffset())
                {
                    NotFoundMessage();
                    return;
                }

                if (firstMatchOffset == -1)
                    firstMatchOffset = GetCurrentOffset();
            }
        }

        private static void NotFoundMessage()
        {
            MessageBox.Show((Form)WorkbenchSingleton.Workbench, ResourceService.GetString("Dialog.NewProject.SearchReplace.SearchStringNotFound"), ResourceService.GetString("Dialog.NewProject.SearchReplace.SearchStringNotFound.Title"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private static bool SearchModeMatches()
        {
            if (SearchModeOptions.GetSearchOption(SearchModeOptions.STR_EmbedSearch) == true && ResultIsGenerated() == false)
                return true;
            else if (SearchModeOptions.GetSearchOption(SearchModeOptions.STR_GeneratedSearch) == true && ResultIsGenerated() == true)
                return true;

            // otherwise, not a valud match!
            return false;
        }

        private static bool ResultIsGenerated()
        {
            IWorkbenchWindow window = WorkbenchSingleton.Workbench.ActiveWorkbenchWindow;

            if (window == null || !(window.ViewContent is ITextEditorControlProvider))
            {
                return false;
            }

            TextEditorControl textArea = ((ITextEditorControlProvider)window.ViewContent).TextEditorControl;
            PweeLineManager customLineManager = textArea.Document.CustomLineManager as PweeLineManager;
            if (customLineManager != null)
            {
                int lineNumber = textArea.Document.GetLineNumberForOffset(textArea.ActiveTextAreaControl.Caret.Offset);
                return customLineManager.IsReadOnly(lineNumber, false);
            }
            return false;
        }

        private static string SelectedText()
        {
            TextEditorControl te = SearchReplaceUtilities.GetActiveTextEditor();
            if (te != null && te.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected)
            {
                return te.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;
            }
            return "";
        }

        private static int GetCurrentOffset()
        {
            TextEditorControl textArea = SearchReplaceUtilities.GetActiveTextEditor();
            if (textArea == null)
                return -1;

            PweeLineManager customLineManager = textArea.Document.CustomLineManager as PweeLineManager;
            if (customLineManager != null)
            {
                return textArea.ActiveTextAreaControl.Caret.Offset;
            }

            return -1;
        }


    }

    public class QuickFindOptions : AbstractMenuCommand
    {
        private ToolBarDropDownButton dropDownButton;

        public override void Run()
        {
        }

        protected override void OnOwnerChanged(EventArgs e)
        {
            base.OnOwnerChanged(e);

            this.dropDownButton = (ToolBarDropDownButton)this.Owner;
            this.dropDownButton.DropDownOpening += new EventHandler(dropDownButton_DropDownOpening);
            this.dropDownButton.DropDown.RenderMode = ToolStripRenderMode.System;

            ToolStripItem[] array = (ToolStripItem[])AddInTree.GetTreeNode("/SoftVelocity/Clarion/ToolBar/EmbedEditor/QuickFindOptions").BuildChildItems(this).ToArray(typeof(ToolStripItem));
            //ToolStripItem[] array = (ToolStripItem[])AddInTree.GetTreeNode("/SharpDevelop/Workbench/ToolBar/Standard/QuickFindOptions").BuildChildItems(this).ToArray(typeof(ToolStripItem));
            foreach (ToolStripItem item in array)
            {
                item.ImageScaling = ToolStripItemImageScaling.None;
                if (item is IStatusUpdate)
                {
                    ((IStatusUpdate)item).UpdateStatus();
                }
            }

            this.dropDownButton.DropDownItems.AddRange(array);
        }

        void dropDownButton_DropDownOpening(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in this.dropDownButton.DropDownItems)
            {
                if (item is IStatusUpdate)
                {
                    ((IStatusUpdate)item).UpdateStatus();
                }
            }
        }
    }

    class StandardSearch : AbstractCheckableMenuCommand
    {
        public override bool IsChecked
        {
            get { return SearchModeOptions.GetSearchOption(SearchModeOptions.STR_StandardSearch); }
            set { SearchModeOptions.SetSearchOptions(SearchModeOptions.STR_StandardSearch, value); }
        }

    }

    class EmbedSearch : AbstractCheckableMenuCommand
    {
        public override bool IsChecked
        {
            get { return SearchModeOptions.GetSearchOption(SearchModeOptions.STR_EmbedSearch); }
            set { SearchModeOptions.SetSearchOptions(SearchModeOptions.STR_EmbedSearch, value); }
        }
    }

    class GeneratedSearch : AbstractCheckableMenuCommand
    {
        public override bool IsChecked
        {
            get { return SearchModeOptions.GetSearchOption(SearchModeOptions.STR_GeneratedSearch); }
            set { SearchModeOptions.SetSearchOptions(SearchModeOptions.STR_GeneratedSearch, value); }
        }
    }

    public class CopyCommand : AbstractMenuCommand
    {

        string curPattern = null;
        List<SearchResult> curResults = null;
        ExtTreeView resultTreeView = new ExtTreeView();

        public override void Run()
        {
            StringBuilder b = new StringBuilder();
            resultTreeView.Clear();
            foreach (SearchAllFinishedEventArgs args in SearchInFilesManager.LastSearches)
            {
                curPattern = args.Pattern;
                curResults = args.Results;
                ShowSearchResultsPerFile();
                foreach (SearchRootNode rnode in resultTreeView.Nodes)
                {
                    b.AppendLine(rnode.Text);
                    foreach (SearchFolderNode fnode in rnode.Nodes)
                    {
                        b.AppendLine("  " + fnode.Text);
                        foreach (SearchResultNode srnode in fnode.Nodes)
                        {
                            b.AppendLine("    " + srnode.Text);
                            foreach (TreeNode tn in srnode.Nodes)
                            {
                                b.AppendLine("tn: " + tn.Text);
                            }
                        }
                    }
                }
                break;
            }

            ClipboardWrapper.SetText(b.ToString());

        }

        void ShowSearchResultsPerFile()
        {
            Dictionary<string, SearchFolderNode> folderNodes = new Dictionary<string, SearchFolderNode>();
            foreach (SearchResult result in curResults)
            {
                if (!folderNodes.ContainsKey(result.FileName))
                {
                    folderNodes[result.FileName] = new SearchFolderNode(result.FileName);
                }
                folderNodes[result.FileName].Results.Add(result);
            }

            SearchRootNode searchRootNode = new SearchRootNode(curPattern, curResults, folderNodes.Count);
            foreach (SearchFolderNode folderNode in folderNodes.Values)
            {
                folderNode.SetText();
                folderNode.PerformInitialization();
                searchRootNode.Nodes.Add(folderNode);
            }

            resultTreeView.Nodes.Add(searchRootNode);
            searchRootNode.Expand();
        }
    }

}
