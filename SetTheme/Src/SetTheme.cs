using System.IO;
using ICSharpCode.Core;

namespace ClarionEdge.SetTheme
{
    class SetTheme
    {
        private string themesPath;
        private string addinPath;

        public string ThemesPath
        {
            get {
                string directory = Path.Combine(AddinPath, "CustomThemes");
                LoggingService.Debug("new path: " + directory);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                themesPath = directory;
                return themesPath ; }
            set { themesPath = value; }
        }

        public string AddinPath
        {
            get { return Path.GetDirectoryName(typeof(SetTheme).Assembly.Location); }
            set { addinPath = value; }
        }        
    }
}
