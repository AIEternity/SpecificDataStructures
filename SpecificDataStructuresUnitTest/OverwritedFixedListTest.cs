using SpecificDataStructures;
using System;
using System.Linq;
using Xunit;

namespace SpecificDataStructuresUnitTest
{
    public class OverwritedFixedListTest
    {
        private const int Size = 5;

        [Fact]
        public void AddTest()
        {
            OverwritedFixedList<int> lst = new OverwritedFixedList<int>(Size);
            Assert.Equal(0, lst.Length);
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
            OverwritedFixedList<int> lst = new OverwritedFixedList<int>(Size);
            Assert.Equal(0, lst.Length);
            for (int i = 1; i <= Size; i++)
                lst.Add(i);
            var expected = Enumerable.Range(1, 3 * Size);
            Assert.True(lst.SequenceEqual(expected.Skip(0).Take(Size)));
            lst.Clear();
            Assert.True(lst.SequenceEqual(new int[0]));
            Assert.Equal(0, lst.Length);
            Assert.Throws<IndexOutOfRangeException>(() => lst[0]);
        }
        [Fact]
        public void GetExceptionTest()
        {
            OverwritedFixedList<int> lst = new OverwritedFixedList<int>(5);
            Assert.Throws<IndexOutOfRangeException>(() => lst[0]);
            lst.Add(0);
            Assert.Equal(0, lst[0]);
            Assert.Throws<IndexOutOfRangeException>(() => lst[1]);
        }
    }
}
