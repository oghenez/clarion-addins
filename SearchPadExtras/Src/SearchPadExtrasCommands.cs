using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;
using SearchAndReplace;
using System;

namespace ClarionEdge.SearchPadExtras
{


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
