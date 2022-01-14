using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8Queen
{
    internal interface IAlgorithm<T>
    {
        public IDecision<T>? GetResult();
        public uint SumDecisionsCount { get; }
        public uint MaximumDecisionsCount { get; }
        public uint IterationsCount { get; }
    }
}
