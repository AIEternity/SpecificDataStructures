using SpecificDataStructures;
using System;
using System.Linq;
using Xunit;

namespace SpecificDataStructuresUnitTest
{
    public class FixedListTest
    {
        private const int Size = 5;

        [Fact]
        public void AddTest()
        {
            FixedList<int> lst = new FixedList<int>(Size);
            Assert.Equal(0, lst.Count);
            for (int i = 1; i <= Size; i++)
                lst.Add(i);

            var expected = Enumerable.Range(1, 3 * Size);
            for (int i = Size; i <= 2 * Size; i++)
            {
                Assert.True(lst.SequenceEqual(expected.Skip(i - Size).Take(Size)));
                lst.Add(i + 1);
            }
        }
        [Fact]
        public void ClearTest()
        {
            FixedList<int> lst = new FixedList<int>(Size);
            Assert.Equal(0, lst.Count);
            for (int i = 1; i <= Size; i++)
                lst.Add(i);
            var expected = Enumerable.Range(1, 3 * Size);
            Assert.True(lst.SequenceEqual(expected.Skip(0).Take(Size)));
            lst.Clear();
            Assert.True(lst.SequenceEqual(new int[0]));
            Assert.Equal(0, lst.Count);
            Assert.Throws<IndexOutOfRangeException>(() => lst[0]);
        }
        [Fact]
        public void GetExceptionTest()
        {
            FixedList<int> lst = new FixedList<int>(5);
            Assert.Throws<IndexOutOfRangeException>(() => lst[0]);
            lst.Add(0);
            Assert.Equal(0, lst[0]);
            Assert.Throws<IndexOutOfRangeException>(() => lst[1]);
        }
    }
}
