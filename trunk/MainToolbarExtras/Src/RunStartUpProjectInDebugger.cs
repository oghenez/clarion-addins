using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Clarion.Core.Redirection;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Debugging;
using ICSharpCode.SharpDevelop.Project;
using SoftVelocity.Common;

namespace ClarionEdge.MainToolbarExtras
{
    public class RunStartUpProjectInDebugger : AbstractMenuCommand
    {
        // NOTE: This code is largely a copy of SoftVelocity.Generator.Commands.RunProject
        // It has been stripped down a little as it is only needed to run the debugger.
        // It would probably be a good idea to check agains the original code on new updates to make sure it will still work!

        private bool _useDebug;
        private bool _fallbackToStartUp;
        protected bool withDebugger = true;

        public override void Run()
        {
            UseDebug = true;
            DoRun(ProjectService.OpenSolution.StartupProject);
        }

        protected void DoRun(IProject project)
        {
            RedirectionFile redFile = CommonClarionProject.CurrentRedirectionFile(project);
            if (project is CompilableProject)
            {
                try
                {
                    CompilableProject project2 = (CompilableProject)project;
                    string path = null;
                    LoggingService.Debug("project2.StartAction=" + project2.StartAction);
                    switch (project2.StartAction)
                    {
                        case StartAction.Project:
                            {
                                if (Path.GetExtension(project2.OutputAssemblyFullPath).ToUpper() == ".EXE")
                                {
                                    path = redFile.OpenName(project2.OutputAssemblyFullPath, project2.Directory);
                                    string startArguments = project2.StartArguments;
                                    Path.GetDirectoryName(path);
                                }
                                break;
                            }
                        case StartAction.Program:
                            {
                                path = redFile.OpenName(project2.StartProgram, "");
                                string text1 = project2.StartArguments;
                                break;
                            }
                        case StartAction.StartURL:
                            {
                                path = project2.StartArguments;
                                break;
                            }
                    }
                    if (path != null)
                    {
                        project.Start(this.UseDebug);
                    }
                    else
                    {
                        if (this.FallbackToStartUp && ProjectService.OpenSolution != null && ProjectService.OpenSolution.StartupProject != null && project.IdGuid != ProjectService.OpenSolution.StartupProject.IdGuid)
                        {
                            this.DoRun(ProjectService.OpenSolution.StartupProject);
                        }
                    }
                    return;
                }
                catch (FileNotFoundException exception)
                {
                    MessageBox.Show(string.Format(ResourceService.GetString("Clarion.Generator.FileNotFoundExceptionMessage"), exception.FileName), ResourceService.GetString("Clarion.Generator.FileNotFoundExceptionCaption"));
                    return;
                }
            }
            this.DoRun(project.OutputAssemblyFullPath, null, null);
        }

        protected void DoRun(string exeName, string arguments, string workingDir)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = exeName;
                if ((workingDir != null) && Directory.Exists(workingDir))
                {
                    processStartInfo.WorkingDirectory = workingDir;
                }
                if (!string.IsNullOrEmpty(arguments))
                {
                    processStartInfo.Arguments = arguments;
                }
                if (this.UseDebug)
                {
                    DebuggerService.CurrentDebugger.Start(processStartInfo);
                }
                else
                {
                    DebuggerService.CurrentDebugger.StartWithoutDebugging(processStartInfo);
                }
            }
            catch (Win32Exception exception)
            {
                if (exception.ErrorCode == -2147467259)
                {
                    MessageBox.Show(exception.Message, ResourceService.GetString("Clarion.Generator.FileNotFoundExceptionCaption"));
                }
            }
            catch (FileNotFoundException exception2)
            {
                MessageBox.Show(string.Format(ResourceService.GetString("Clarion.Generator.FileNotFoundExceptionMessage"), exception2.FileName), ResourceService.GetString("Clarion.Generator.FileNotFoundExceptionCaption"));
            }
            catch (Exception exception3)
            {
                MessageBox.Show(exception3.Message, ResourceService.GetString("Clarion.Generator.FileNotFoundExceptionCaption"));
            }
        }

        public bool UseDebug
        {
            get
            {
                return this._useDebug;
            }
            set
            {
                this._useDebug = value;
            }
        }
        
        public bool FallbackToStartUp
        {
            get
            {
                return this._fallbackToStartUp;
            }
            set
            {
                this._fallbackToStartUp = value;
            }
        }
    }
}
