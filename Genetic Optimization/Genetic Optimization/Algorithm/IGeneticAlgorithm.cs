namespace Genetic_Optimization
{
    internal interface IGeneticAlgorithm<T>
    {
        public LinkedList<ISolutionPhenotype<T>> Population { get; }
        public void Iterate();
    }
}
