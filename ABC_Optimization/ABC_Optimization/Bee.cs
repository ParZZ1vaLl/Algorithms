namespace ABC_Optimization
{
    internal class BeeScout
    {
        private int? nectarCount;
        public int Vertex { get; set; }
        public int NectarCount => nectarCount ??= Graph.getInstance().getNeighbors(Vertex).Count;
    }
}
