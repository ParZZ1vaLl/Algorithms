namespace Logic
{
    using System;

    public abstract class Player
    {
        public bool IsActiveTurn;
        protected Field field;
        public event Action<Player> OnTurnEnded;

        public int Color { protected get; set; }
        

        public Player(Field field)
        {
            this.field = field;
        }

        public virtual void StartTurn()
        {
            IsActiveTurn = true;
        }

        public virtual void EndTurn()
        {
            IsActiveTurn = false;
            OnTurnEnded?.Invoke(this);
        }
    }

    public enum Color
    {
        Red,
        Blue
    }
}