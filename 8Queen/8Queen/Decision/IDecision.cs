using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8Queen
{
    internal interface IDecision<T>
    {
        public T Data { get; set; }
        public bool isValid { get; }
        public int heuristic { get; }
        public string ToString();
    }
}
