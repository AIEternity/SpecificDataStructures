using System;
using System.Collections.Generic;

namespace SpecificDataStructures
{
    public partial class VersionArray<T>
    {
        private class ChangeLog
        {
            private ChangeLog _parent;            
            private readonly List<ChangeLog> _children = new List<ChangeLog>();
            private Dictionary<int, T> _changes { get; set; } = new Dictionary<int, T>();   
            public ChangeLog(ChangeLog parrent)
            {
                _parent = parrent;
            }            
            public IEnumerable<Dictionary<int, T>> GetChanges()
            {
                ChangeLog node = this;
                while (node != null)
                {
                    yield return node._changes;
                    node = node._parent;
                }
            }
            
            public void Add(int index, T value)
            {
                _changes[index] = value;
            }

            public ChangeLog NewBranch()
            {
                ChangeLog node = new ChangeLog(this);
                _children.Add(node);
                return node;
            }

        }
    }
}
