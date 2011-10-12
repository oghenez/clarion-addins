using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UacHelpers;

namespace UacHelpers.EmbedManifest
{
    /// <summary>
    /// Obtains version and architecture information for an executable image.
    /// </summary>
    /// <remarks>
    /// This class loads .NET assemblies into a separate reflection-only domain
    /// and obtains the version and architecture from the assembly name.  For
    /// native assemblies, it uses the PE header to extract the architecture,
    /// and resorts to the <see cref="FileVersionInfo"/> class for the version.
    /// </remarks>
    public class BinaryInfo
    {
        class AssemblyNameObtainer : MarshalByRefObject
        {
            public string GetAssemblyName(string assemblyFile)
            {
                return Assembly.ReflectionOnlyLoadFrom(assemblyFile).FullName;
            }
        }

        private readonly string _version;
        private readonly ProcessorArchitecture _arch;
        private readonly string _appName;

        /// <summary>
        /// Constructs a new instance of the <see cref="BinaryInfo"/> class
        /// using the specified executable file.
        /// </summary>
        /// <param name="binaryFile">The executable file to analyze.</param>
        public BinaryInfo(string binaryFile)
        {
            _appName = Path.GetFileNameWithoutExtension(binaryFile);

            //If it's a .NET assembly, we can load it and get the version info.
            try
            {
                AppDomain appDomain = AppDomain.CreateDomain("ReflectionDomain");
                AssemblyNameObtainer ano =
                    (AssemblyNameObtainer)
                    appDomain.CreateInstanceFromAndUnwrap(Assembly.GetExecutingAssembly().Location,
                                                          typeof(AssemblyNameObtainer).FullName);
                AssemblyName an = new AssemblyName(ano.GetAssemblyName(binaryFile));
                AppDomain.Unload(appDomain);
                _version = an.Version.ToString();
                if (an.ProcessorArchitecture != ProcessorArchitecture.None)
                    _arch = an.ProcessorArchitecture;
                else _arch = ProcessorArchitecture.MSIL;
            }
            catch
            {
                //Else . . .
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(binaryFile);
                _version = fvi.FileVersion;
                ImageInfo imageInfo = new ImageInfo(binaryFile);
                _arch = imageInfo.Architecture;
            }
        }

        /// <summary>
        /// Returns the processor architecture of the executable image.  This can be
        /// any of the <see cref="ProcessorArchitecture"/> values.
        /// </summary>
        public ProcessorArchitecture Architecture
        {
            get { return _arch;}
        }
        
        /// <summary>
        /// Returns the version of the executable image.
        /// </summary>
        public string Version
        {
            get { return _version; }
        }

        internal string AppName
        {
            get { return _appName; }
        }
    }
}
