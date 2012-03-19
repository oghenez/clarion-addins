using System;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.BrowserDisplayBinding;
using ICSharpCode.SharpDevelop.Gui;

namespace ClarionAddins.UrlBinding
{
    public class UrlViewerDisplayBinding : IDisplayBinding
    {
        public bool CanCreateContentForFile(string fileName)
        {
            return true;
        }
        public IViewContent CreateContentForFile(string fileName)
        {
            IniFile ini = new IniFile(fileName);
            BrowserPane browserPane = new BrowserPane();
            browserPane.Load(ini.IniReadValue("InternetShortcut", "URL"));
            browserPane.FileName = fileName;
            return browserPane;
        }
        public bool CanCreateContentForLanguage(string languageName)
        {
            return false;
        }
        public IViewContent CreateContentForLanguage(string languageName, string content)
        {
            throw new NotImplementedException();
        }
    }
}
