using System.Drawing;
using System.Windows.Forms;
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
            PropertyGridHelper.Refresh(grid, grid.Font.Size.ToString());
        }
    }

    class ShrinkFont : AbstractMenuCommand
    {
        public override void Run()
        {
            PropertyGridSV grid = PropertyPad.Grid;
            grid.Font = new Font(grid.Font.Name, grid.Font.Size - 1.0f, grid.Font.Style, grid.Font.Unit);
            PropertyGridHelper.Refresh(grid, grid.Font.Size.ToString());
        }
    }
    class ResetFont : AbstractMenuCommand
    {
        public override void Run()
        {
            Form mf = WorkbenchSingleton.MainForm;
            PropertyGridSV grid = PropertyPad.Grid;
            grid.Font = new Font(grid.Font.Name, 
                mf.Font.Size,
                grid.Font.Style, 
                mf.Font.Unit);
            PropertyGridHelper.Refresh(grid, grid.Font.Size.ToString(), true);
        }
    }

}
