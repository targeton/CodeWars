using CodeWars._4kyu.SortBinaryTreeByLevels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CodeWarsTest._4kyu.SortBinaryTreeByLevels
{
    [TestClass]
    public class SolutionTest
    {
        [TestMethod]
        public void NullTest()
        {
            CollectionAssert.AreEqual(new List<int>(), Kata.TreeByLevels(null));
        }

        [TestMethod]
        public void BasicTest()
        {
            CollectionAssert.AreEqual(new List<int>() { 1, 2, 3, 4, 5, 6 }, Kata.TreeByLevels(new Node(new Node(null, new Node(null, null, 4), 2), new Node(new Node(null, null, 5), new Node(null, null, 6), 3), 1)));
        }
    }
}
