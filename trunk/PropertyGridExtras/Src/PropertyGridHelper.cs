using System;
using System.Drawing;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;
using VisualHint.SmartPropertyGrid;

namespace ClarionEdge.PropertyGridExtras
{
    public static class PropertyGridHelper
    {
        internal static void AutoAdjustLabelColumn()
        {
            if (Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.AutoAdjustLabelColumn", "False")) == false)
                return;
            PropertyGridSV grid = PropertyPad.Grid;
            if (grid != null)
            {
                grid.AdjustLabelColumn();
                Refresh(grid);
            }
        }

        internal static void ShowAdditionalIndentation()
        {
            PropertyGridSV grid = PropertyPad.Grid;
            grid.ShowAdditionalIndentation = Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.ShowAdditionalIndentation", "false"));
            AutoAdjustLabelColumn();
            Refresh(grid);
        }

        internal static void Refresh(PropertyGridSV grid)
        {
            grid.RefreshProperties();
        }

        internal static void Refresh(PropertyGridSV grid, string pFontSize)
        {
            PropertyService.Set("ClarionEdge.PropertyGridExtras.FontSize", pFontSize);
            AutoAdjustLabelColumn();
            Refresh(grid);
        }

        internal static void Refresh(PropertyGridSV grid, string pFontSize, bool pResetDone)
        {
            PropertyService.Set("ClarionEdge.PropertyGridExtras.ResetDone", "true");
            Refresh(grid, pFontSize);
        }

        internal static void SetDrawManager(PropertyGrid.DrawManagers drawManagers, bool value)
        {
            object propertyStates = null;
            PropertyGridSV grid = PropertyPad.Grid;
            if (grid != null)
            {
                // Reset to the original draw manager if both of these are false
                if (Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.UseLightDrawManager", "false")) == false &&
                    Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.UseDefaultDrawManager", "false")) == false &&
                    Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.UseCustomDrawManager", "false")) == false)
                {
                    drawManagers = PropertyGrid.DrawManagers.DotnetDrawManager;
                }

                // Make sure that only one menu item is checked... yes, a fake option control!
                if (drawManagers == PropertyGrid.DrawManagers.DotnetDrawManager || 
                    (drawManagers == PropertyGrid.DrawManagers.LightColorDrawManager && value == true))
                {
                    PropertyService.Set("ClarionEdge.PropertyGridExtras.UseDefaultDrawManager", "False");
                    PropertyService.Set("ClarionEdge.PropertyGridExtras.UseCustomDrawManager", "False");
                }
                if (drawManagers == PropertyGrid.DrawManagers.DotnetDrawManager || 
                    (drawManagers == PropertyGrid.DrawManagers.DefaultDrawManager && value == true))
                {
                    PropertyService.Set("ClarionEdge.PropertyGridExtras.UseLightDrawManager", "False");
                    PropertyService.Set("ClarionEdge.PropertyGridExtras.UseCustomDrawManager", "False");
                }
                if (drawManagers == PropertyGrid.DrawManagers.DotnetDrawManager ||
                    (drawManagers == PropertyGrid.DrawManagers.CustomDrawManager && value == true))
                {
                    PropertyService.Set("ClarionEdge.PropertyGridExtras.UseLightDrawManager", "False");
                    PropertyService.Set("ClarionEdge.PropertyGridExtras.UseDefaultDrawManager", "False");
                }
                if (drawManagers == PropertyGrid.DrawManagers.CustomDrawManager)
                    grid.DrawManager = new CustomLightColorDrawManager();
                else
                    grid.DrawingManager = drawManagers;

                if (grid.DrawingManager == PropertyGrid.DrawManagers.LightColorDrawManager)
                {
                    LightColorDrawManager manager = grid.DrawManager as LightColorDrawManager;
                    manager.SetCategoryBackgroundColors(Color.FromArgb(199, 218, 255), Color.White);
                    manager.SetSubCategoryBackgroundColors(Color.FromArgb(197, 242, 191), Color.White);
                }

                if (grid.DrawingManager == PropertyGrid.DrawManagers.CustomDrawManager)
                {
                    propertyStates = grid.SavePropertiesStates(PropertyGrid.PropertyStateFlags.All);

                    grid.GridBackColor = Color.Black;
                    grid.GridForeColor = Color.FromArgb(192, 192, 192);
                    grid.GridColor = Color.FromArgb(43, 43, 64);

                    PropertyEnumerator propEnum = grid.FirstProperty;
                    while (propEnum != propEnum.RightBound)
                    {
                        if (propEnum.Property.IsSetBackColor)
                            propEnum.Property.BackColor = Color.Empty;
                        if (propEnum.Property.IsSetForeColor)
                            propEnum.Property.ForeColor = Color.Empty;

                        PropertyValue propValue = propEnum.Property.Value;
                        if (propValue != null)
                        {
                            if (propEnum.Property.Value.IsSetBackColor)
                                propEnum.Property.Value.BackColor = Color.Empty;
                            if (propEnum.Property.Value.IsSetForeColor)
                                propEnum.Property.Value.ForeColor = Color.Empty;
                        }

                        propEnum.MoveNext();
                    }

                    LightColorDrawManager manager = grid.DrawManager as LightColorDrawManager;
                    if (manager != null)
                    {
                        manager.SetCategoryBackgroundColors(Color.FromArgb(65, 76, 96), Color.FromArgb(65, 76, 96));
                        manager.SetSubCategoryBackgroundColors(Color.FromArgb(49, 49, 57), Color.FromArgb(49, 49, 57));
                        manager.UseBoldFontForCategories = true;
                    }
                }
                else
                {
                    grid.GridBackColor = SystemColors.Window;
                    grid.GridForeColor = SystemColors.ControlText;
                    grid.GridColor = SystemColors.ActiveBorder;

                    if (propertyStates != null)
                    {
                        grid.RestorePropertiesStates(propertyStates);
                        propertyStates = null;
                    }
                }

            }
        }

        internal static void SetDrawManager()
        {
            if (Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.UseLightDrawManager", "false")) == true)
            {
                SetDrawManager(PropertyGrid.DrawManagers.LightColorDrawManager, true);
                return;
            }
            if (Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.UseDefaultDrawManager", "false")) == true)
            {
                SetDrawManager(PropertyGrid.DrawManagers.DefaultDrawManager, true);
                return;
            }
            if (Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.UseCustomDrawManager", "false")) == true)
            {
                SetDrawManager(PropertyGrid.DrawManagers.CustomDrawManager, true);
                return;
            }
            SetDrawManager(PropertyGrid.DrawManagers.DotnetDrawManager, true);
        }
    }
}
