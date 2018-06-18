using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceCollection
{

    public class Sequence<T> : ISequence<T>
    {

        private readonly List<T> _sequence = new List<T>();

        public Sequence(IEnumerable<T> seq)
        {
            _sequence = seq.ToList();
        }

        public Sequence(params T[] seq)
        {
            _sequence = seq.ToList();
        }

        public T this[int index]
        {
            get
            {
                index = Math.Min(Math.Max(0, index),_sequence.Count-1);
                return _sequence[index];
            }
        }

        public int Count => _sequence.Count;

        public IEnumerator<T> GetEnumerator()
        {
            return _sequence.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _sequence.GetEnumerator();
        }
    }
}
