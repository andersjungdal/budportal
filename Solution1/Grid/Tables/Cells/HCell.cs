namespace Grid.Tables.Cells
{
    public class HCell
    {
        public int Columspan { get; set; } = 1;
        public int RowSpan { get; set; } = 1;
        public string Value { get; set; }
        public int Width { get; set; } = 100;
        public int Hight { get; set; } = 40;
    }
}