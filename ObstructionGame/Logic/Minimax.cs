namespace Logic
{
    using System;
    using System.Collections.Generic;

    public class Minimax
    {
        public static int Eval(Field field, bool isBotMove)
        {
            if (field.GetFreeCells().Count == 0)
            {
                return isBotMove ? int.MaxValue : int.MinValue;
            }

            var clustersCount = field.GetFreeCells().Count / 4;

            if (clustersCount % 2 == 0 == !isBotMove)
            {
                return clustersCount;
            }

            return -clustersCount;
        }

        public static (Turn, int) Execute(int depth, bool isMax, Field field, int a = Int32.MinValue,
            int b = Int32.MaxValue)
        {
            if (depth == 0)
            {
                return (null, Eval(field, !isMax));
            }

            var color = isMax ? 2 : 1;
            var possibleTurns = new List<Turn>();

            foreach (var cell in field.GetFreeCells())
            {
                possibleTurns.Add(new Turn(field, cell, color));
            }

            if (possibleTurns.Count == 0)
            {
                return (null, isMax ? int.MinValue : int.MaxValue);
            }

            if (isMax)
            {
                var best = int.MinValue;
                Turn bestTurn = possibleTurns[0];
                foreach (var turn in possibleTurns)
                {
                    turn.Do();
                    var eval = Execute(depth - 1, !isMax, field, a, b).Item2;
                    turn.Undo();

                    if (eval > best)
                    {
                        best = eval;
                        bestTurn = turn;
                        a = Math.Max(best, a);
                        if (b <= a)
                        {
                            return (bestTurn, best);
                        }
                    }
                }

                return (bestTurn, best);
            }
            else
            {
                var best = int.MaxValue;
                Turn bestTurn = null;
                foreach (var turn in possibleTurns)
                {
                    turn.Do();
                    var eval = Execute(depth - 1, !isMax, field, a, b).Item2;
                    turn.Undo();

                    if (eval < best)
                    {
                        best = eval;
                        bestTurn = turn;
                        b = Math.Min(best, b);
                        if (b <= a)
                        {
                            return (bestTurn, best);
                        }
                    }
                }

                return (bestTurn, best);
            }
        }
    }
}