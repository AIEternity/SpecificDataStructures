using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceCollection
{
    public static class LazySequence
    {
        #region Range
        public static ISequence<double> RangeDouble((int? begin, int end, int? step) index)
        {
            if (index.begin == null)
                index.begin = 0;
            if (index.step == null)
                index.step = 1;
            int count = (int)Math.Round((index.end - ((int)index.begin)) / (double)index.step);
            return new LazySequence<double>(i => (int)index.begin + (i * (int)index.step), count).AsSequence();
        }

        public static ISequence<double> RangeDouble((int end, int? step) index)
        {
            if (index.step == null)
                index.step = 1;
            return RangeDouble((0, index.end, index.step));
        }

        public static ISequence<double> RangeDouble(int end)
        {
            return RangeDouble((0, end, 1));
        }

        public static ISequence<float> RangeFloat((int? begin, int end, int? step) index)
        {
            if (index.begin == null)
                index.begin = 0;
            if (index.step == null)
                index.step = 1;
            int count = (int)Math.Round((index.end - ((int)index.begin)) / (double)index.step);
            return new LazySequence<float>(i => (int)index.begin + (i * (int)index.step), count).AsSequence();
        }

        public static ISequence<float> RangeFloat((int end, int? step) index)
        {
            if (index.step == null)
                index.step = 1;
            return RangeFloat((0, index.end, index.step));
        }

        public static ISequence<float> RangeFloat(int end)
        {
            return RangeFloat((0, end, 1));
        }

        public static ISequence<int> RangeInt((int? begin, int end, int? step) index)
        {
            if (index.begin == null)
                index.begin = 0;
            if (index.step == null)
                index.step = 1;
            int count = (int)Math.Round((index.end - ((int)index.begin)) / (double)index.step);
            return new LazySequence<int>(i => (int)index.begin + (i * (int)index.step), count).AsSequence();
        }

        public static ISequence<int> RangeInt((int end, int? step) index)
        {
            if (index.step == null)
                index.step = 1;
            return RangeInt((0, index.end, index.step));
        }

        public static ISequence<int> RangeInt(int end)
        {
            return RangeInt((0, end, 1));
        }
        #endregion
    }

    public class LazySequence<T> : ISequence<T>
    {
        private readonly Func<int, T> _action;
        private readonly Func<int> _length;

        public LazySequence(Func<int, T> action,int length, int capacity = 0)
        {
            _action = action;
            _length = ()=>length;
        }
        
        public LazySequence(Func<int, T> action, Func<int> length, int capacity = 0)
        {
            _action = action;
            _length = length;
        }

        public T this[int index] { get => _action(index); }
        public  int Count => _length();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            int length = _length();
            for (int i=0;i< length; i++)
            {
                yield return _action(i);
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            int length = _length();
            for (int i = 0; i < length; i++)
            {
                yield return _action(i);
            }
        }
    }
}
