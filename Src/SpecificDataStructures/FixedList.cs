using System;
using System.Collections;
using System.Collections.Generic;

namespace SpecificDataStructures
{
    /// <summary>
    /// fixed-size collection that, once the size limit has been reached, replaces the oldest record whenever a new one is added
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FixedList<T> : IEnumerable<T>
    {
        private readonly T[] _stoarage;
        private readonly int _maxSize;
        private int _length;
        private int _finishIndex = -1;

        public FixedList(int size)
        {
            _stoarage = new T[size];
            _maxSize = size;
            _length = 0;
        }

        public T this[int index]
        {
            get
            {
                int ind = DefineIndex(index);
                if (ind >= _length || ind < 0)
                    throw new IndexOutOfRangeException();
                return _stoarage[ind];
            }
        }

        public int Length => _length;


        public void Add(T item)
        {
            _finishIndex = (_finishIndex + 1) % _maxSize;
            _stoarage[_finishIndex] = item;
            _length = Math.Min(_length + 1, _maxSize);
        }

        public void Clear()
        {
            _finishIndex = -1;
            _length = 0;
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _length; i++)
            {
                yield return _stoarage[DefineIndex(i)];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        private int DefineIndex(int index)
        {
            int startIndex = _length < _maxSize ? 0 : (_finishIndex + 1) % _maxSize;
            return (startIndex + index) % _maxSize;
        }
    }
}
