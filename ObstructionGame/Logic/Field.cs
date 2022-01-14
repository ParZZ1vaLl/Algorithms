namespace Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Field
    {
        public Cell[,] Cells { private set; get; }

        public Field(int xSize, int ySize)
        {
            Cells = new Cell[ySize, xSize];
            for (int i = 0; i < ySize; i++)
            {
                for (int j = 0; j < xSize; j++)
                {
                    Cells[i, j] = new Cell {State = Cell.CellState.Free, X = i, Y = j};
                }
            }
        }

        public List<Cell> GetFreeCells()
        {
            return Cells.Cast<Cell>().Where(cell => cell.State == Cell.CellState.Free).ToList();
        }

        public void PaintCell(int x, int y, int color)
        {
            if (Cells[x, y].State != Cell.CellState.Free)
            {
                throw new Exception($"You cant paint this cell : ({x} : {y}). Its state is {Cells[x, y].State}");
            }

            Cells[x, y].State = color == 1 ? Cell.CellState.Blue : Cell.CellState.Red;
            foreach (var neighbour in GetNeighbours(Cells[x, y]))
            {
                neighbour.State = Cell.CellState.Blocked;
            }
        }

        public void UnPaintCell(int x, int y)
        {
            Cells[x, y].State = Cell.CellState.Free;
            foreach (var neighbour in GetNeighbours(Cells[x, y]))
            {
                if (GetNeighbours(neighbour)
                    .Exists(c => c.State == Cell.CellState.Red || c.State == Cell.CellState.Blue))
                {
                    neighbour.State = Cell.CellState.Blocked;
                }
                else
                {
                    neighbour.State = Cell.CellState.Free;
                }
            }
        }

        private List<Cell> GetNeighbours(Cell centralCell)
        {
            var res = new List<Cell>();
            var x = centralCell.X;
            var y = centralCell.Y;
            if (x > 0)
            {
                if (y > 0)
                {
                    res.Add(Cells[x - 1, y - 1]);
                }

                res.Add(Cells[x - 1, y]);
                if (y < Cells.GetLength(1) - 1)
                {
                    res.Add(Cells[x - 1, y + 1]);
                }
            }

            if (y > 0)
            {
                res.Add(Cells[x, y - 1]);
            }

            if (y < Cells.GetLength(1) - 1)
            {
                res.Add(Cells[x, y + 1]);
            }

            if (x < Cells.GetLength(0) - 1)
            {
                if (y > 0)
                {
                    res.Add(Cells[x + 1, y - 1]);
                }

                res.Add(Cells[x + 1, y]);
                if (y < Cells.GetLength(1) - 1)
                {
                    res.Add(Cells[x + 1, y + 1]);
                }
            }

            return res;
        }
    }
}