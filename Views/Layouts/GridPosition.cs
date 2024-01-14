using Avalonia.Controls;

namespace Graven.Hearts.MakeGLTF.Views.Layouts
{
    public class GridPosition(int column, int row)
    {
        public int Column { get; } = column;
        public int Row { get; } = row;
    }
}