namespace Genetic_Optimization
{
    internal class VertexCoverGeneticAlgorithm : IGeneticAlgorithm<int>
    {
        public LinkedList<ISolutionPhenotype<int>> Population { get; }

        public VertexCoverGeneticAlgorithm()
        {
            Population = new LinkedList<ISolutionPhenotype<int>>();
            for (int i = 0; i < AlgorithmProperties.PopulationSize; i++)
                Population.AddLast(new VertexCoverSolutionPhenotype());
        }

        public void Iterate()
        {
            var random = new Random();
            //Выбираем родителей турнирной селекцией
            int[] points = new int[4];
            for(int i = 0; i < points.Count(); i++) points[i] = random.Next(AlgorithmProperties.PopulationSize);
            Array.Sort(points);
            var parent1 = Population.ElementAt(points[1]);
            for(int i = points[0]; i < points[1]; i++)
            {
                if(Population.ElementAt(i).Fitness < parent1.Fitness)
                    parent1 = Population.ElementAt(i); 
            }
            var parent2 = Population.ElementAt(points[3]);
            for (int i = points[2]; i < points[3]; i++)
            {
                if (Population.ElementAt(i).Fitness < parent1.Fitness)
                    parent2 = Population.ElementAt(i);
            }
            //размножаемся
            var newPhenotype = parent1.Crossover(parent2);
            if (newPhenotype == null)
                return;
            //мутируем
            var mutatedPhenotype = newPhenotype.Mutate(AlgorithmProperties.MutationProbability);
            if (mutatedPhenotype != null)
                newPhenotype = mutatedPhenotype;
            //улучшаемся
            newPhenotype.Upgrade();
            //добавляем новый фенотип в популяцию
            Population.AddLast(newPhenotype);
            //убираем самого неприспособленного
            var leastFitness = Population.Max(p => p.Fitness);
            Population.Remove(Population.Where(p => p.Fitness == leastFitness).First());
        }
    }
}
