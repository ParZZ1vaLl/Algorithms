using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObstructionGame
{
    using Logic;
    using Color = System.Drawing.Color;

    public partial class Form1 : Form
    {
        private Game game;

        public Form1(int difficulty = 2)
        {
            InitializeComponent();
            Random r = new Random();

            var isBotFirst = r.Next(2) == 1;
            game = new Game(8, 8, isBotFirst, difficulty);
            game.OnUpdate += UpdateForm;
            game.OnGameFinished += FinishGame;

            game.GameStart();
        }

        private void FinishGame(string winner)
        {
            MessageBox.Show(winner + " won ", "Game over");
        }

        private void UpdateForm(Field field)
        {
            for (int i = 0; i < field.Cells.GetLength(0); i++)
            {
                for (int j = 0; j < field.Cells.GetLength(1); j++)
                {
                    switch (field.Cells[i, j].State)
                    {
                        case Cell.CellState.Free:
                            buttons[i, j].BackColor = Color.Azure;
                            break;
                        case Cell.CellState.Blue:
                            buttons[i, j].BackColor = Color.CornflowerBlue;
                            buttons[i, j].Click -= ButtonClick;
                            break;
                        case Cell.CellState.Red:
                            buttons[i, j].BackColor = Color.IndianRed;
                            buttons[i, j].Click -= ButtonClick;
                            break;
                        case Cell.CellState.Blocked:
                            buttons[i, j].BackColor = Color.Gray;
                            buttons[i, j].Click -= ButtonClick;
                            break;
                    }
                }
            }
        }

        private void ButtonClick(object clickedButton, EventArgs e)
        {
            var coords = GetButtonPosition(clickedButton);
            game.Input(coords);
        }

        private (int Row, int Column) GetButtonPosition(object clickedButton)
        {
            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    if (buttons[i, j] == clickedButton)
                    {
                        return (i, j);
                    }
                }
            }

            return (-1, -1);
        }
    }
}