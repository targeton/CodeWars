using CodeWars._4kyu.SortBinaryTreeByLevels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWarsTest._4kyu.SortBinaryTreeByLevels
{
    [TestClass]
    public class SolutionTest
    {
        [TestMethod]
        public void NullTest()
        {
            CollectionAssert.AreEqual(new List<int>(), Solution.TreeByLevels(null));
        }

        [TestMethod]
        public void BasicTest()
        {
            CollectionAssert.AreEqual(new List<int>() { 1, 2, 3, 4, 5, 6 }, Solution.TreeByLevels(new Node(new Node(null, new Node(null, null, 4), 2), new Node(new Node(null, null, 5), new Node(null, null, 6), 3), 1)));
        }
    }
}
