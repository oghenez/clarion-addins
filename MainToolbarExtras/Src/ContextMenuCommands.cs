using System;
using System.Threading;
using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;
using MouseKeyboardLibrary;
using SoftVelocity.Generator.UI;

namespace ClarionEdge.MainToolbarExtras
{

    class SetToolbarSizeCommand : AbstractCommand
    {
        public override void Run()
        {
            Form setToolbarSize = new SetToolbarSize();
            if (setToolbarSize.ShowDialog(WorkbenchSingleton.MainForm) == DialogResult.OK)
            {
                ToolbarHelper.UpdateMainToolbarSize();
            }
            setToolbarSize.Dispose();
        }
    }

    class DisableSetToolbarSize : AbstractCheckableMenuCommand
    {
        public override bool IsChecked
        {
            get
            {
                return Convert.ToBoolean(PropertyService.Get("ClarionEdge.MainToolbarExtras.DisableSetToolbarSize", "false"));
            }
            set
            {
                PropertyService.Set("ClarionEdge.MainToolbarExtras.DisableSetToolbarSize", Convert.ToString(value));
            }
        }
    }

    class UpdateOtherToolbars : AbstractCheckableMenuCommand
    {
        public override bool IsChecked
        {
            get
            {
                return Convert.ToBoolean(PropertyService.Get("ClarionEdge.MainToolbarExtras.updateOtherToolbars", "false"));
            }
            set
            {
                PropertyService.Set("ClarionEdge.MainToolbarExtras.updateOtherToolbars", Convert.ToString(value));
                ToolbarHelper.UpdateOtherToolbars();
            }
        }
    }

    class ShowRunStartUpProjectInDebugger : AbstractCheckableMenuCommand
    {
        public override bool IsChecked
        {
            get
            {
                return Convert.ToBoolean(PropertyService.Get("ClarionEdge.MainToolbarExtras.ShowRunStartUpProjectInDebugger", "false"));
            }
            set
            {
                PropertyService.Set("ClarionEdge.MainToolbarExtras.ShowRunStartUpProjectInDebugger", Convert.ToString(value));
            }
        }
    }

    class ShowRunExeNonElevated : AbstractCheckableMenuCommand
    {
        public override bool IsChecked
        {
            get
            {
                return Convert.ToBoolean(PropertyService.Get("ClarionEdge.MainToolbarExtras.ShowRunExeNonElevated", "true"));
            }
            set
            {
                PropertyService.Set("ClarionEdge.MainToolbarExtras.ShowRunExeNonElevated", Convert.ToString(value));
            }
        }
    }

    class ShowOpenAppGenSourceItem : AbstractCheckableMenuCommand
    {
        public override bool IsChecked
        {
            get
            {
                return Convert.ToBoolean(PropertyService.Get("ClarionEdge.MainToolbarExtras.ShowOpenAppGenSourceItem", "true"));
            }
            set
            {
                PropertyService.Set("ClarionEdge.MainToolbarExtras.ShowOpenAppGenSourceItem", Convert.ToString(value));
            }
        }
    }

    class OpenAppGenSourceCommand : AbstractCommand
    {

        public override void Run()
        {
            if (WorkbenchSingleton.Workbench.ActiveWorkbenchWindow != null &&
                WorkbenchSingleton.Workbench.ActiveWorkbenchWindow.ActiveViewContent != null &&
                WorkbenchSingleton.Workbench.ActiveWorkbenchWindow.ViewContent.GetType() == typeof(ApplicationMainWindowControl_ViewContent))
            {
                Thread.Sleep(100);
                //KeyboardSimulator.KeyUp(Keys.Alt);
                KeyboardSimulator.KeyDown(Keys.Shift);
                KeyboardSimulator.KeyPress(Keys.F10);
                KeyboardSimulator.KeyUp(Keys.Shift);
                KeyboardSimulator.KeyPress(Keys.S);
            }
        }
    }

    class ReOpenStartPage : AbstractCheckableMenuCommand
    {
        public override bool IsChecked
        {
            get
            {
                return Convert.ToBoolean(PropertyService.Get("ClarionEdge.MainToolbarExtras.ReOpenStartPage", "true"));
            }
            set
            {
                PropertyService.Set("ClarionEdge.MainToolbarExtras.ReOpenStartPage", Convert.ToString(value));
            }
        }
    }

    class AutoRefreshStartPage : AbstractCheckableMenuCommand
    {
        public override bool IsChecked
        {
            get
            {
                return Convert.ToBoolean(PropertyService.Get("ClarionEdge.MainToolbarExtras.AutoRefreshStartPage", "true"));
            }
            set
            {
                PropertyService.Set("ClarionEdge.MainToolbarExtras.AutoRefreshStartPage", Convert.ToString(value));
            }
        }
    }
}
