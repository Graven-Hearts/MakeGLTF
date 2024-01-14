using System.Collections;
using System.Collections.Generic;
using Avalonia.Controls;

namespace Graven.Hearts.MakeGLTF.Views.Layouts
{
    public class GridLayout(ColumnDefinitions columnDefinitions,
        RowDefinitions rowDefinitions,
        IReadOnlyDictionary<string, GridPosition> positions)
    {
        public ColumnDefinitions ColumnDefinitions { get; } = columnDefinitions;
        public RowDefinitions RowDefinitions { get; } = rowDefinitions;
        public IReadOnlyDictionary<string, GridPosition> Positions { get; } = positions;
    }
}