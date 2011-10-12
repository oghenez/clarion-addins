using System.IO;
using System.Reflection;
using System.Xml;

namespace UacHelpers.EmbedManifest
{
    /// <summary>
    /// Specifies the UAC execution level required for an executable.
    /// </summary>
    public enum ExecutionLevel
    {
        /// <summary>
        /// Run the process under the same token as the creating process.
        /// </summary>
        AsInvoker,
        /// <summary>
        /// Run the process under the highest available credentials.  If the user
        /// is an administrator, show an elevation prompt; otherwise, run the process
        /// under the standard user credentials.
        /// </summary>
        HighestAvailable,
        /// <summary>
        /// Run the process under an administrator token.  If the current process
        /// is not elevated, this will show an elevation prompt.
        /// </summary>
        RequireAdministrator
    }

    /// <summary>
    /// Simplifies the creation of a UAC manifest for an application.  The application
    /// might already have an existing manifest.
    /// </summary>
    public static class ManifestDocument
    {
        private static readonly XmlNamespaceManager ManifestNamespaceManager = new XmlNamespaceManager(new NameTable());
        private const string ManifestNamespaceV2 = "urn:schemas-microsoft-com:asm.v2";
        private const string ManifestNamespaceV1 = "urn:schemas-microsoft-com:asm.v1";
        private const string ManifestNamespaceV3 = "urn:schemas-microsoft-com:asm.v3";

        static ManifestDocument()
        {
            ManifestNamespaceManager.AddNamespace("asmv1", ManifestNamespaceV1);
            ManifestNamespaceManager.AddNamespace("asmv2", ManifestNamespaceV2);
            ManifestNamespaceManager.AddNamespace("asmv3", ManifestNamespaceV3);            
        }

        /// <summary>
        /// Creates a manifest document using an existing manifest document and the
        /// specified execution level required.
        /// </summary>
        /// <param name="existingManifest">The existing manifest text.</param>
        /// <param name="execLevel">The required execution level.</param>
        /// <returns>A merged manifest document with the specified execution level.</returns>
        public static string ForExistingManifest(string existingManifest, ExecutionLevel execLevel)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(existingManifest);
            
            XmlElement assemblyElem = doc.SelectSingleNode("/asmv1:assembly", ManifestNamespaceManager) as XmlElement;
            if (assemblyElem == null)
                throw new InvalidDataException("Manifest does not contain root 'assembly' element.");

            //Try asmv3:trustInfo, then asmv2:trustInfo.  If none found, create one.
            XmlElement trustInfoElem = assemblyElem.SelectSingleNode("//asmv3:trustInfo", ManifestNamespaceManager) as XmlElement;
            if (trustInfoElem != null)
            {
                SetExecutionLevelForTrustInfoElem(doc, trustInfoElem, execLevel, ManifestNamespaceV3);
            }
            else
            {
                trustInfoElem = assemblyElem.SelectSingleNode("//asmv2:trustInfo", ManifestNamespaceManager) as XmlElement;
                if (trustInfoElem != null)
                {
                    SetExecutionLevelForTrustInfoElem(doc, trustInfoElem, execLevel, ManifestNamespaceV2);
                }
                else
                {
                    trustInfoElem = doc.CreateElement("trustInfo", ManifestNamespaceV2);
                    assemblyElem.AppendChild(trustInfoElem);
                    SetExecutionLevelForTrustInfoElem(doc, trustInfoElem, execLevel, ManifestNamespaceV2);
                }
            }
            return doc.OuterXml;
        }

        private static void SetExecutionLevelForTrustInfoElem(XmlDocument doc, XmlElement trustInfoElem, ExecutionLevel execLevel, string xmlNamespace)
        {
            string namespacePrefix = ManifestNamespaceManager.LookupPrefix(xmlNamespace);
            XmlElement securityElem = trustInfoElem.SelectSingleNode(namespacePrefix + ":security", ManifestNamespaceManager) as XmlElement;
            if (securityElem == null)
            {
                securityElem = doc.CreateElement("security", xmlNamespace);
                trustInfoElem.AppendChild(securityElem);
            }
            XmlElement requestedPrivilegesElem = securityElem.SelectSingleNode(namespacePrefix + ":requestedPrivileges", ManifestNamespaceManager) as XmlElement;
            if (requestedPrivilegesElem == null)
            {
                requestedPrivilegesElem = doc.CreateElement("requestedPrivileges", xmlNamespace);
                securityElem.AppendChild(requestedPrivilegesElem);
            }
            XmlElement requestedExecutionLevelElem = requestedPrivilegesElem.SelectSingleNode(namespacePrefix + ":requestedExecutionLevel", ManifestNamespaceManager) as XmlElement;
            if (requestedExecutionLevelElem == null)
            {
                requestedExecutionLevelElem = doc.CreateElement("requestedExecutionLevel", xmlNamespace);
                requestedPrivilegesElem.AppendChild(requestedExecutionLevelElem);
            }
            XmlAttribute execLevelAttr = requestedExecutionLevelElem.Attributes["level"];
            if (execLevelAttr == null)
            {
                execLevelAttr = doc.CreateAttribute("level");
                requestedExecutionLevelElem.Attributes.Append(execLevelAttr);
            }
            execLevelAttr.Value = ToStringExecutionLevel(execLevel);
        }

        /// <summary>
        /// Creates a manifest with the specified required execution level
        /// for an application which does not yet have a manifest.
        /// </summary>
        /// <param name="appName">The short application name.</param>
        /// <param name="description">The application description.</param>
        /// <param name="version">The application version.</param>
        /// <param name="architecture">The application processor architecture.</param>
        /// <param name="execLevel">The required execution level.</param>
        /// <returns>A new manifest for the application with the required execution level.</returns>
        public static string ForApplication(string appName, string description, string version, ProcessorArchitecture architecture, ExecutionLevel execLevel)
        {
            string newManifest = @"<?xml version='1.0' encoding='utf-8' ?>
<assembly xmlns='urn:schemas-microsoft-com:asm.v1' manifestVersion='1.0'>
  <assemblyIdentity version='" + version + @"' 
      processorArchitecture='" + architecture.ToString().ToUpper() + @"'
      name='" + appName + @"'
      type='win32' />
  <description>" + description + @"</description>
  <trustInfo xmlns='urn:schemas-microsoft-com:asm.v2'>
    <security>
      <requestedPrivileges>
        <requestedExecutionLevel level='" + ToStringExecutionLevel(execLevel) + @"' />
      </requestedPrivileges>
    </security>
  </trustInfo>
</assembly>";
            return newManifest;
        }

        private static string ToStringExecutionLevel(ExecutionLevel execLevel)
        {
            string str = execLevel.ToString();
            str = str.Substring(0, 1).ToLower() + str.Substring(1);
            return str; //Uncapitalize the first letter
        }
    }
}
