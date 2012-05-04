using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.DefaultEditor.Gui.Editor;
using ICSharpCode.SharpDevelop.Gui;
using MouseKeyboardLibrary;

namespace ClarionAddins.EditorMacros
{
    public partial class EditorMacrosPadControl : UserControl
    {
        private MacroRecording _macro;

        public EditorMacrosPadControl()
        {
            LoggingService.Debug("EditorMacrosPadControl Constructor");

            InitializeComponent();
            cbDisableCCWhileRecording.Checked = PropertyService.Get<bool>("ClarionEdge.EditorMacros.cbDisableCCWhileRecording", false);
            _macro = new MacroRecording();
            listBoxDebug.DataSource = _macro.Events;
            listBoxDebug.DisplayMember = "DisplayString";
            if (_macro.Events.Count == 0)
            {
                buttonPlayBack.Enabled = false;
            }
        }

        public void PlayCurrentMacro()
        {
            buttonPlayBack.PerformClick();
        }

        public void GainFocus()
        {
            if (buttonPlayBack.Enabled == true)
            {
                buttonPlayBack.Select();
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (buttonStartRecording.Text == "Stop")
            {
                buttonStartRecording.Image = ClarionAddins.Properties.Resources.record;
                buttonStartRecording.Text = "Start";
                _macro.StopRecording();
                if (_macro.Events.Count > 0)
                    buttonPlayBack.Enabled = true;
            }
            else
            {
                IWorkbenchWindow window = WorkbenchSingleton.Workbench.ActiveWorkbenchWindow;
                if (window == null || !(window.ViewContent is ITextEditorControlProvider))
                {
                    MessageBox.Show("Macros can only be recorded when there is an editor window open!");
                    return;
                }
                buttonStartRecording.Image = ClarionAddins.Properties.Resources.stop;
                buttonStartRecording.Text = "Stop";
                buttonPlayBack.Enabled = false;
                WorkbenchSingleton.Workbench.ActiveWorkbenchWindow.SelectWindow();
                _macro.StartRecording();
            }
        }

        private void buttonStartPlayback_Click(object sender, EventArgs e)
        {
            listBoxDebug.Select();
            _macro.Play();
        }

        private void listBoxDebug_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int indexOfSelection = listBoxDebug.IndexFromPoint(e.X, e.Y);
                if (indexOfSelection != ListBox.NoMatches)
                {
                    listBoxDebug.SelectedIndex = indexOfSelection;
                }
            }
        }

        private void listBoxDebug_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int indexOfSelection = listBoxDebug.IndexFromPoint(e.X, e.Y);
                if (indexOfSelection != ListBox.NoMatches)
                {
                    listBoxDebug.SelectedIndex = indexOfSelection;
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LoggingService.Debug("remove clicked");
            try
            {
                if (listBoxDebug.SelectedIndex >= 0)
                {
                    _macro.Events.RemoveAt(listBoxDebug.SelectedIndex);
                    listBoxDebug.ClearSelected();
                }
            }
            catch (Exception ex)
            {
                LoggingService.Debug("exception=" + ex.Message);
            }
        }

        private void cbDisableCCWhileRecording_CheckedChanged(object sender, EventArgs e)
        {
            PropertyService.Set<bool>("ClarionEdge.EditorMacros.cbDisableCCWhileRecording", cbDisableCCWhileRecording.Checked);
        }
    }



}
