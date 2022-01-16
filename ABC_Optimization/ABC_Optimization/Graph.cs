namespace ABC_Optimization
{
    internal class Graph
    {
        private bool[][] adjacencyMatrix;
        public int Size = 150;

        private Graph()
        {
            adjacencyMatrix = new bool[Size][];
            for (int i = 1; i < Size; i++)
            {
                adjacencyMatrix[i] = new bool[i];
            }

            var random = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < Size; i++)
            {
                //выбираем кол-во ребер от 1 до 30
                var edgeCount = random.Next(Math.Min(29, Size / 2));
                //считаем кол-во ребер, которые уже есть
                for (int j = 0; j < Size; j++)
                    if (this[i, j])
                        --edgeCount;
                //добавляем новые ребра
                for (int k = 0; k < edgeCount + 1 && k < Size - 1; k++)
                {
                    int j;
                    do
                    {
                        //генерируем номер второй вершины графа
                        j = random.Next(Size - 1);
                        //во избежание петли, пропускаем i-тую вершину
                        if (j >= i)
                            j++;
                    } while (this[i, j]); //чтобы не повторить существующее ребро
                    this[i, j] = true;
                }
            }
        }

        private static Graph? instance;

        public static Graph getInstance()
        {
            if (instance == null)
                instance = new Graph();
            return instance;
        }

        private bool transformCoordinates(ref int i, ref int j)
        {
            if (i > Size || j > Size || i == j)
                return false;

            if (i - j < 0)
            {
                var temp = i;
                i = j;
                j = temp;
            }
            return true;
        }

        public bool this[int i, int j]
        {
            get
            {
                if (i == j)
                    return false;
                if (!transformCoordinates(ref i, ref j))
                    throw new ArgumentOutOfRangeException();
                return adjacencyMatrix[i][j];
            }
            set
            {
                if (i == j)
                    return;
                if (!transformCoordinates(ref i, ref j))
                    return;
                adjacencyMatrix[i][j] = value;
            }
        }
        
        public List<int> getNeighbors(int index)
        {
            var neighbors = new List<int>();
            for(int i = 0; i < Size; i++)
            {
                if (this[i, index])
                    neighbors.Add(i);
            }
            return neighbors;
        }
    }
}

