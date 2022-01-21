namespace Logic
{
    public class PersonPlayer : Player
    {
        public PersonPlayer(Field field) : base(field)
        {
        }

        public void TryMakeMove((int Row, int Column) coords)
        {
            if (IsActiveTurn)
            {
                Turn newTurn = new Turn(field, coords.Row, coords.Column, Color);
                newTurn.Do();
                EndTurn();
            }
        }
    }
}