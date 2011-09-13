using System;
using System.IO;
using System.Reflection;
using ICSharpCode.Core;

namespace ClarionEdge.RemoveLine
{
    class AssemblyResolve : AbstractCommand
    {
        public override void Run()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += new ResolveEventHandler(currentDomain_AssemblyResolve);
        }

        System.Reflection.Assembly currentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            //This handler is called only when the common language runtime tries to bind to the assembly and fails.

            Assembly MyAssembly;
            string strTempAssmbPath = String.Empty;
            AppDomain currentDomain = AppDomain.CurrentDomain;
            string[] files = Directory.GetFiles(
                Path.Combine(currentDomain.BaseDirectory, @"Addins\BackendBindings\ClarionBinding\")
                , args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll"
                , SearchOption.AllDirectories);
            if (files != null && files[0] != String.Empty)
            {
                strTempAssmbPath = files[0];
            }
            /*
             * This was the old hard coded method... the above use of GetFiles seems to be working ok and is much more generic... assuming that there is not two files with the same name in the path!
            switch(args.Name.Substring(0, args.Name.IndexOf(",")))
            {
                case "CommonSources":
                    strTempAssmbPath = @"Addins\BackendBindings\ClarionBinding\Common\CommonSources.dll";
                    break;
                case "CommonControl":
                    strTempAssmbPath = @"Addins\BackendBindings\ClarionBinding\Common\Controls\CommonControl.dll";
                    break;
                case "ClarionNetWindow":
                    strTempAssmbPath = @"Addins\BackendBindings\ClarionBinding\Common\ClarionNetWindow.dll";
                    break;
                case "MenubarControl":
                    strTempAssmbPath = @"Addins\BackendBindings\ClarionBinding\Common\Controls\MenubarControl.dll";
                    break;
                case "CWBinding":
                    strTempAssmbPath = @"Addins\BackendBindings\ClarionBinding\ClarionWin\CWBinding.dll";
                    break;

            }
            */
            if (strTempAssmbPath != String.Empty) {
                strTempAssmbPath = Path.Combine(currentDomain.BaseDirectory, strTempAssmbPath);
                }

            LoggingService.Debug(" @@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Inside ClarionEdge.RemoveLine ResolveEventHandler @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            LoggingService.Debug("            args.Name=" + args.Name);
            LoggingService.Debug("            strTempAssmbPath=" + strTempAssmbPath);
            LoggingService.Debug(" @@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Inside ClarionEdge.RemoveLine ResolveEventHandler @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            //Load the assembly from the specified path.
            MyAssembly = Assembly.LoadFrom(strTempAssmbPath);
            
            //Return the loaded assembly.
            return MyAssembly;
        }

    }
}
