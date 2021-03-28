using System.Collections.Generic;
using Grid.Tables.Cells;

namespace Grid.Tables
{
    public class GridContext
    {
        public int RowHeaters { get; set; }
        public List<ICell> Cells { get; set; }
    }
}