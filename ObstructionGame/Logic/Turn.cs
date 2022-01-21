namespace Logic
{
    using System.Data;

    public class Turn
    {
        private Field field;
        private int x, y;
        private int color;

        public Turn(Field field, int x, int y, int color)
        {
            this.field = field;
            this.x = x;
            this.y = y;
            this.color = color;
        }

        public Turn(Field field, Cell cell, int color)
        {
            this.field = field;
            x = cell.X;
            y = cell.Y;
            this.color = color;
        }

        public void Do()
        {
            field.PaintCell(x, y, color);
        }

        public void Undo()
        {
            field.UnPaintCell(x, y);
        }
    }
}