using SpecificDataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SpecificDataStructuresUnitTest
{
    public class VersionArrayTest
    {

        [Fact]
        public void IndexerTest()
        {
            VersionArray<int> arr = new VersionArray<int>(0, 1, 2);
            var arr2 = arr.NewBranch();
            arr2[1] = 5;
            var arr3 = arr2.NewBranch();
            arr3[2] = 10;

            var arr4 = arr.NewBranch();
            arr4[1] = 11;
            arr4[0] = 12;
            Assert.True(Enumerable.SequenceEqual(new[] { 0, 1, 2 }, arr));
            Assert.True(Enumerable.SequenceEqual(new[] { 0, 5, 2 }, arr2));
            Assert.True(Enumerable.SequenceEqual(new[] { 0, 5, 10 }, arr3));
            Assert.True(Enumerable.SequenceEqual(new[] { 12, 11, 2 }, arr4));
        }
        [Fact]
        public void MakeRootTest()
        {
            VersionArray<int> arr = new VersionArray<int>(0, 1, 2);
            var arr2 = arr.NewBranch();           

            arr2.MakeRoot();
            arr[0] = 5;
            arr2[1] = 10;
            arr2[2] = 11;
            Assert.True(Enumerable.SequenceEqual(new[] { 5, 1, 2 }, arr));
            Assert.True(Enumerable.SequenceEqual(new[] { 0, 10, 11 }, arr2));
            
        }
        [Fact]
        public void MakeRootTest2()
        {
            VersionArray<int> arr = new VersionArray<int>(0, 1, 2);
            var arr2 = arr.NewBranch();
            var arr3 = arr2.NewBranch();
            arr[0] = 5;
            arr2[1] = 10;
            arr2[2] = 11;
            arr3[2] = 13;
            arr3.MakeRoot();
            Assert.True(Enumerable.SequenceEqual(new[] { 5, 10, 13 }, arr3));
        }
    }
}
