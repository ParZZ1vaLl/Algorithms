namespace Genetic_Optimization
{
    enum CrossoverType
    {
        SinglePoint,
        NPoint,
        Uniform
    }

    enum MutationType
    {
        SinglePoint,
        Macro
    }

    enum LocalUpgradeType
    {
        Lamark,
        Boldwin
    }
    static class AlgorithmProperties
    {
        public static float MutationProbability { get; set; }
        public static int PopulationSize { get; set; }
        public static CrossoverType? crossoverType { get; set; }
        public static MutationType? mutationType { get; set; }
        public static LocalUpgradeType? localUpgradeType { get; set; }
    }
}
