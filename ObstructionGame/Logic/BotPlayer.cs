namespace Logic
{
    using System.Runtime.Remoting.Messaging;

    public class BotPlayer : Player
    {
        private int minimaxDepth;


        public void SetDifficulty(int difficulty)
        {
            minimaxDepth = difficulty * difficulty;
        }

        public override void StartTurn()
        {
            base.StartTurn();
            var bestTurn = Minimax.Execute(minimaxDepth, false, field).Item1;
            bestTurn.Do();
            EndTurn();
        }

        public override void EndTurn()
        {
            base.EndTurn();
        }

        public BotPlayer(int difficulty, Field field) : base(field)
        {
            minimaxDepth = difficulty * difficulty;
        }
    }
}