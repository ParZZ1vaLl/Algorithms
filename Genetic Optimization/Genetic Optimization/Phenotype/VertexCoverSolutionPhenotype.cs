namespace Genetic_Optimization
{
    internal class VertexCoverSolutionPhenotype : ISolutionPhenotype<int>
    {
        private readonly Graph graph;
        private readonly Random random = new Random();
        private List<int> genes;

        public float Fitness => Genes.Count;

        public List<int> Genes 
        {
            get
            {
                genes = genes.Distinct().ToList();
                return genes;
            }
            set 
            {
                genes = value;
            }
        }
        public List<int>? ChangedGenes { get; set; }

        public VertexCoverSolutionPhenotype(List<int> genes)
        {
            this.genes = genes;
            graph = Graph.getInstance();
        }

        public VertexCoverSolutionPhenotype()
        {
            graph = Graph.getInstance();
            genes = new List<int>();
            while (!isValid(Genes))
                Genes.Add(random.Next(graph.Size));
        }

        public ISolutionPhenotype<int>? Crossover(ISolutionPhenotype<int> other)
        {
            var resultGenes = new List<int>();

            //если введена болдвиновская модель, для размножения необходимо возвращать гены к изначальным значениям
            bool getGene(int i, ISolutionPhenotype<int> phenotype) => phenotype.Genes.Contains(i) ^
                    (phenotype.ChangedGenes != null && phenotype.ChangedGenes.Contains(i));

            //равномерный оператор скрещивания
            if (AlgorithmProperties.crossoverType.HasValue && AlgorithmProperties.crossoverType.Value == CrossoverType.Uniform)
            {
                for (int i = 0; i < graph.Size; i++)
                {
                    if (random.Next(2) == 0)
                    {
                        if (getGene(i, this))
                            resultGenes.Add(i);
                    }
                    else
                    {
                        if (getGene(i, other))
                            resultGenes.Add(i);
                    }
                }
            }
            //двоточечный оператор
            else if(AlgorithmProperties.crossoverType.HasValue && AlgorithmProperties.crossoverType.Value == CrossoverType.NPoint)
            {
                int cutPosition1 = random.Next(graph.Size);
                int cutPosition2 = random.Next(graph.Size);
                if(cutPosition2 < cutPosition1)
                {
                    var temp = cutPosition1;
                    cutPosition1 = cutPosition2;
                    cutPosition2 = temp;
                }
                for (int i = 0; i < cutPosition1; i++)
                {
                    if (getGene(i, this))
                        resultGenes.Add(i);
                }
                for (int i = cutPosition1; i < cutPosition2; i++)
                {
                    if (getGene(i, other))
                        resultGenes.Add(i);
                }
                for (int i = cutPosition2; i < graph.Size; i++)
                {
                    if (getGene(i, this))
                        resultGenes.Add(i);
                }
            }
            // по умолчанию - одноточечный оператор
            else
            {
                int cutPosition = random.Next(graph.Size);
                for (int i = 0; i < cutPosition; i++)
                {
                    if (getGene(i, this))
                        resultGenes.Add(i);
                }
                for (int i = cutPosition; i < graph.Size; i++)
                {
                    if (getGene(i, other))
                        resultGenes.Add(i);
                }
            }

            if (isValid(resultGenes))
                return new VertexCoverSolutionPhenotype(resultGenes);
            return null;
        }

        public ISolutionPhenotype<int>? Mutate(float mutationProbability)
        {
            var mutatedGenes = Genes;
            //по умолчанию - точечная мутация
            if (!AlgorithmProperties.mutationType.HasValue || AlgorithmProperties.mutationType.Value == MutationType.SinglePoint)
            {
                int gene = random.Next(graph.Size);
                if (!mutatedGenes.Remove(gene))
                    mutatedGenes.Add(gene);
            }
            else if (AlgorithmProperties.mutationType.Value == MutationType.Macro)
            {
                for (int i = 0; i < graph.Size; i++)
                {
                    if (random.NextDouble() < mutationProbability)
                    {
                        if (!mutatedGenes.Remove(i))
                            mutatedGenes.Add(i);
                    }
                }
            }

            if (isValid(mutatedGenes))
                return new VertexCoverSolutionPhenotype(mutatedGenes);
            return null;
        }

        private bool isValid(List<int> vertexes)
        {
            for (int i = 0; i < graph.Size; i++)
                for (int j = 0; j < graph.Size; j++)
                    if (graph[i, j] &&
                        !vertexes.Contains(i) && 
                        !vertexes.Contains(j))
                    {
                        return false;
                    }
            return true;
        }

        public ISolutionPhenotype<int>? Upgrade()
        {
            if(!AlgorithmProperties.localUpgradeType.HasValue)
            {
                return null;
            }
            else if(AlgorithmProperties.localUpgradeType.Value == LocalUpgradeType.Lamark)
            {
                var upgradedGenes = Genes;
                for(int i = 0; i < Genes.Count; i++)
                {
                    var temp = Genes[i];
                    upgradedGenes.RemoveAt(0);
                    if (!isValid(upgradedGenes))
                        upgradedGenes.Add(temp);
                }
                return upgradedGenes.Count < Fitness ? new VertexCoverSolutionPhenotype(upgradedGenes) : null;
            }
            else
            {
                VertexCoverSolutionPhenotype upgradedPhenotype = new VertexCoverSolutionPhenotype(Genes);
                for (int i = 0; i < Genes.Count; i++)
                {
                    var temp = Genes[i];
                    upgradedPhenotype.Genes.RemoveAt(0);
                    if (!isValid(upgradedPhenotype.Genes))
                        upgradedPhenotype.Genes.Add(temp);
                    else
                    {
                        if (!(upgradedPhenotype.ChangedGenes == null))
                            upgradedPhenotype.ChangedGenes = new List<int>();
                        upgradedPhenotype.ChangedGenes?.Add(temp);
                    }
                }
                return upgradedPhenotype.ChangedGenes != null ? upgradedPhenotype : null;
            }
        }
    }
}
