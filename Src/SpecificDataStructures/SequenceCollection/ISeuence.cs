using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceCollection
{
    public  interface ISequence<T>: IEnumerable<T>
    {
        int Count { get; }
        T this[int index] { get; }
    }
}
