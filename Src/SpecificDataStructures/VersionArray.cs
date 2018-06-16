using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecificDataStructures
{
    public partial class VersionArray<T>: IEnumerable<T>
    {
        T[] _base;
        ChangeLog _changeLog;


        private VersionArray(T[] @base, ChangeLog changeLog)
        {
          
            _base = @base;
            _changeLog = changeLog;
        }
        public VersionArray(params T[] @base)
        {
            _base = @base ?? throw new ArgumentNullException("array mustn't be null");
            _changeLog = new ChangeLog(null);
        }
       
        public int Lenght => _base.Length;        
           

        public T this[int index]
        {
            get
            {
                if(index<0 || index>=_base.Length)
                    throw new IndexOutOfRangeException();
                foreach (var item in _changeLog.GetChanges())
                {
                    if (item.TryGetValue(index, out T value))
                    {
                        return value;
                    }
                }
                return _base[index];
            }
            set
            {
                _changeLog.Add(index,value);
            }
        }

        public void MakeRoot()
        {
            T[] array = new T[Lenght];
            _base.CopyTo(array, 0);
            foreach(var changes in _changeLog.GetChanges().Reverse())
            {
                foreach((int i, T value) in changes)
                {
                    array[i] = value;
                }
            }
            ChangeLog log = new ChangeLog(null);
            _base = array;
            _changeLog = log;

        }
       
        public VersionArray<T> NewBranch()
        {
            var changeLogItem = _changeLog.NewBranch();
            return new VersionArray<T>(_base, changeLogItem);
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Lenght; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
    }
}
