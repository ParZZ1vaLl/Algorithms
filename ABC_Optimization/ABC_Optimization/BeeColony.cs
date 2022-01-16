namespace ABC_Optimization
{
    internal class BeeColony
    {
        private List<int> allColors;
        private int?[] vertexColors = new int?[Graph.getInstance().Size];
        private List<int> visitedVertexes = new();

        private List<BeeScout> scouts;
        private int observersCount;

        private Random random = new(DateTime.Now.Millisecond);
        private Graph graph = Graph.getInstance();

        private List<int> UsedColors {
            get
            { 
                var colors = vertexColors.Distinct().ToList();
                var result = new List<int>();
                foreach (var color in colors)
                    if (color.HasValue)
                        result.Add(color.Value);
                return result;
            } 
        }
        public int ColorsCount { get => UsedColors.Count; }
        public int VisitedCount { get => visitedVertexes.Count; }
        public BeeColony(int scoutsCount, int observersCount)
        {
            allColors = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 }.ToList();
            scouts = new List<BeeScout>(scoutsCount);
            for (int i = 0; i < scoutsCount; i++)
                scouts.Add(new BeeScout());
            this.observersCount = observersCount;
        }

        public bool isGraphPainted()
        {
            foreach (var color in vertexColors)
                if (!color.HasValue)
                    return false;
            return true;
        }        
        
        public List<int> paintedVertexes()
        {
            var result = new List<int>();
            foreach (var color in vertexColors)
                if (color.HasValue)
                    result.Add(color.Value);
            return result;
        }

        public void Iterate()
        {
            int sum = 0;
            foreach(var scout in scouts)
            {
                do
                {
                    scout.Vertex = random.Next(0, graph.Size);
                }
                while (false); //(visitedVertexes.Contains(scout.Vertex));
                sum += scout.NectarCount;
            }

            var observersColonies = new int[scouts.Count];

            observersColonies[observersColonies.Length - 1] = observersCount;
            for(int i = 0; i < observersColonies.Length - 1; i++)
            {
                observersColonies[i] = (int)Math.Round((double)observersCount * scouts[i].NectarCount / sum);
                observersColonies[observersColonies.Length - 1] -= observersColonies[i];
            }

            for(int i = 0; i < scouts.Count; i++)
            {
                var neighbors = graph.getNeighbors(scouts[i].Vertex);
                if(neighbors.Count == 0)
                {
                    if (UsedColors.Count > 0)
                        vertexColors[scouts[i].Vertex] = UsedColors[0];
                    else
                    {
                        vertexColors[scouts[i].Vertex] = allColors[0];
                        UsedColors.Add(allColors[0]);
                    }
                }

                List<int> getNeighborsColors(int vertex)
                {
                    var _neighbors = graph.getNeighbors(vertex);
                    var neighborsColors = new List<int>();
                    for (int j = 0; j < _neighbors.Count(); j++)
                    {
                        var color = vertexColors[_neighbors[j]];
                        if(color.HasValue)
                        {
                            neighborsColors.Add(color.Value);
                        }
                        neighborsColors = neighborsColors.Distinct().ToList();
                    }
                    return neighborsColors;
                }

                var neighborsColors = getNeighborsColors(scouts[i].Vertex);
                void paintNeighbor(int index)
                {
                    var surroundingColors = getNeighborsColors(neighbors[index]);
                    //пытаемся взять валидный цвет из neighborsColors
                    foreach (var color in neighborsColors)
                    {
                        if (!surroundingColors.Contains(color))
                        {
                            vertexColors[neighbors[index]] = color;
                            return;
                        }
                    }
                    //некст попытка - usedColors
                    foreach(var color in UsedColors)
                    {
                        if (!surroundingColors.Contains(color))
                        {
                            vertexColors[neighbors[index]] = color;
                            neighborsColors.Add(color);
                            return;
                        }
                    }
                    //ну и если ваще не судьба, all colors
                    foreach (var color in allColors)
                    {
                        if (!surroundingColors.Contains(color))
                        {
                            vertexColors[neighbors[index]] = color;
                            UsedColors.Add(color);
                            neighborsColors.Add(color);
                            return;
                        }
                    }
                }

                int ind = observersColonies[i];
                //в первую очередь красим неокрашенные вершины
                for (int j = 0; j < neighbors.Count() && ind > 0; j++)
                {
                    if (!vertexColors[neighbors[j]].HasValue)
                    {
                        paintNeighbor(j);
                        ind--;
                    }
                }

                //оставшиеся пчелы красят оставшиеся вершины
                for(int j = 0; j < ind; j++)
                {
                    paintNeighbor(random.Next(0, neighbors.Count()));
                }

                //если соседи окрашены, окрасить текущую вершину и сетапнуть ее нектар в ноль
                if(!neighbors.Any(n => !vertexColors[n].HasValue))
                {
                    foreach (var color in UsedColors)
                    {
                        if (!neighborsColors.Contains(color))
                        {
                            vertexColors[scouts[i].Vertex] = color;
                            break;
                        }
                    }
                    foreach (var color in allColors)
                    {
                        if (!neighborsColors.Contains(color))
                        {
                            vertexColors[scouts[i].Vertex] = color;
                            break;
                        }
                    }
                    visitedVertexes.Add(scouts[i].Vertex);
                }
            }
        }
    }
}
