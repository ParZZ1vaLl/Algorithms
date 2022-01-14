// See https://aka.ms/new-console-template for more information


using _8Queen;

byte[][] dataset =
{
    new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 },
    new byte[] { 1, 4, 5, 2, 3, 7, 6, 0 },
    new byte[] { 7, 3, 5, 0, 1, 2, 4, 6 },
    new byte[] { 2, 3, 5, 0, 4, 7, 1, 6 },
    new byte[] { 2, 1, 5, 4, 3, 7, 6, 0 },
    new byte[] { 5, 6, 3, 0, 2, 1, 4, 7 },
    new byte[] { 3, 2, 4, 6, 0, 7, 5, 1 },
    new byte[] { 5, 1, 0, 4, 6, 3, 2, 7 },
    new byte[] { 7, 0, 2, 3, 1, 6, 4, 5 },
    new byte[] { 2, 7, 0, 5, 3, 4, 6, 1 },
    new byte[] { 6, 0, 1, 2, 7, 4, 3, 5 },
    new byte[] { 6, 1, 5, 7, 3, 4, 0, 2 }, 
    new byte[] { 1, 3, 5, 4, 2, 7, 6, 0 },
    new byte[] { 7, 1, 4, 5, 3, 6, 2, 0 },
    new byte[] { 0, 1, 4, 5, 2, 6, 3, 7 },
    new byte[] { 5, 2, 0, 3, 1, 4, 7, 6 },
    new byte[] { 5, 2, 0, 4, 1, 7, 6, 3 },
    new byte[] { 7, 6, 4, 3, 2, 5, 1, 0 },
    new byte[] { 0, 4, 1, 3, 7, 2, 5, 6 },
    new byte[] { 0, 4, 7, 3, 5, 1, 6, 2 }        
};

uint iddfsMaxAvg = 0;
uint astarMaxAvg = 0;
uint iddfsIterAvg = 0;
uint astarIterAvg = 0;

foreach (var data in dataset)
{
    var initial = new _8QueenDecision(data);
    IGraph<byte[]> graph = new _8QueenDecisionGraph(initial);

    Console.WriteLine(initial.ToString());

    var iddfs = new IDDFS<byte[]>(graph);
    iddfs.MaxDepth = -1;
    var result = iddfs.GetResult();

    Console.WriteLine("iddfs sum: " + iddfs.SumDecisionsCount);
    Console.WriteLine("iddfs max: " + iddfs.MaximumDecisionsCount);
    Console.WriteLine("iddfs iter: " + iddfs.IterationsCount);

    iddfsMaxAvg += iddfs.MaximumDecisionsCount;
    iddfsIterAvg += iddfs.IterationsCount;


    var astar = new AStar<byte[]>(graph);
    result = astar.GetResult();

    Console.WriteLine("astar sum: "+astar.SumDecisionsCount);
    Console.WriteLine("astar max: "+astar.MaximumDecisionsCount);
    Console.WriteLine("astar iter: "+astar.IterationsCount);

    astarMaxAvg += astar.MaximumDecisionsCount;
    astarIterAvg +=  astar.IterationsCount;

    Console.WriteLine();
}

Console.WriteLine(iddfsMaxAvg / dataset.Length);
Console.WriteLine(iddfsIterAvg / dataset.Length);
Console.WriteLine(astarMaxAvg / dataset.Length);
Console.WriteLine(astarIterAvg / dataset.Length);