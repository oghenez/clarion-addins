using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.SharpDevelop.Gui;
using MouseKeyboardLibrary;
using ICSharpCode.SharpDevelop.DefaultEditor.Gui.Editor;
using ICSharpCode.TextEditor;
using ICSharpCode.Core;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ICSharpCode.SharpDevelop;

namespace ClarionEdge.EditorMacros
{
    public partial class EditorMacrosPadControl : UserControl
    {
        BindingList<MacroEvent> events = new BindingList<MacroEvent>();
        int lastTimeRecorded = 0;

        KeyboardHook keyboardHook = new KeyboardHook();
        TextArea textArea;

        public EditorMacrosPadControl()
        {
            InitializeComponent();
            //headerGroup.ValuesPrimary.Image = CommonResources.Properties.famfamfam_silk.famfamfam_silk_script_edit;
            buttonPlayBack.Enabled = false;

            listBoxDebug.DataSource = events;
            listBoxDebug.DisplayMember = "DisplayString";

            keyboardHook.KeyDown += new System.Windows.Forms.KeyEventHandler(keyboardHook_KeyDown);
            keyboardHook.KeyUp += new System.Windows.Forms.KeyEventHandler(keyboardHook_KeyUp);

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (buttonStartRecording.Text == "Stop")
            {
                buttonStartRecording.Image = ClarionEdge.EditorMacros.Properties.Resources.record;
                buttonStartRecording.Text = "Start";
                keyboardHook.Stop();
                if (events.Count > 0)
                {
                    buttonPlayBack.Enabled = true;
                }
                if (textArea == null)
                {
                    return;
                }
            }
            else
            {
                buttonStartRecording.Image = ClarionEdge.EditorMacros.Properties.Resources.stop;
                buttonStartRecording.Text = "Stop";
                IWorkbenchWindow window = WorkbenchSingleton.Workbench.ActiveWorkbenchWindow;
                if (window == null) return;
                ITextEditorControlProvider provider = window.ActiveViewContent as ITextEditorControlProvider;
                if (provider == null)
                {
                    MessageBox.Show("Macros can only be recorded when there is an editor window open!");
                    return;
                }

                buttonPlayBack.Enabled = false;
                textArea = provider.TextEditorControl.ActiveTextAreaControl.TextArea;

                events.Clear();
                lastTimeRecorded = Environment.TickCount;

                keyboardHook.Start();
                WorkbenchSingleton.Workbench.ActiveWorkbenchWindow.SelectWindow();
            }
        }

        void keyboardHook_KeyDown(object sender, KeyEventArgs e)
        {

            events.Add(
                new MacroEvent(
                    MacroEventType.KeyDown,
                    e,
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;

        }

        void keyboardHook_KeyUp(object sender, KeyEventArgs e)
        {

            events.Add(
                new MacroEvent(
                    MacroEventType.KeyUp,
                    e,
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;

        }

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            LoggingService.Debug("HandleKeyDown: " + e.KeyCode.ToString());
            events.Add(
                new MacroEvent(
                    MacroEventType.KeyDown,
                    e,
                    Environment.TickCount - lastTimeRecorded
                ));
            lastTimeRecorded = Environment.TickCount;

        }

        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            events.Add(
                new MacroEvent(
                    MacroEventType.KeyUp,
                    e,
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;
        }

        private void buttonStartPlayback_Click(object sender, EventArgs e)
        {

            bool originalCC = CodeCompletionOptions.EnableCodeCompletion;
            CodeCompletionOptions.EnableCodeCompletion = false;
            WorkbenchSingleton.Workbench.ActiveWorkbenchWindow.SelectWindow();

            foreach (MacroEvent macroEvent in events)
            {
                switch (macroEvent.MacroEventType)
                {
                    case MacroEventType.KeyDown:
                        {
                            KeyEventArgs keyArgs = (KeyEventArgs)macroEvent.EventArgs;
                            KeyboardSimulator.KeyDown(keyArgs.KeyCode);
                        }
                        break;
                    case MacroEventType.KeyUp:
                        {
                            KeyEventArgs keyArgs = (KeyEventArgs)macroEvent.EventArgs;
                            KeyboardSimulator.KeyUp(keyArgs.KeyCode);
                        }
                        break;
                    default:
                        break;
                }
            }
            CodeCompletionOptions.EnableCodeCompletion = originalCC;
        }

        public void GainFocus()
        {
            if (buttonPlayBack.Enabled == true)
            {
                buttonPlayBack.Select();
            }
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
                    events.RemoveAt(listBoxDebug.SelectedIndex);
                    listBoxDebug.ClearSelected();
                }
            }
            catch (Exception ex)
            {
                LoggingService.Debug("exception=" + ex.Message);
            }
        }
    }
}
