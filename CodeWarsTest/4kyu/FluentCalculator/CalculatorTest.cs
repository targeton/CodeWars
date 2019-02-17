namespace CodeWarsTest._4kyu.FluentCalculator
{
    using NUnit.Framework;
    using CodeWars._4kyu.FluentCalculator;

    [TestFixture]
    public class CalculatorTest
    {
        [Test]
        public static void BasicAddition()
        {
            var calculator = new Calculator();

            //Test Result Call
            Assert.AreEqual(3, calculator.One.Plus.Two.Result());
        }

        [Test]
        public static void MultipleInstances()
        {
            var calculatorOne = new Calculator();
            var calculatorTwo = new Calculator();

            Assert.AreNotEqual((double)calculatorOne.Five.Plus.Five, (double)calculatorTwo.Seven.Times.Three);
        }

        [Test]
        public static void MultipleCalls()
        {
            //Testing that the expression or reference clears between calls
            var calculator = new Calculator();
            Assert.AreEqual(4, calculator.One.Plus.One.Result() + calculator.One.Plus.One.Result());
        }

        [Test]
        public static void Bedmas()
        {
            //Testing Order of Operations
            var calculator = new Calculator();
            Assert.AreEqual(58, (double)calculator.Six.Times.Six.Plus.Eight.DividedBy.Two.Times.Two.Plus.Ten.Times.Four.DividedBy.Two.Minus.Six);
            Assert.AreEqual(-11.972, calculator.Zero.Minus.Four.Times.Three.Plus.Two.DividedBy.Eight.Times.One.DividedBy.Nine, 0.01);
        }

        [Test]
        public static void StaticCombinationCalls()
        {
            //Testing Implicit Conversions
            var calculator = new Calculator();
            Assert.AreEqual(177.5, 10 * calculator.Six.Plus.Four.Times.Three.Minus.Two.DividedBy.Eight.Times.One.Minus.Five.Times.Zero);
        }
    }
}
