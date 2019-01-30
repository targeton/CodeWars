using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeWars.AddingBigNumbers
{
    [TestFixture]
    public class SolutionTest
    {
        [Test]
        public void Test0()
        {
            Assert.AreEqual(Solution.Add("1234567890123456", "23456"), "1234567890146912");
        }

        [Test]
        public void Test1()
        {
            Assert.AreEqual(Solution.Add("123", "34"), "157");
        }

        [Test]
        public void Test2()
        {
            Assert.AreEqual(Solution.Add("11111111222222223333333344444444", "11111111222222223333333344444444"), "22222222444444446666666688888888");
        }

        [Test]
        public void Test3()
        {
            Assert.AreEqual(Solution.Add("999999999999999999999", "1"), "1000000000000000000000");
        }
    }
}
