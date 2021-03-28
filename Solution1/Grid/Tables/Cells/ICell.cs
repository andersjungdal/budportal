using System;
using System.Collections.Generic;
using System.Text;

namespace Grid.Tables.Cells
{
    public interface ICell
    {
        public string Value { get; set; }
        public string InputType { get; set; }
        public bool Unchangeable { get; set; }
    }
}
