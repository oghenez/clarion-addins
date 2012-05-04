using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;
using System;

namespace ClarionAddins.ImageViewer
{
    public class ImageViewContent : AbstractViewContent
    {
        ImageViewerControl _control = new ImageViewerControl();
        FileChangeWatcher watcher;

        public override Control Control
        {
            get
            {
                return _control;
            }
        }
        public override string TabPageText
        {
            get
            {
                return "Image Viewer";
            }
        }

        public ImageViewContent()
        {
            watcher = new FileChangeWatcher(this);
        }

        public override void Dispose()
        {
            watcher.Dispose();
            _control.Dispose();
            base.Dispose();
        }
        public override void Load(string fileName)
        {
            this.IsDirty = false;
            watcher.SetWatcher(fileName);
            this.FileName = fileName;
            this.TitleName = Path.GetFileName(fileName);
            LoggingService.Debug("ImageViewContent, TitleName=" + this.TitleName);
            _control.FileName = fileName;
            //Image image = Image.FromFile(fileName);
            //_control.pictureBox.Image = image;
            //_control.pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            // Specify a valid picture file path on your computer.
            FileStream fs;
            fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            _control.pictureBox.Image = System.Drawing.Image.FromStream(fs);
            fs.Close();
            _control.pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            LoggingService.Debug("ImageViewContent, Image Loaded");
        }

        public override void Save(string fileName)
        {
            watcher.Disable();
            this.IsDirty = false;
            this.FileName = fileName;
            this.TitleName = Path.GetFileName(fileName);
            watcher.SetWatcher(this.FileName);
            _control.pictureBox.Image.Save(fileName);
        }

        public override bool IsReadOnly
        {
            get
            {
                try
                {
                    return (File.GetAttributes(this.FileName)
                            & FileAttributes.ReadOnly) != 0;
                }
                catch (FileNotFoundException)
                {
                    return false;
                }
                catch (IOException)
                {
                    return true;
                }
            }
        }
    }

    public sealed class FileChangeWatcher : IDisposable
    {
        FileSystemWatcher watcher;
        bool wasChangedExternally = false;
        string fileName;
        AbstractViewContent viewContent;

        public FileChangeWatcher(AbstractViewContent viewContent)
        {
            this.viewContent = viewContent;
            WorkbenchSingleton.MainForm.Activated += GotFocusEvent;
        }

        public void Dispose()
        {
            WorkbenchSingleton.MainForm.Activated -= GotFocusEvent;
            if (watcher != null)
            {
                watcher.Dispose();
            }
        }

        public void Disable()
        {
            if (watcher != null)
            {
                watcher.EnableRaisingEvents = false;
            }
        }

        public void SetWatcher(string fileName)
        {
            this.fileName = fileName;
            try
            {
                if (this.watcher == null)
                {
                    this.watcher = new FileSystemWatcher();
                    this.watcher.SynchronizingObject = WorkbenchSingleton.MainForm;
                    this.watcher.Changed += new FileSystemEventHandler(this.OnFileChangedEvent);
                }
                else
                {
                    this.watcher.EnableRaisingEvents = false;
                }
                this.watcher.Path = Path.GetDirectoryName(fileName);
                this.watcher.Filter = Path.GetFileName(fileName);
                this.watcher.NotifyFilter = NotifyFilters.LastWrite;
                this.watcher.EnableRaisingEvents = true;
            }
            catch (PlatformNotSupportedException)
            {
                if (watcher != null)
                {
                    watcher.Dispose();
                }
                watcher = null;
            }
        }

        void OnFileChangedEvent(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Deleted)
            {
                wasChangedExternally = true;
                if (ICSharpCode.SharpDevelop.Gui.WorkbenchSingleton.Workbench.IsActiveWindow)
                {
                    // delay showing message a bit, prevents showing two messages
                    // when the file changes twice in quick succession
                    WorkbenchSingleton.SafeThreadAsyncCall(GotFocusEvent, this, EventArgs.Empty);
                }
            }
        }

        void GotFocusEvent(object sender, EventArgs e)
        {
            if (wasChangedExternally)
            {
                wasChangedExternally = false;

                string message = StringParser.Parse("${res:ICSharpCode.SharpDevelop.DefaultEditor.Gui.Editor.TextEditorDisplayBinding.FileAlteredMessage}", new string[,] { { "File", Path.GetFullPath(fileName) } });
                if (MessageBox.Show(message,
                                    StringParser.Parse("${res:MainWindow.DialogName}"),
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (File.Exists(fileName))
                    {
                        viewContent.Load(fileName);
                    }
                }
                else
                {
                    viewContent.IsDirty = true;
                }
            }
        }
    }
}
