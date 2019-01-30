using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace CodeWars.BinomalExpansion
{
    [TestFixture]
    public class SolutionTest
    {
        [Test]
        public void testBPositive()
        {
            Assert.AreEqual("1", Solution.Expand("(x+1)^0"));
            Assert.AreEqual("x+1", Solution.Expand("(x+1)^1"));
            Assert.AreEqual("x^2+2x+1", Solution.Expand("(x+1)^2"));
        }

        [Test]
        public void testBNegative()
        {
            Assert.AreEqual("1", Solution.Expand("(x-1)^0"));
            Assert.AreEqual("x-1", Solution.Expand("(x-1)^1"));
            Assert.AreEqual("x^2-2x+1", Solution.Expand("(x-1)^2"));
        }

        [Test]
        public void testAPositive()
        {
            Assert.AreEqual("625m^4+1500m^3+1350m^2+540m+81", Solution.Expand("(5m+3)^4"));
            Assert.AreEqual("8x^3-36x^2+54x-27", Solution.Expand("(2x-3)^3"));
            Assert.AreEqual("1", Solution.Expand("(7x-7)^0"));
        }

        [Test]
        public void testANegative()
        {
            Assert.AreEqual("625m^4-1500m^3+1350m^2-540m+81", Solution.Expand("(-5m+3)^4"));
            Assert.AreEqual("-8k^3-36k^2-54k-27", Solution.Expand("(-2k-3)^3"));
            Assert.AreEqual("1", Solution.Expand("(-7x-7)^0"));
        }
    }
}
