namespace Logic
{
    using System.Security.AccessControl;

    public class Cell
    {
        public CellState State { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public enum CellState
        {
            Blocked,
            Free,
            Red,
            Blue
        }
    }
}