using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8Queen
{
    internal class _8QueenDecisionGraph : IGraph<byte[]>
    {
        public IDecision<byte[]> First { get; }

        public _8QueenDecisionGraph(_8QueenDecision initialState)
        {
            First = initialState;
        }

        public ICollection<IDecision<byte[]>> Neighbors(IDecision<byte[]> node)
        {
            var result = new List<IDecision<byte[]>>();
            for(int i = 0; i < 8; i++)
            {
                var data = new byte[node.Data.Length];
                for(int j = i + 1; j < 8; j++)
                {
                    Array.Copy(node.Data, data, data.Length);
                    var temp = data[i];
                    data[i] = data[j];
                    data[j] = temp;
                    var decision = new _8QueenDecision(new byte[data.Length]);
                    Array.Copy(data, decision.Data, data.Length);
                    result.Add(decision);
                }
            }
            return result;
        }
    }
}
