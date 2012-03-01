using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.BrowserDisplayBinding;
using ICSharpCode.SharpDevelop.Gui;
using System;

namespace ClarionEdge.MainToolbarExtras
{
    class StartPageCommands
    {
        internal static void Refresh()
        {
            // Only attempt to refresh the start page on these conditions. 
            // If CloseStartPageOnSolutionOpening == true then there is no point in trying to refresh it because it will not be there!
            if (PropertyService.Get<bool>("SharpDevelop.CloseStartPageOnSolutionOpening", true) == true ||
                PropertyService.Get<bool>("ClarionEdge.MainToolbarExtras.AutoRefreshStartPage", true) == false)
                return;

            // Find the StartPage view and refresh it!
            foreach (IViewContent current in WorkbenchSingleton.Workbench.ViewContentCollection)
            {
                BrowserPane browserPane = current as BrowserPane;
                if (browserPane != null && browserPane.Url.Scheme == "startpage")
                {
                    browserPane.Load("startpage://project/");
                    LoggingService.Debug("attempting to refresh startpage!");
                }
            }
        }

        internal static void ReOpen()
        {
            LoggingService.Debug("ClarionEdge.MainToolbarExtras, ProjectService_SolutionClosed");

            if (WorkbenchSingleton.Workbench.ViewContentCollection.Count == 0)
            {
                // For some reason there was an exception if I tried to combine these two if statements with &&
                // go figure, this works ok for now it seems!
                if (Convert.ToBoolean(PropertyService.Get("ClarionEdge.MainToolbarExtras.ReOpenStartPage", "true")) == true)
                {
                    ToolbarHelper.RunAbstractCommand("/SharpDevelop/Workbench/ToolBar/Standard", "StartPage");
                }
            }
        }

    }
}
