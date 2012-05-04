using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using ICSharpCode.Core;
using MouseKeyboardLibrary;
using System.Windows.Forms;
using System.Diagnostics;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Gui;
using System.Threading;

namespace ClarionAddins.EditorMacros
{
    class MacroRecording
    {

        KeyboardHook _keyboardHook = new KeyboardHook();
        private Keys _lastKeyDownKeyCode = Keys.None;
        private int _lastTimeRecorded = 0;
        private bool _originalCC;
        private ICSharpCode.Core.Properties _properties; 
        private BindingList<MacroEvent> _events = new BindingList<MacroEvent>();

        public BindingList<MacroEvent> Events
        {
            get { return _events; }
            set { _events = value; }
        }

        public MacroRecording()
        {
            _keyboardHook.KeyDown += new System.Windows.Forms.KeyEventHandler(keyboardHook_KeyDown);
            _keyboardHook.KeyUp += new System.Windows.Forms.KeyEventHandler(keyboardHook_KeyUp);
            _properties = PropertyService.Get("ClarionEdge.EditorMacros.LastMacro", new ICSharpCode.Core.Properties());
            int index = 0;
            while (true)
            {
                index++;
                MacroEventType eType = _properties.Get<MacroEventType>("MacroEventType" + index, 0);
                Keys eKeys = _properties.Get<Keys>("EventArgs" + index, Keys.None);
                if (eType == 0 || eKeys == Keys.None)
                    break;

                KeyEventArgs eArgs = new KeyEventArgs(eKeys);
                MacroEvent e = new MacroEvent(eType, eArgs, 1);
                _events.Add(e);
            }
        }

        private void keyboardHook_KeyDown(object sender, KeyEventArgs e)
        {
            LoggingService.Debug("keyboardHook_KeyDown");
            if (SkipRepeatedEvents(e.KeyCode, _lastKeyDownKeyCode) == true)
                return;

            _events.Add(
                new MacroEvent(
                    MacroEventType.KeyDown,
                    e,
                    Environment.TickCount - _lastTimeRecorded
                ));

            _lastTimeRecorded = Environment.TickCount;
            _lastKeyDownKeyCode = e.KeyCode;

        }

        void keyboardHook_KeyUp(object sender, KeyEventArgs e)
        {
            LoggingService.Debug("keyboardHook_KeyUp");
            _lastKeyDownKeyCode = Keys.None;

            _events.Add(
                new MacroEvent(
                    MacroEventType.KeyUp,
                    e,
                    Environment.TickCount - _lastTimeRecorded
                ));

            _lastTimeRecorded = Environment.TickCount;

        }

        private bool SkipRepeatedEvents(Keys keys, Keys lastKeyDown)
        {
            if (keys != lastKeyDown)
                return false;

            switch (keys)
            {
                case Keys.LControlKey:
                case Keys.RControlKey:
                case Keys.Control:
                case Keys.ControlKey:
                case Keys.Shift:
                case Keys.ShiftKey:
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                case Keys.Alt:
                    return true;
                default:
                    return false;
            }
        }

        internal void StartRecording()
        {
            if (PropertyService.Get<bool>("ClarionEdge.EditorMacros.cbDisableCCWhileRecording", false) == true)
            {
                _originalCC = CodeCompletionOptions.EnableCodeCompletion;
                CodeCompletionOptions.EnableCodeCompletion = false;
            }

            _events.Clear();
            _lastTimeRecorded = Environment.TickCount;
            _keyboardHook.Start();
        }

        internal void StopRecording()
        {
            _keyboardHook.Stop();
            _properties = new ICSharpCode.Core.Properties();
            if (_events.Count > 0)
            {
                int index = 0;
                foreach (MacroEvent evt in _events)
                {
                    index++;
                    _properties.Set<MacroEventType>("MacroEventType" + index, evt.MacroEventType);
                    _properties.Set<Keys>("EventArgs" + index, ((KeyEventArgs)evt.EventArgs).KeyCode);
                }
                PropertyService.Set("ClarionEdge.EditorMacros.LastMacro", _properties);
                PropertyService.Save();
            }
            if (PropertyService.Get<bool>("ClarionEdge.EditorMacros.cbDisableCCWhileRecording", false) == true)
            {
                CodeCompletionOptions.EnableCodeCompletion = _originalCC;
            }
        }

        internal void Play()
        {
            // Wait until the ModifierKeys used to launch the shortcut has been release. Otherwise really odd key combinations occur!
            BackgroundWorker bwCloseModifiers = new BackgroundWorker();
            bwCloseModifiers.WorkerSupportsCancellation = true;
            bwCloseModifiers.DoWork += new DoWorkEventHandler(_bwCloseModifiers_DoWork);
            bwCloseModifiers.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bwCloseModifiers_RunWorkerCompleted);
            if (bwCloseModifiers.IsBusy != true)
            {
                bwCloseModifiers.RunWorkerAsync();
            }
        }

        void _bwCloseModifiers_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (true)
            {
                if (sw.ElapsedMilliseconds > 5000)
                {
                    //MessageBox.Show("Editor Macros thinks that maybe you are holding down one of the Ctrl/Alt/Shift keys... You need to release them for playback to work!\n\nPlease try again.");
                    e.Cancel = true;
                    break;
                }
                if (Control.ModifierKeys == Keys.Control)
                {
                    KeyboardSimulator.KeyUp(Keys.Control);
                    continue; //MessageBox.Show("1");
                }
                else if (Control.ModifierKeys == Keys.Shift)
                {
                    KeyboardSimulator.KeyUp(Keys.Shift);
                    continue; //MessageBox.Show("2");
                }
                else if (Control.ModifierKeys == Keys.Alt)
                {
                    KeyboardSimulator.KeyUp(Keys.Alt);
                    continue; //MessageBox.Show("3");
                }
                else
                {
                    LoggingService.Debug("_bwCloseModifiers_DoWork, sw.ElapsedMilliseconds" + sw.ElapsedMilliseconds);
                    break;
                }
            }
        }

        void _bwCloseModifiers_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == false)
                DoPlayMacro();
        }

        private void DoPlayMacro()
        {
            _originalCC = CodeCompletionOptions.EnableCodeCompletion;
            CodeCompletionOptions.EnableCodeCompletion = false;

            WorkbenchSingleton.Workbench.ActiveWorkbenchWindow.SelectWindow();

            foreach (MacroEvent macroEvent in _events)
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

            BackgroundWorker bwDisableCC = new BackgroundWorker();
            bwDisableCC.DoWork += new DoWorkEventHandler(bwDisableCC_DoWork);
            bwDisableCC.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwDisableCC_RunWorkerCompleted);
            if (bwDisableCC.IsBusy != true)
            {
                bwDisableCC.RunWorkerAsync();
            }
        }

        void bwDisableCC_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CodeCompletionOptions.EnableCodeCompletion = _originalCC;
            WorkbenchSingleton.Workbench.ActiveWorkbenchWindow.SelectWindow();
        }

        void bwDisableCC_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            Thread.Sleep(_events.Count * 20);
        }

    }
}
