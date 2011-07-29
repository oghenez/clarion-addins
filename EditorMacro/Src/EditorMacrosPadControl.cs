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
            buttonStopRecording.Enabled = false;
            buttonPlayBack.Enabled = false;

            listBoxDebug.DataSource = events;
            listBoxDebug.DisplayMember = "DisplayString";

            keyboardHook.KeyDown += new System.Windows.Forms.KeyEventHandler(keyboardHook_KeyDown);
            keyboardHook.KeyUp += new System.Windows.Forms.KeyEventHandler(keyboardHook_KeyUp);

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {

            IWorkbenchWindow window = WorkbenchSingleton.Workbench.ActiveWorkbenchWindow;
            if (window == null) return;
            ITextEditorControlProvider provider = window.ActiveViewContent as ITextEditorControlProvider;
            if (provider == null)
            {
                MessageBox.Show("Macros can only be recorded when there is an editor window open!");
                return;
            }

            buttonPlayBack.Enabled = false;
            buttonStopRecording.Enabled = true;
            buttonPlayBack.Enabled = false;

            textArea = provider.TextEditorControl.ActiveTextAreaControl.TextArea;
            //textArea.KeyEventHandler += new ICSharpCode.TextEditor.KeyEventHandler(textArea_KeyEventHandler);
            //textArea.KeyDown += new System.Windows.Forms.KeyEventHandler(HandleKeyDown);
            //textArea.KeyUp += new System.Windows.Forms.KeyEventHandler(HandleKeyUp);

            events.Clear();
            lastTimeRecorded = Environment.TickCount;

            keyboardHook.Start();
            WorkbenchSingleton.Workbench.ActiveWorkbenchWindow.SelectWindow();

        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            keyboardHook.Stop();
            if (events.Count > 0)
            {
                buttonPlayBack.Enabled = true;
            }

            if (textArea == null)
            {
                return;
            }

            //textArea.KeyDown -= HandleKeyDown;
            //textArea.KeyUp -= HandleKeyUp;
            buttonStartRecording.Enabled = true;
            buttonStopRecording.Enabled = false;
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
            WorkbenchSingleton.Workbench.ActiveWorkbenchWindow.SelectWindow();
            LoggingService.Debug("about to start playback");
            foreach (MacroEvent macroEvent in events)
            {
                LoggingService.Debug("event: " + macroEvent.DisplayString);
                //Thread.Sleep(1000);

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


        }
    }
}
