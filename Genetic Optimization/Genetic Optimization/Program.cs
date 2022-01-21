using Genetic_Optimization;

int iterationsCount = 20;

AlgorithmProperties.PopulationSize = 5;
AlgorithmProperties.MutationProbability = 0.04f;

//statistics
//Graph.getInstance().Size = 300;

//for (int mt = 0; mt < 2; mt++)
//{
//    AlgorithmProperties.mutationType = (MutationType?)mt;
//    for (int lo = 0; lo < 3; lo++)
//    {
//        AlgorithmProperties.localUpgradeType = (LocalUpgradeType?)lo;
//        for (int ct = 0; ct < 3; ct++)
//        {
//            AlgorithmProperties.crossoverType = (CrossoverType?)ct;
//            Console.WriteLine("mutation type: " + ((MutationType?)mt).ToString());
//            Console.WriteLine("local upgrade type: " + ((LocalUpgradeType?)lo).ToString());
//            Console.WriteLine("crossover type: " + ((CrossoverType?)ct).ToString());

//            var algorithm = new VertexCoverGeneticAlgorithm();
//            for (int i = 0; i < iterationsCount; i++)
//            {
//                algorithm.Iterate();
//            }
//            Console.WriteLine(algorithm.Population.Min(p => p.Fitness));
//            Console.WriteLine();
//        }
//    }
//}


//concrete
Graph.getInstance().Size = 10;
var algorithm = new VertexCoverGeneticAlgorithm();
AlgorithmProperties.mutationType = MutationType.Macro;
AlgorithmProperties.localUpgradeType = LocalUpgradeType.Boldwin;
AlgorithmProperties.crossoverType = CrossoverType.NPoint;

var graph = Graph.getInstance();
for (int i = 0; i < graph.Size; i++)
{
    for (int j = 0; j < graph.Size; j++)
    {
        if (i == j)
            Console.Write("----" + "\t");
        else
            Console.Write(graph[i, j] + "\t");
    }
    Console.WriteLine();
}
Console.WriteLine();

for (int i = 0; i < iterationsCount; i++)
{
    for (int j = 0; j < AlgorithmProperties.PopulationSize; j++)
    {
        var phenotype = algorithm.Population.ElementAt(j);
        phenotype.Genes.Sort();
        for (int k = 0; k < phenotype.Genes.Count; k++)
            Console.Write(phenotype.Genes[k] + ", ");
        Console.WriteLine();
    }
    Console.WriteLine();
    algorithm.Iterate();
}