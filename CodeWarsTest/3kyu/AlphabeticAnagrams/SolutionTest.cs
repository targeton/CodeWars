namespace CodeWarsTest._3kyu.AlphabeticAnagrams
{
    using System;
    using NUnit.Framework;
    using CodeWars._3kyu.AlphabeticAnagrams;

    [TestFixture]
    public class SolutionTest
    {
        [TestCase("A", 1)]
        [TestCase("ABAB", 2)]
        [TestCase("AAAB", 1)]
        [TestCase("BAAA", 4)]
        [TestCase("QUESTION", 24572)]
        [TestCase("BOOKKEEPER", 10743)]
        public void TestNumberToOrdinal(string value, long expected)
        {
            Assert.AreEqual(expected, Solution.ListPosition(value), string.Format("Input {0}", value));
        }
    }
}
