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

            //Retrieve the list of referenced assemblies in an array of AssemblyName.
            Assembly MyAssembly, objExecutingAssemblies;
            string strTempAssmbPath = "";

            objExecutingAssemblies = Assembly.GetExecutingAssembly();
            AssemblyName[] arrReferencedAssmbNames = objExecutingAssemblies.GetReferencedAssemblies();

            //Loop through the array of referenced assembly names.
            foreach (AssemblyName strAssmbName in arrReferencedAssmbNames)
            {
                //Check for the assembly names that have raised the "AssemblyResolve" event.
                if (strAssmbName.FullName.Substring(0, strAssmbName.FullName.IndexOf(",")) == args.Name.Substring(0, args.Name.IndexOf(",")))
                {
                    //Build the path of the assembly from where it has to be loaded.
                    if (args.Name.Substring(0, args.Name.IndexOf(",")) == "CommonSources")
                    {
                        AppDomain currentDomain = AppDomain.CurrentDomain;
                        strTempAssmbPath = Path.Combine(currentDomain.BaseDirectory, @"Addins\BackendBindings\ClarionBinding\Common\CommonSources.dll");
                        LoggingService.Debug("*** RemoveLine, strTempAssmbPath: " + strTempAssmbPath);
                    }
                    break;
                }

            }
            //Load the assembly from the specified path.
            MyAssembly = Assembly.LoadFrom(strTempAssmbPath);

            //Return the loaded assembly.
            return MyAssembly;
        }

    }
}
