using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using ICSharpCode.Core;
using System.IO;

namespace ClarionAddins.IdeDebug
{
    public partial class DebugViewPadControl : UserControl
    {
        IntPtr _dbgViewWin;

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll")]
        static extern bool MoveWindow(IntPtr Handle, int x, int y, int w, int h, bool repaint);
        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, Int32 wParam, Int32 lParam);

        // Constant values were found in the "windows.h" header file.
        private const int WM_ACTIVATEAPP = 0x001C,
                          GWL_STYLE = -16,
                          WM_CLOSE = 0x10;

        public static class WindowStyles
        {
            public static readonly Int32
            WS_BORDER = 0x00800000,
            WS_CAPTION = 0x00C00000,
            WS_CHILD = 0x40000000,
            WS_CHILDWINDOW = 0x40000000,
            WS_CLIPCHILDREN = 0x02000000,
            WS_CLIPSIBLINGS = 0x04000000,
            WS_DISABLED = 0x08000000,
            WS_DLGFRAME = 0x00400000,
            WS_GROUP = 0x00020000,
            WS_HSCROLL = 0x00100000,
            WS_ICONIC = 0x20000000,
            WS_MAXIMIZE = 0x01000000,
            WS_MAXIMIZEBOX = 0x00010000,
            WS_MINIMIZE = 0x20000000,
            WS_MINIMIZEBOX = 0x00020000,
            WS_OVERLAPPED = 0x00000000,
            WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
            WS_POPUP = unchecked((int)0x80000000),
            WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,
            WS_SIZEBOX = 0x00040000,
            WS_SYSMENU = 0x00080000,
            WS_TABSTOP = 0x00010000,
            WS_THICKFRAME = 0x00040000,
            WS_TILED = 0x00000000,
            WS_TILEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
            WS_VISIBLE = 0x10000000,
            WS_VSCROLL = 0x00200000;
        }

        public DebugViewPadControl()
        {
            InitializeComponent();
            AttachDebugView();
        }

        private void AttachDebugView()
        {
            string debugViewPath = PropertyService.Get("ClarionAddins.IdeDebug.tbDebugViewPath", "");
            if (debugViewPath != "" && File.Exists(debugViewPath) == true)
            {
                groupBox1.Visible = false;
                Process p = Process.Start(debugViewPath, "/f");
                p.WaitForInputIdle();
                _dbgViewWin = p.MainWindowHandle;
                SetParent(_dbgViewWin, panel1.Handle);

                // Remove border and whatnot
                SetWindowLong(_dbgViewWin, GWL_STYLE, WindowStyles.WS_VISIBLE);

                // Move the window to overlay it on this window
                MoveWindow(_dbgViewWin, 0, 0, this.Width, this.Height, true);
            }
        }

        private void DebugViewPadControl_Resize(object sender, EventArgs e)
        {
            if (_dbgViewWin != IntPtr.Zero)
            {
                MoveWindow(_dbgViewWin, 0, 0, this.Width, this.Height, true);
            }
            //base.OnResize(e);

        }
        
        protected override void OnHandleDestroyed(EventArgs e)
        {
            // Stop the application
            if (_dbgViewWin != IntPtr.Zero)
            {

                // Post a close message
                PostMessage(_dbgViewWin, WM_CLOSE, 0, 0);

                // Delay for it to get the message
                System.Threading.Thread.Sleep(1000);

                // Clear internal handle
                _dbgViewWin = IntPtr.Zero;

            }

            base.OnHandleDestroyed(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Exe Files (*.exe)|*.exe|All Files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            ofd.AutoUpgradeEnabled = true;
            ofd.Title = "Select DebugView EXE";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbDebugViewPath.Text = ofd.FileName;
                PropertyService.Set("ClarionAddins.IdeDebug.tbDebugViewPath", tbDebugViewPath.Text);
                AttachDebugView();
            }
        }
        
    }
}
