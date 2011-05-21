using System;
using ICSharpCode.Core;
using VisualHint.SmartPropertyGrid;

namespace ClarionEdge.PropertyGridExtras
{

    class AutoAdjustLabelColumn : AbstractCheckableMenuCommand
    {
        public override bool IsChecked
        {
            get
            {
                return Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.AutoAdjustLabelColumn", "false"));
            }
            set
            {
                PropertyService.Set("ClarionEdge.PropertyGridExtras.AutoAdjustLabelColumn", Convert.ToString(value));
                PropertyGridHelper.AutoAdjustLabelColumn();
            }
        }
    }

    class ShowAdditionalIndentation : AbstractCheckableMenuCommand
    {
        public override bool IsChecked
        {
            get
            {
                return Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.ShowAdditionalIndentation", "false"));
            }
            set
            {
                PropertyService.Set("ClarionEdge.PropertyGridExtras.ShowAdditionalIndentation", Convert.ToString(value));
                PropertyGridHelper.ShowAdditionalIndentation();
            }
        }
    }

    class UseDefaultDrawManager : AbstractCheckableMenuCommand
    {
        public override bool IsChecked
        {
            get
            {
                return Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.UseDefaultDrawManager", "false"));
            }
            set
            {
                PropertyService.Set("ClarionEdge.PropertyGridExtras.UseDefaultDrawManager", Convert.ToString(value));
                PropertyGridHelper.SetDrawManager(PropertyGrid.DrawManagers.DefaultDrawManager, value);
            }
        }
    }

    class UseLightDrawManager : AbstractCheckableMenuCommand
    {
        public override bool IsChecked
        {
            get
            {
                return Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.UseLightDrawManager", "false"));
            }
            set
            {
                PropertyService.Set("ClarionEdge.PropertyGridExtras.UseLightDrawManager", Convert.ToString(value));
                PropertyGridHelper.SetDrawManager(PropertyGrid.DrawManagers.LightColorDrawManager, value);
            }
        }
    }

    class UseCustomDrawManager : AbstractCheckableMenuCommand
    {
        public override bool IsChecked
        {
            get
            {
                return Convert.ToBoolean(PropertyService.Get("ClarionEdge.PropertyGridExtras.UseCustomDrawManager", "false"));
            }
            set
            {
                PropertyService.Set("ClarionEdge.PropertyGridExtras.UseCustomDrawManager", Convert.ToString(value));
                PropertyGridHelper.SetDrawManager(PropertyGrid.DrawManagers.CustomDrawManager, value);
            }
        }
    }

}


