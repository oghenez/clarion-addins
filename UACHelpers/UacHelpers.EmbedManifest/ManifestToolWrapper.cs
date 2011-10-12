using System.Diagnostics;
using System.IO;

namespace UacHelpers.EmbedManifest
{
    /// <summary>
    /// Wraps the SDK Manifest Tool (mt.exe) to simplify extraction and insertion
    /// of a manifest into an executable programmatically.
    /// </summary>
    /// <remarks>
    /// If the Windows SDK 6.0 is not installed in the default directory, then in
    /// order to use this class the "mt.exe" executable must be in the path environment
    /// variable.
    /// </remarks>
    public static class ManifestToolWrapper
    {
        private static string ManifestToolExePath
        {
            get
            {
                if (!string.IsNullOrEmpty(_manifestToolExePath))
                    return _manifestToolExePath;

                if (File.Exists(DefaultManifestToolExePath))
                    _manifestToolExePath = DefaultManifestToolExePath;
                else
                {
                    try
                    {
                        string path = Path.GetFullPath(ManifestToolExeName);
                        _manifestToolExePath = path;
                    }
                    catch{}
                }

                if (!string.IsNullOrEmpty(_manifestToolExePath))
                    return _manifestToolExePath;

                throw new FileNotFoundException(
                    "Unable to find Manifest Tool executable; ensure you have it in your path.");
            }
        }

        private static string _manifestToolExePath;
        private const string DefaultManifestToolExePath = "C:\\Program Files\\Microsoft SDKs\\Windows\\v6.0\\Bin\\mt.exe";
        private const string ManifestToolExeName = "mt.exe";

        /// <summary>
        /// Extracts the manifest from an executable image.
        /// </summary>
        /// <param name="binary">The executable image.</param>
        /// <returns>The manifest for the image if it exists; <b>null</b> otherwise.</returns>
        public static string ExtractManifestFrom(string binary)
        {
            string tempFile = Path.GetTempFileName();
            if (File.Exists(tempFile))
                File.Delete(tempFile);
            string extractManifestArgs = "-inputresource:\"" + binary + "\";#1 -out:\"" + tempFile + "\"";
            ExecuteAndPrint(ManifestToolExePath, extractManifestArgs);
            if (!File.Exists(tempFile))
                return null;
            string manifest = File.ReadAllText(tempFile);
            File.Delete(tempFile);
            return manifest;
        }

        /// <summary>
        /// Inserts a manifest into an executable image.
        /// </summary>
        /// <param name="manifest">The manifest text.</param>
        /// <param name="binary">The executable image.</param>
        public static void InsertManifestTo(string manifest, string binary)
        {
            string tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, manifest);
            string insertManifestArgs = "-manifest \"" + tempFile + "\" -outputresource:\"" + binary + "\";#1";
            ExecuteAndPrint(ManifestToolExePath, insertManifestArgs);
            File.Delete(tempFile);
        }

        private static void ExecuteAndPrint(string exe, string args)
        {
            ProcessStartInfo psi = new ProcessStartInfo(exe, args);
            //psi.UseShellExecute = false;
            //psi.RedirectStandardOutput = true;
            //psi.RedirectStandardError = true;
            Process process = Process.Start(psi);
            //Console.WriteLine(process.StandardOutput.ReadToEnd());
            //Console.WriteLine(process.StandardError.ReadToEnd());
            process.WaitForExit();
        }
    }
}
