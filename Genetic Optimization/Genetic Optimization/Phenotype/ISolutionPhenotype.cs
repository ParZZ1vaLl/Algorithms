namespace Genetic_Optimization
{
    internal interface ISolutionPhenotype<T>
    {
        public float Fitness { get; }
        //индексы генов, измененных в рамках Болдвиновской модели локального улучшения
        public List<int>? ChangedGenes { get; set; }

        public List<T> Genes { get; }

        public ISolutionPhenotype<T>? Mutate(float mutationProbability);
        public ISolutionPhenotype<T>? Crossover(ISolutionPhenotype<T> other);
        public ISolutionPhenotype<T>? Upgrade();
    }
}
