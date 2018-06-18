using SequenceCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SpecificDataStructuresUnitTest
{
    public class SequenceTest
    {
        [Fact]
        public void RangeTest()
        {
            var result = LazySequence.RangeInt((0, 7, 1));
            for (int i = 0; i < 7; i++)
            {
                Assert.Equal(i, result[i]);
            }
            Assert.Equal(7, result.Count);
        }
        [Fact]
        public void RangeTest2()
        {
            var result = LazySequence.RangeInt((0, 7, 2));
            int j = 0;
            for (int i = 0; i < 7; i += 2)
            {
                Assert.Equal(i, result[j++]);
            }
            Assert.Equal(4, result.Count);
        }
        [Fact]
        public void RangeTest3()
        {
            var result = LazySequence.RangeInt((7, 0, -2));
            int j = 0;
            for (int i = 7; i < 0; i -= 2)
            {
                Assert.Equal(i, result[j++]);
            }
            Assert.Equal(4, result.Count);
        }
        [Fact]
        public void RangeTest4()
        {
            var result = LazySequence.RangeInt((3, 11, 3));
            int j = 0;
            for (int i = 3; i < 11; i += 3)
            {
                Assert.Equal(i, result[j++]);
            }
            Assert.Equal(3, result.Count);
        }
        [Fact]
        public void RangeTest5()
        {
            var result = LazySequence.RangeInt((11, 3, -3));
            int j = 0;
            for (int i = 11; i < 3; i -= 3)
            {
                Assert.Equal(i, result[j++]);
            }
            Assert.Equal(3, result.Count);
        }
        [Fact]
        public void RangeTest6()
        {
            var result = LazySequence.RangeInt((15, 4));
            int j = 0;
            for (int i = 0; i < 15; i += 4)
            {
                Assert.Equal(i, result[j++]);
            }
            Assert.Equal(4, result.Count);
        }
        [Fact]
        public void RangeTest7()
        {
            var result = LazySequence.RangeInt(15);
            int j = 0;
            for (int i = 0; i < 15; i += 1)
            {
                Assert.Equal(i, result[j++]);
            }
            Assert.Equal(15, result.Count);
        }

        [Fact]
        public void SplitSequenceTest()
        {
            var result = LazySequence.RangeInt(7).SplitSequence(2);
            Assert.Equal(0, result[0].First());
            Assert.Equal(1, result[0].Last());

            Assert.Equal(2, result[1].First());
            Assert.Equal(3, result[1].Last());

            Assert.Equal(4, result[2].First());
            Assert.Equal(5, result[2].Last());

            Assert.Equal(6, result[3].First());
            Assert.Equal(6, result[3].Last());
            Assert.Equal(4, result.Count);
        }

        [Fact]
        public void SliceTest()
        {
            var sqnc = LazySequence.RangeInt(10);
            sqnc = sqnc.Slice((null, 4, 1));
            for (int i = 0; i < 4; i++)
            {
                Assert.Equal(i, sqnc[i]);
            }
            Assert.Equal(4, sqnc.Count);
        }

        [Fact]
        public void DelayTest()
        {
            var sqnc = LazySequence.RangeInt(10);
            sqnc = sqnc.Delay(2);
            Assert.Equal(0, sqnc[0]);
            Assert.Equal(0, sqnc[1]);
            for (int i = 2; i < 10; i++)
            {
                Assert.Equal(i - 2, sqnc[i]);
            }
            Assert.Equal(10, sqnc.Count);
        }

        [Fact]
        public void AvgTest()
        {
            var sqnc = LazySequence.RangeDouble((0, 10, 2));
            sqnc = sqnc.Avg(LazySequence.RangeDouble((2, 12, 2)));
            Assert.Equal(1, sqnc[0]);
            Assert.Equal(3, sqnc[1]);
            Assert.Equal(5, sqnc[2]);
            Assert.Equal(7, sqnc[3]);
            Assert.Equal(9, sqnc[4]);
            Assert.Equal(5, sqnc.Count);
        }
        [Fact]
        public void MaxTest()
        {
            var sqnc = LazySequence.RangeDouble((100, 0, -1)).Max(5);
            Assert.Equal(100, sqnc[0]);
            Assert.Equal(100, sqnc[1]);
            Assert.Equal(100, sqnc[2]);
            Assert.Equal(100, sqnc[3]);
            Assert.Equal(100, sqnc[4]);
            Assert.Equal(99, sqnc[5]);
        }
        [Fact]
        public void MinTest()
        {
            var sqnc = LazySequence.RangeDouble((0, 100, 1)).Min(5);
            Assert.Equal(0, sqnc[0]);
            Assert.Equal(0, sqnc[1]);
            Assert.Equal(0, sqnc[2]);
            Assert.Equal(0, sqnc[3]);
            Assert.Equal(0, sqnc[4]);
            Assert.Equal(1, sqnc[5]);
        }

        [Fact]
        public void NormolizeTest()
        {
            var sqnc = LazySequence.RangeDouble((0, 100, 1)).Normolize();
            for (int i = 0; i < 100; i++)
                Assert.False(double.IsNaN(sqnc[i]));
        }

       
    }
}
