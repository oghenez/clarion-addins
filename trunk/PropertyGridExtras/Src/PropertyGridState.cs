using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ICSharpCode.Core;
using System.Xml;
using ICSharpCode.SharpDevelop.Gui;
using VisualHint.SmartPropertyGrid;

namespace ClarionEdge.PropertyGridExtras
{
    class PropertyGridState 
    {
        private ICSharpCode.Core.Properties _properties;
        private string _propertiesFileName;

        public void SetFileName(PropertyGridSV grid)
        {
            PGHelper.Log("");

            object p = grid.SelectedObject;
            if (p == null)
                return;

            _propertiesFileName = Path.Combine(
                Path.Combine(PropertyService.ConfigDirectory, "temp"),
                "propertygridextras." + p.GetType().ToString() + ".xml");

            _properties = LoadProperties(_propertiesFileName);
        }

        private ICSharpCode.Core.Properties LoadProperties(string fileName)
        {
            PGHelper.Log("fileName=" + fileName);
            if (!File.Exists(fileName))
            {
                PGHelper.Log("file not found");
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
                                PGHelper.Log("existing properties found");
                                return properties;
                        }
                    }
                }
            }
            return null;
        }

        internal void SaveProperty(VisualHint.SmartPropertyGrid.PropertyExpandedEventArgs e)
        {
            PGHelper.Log("property (" + e.PropertyEnum.Property.Id + ") " + e.PropertyEnum.Property.DisplayName + " state=" + e.Expanded.ToString());
            _properties.Set<bool>("id" + e.PropertyEnum.Property.Id, e.Expanded);
            _properties.Save(_propertiesFileName);
        }

        internal bool GetPropertyState(VisualHint.SmartPropertyGrid.PropertyEnumerator propEnum)
        {
            //PropertyGridHelper.Log("");
            if (propEnum == null ||
                propEnum.Property == null ||
                _properties == null ||
                _properties.Get("id" + propEnum.Property.Id) == null)
            {
                return true;
            }
            else
            {
                return (bool)_properties.Get("id" + propEnum.Property.Id, true);
            }
        }

        internal void SaveSelected(PropertyEnumerator propEnum)
        {
            if (propEnum == null || propEnum.Property == null || _properties == null)
                return;

            try
            {
                PGHelper.Log(propEnum.Property.Id.ToString());
                _properties.Set<int>("SelectedPropertyID", propEnum.Property.Id);

                if (propEnum.Property.Id == 0 && propEnum.HasParent == true)
                {
                    _properties.Set<int>("SelectedPropertyParentID", propEnum.Parent.Property.Id);
                    _properties.Set<string>("SelectedPropertyName", propEnum.Property.Name);
                    if (propEnum.Parent.Property.Id == 0 && propEnum.Parent.HasParent == true)
                    {
                        _properties.Set<string>("SelectedPropertyParentName", propEnum.Parent.Property.Name);
                        _properties.Set<int>("SelectedPropertyGrandParentID", propEnum.Parent.Parent.Property.Id);
                    }
                }
                _properties.Save(_propertiesFileName);
            }
            catch (Exception e)
            {
                // ignore this?
                PGHelper.Log("Exception=" + e.ToString());
            }
        }

        internal int GetSelected()
        {
            int propID = _properties.Get<int>("SelectedPropertyID", 0);
            PGHelper.Log("propID=" + propID);
            return propID;
        }

        internal void ReloadState(PropertyGridSV grid, PropertyGridStateHandler stateHandler)
        {
            PGHelper.Log("");
            SetFileName(grid);
            grid.BeginUpdate();
            PropertyDeepEnumerator selectedPropEnum = null;
            PropertyDeepEnumerator propEnum = grid.FirstProperty.GetDeepEnumerator();
            while (propEnum != propEnum.RightBound)
            {
                stateHandler.AllowExpand = true;
                grid.ExpandProperty(propEnum, GetPropertyState(propEnum));
                stateHandler.AllowExpand = false;
                if (IsLastSelectedProperty(propEnum) == true)
                {
                    PGHelper.Log("selectedPropEnum has been set");
                    selectedPropEnum = (PropertyDeepEnumerator)propEnum.Clone();
                }
                propEnum.MoveNext();
            }
            if (selectedPropEnum != null && Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.RememberSelectedProperty", "true")) == true)
            {
                PGHelper.Log("selectedPropEnum... yay!");

                if (selectedPropEnum.Property != null)
                    PGHelper.Log("selectedPropEnum=" + selectedPropEnum.Property.Name);

                grid.SelectProperty(selectedPropEnum);
                grid.EnsureVisible(selectedPropEnum);

            }
            else
            {
                PGHelper.Log("selectedPropEnum is null!");
            }
            grid.EndUpdate();
        }

        private bool IsLastSelectedProperty(PropertyDeepEnumerator propEnum)
        {
            //Yes, it would probably be better to do this by some kind of recursion but right now I could not be bothered... this should work well enough.

            // normal properties
            if (propEnum.Property.Id != 0 && propEnum.Property.Id == GetSelected())
            {
                return true;
            }

            // First level child properties
            if (propEnum.Property.Id == GetSelected() &&
                propEnum.HasParent == true &&
                propEnum.Parent.Property.Id != 0 && // <-- this one skips grandchildren for now...
                propEnum.Parent.Property.Id == _properties.Get<int>("SelectedPropertyParentID", 0) &&
                propEnum.Property.Name == _properties.Get<string>("SelectedPropertyName", ""))
            {
                return true;
            }

            //Grandchild properties
            if (propEnum.Property.Id == GetSelected() &&
                propEnum.HasParent == true &&
                propEnum.Parent.Property.Id == 0 &&
                propEnum.Parent.HasParent == true &&
                propEnum.Parent.Property.Name == _properties.Get<string>("SelectedPropertyParentName", "") &&
                propEnum.Parent.Parent.Property.Id == _properties.Get<int>("SelectedPropertyGrandParentID", 0) &&
                propEnum.Property.Name == _properties.Get<string>("SelectedPropertyName", ""))
            {
                return true;
            }

            return false;
        }

    }
}
