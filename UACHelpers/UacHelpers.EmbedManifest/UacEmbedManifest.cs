using System;
using System.IO;

namespace UacHelpers.EmbedManifest
{
    class UacEmbedManifest
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: EmbedManifest <binary_name> <description> <execution_level>");
                Console.WriteLine("Execution level can be any of: [ highestAvailable | requireAdministrator | asInvoker ]");
                return;
            }

            Console.ReadLine();

            string binary = Path.GetFullPath(args[0]);
            ExecutionLevel execLevel = (ExecutionLevel) Enum.Parse(typeof (ExecutionLevel), args[2], true);

            //Is there a way to do this programmatically (without mt.exe)? . . .

            string oldManifest = ManifestToolWrapper.ExtractManifestFrom(binary);
            string newManifest;
            if (!String.IsNullOrEmpty(oldManifest))   //We already have a manifest!
            {
                Console.WriteLine("Old manifest:" + Environment.NewLine + oldManifest);
                newManifest = ManifestDocument.ForExistingManifest(oldManifest, execLevel);
            }
            else
            {
                string description = args[1];

                BinaryInfo binaryInfo = new BinaryInfo(binary);
                newManifest = ManifestDocument.ForApplication(binaryInfo.AppName, description, binaryInfo.Version,
                                                              binaryInfo.Architecture, execLevel);
            }

            Console.WriteLine("Generated manifest:" + Environment.NewLine + newManifest);
            ManifestToolWrapper.InsertManifestTo(newManifest, binary);

            Console.WriteLine("Embedded manifest in " + binary);
        }
    }
}
