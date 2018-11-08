using System;
using NUnit.Framework;

namespace CodeWars
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void test1()
        {
            Assert.AreEqual("now", Program.formatDuration(0));
        }

        [Test]
        public void test2()
        {
            Assert.AreEqual("1 second", Program.formatDuration(1));
        }

        [Test]
        public void test3()
        {
            Assert.AreEqual("1 minute and 2 seconds", Program.formatDuration(62));
        }

        [Test]
        public void test4()
        {
            Assert.AreEqual("2 minutes", Program.formatDuration(120));
        }

        [Test]
        public void test5()
        {
            Assert.AreEqual("1 hour, 1 minute and 2 seconds", Program.formatDuration(3662));
        }

        [Test]
        public void test6()
        {
            Assert.AreEqual("1 hour and 2 seconds", Program.formatDuration(3602));
        }
    }
}
