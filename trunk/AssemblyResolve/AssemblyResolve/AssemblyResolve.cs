using System;
using System.IO;
using System.Reflection;
using ICSharpCode.Core;

namespace ClarionAddins.AssemblyResolve
{
    class RegisterHandler : AbstractCommand
    {
        static String _nameSpace;
        public override void Run()
        {
            _nameSpace = GetType().ToString();
            LoggingService.Debug(_nameSpace + " - Starting the Assembly resolver");
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(currentDomain_AssemblyResolve);
        }

        static System.Reflection.Assembly currentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {

            //This handler is called only when the common language runtime tries to bind to the assembly and fails.
            LoggingService.Debug(_nameSpace + " - Assembly resolve requested for " + args.Name);
            Assembly MyAssembly = null;
            string strTempAssmbPath = String.Empty;

            // I was doing this but it seems that some assemblies (Clarion.core!!!) are in the BaseDirectory (and in later versions of clarion, also the backenbindings...)
            //Path.Combine(currentDomain.BaseDirectory, @"Addins\BackendBindings\ClarionBinding\")
            strTempAssmbPath = FindAssembly(args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll");
            if (strTempAssmbPath != String.Empty)
            {
                LoggingService.Debug(_nameSpace + " - @@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Inside ResolveEventHandler @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                LoggingService.Debug("            args.Name=" + args.Name);
                LoggingService.Debug("            strTempAssmbPath=" + strTempAssmbPath);
                LoggingService.Debug(_nameSpace + " - @@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Inside ClarionEdge.SetTheme ResolveEventHandler @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");

                //Load the assembly from the specified path.
                MyAssembly = Assembly.LoadFrom(strTempAssmbPath);
            }
            //Return the loaded assembly.
            return MyAssembly;
        }

        private static string FindAssembly(string pAssemblyName)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            string[] files;
            string path;
            // First attempt to find it in the BaseDirectory
            path = currentDomain.BaseDirectory;
            LoggingService.Debug(_nameSpace + " - Check BaseDirectory (" + path + ")");
            files = Directory.GetFiles(path, pAssemblyName, SearchOption.AllDirectories);
            if (files.Length > 0)
            {
                LoggingService.Debug(_nameSpace + " - Found in BaseDirectory, files[0]=" + files[0]);
                return files[0];
            }

            // Or, try the addin directories
            // %appdata%\SoftVelocity\Clarion\8.0\Addins
            path = Path.Combine(PropertyService.ConfigDirectory, "AddIns");
            LoggingService.Debug(_nameSpace + " - Check ConfigDirectory (" + path + ")");
            files = Directory.GetFiles(path, pAssemblyName, SearchOption.AllDirectories);
            if (files.Length > 0)
            {
                LoggingService.Debug(_nameSpace + " - Found in ConfigDirectory, files[0]=" + files[0]);
                return files[0];
            }

            // accessory
            path = FileUtility.Combine(new string[] { FileUtility.ApplicationRootPath, "Accessory", "AddIns" });
            LoggingService.Debug(_nameSpace + " - Check ApplicationRootPath (" + path + ")");
            files = Directory.GetFiles(path, pAssemblyName, SearchOption.AllDirectories);
            if (files.Length > 0)
            {
                LoggingService.Debug(_nameSpace + " - Found in ApplicationRootPath, files[0]=" + files[0]);
                return files[0];
            }

            LoggingService.Debug(_nameSpace + " - Not Found!");
            return String.Empty;
        }

    }
}
