using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ICSharpCode.Core;
using System.Xml;
using ICSharpCode.SharpDevelop.Gui;

namespace ClarionEdge.PropertyGridExtras
{
    class PropertyGridState 
    {
        private ICSharpCode.Core.Properties _properties;
        private string _propertiesFileName;

        public void SetFileName()
        {
            PropertyGridHelper.Log("");

            object p = PropertyPad.Grid.SelectedObject;
            if (p == null)
                return;

            _propertiesFileName = Path.Combine(
                Path.Combine(PropertyService.ConfigDirectory, "temp"),
                "propertygridextras." + p.GetType().ToString() + ".xml");

            _properties = LoadProperties(_propertiesFileName);
        }

        private ICSharpCode.Core.Properties LoadProperties(string fileName)
        {
            PropertyGridHelper.Log("fileName=" + fileName);
            if (!File.Exists(fileName))
            {
                PropertyGridHelper.Log("file not found");
                ICSharpCode.Core.Properties properties = new ICSharpCode.Core.Properties();
                return properties;
            }
            using (XmlTextReader reader = new XmlTextReader(fileName))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.LocalName)
                        {
                            case "Properties":
                                ICSharpCode.Core.Properties properties = new ICSharpCode.Core.Properties();
                                properties.ReadProperties(reader, "Properties");
                                PropertyGridHelper.Log("existing properties found");
                                return properties;
                        }
                    }
                }
            }
            return null;
        }

        internal void SaveProperty(VisualHint.SmartPropertyGrid.PropertyExpandedEventArgs e)
        {
            PropertyGridHelper.Log("property (" + e.PropertyEnum.Property.Id + ") " + e.PropertyEnum.Property.DisplayName + " state=" + e.Expanded.ToString());
            _properties.Set<bool>("id" + e.PropertyEnum.Property.Id, e.Expanded);
            _properties.Save(_propertiesFileName);
        }

        internal bool GetPropertyState(VisualHint.SmartPropertyGrid.PropertyEnumerator propEnum)
        {
            PropertyGridHelper.Log("");
            if (propEnum == null ||
                propEnum.Property == null ||
                _properties == null ||
                _properties.Get("id" + propEnum.Property.Id) == null)
            {
                PropertyGridHelper.Log("default to TRUE");
                return true;
            }
            else
            {
                PropertyGridHelper.Log("ELSE");
                return (bool)_properties.Get("id" + propEnum.Property.Id, true);
            }
        }
    }
}
