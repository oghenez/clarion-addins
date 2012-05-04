using System;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Gui;

namespace ClarionAddins.ImageViewer
{
    public class ImageViewerDisplayBinding : IDisplayBinding
    {
        public bool CanCreateContentForFile(string fileName)
        {
            return true;
        }
        public IViewContent CreateContentForFile(string fileName)
        {
            ImageViewContent vc = new ImageViewContent();
            vc.Load(fileName);
            return vc;
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
