using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars._4kyu.FluentCalculator
{
    /// <summary>
    /// A Builder node which may have a parent and an operand. Has number properties to build expressions on.
    /// </summary>
    public class FluentOperandBuilder
    {
        private FluentNumberBuilder _parent;
        private TokenOperation? _tokenOperation;
        internal FluentNumberBuilder Parent { get{return _parent;} }
        internal TokenOperation? TokenOperation { get{return _tokenOperation;} }

        public FluentNumberBuilder Zero{ get { return new FluentNumberBuilder(this, 0.0); }}
        public FluentNumberBuilder One {get{return  new FluentNumberBuilder(this, 1.0);}}
        public FluentNumberBuilder Two{get{return new FluentNumberBuilder(this, 2.0);}}
        public FluentNumberBuilder Three {get{return new FluentNumberBuilder(this, 3.0);}}
        public FluentNumberBuilder Four {get{return new FluentNumberBuilder(this, 4.0);}}
        public FluentNumberBuilder Five {get{return new FluentNumberBuilder(this, 5.0);}}
        public FluentNumberBuilder Six {get{return new FluentNumberBuilder(this, 6.0);}}
        public FluentNumberBuilder Seven {get{return new FluentNumberBuilder(this, 7.0);}}
        public FluentNumberBuilder Eight {get{return new FluentNumberBuilder(this, 8.0); }}
        public FluentNumberBuilder Nine {get{return new FluentNumberBuilder(this, 9.0);}}
        public FluentNumberBuilder Ten {get{return new FluentNumberBuilder(this, 10.0);}}

        protected FluentOperandBuilder()
        {
            _parent = null;
            _tokenOperation = null;
        }
        
        internal FluentOperandBuilder(FluentNumberBuilder fluentOperandBuilder, TokenOperation tokenOperation)
        {
            _parent = fluentOperandBuilder;
            _tokenOperation = tokenOperation;
        }
    }

    /// <summary>
    /// A Builder node which will have a parent and a double value. Has operand properties to build expressions on.
    /// </summary>
    public class FluentNumberBuilder
    {
        private FluentOperandBuilder _parent;
        private double _value;

        private bool _dirty;
        private double _lastResolvedValue;
        
        public FluentOperandBuilder Plus { get{return new FluentOperandBuilder(this, TokenOperation.Add);}}
        public FluentOperandBuilder Minus{ get {return new FluentOperandBuilder(this, TokenOperation.Subtract);}}
        public FluentOperandBuilder Times { get{return new FluentOperandBuilder(this, TokenOperation.Multiply);}}
        public FluentOperandBuilder DividedBy {get {return new FluentOperandBuilder(this, TokenOperation.Divide);}}

        internal FluentNumberBuilder(FluentOperandBuilder fluentNumberBuilder, double value)
        {
            _parent = fluentNumberBuilder;
            _value = value;
            _dirty = true;
            _lastResolvedValue = 0.0;
        }

        /// <summary>
        /// Resolves the results of the expression associated with this objected built by traveling up its parent
        /// builder nodes, and tokenizing it onto a stack. The token stack is evaluated and resolved into a double
        /// value.
        /// </summary>
        /// <returns>The resolved value of the expression we've generated.</returns>
        public double Result()
        {
            if (!_dirty) return _lastResolvedValue;
            
            if (_parent == null) return _value;

            var tokenStack = new Stack<IToken>();

            tokenStack.Push(new ValueToken<double>(_value));
            
            object parent = _parent;
            while (parent != null)
            {
                if (parent is FluentNumberBuilder)
                {
                    var parentNumberBuilder = parent as FluentNumberBuilder;
                    tokenStack.Push(new ValueToken<double>(parentNumberBuilder._value));
                    parent = parentNumberBuilder._parent;
                }
                else if (parent is FluentOperandBuilder)
                {
                    var  parentOperandBuilder = parent as FluentOperandBuilder;
                    // null operand means head of the linked list.
                    if (parentOperandBuilder.TokenOperation == null) break;
                    tokenStack.Push(new ValueToken<TokenOperation>(parentOperandBuilder.TokenOperation.Value));
                    parent = parentOperandBuilder.Parent;
                }
                else
                {
                    break;
                }
            }

            var evaluator = new TokenEvaluator(tokenStack);
            var evaluated = evaluator.ResolveExpression();

            _dirty = false;
            return _lastResolvedValue = evaluated;
        }

        public static implicit operator double(FluentNumberBuilder fluentNumberBuilder)
        {
            return fluentNumberBuilder.Result();
        }
    }
    
    /// <summary>
    /// Wrapper Calculator Type to match the specific name scheme used by the Kata.
    /// </summary>
    public class Calculator : FluentOperandBuilder
    {
        public Calculator() : base()
        {
            
        }
    }

    enum TokenOperation
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }
    
    public interface IToken
    {
    }

    class ValueToken<T> : IToken
    {
        private T _value;
        public T Value { get{return _value;} }

        public ValueToken(T value)
        {
            _value = value;
        }
    }

    /// <summary>
    /// A token parser that evaluates the result of the tokens on the fly.
    /// </summary>
    /// <remarks>
    /// Most parsers generate an AST, but since our use case has a very simple grammar I didn't think creating a whole
    /// AST walker with bells and whistles was necessary. However, this could very easily be expanded upon to parser
    /// into an AST for an AST walker to walk down.
    /// </remarks>
    public class TokenEvaluator
    {
        private IToken[] _tokens;
        private int _index;
        private IToken CurrentToken { get { return _index < _tokens.Length ? _tokens[_index] : null; } }

        public TokenEvaluator(IEnumerable<IToken> tokens)
        {
            _tokens = tokens.ToArray();
            _index = 0;
        }

        private void Eat()
        {
            _index++;
        }

        private double ResolveFactor()
        {
            if (!(CurrentToken is ValueToken<double> )) throw new InvalidOperationException();
            var valueToken = CurrentToken as ValueToken<double>;
            Eat();
            return valueToken.Value;
        }

        private double ResolveMultiplicativeTerm()
        {
            var result = ResolveFactor();
            ValueToken<TokenOperation> op = null;
            while (CurrentToken.Is<ValueToken<TokenOperation>>(out op) &&
                   (op.Value == TokenOperation.Multiply || op.Value == TokenOperation.Divide))
            {
                switch (op.Value)
                {
                    case TokenOperation.Multiply:

                        Eat();
                        result *= ResolveFactor();
                        break;

                    case TokenOperation.Divide:

                        Eat();
                        result /= ResolveFactor();
                        break;
                }
            }

            return result;
        }
        
        /// <summary>
        /// Resolve a value from the expression created by the tokens in this TokenEvaluator.
        /// </summary>
        /// <returns>The resulting value from resolving the expression.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public double ResolveExpression()
        {
            /*
             * After the first operand, there should be an even number of operands and operators.
             * This is a half-arsed check to ensure that at least the sum of both is correct (should be even).
             */
            if ((_tokens.Length - 1) % 2 != 0) throw new InvalidOperationException();

            var result = ResolveMultiplicativeTerm();
            ValueToken<TokenOperation> op = null;
            while (CurrentToken.Is<ValueToken<TokenOperation>>(out op) &&
                   (op.Value == TokenOperation.Add || op.Value == TokenOperation.Subtract))
            {
                switch (op.Value)
                {
                    case TokenOperation.Add:
                        Eat();
                        result += ResolveMultiplicativeTerm();
                        break;
                    case TokenOperation.Subtract:
                        Eat();
                        result -= ResolveMultiplicativeTerm();
                        break;
                }
            }

            return result;
        }
    }
    
    public static class ObjectExtensions
    {
        /// <summary>
        /// Evaluates if a specified object is of the generic type provided.
        /// </summary>
        /// <param name="obj">An object to make a type comparison on</param>
        /// <typeparam name="T">The type to compare obj against</typeparam>
        /// <returns>true if obj is of type T, false otherwise.</returns>
        public static bool Is<T>(this object obj)
        {
            return obj is T;
        }

        /// <summary>
        /// Evaluates if a specified object is of the generic type provided. If it is, then the out value is set to its
        /// pattern matched type - otherwise the out value is set to default(T). 
        /// </summary>
        /// <param name="obj">An object to make a type comparison on</param>
        /// <param name="value">The out value of the pattern matched object, if it matches the generic type T.</param>
        /// <typeparam name="T">The type to compare obj against</typeparam>
        /// <returns>true if obj is of type T, false otherwise.</returns>
        public static bool Is<T>(this object obj, out T value)
        {
            if (obj is T)
            {
                T tValue = (T)obj;
                value = tValue;
                return true;
            }

            value = default(T);
            return false;
        }
    }
}
