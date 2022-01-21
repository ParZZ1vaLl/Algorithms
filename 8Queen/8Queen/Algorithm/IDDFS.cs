using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8Queen
{
    internal class IDDFS<T> : IAlgorithm<T>
    {
        private IGraph<T> graph;

        private Dictionary<T, bool> visited;
        private IDecision<T> result;
        private uint sumDecisionsCount = 0;
        private uint maximumDecisionsCount = 0;
        private uint currentDecisionsCount = 0;
        private uint iterationsCount = 0;

        public int MaxDepth { get; set; }

        public uint SumDecisionsCount => sumDecisionsCount;
        public uint MaximumDecisionsCount => maximumDecisionsCount;
        public uint IterationsCount => iterationsCount;
        public IDDFS(IGraph<T> graph)
        {
            this.graph = graph;
        }

        public IDecision<T>? GetResult()
        {
            return IDS(MaxDepth) ? result : null;
        }

        private bool IDS(int maxDepth)
        {
            for (int i = 0; i < maxDepth || maxDepth == -1; i++)
            {
                visited = new Dictionary<T, bool>();
                if (DFS(graph.First, i))
                    return true;

                currentDecisionsCount = 0;

                i++;
            }
            return false;
        }

        private bool DFS(IDecision<T> start, int depth)
        {
            //если решение валидно, вернуть
            if (start.isValid)
            {
                result = start;
                return true;
            }

            visited.Add(start.Data, true);

            //если глубина меньше двух, нет смысла идти в цикл
            if (depth < 2)
                return false;

            var neighbors = graph.Neighbors(start);
            //обновляем статистику
            iterationsCount++;
            currentDecisionsCount += (uint)neighbors.Count;
            sumDecisionsCount += (uint)neighbors.Count;
            maximumDecisionsCount = Math.Max(currentDecisionsCount, maximumDecisionsCount);
            //итерируемся по всем соседям
            foreach (var dec in neighbors)
            {
                //если вершина не была посещена
                if (!visited.ContainsKey(dec.Data) || visited[dec.Data] == false)
                {
                    //рекурсивно запускаем DFS
                    if (DFS(dec, depth - 1))
                    {
                        return true;
                    }
                }
            }

            //если все вершины были посещены, и ничего не получилось
            return false;
        }
    }
}
