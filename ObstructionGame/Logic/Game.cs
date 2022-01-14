namespace Logic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class Game
    {
        private Field field;
        private Player[] players;

        public event Action<Field> OnUpdate;
        public event Action<string> OnGameFinished;

        public Game(int x, int y, bool isBotFirst, int difficulty)
        {
            field = new Field(x, y);

            players = new Player[2];
            var botPlayer = new BotPlayer(difficulty, field);
            var personPlayer = new PersonPlayer(field);

            botPlayer.Color = 1;
            personPlayer.Color = 2;

            if (isBotFirst)
            {
                players[0] = botPlayer;
                players[1] = personPlayer;
            }
            else
            {
                players[0] = personPlayer;
                players[1] = botPlayer;
            }

            foreach (var player in players)
            {
                player.OnTurnEnded += SwitchTurn;
            }
        }


        private void SwitchTurn(Player p)
        {
            OnUpdate?.Invoke(field);

            if (field.GetFreeCells().Count == 0)
            {
                OnGameFinished?.Invoke(p.GetType() == typeof(PersonPlayer) ? "Player" : "Bot");
                return;
            }

            var nextPlayer = players.FirstOrDefault(player => player != p);
            nextPlayer?.StartTurn();
        }

        public void Input((int Row, int Column) coords)
        {
            var personPlayer = (PersonPlayer) players.FirstOrDefault(p => p.GetType() == typeof(PersonPlayer));
            personPlayer.TryMakeMove(coords);
        }

        public void GameStart()
        {
            
            OnUpdate?.Invoke(field);
            players[0].StartTurn();
        }
    }
}