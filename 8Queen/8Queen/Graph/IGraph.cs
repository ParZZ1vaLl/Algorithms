using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8Queen
{
    internal interface IGraph<T> 
    {
        IDecision<T> First { get; }
        ICollection<IDecision<T>> Neighbors(IDecision<T> node);
    }
}
