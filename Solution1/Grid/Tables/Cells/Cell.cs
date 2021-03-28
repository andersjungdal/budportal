namespace Grid.Tables.Cells
{
    public class Cell : ICell
    {
        public string Value { get; set; }
        public string InputType { get; set; }
        public bool Unchangeable { get; set; } = false;
    }
}