using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8Queen
{
    internal class AStar<T> : IAlgorithm<T>
    {
        private IGraph<T> graph;

        private List<IDecision<T>> closed;
        private List<KeyValuePair<int, IDecision<T>>> open;
        private IDecision<T> result;

        private uint sumDecisionsCount = 0;
        private uint maximumDecisionsCount = 0;
        private uint iterationsCount = 0;
        public AStar(IGraph<T> graph)
        {
            this.graph = graph;
            closed = new List<IDecision<T>>();
            open = new List<KeyValuePair<int, IDecision<T>>>();
            open.Add(new KeyValuePair<int, IDecision<T>>(graph.First.heuristic, graph.First));
        }

        public uint SumDecisionsCount => sumDecisionsCount;
        public uint MaximumDecisionsCount => maximumDecisionsCount;
        public uint IterationsCount => iterationsCount;

        public IDecision<T>? GetResult()
        {
            return _AStar(graph.First.heuristic, graph.First, 0) ? result : null;
        }

        private bool _AStar(int f, IDecision<T> start, int depth)
        {
            if (start.isValid)
            {
                result = start;
                return true;
            }
            if (open.Count == 0)
                return false;

            closed.Add(start);
            open.RemoveAll(d => d.Value == start);
            ICollection<IDecision<T>> neighbors = graph.Neighbors(start);
            foreach (var n in neighbors)
            {
                if(!closed.Contains(n))
                    open.Add(new KeyValuePair<int, IDecision<T>>(depth + n.heuristic, n));
            }

            iterationsCount++;
            sumDecisionsCount += (uint)neighbors.Count;
            int currentDecisionsCount = open.Count + closed.Count;
            maximumDecisionsCount = Math.Max((uint)currentDecisionsCount, maximumDecisionsCount);

            var next = open.MinBy(v => v.Key);
            return _AStar(next.Key, next.Value, depth + 1);
        }
    }
}
