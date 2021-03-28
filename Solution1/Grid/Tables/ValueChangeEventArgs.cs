using System;

namespace Grid.Tables
{
    public class ValueChangeEventArgs:EventArgs
    {
        public ValueChangeEventArgs(int x, int y, string value)
        {
            X = x;
            Y = y;
            Value = value;
        }
        public int X { get; }
        public int Y { get; }
        public string Value { get; }
    }
}