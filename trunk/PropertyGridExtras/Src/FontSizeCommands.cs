using System.Drawing;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;
using VisualHint.SmartPropertyGrid;

namespace ClarionEdge.PropertyGridExtras
{
    class GrowFont : AbstractMenuCommand
    {
        public override void Run()
        {
            PropertyGridSV grid = PropertyPad.Grid;
            grid.Font = new Font(grid.Font.Name, grid.Font.Size + 1.0f, grid.Font.Style, grid.Font.Unit);
            PropertyGridHelper.RefreshFont(grid.Font);
        }
    }

    class ShrinkFont : AbstractMenuCommand
    {
        public override void Run()
        {
            PropertyGridSV grid = PropertyPad.Grid;
            grid.Font = new Font(grid.Font.Name, grid.Font.Size - 1.0f, grid.Font.Style, grid.Font.Unit);
            PropertyGridHelper.RefreshFont(grid.Font);
        }
    }

    class ResetFont : AbstractMenuCommand
    {
        public override void Run()
        {
            PropertyGridHelper.RestoreOriginalFont();
        }
    }

}
