using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Clarion.Core.Redirection;
using CWBinding.Project;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Debugging;
using ICSharpCode.SharpDevelop.Project;
using SoftVelocity.Common;

namespace ClarionEdge.MainToolbarExtras
{
    class DoRun
    {
        // NOTE: This code is largely a copy of SoftVelocity.Generator.Commands.RunProject
        // It has been stripped down a little as it is only needed to run the debugger.
        // It would probably be a good idea to check against the original code on new updates to make sure it will still work!


        private bool _useDebug;
        private bool _fallbackToStartUp;
        private bool _nonElevated;

        internal void Run(IProject project)
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
                        LoggingService.Debug("DoRun.Run, 1");
                        // a bit of a nasty hack, for non elevated starting we are going to bypass the project start mechanism
                        if (NonElevated != true)
                            project.Start(this.UseDebug);
                        else
                            StartNonElevated(project);
                    }
                    else
                    {
                        if (this.FallbackToStartUp && ProjectService.OpenSolution != null && ProjectService.OpenSolution.StartupProject != null && project.IdGuid != ProjectService.OpenSolution.StartupProject.IdGuid)
                        {
                            LoggingService.Debug("DoRun.Run, 2");
                            this.Run(ProjectService.OpenSolution.StartupProject);
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
            LoggingService.Debug("DoRun.Run, 3");
            this.Run(project.OutputAssemblyFullPath, null, null);
        }

        private void StartNonElevated(IProject project)
        {
            RedirectionFile redFile = CommonClarionProject.CurrentRedirectionFile(project);
            CWProject cwProject = (CWProject)project;
            string text = string.Empty;
            if (!string.IsNullOrEmpty(cwProject.OutputPath))
            {
                text = cwProject.OutputPath + Path.DirectorySeparatorChar;
            }
            text = string.Concat(new object[]
            {
                text, 
                ExpandName(cwProject.OutputName, cwProject), 
                '.', 
                "exe"
            });
            Start(redFile.OpenName(text, cwProject.Directory), cwProject);
        }

        private void Start(string program, CWProject cwProject)
        {
            program = Path.GetFullPath(ExpandName(program, cwProject));
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.UseShellExecute = true;
            if (!File.Exists(program))
            {
                MessageService.ShowError(string.Format(StringParser.Parse("${res:Clarion.Compile.NoExe}"), program));
                return;
            }
            string text = StringParser.Parse(cwProject.StartWorkingDirectory);
            if (text.Length == 0)
            {
                processStartInfo.WorkingDirectory = cwProject.Directory;
            }
            else
            {
                processStartInfo.WorkingDirectory = Path.Combine(cwProject.Directory, text);
            }
            /*
            if (withDebugging)
            {
                string location = Assembly.GetEntryAssembly().Location;
                processStartInfo.FileName = Path.Combine(Path.GetDirectoryName(location), Version.Prefix + "db.exe");
                processStartInfo.Arguments = "-s " + base.ActiveConfiguration + " ";
                if (!string.IsNullOrEmpty(Versions.GetActiveVersion(true)))
                {
                    ProcessStartInfo expr_C7 = processStartInfo;
                    expr_C7.Arguments = expr_C7.Arguments + "-v \"" + Versions.GetActiveVersion(true) + "\" ";
                }
                ProcessStartInfo expr_E8 = processStartInfo;
                string arguments = expr_E8.Arguments;
                expr_E8.Arguments = string.Concat(new string[]
        {
            arguments, 
            "\"", 
            program, 
            "\" ", 
            StringParser.Parse(base.StartArguments)
        });
            }
            else
            {
             */
                processStartInfo.FileName = program;
                processStartInfo.Arguments = StringParser.Parse(cwProject.StartArguments);
            //}
            try
            {
                LoggingService.Debug("DoRun.Start, About to start process: " + processStartInfo.FileName);
                //MessageBox.Show("DoRun.Start, About to start process: " + processStartInfo.FileName);
                UacHelpers.UserAccountControl.CreateProcessAsStandardUser(processStartInfo.FileName, processStartInfo.Arguments);
            }
            catch (Exception ex)
            {
                MessageService.ShowError(ex);
            }
        }

        private string ExpandName(string name, CWProject cwProject)
        {
            StringBuilder stringBuilder = new StringBuilder(name);
            stringBuilder.Replace("%V%", "70");
            stringBuilder.Replace("%X%", "");
            stringBuilder.Replace("%L%", (cwProject.Model == "Lib") ? "L" : "");
            return stringBuilder.ToString();
        }

        protected void Run(string exeName, string arguments, string workingDir)
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

        public bool NonElevated
        {
            get
            {
                return this._nonElevated;
            }
            set
            {
                this._nonElevated = value;
            }
        }

    }
}
