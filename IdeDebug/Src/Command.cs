﻿using System;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;
using ICSharpCode.SharpDevelop.Project;
using log4net;
using log4net.Repository.Hierarchy;
using log4net.Appender;
using log4net.Layout;

namespace ClarionAddins.IdeDebug
{
    class AutoStartCommand : AbstractCommand
    {

        static MessageViewCategory messageViewCategory = new MessageViewCategory("IDEDebug", "IDE Debug");

        public override void Run()
        {
            if (PropertyService.Get<bool>("ClarionAddins.IdeDebug.cbOutputDebugStringAppender", false) == true)
            {
                EnableLogging();
            }
            if (PropertyService.Get<bool>("ClarionAddins.IdeDebug.cbExtraDebugInfo", false) == false)
            {
                // extra debugging not enabled so lets bail out.
                return;
            }
            
            LoggingService.Debug("ClarionAddins.IdeDebug.cbExtraDebugInfo: Attaching to events");
            WorkbenchSingleton.WorkbenchCreated += new EventHandler(WorkbenchSingleton_WorkbenchCreated);
        }

        private static void EnableLogging()
        {
            Logger root;
            root = ((Hierarchy)LogManager.GetRepository()).Root;
            root.AddAppender(GetOutputDebugStringAppender());
            root.Level = log4net.Core.Level.All;
            root.Repository.Configured = true;
            LogManager.GetRepository().Threshold = LogManager.GetRepository().LevelMap["ALL"];
        }

        private static OutputDebugStringAppender GetOutputDebugStringAppender()
        {
            OutputDebugStringAppender lAppender = new OutputDebugStringAppender();
            lAppender.Name = "OutputDebugStringAppender";
            lAppender.Layout = new PatternLayout("(ClarionAddins)[%thread] %-5level- %message%newline");
            lAppender.Threshold = log4net.Core.Level.All;
            lAppender.ActivateOptions();
            return lAppender;
        }

        void WorkbenchSingleton_WorkbenchCreated(object sender, EventArgs e)
        {
            LoggingService.Debug("ClarionAddins.IdeDebug.cbExtraDebugInfo: WorkbenchCreated");
            WorkbenchSingleton.Workbench.ActiveWorkbenchWindowChanged +=new EventHandler(Workbench_ActiveWorkbenchWindowChanged);
            WorkbenchSingleton.Workbench.ViewClosed += new ViewContentEventHandler(Workbench_ViewClosed);
            WorkbenchSingleton.Workbench.ViewOpened += new ViewContentEventHandler(Workbench_ViewOpened);
            ProjectService.SolutionLoaded += new EventHandler<SolutionEventArgs>(ProjectService_SolutionLoaded);
            ProjectService.SolutionClosing += new EventHandler<SolutionCancelEventArgs>(ProjectService_SolutionClosing);
        }

        void ProjectService_SolutionClosing(object sender, SolutionCancelEventArgs e)
        {
            LoggingService.Debug("ClarionAddins.IdeDebug.cbExtraDebugInfo: SolutionClosing");
        }

        void ProjectService_SolutionLoaded(object sender, SolutionEventArgs e)
        {
            LoggingService.Debug("ClarionAddins.IdeDebug.cbExtraDebugInfo: SolutionLoaded");
            LoggingService.Debug("1  * e.Solution.Directory= " + e.Solution.Directory);

        }

        void Workbench_ViewOpened(object sender, ViewContentEventArgs e)
        {
            LoggingService.Debug("ClarionAddins.IdeDebug.cbExtraDebugInfo: ViewOpened");
            LoggingService.Debug("2  * " + e.Content.GetType().ToString());
        }

        void Workbench_ViewClosed(object sender, ViewContentEventArgs e)
        {
            LoggingService.Debug("ClarionAddins.IdeDebug.cbExtraDebugInfo: ViewClosed");
            LoggingService.Debug("3  * " + e.Content.GetType().ToString());
        }

        void Workbench_ActiveWorkbenchWindowChanged(object sender, EventArgs e)
        {
            LoggingService.Debug("ClarionAddins.IdeDebug.cbExtraDebugInfo: ActiveWorkbenchWindowChanged");
            if (WorkbenchSingleton.Workbench.ActiveContent != null)
            {
                LoggingService.Debug("4  * " + WorkbenchSingleton.Workbench.ActiveContent.GetType().ToString());
                //if (WorkbenchSingleton.Workbench.ActiveContent is SoftVelocity.Generator.Editor.CommonClarionGenDesignerView)
                //{
                    //LayoutConfiguration.CurrentLayoutName = "Designer";
                //}
                //else
                //{
                    //LayoutConfiguration.CurrentLayoutName = "Default";
                //}
            }
        }

    }
}
