# Question

[Fluent Calculator](https://www.codewars.com/kata/fluent-calculator-1/train/csharp)

## Instructions

**Introduction**

Created into a new kata because of a certain limitation the Ruby kata posseses that this kata should also have if translated, which is what lead me to create a new one.

#Fluent Calculator Your task is to implement a simple calculator with fluent syntax

var FluentCalculator = /* Magic */;
FluentCalculator should be separated in two, the Values and the Operations, one can call the other, but cannot call one of his own.

	- A Value can call an Operation, but cannot call a value
		FluenCalculator.One.Plus
	
	- An Operation can call a Value, but cannont call a operation
		FluenCalculator.one.plus.two //this should have a value of three
	
	- Pairs of Value and Operation should be stackable to infinity
		FluentCalculator.One.Plus.Two.Plus.Three.Minus.One.Minus.Two.Minus.Four // Should be -1
	
	- A Value should resolve to a double
		Fluent.Calculator.Six.Plus.Four - 10.5 // Should be -0.5

#Now, the fun part... Rules

	- You'll need to evaluate the expression as a whole. Calculator.Two.Times.Four.Plus.Two.Times.Six => 2x4 + 2x6
	- Values in FluentCalculator should go from zero to ten.
	- Supported Operations are Plus, Minus, Times, DividedBy
	- Rules mentioned above
		* FluentCalculator should be stackable to infinity
		* A Value can only call an Operation
		* An Operation can only call a Value
		* A Value should be resolvable to a double, if needed as such