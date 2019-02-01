﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars._4kyu.FunctionalStreams
{
    /*
    A Stream is an infinite sequence of items. It is defined recursively
    as a head item followed by the tail, which is another stream.
    Consequently, the tail has to be wrapped with Lazy to prevent
    evaluation.
    */
    public class Stream<T>
    {
        public readonly T Head;
        public readonly Lazy<Stream<T>> Tail;

        public Stream(T head, Lazy<Stream<T>> tail)
        {
            Head = head;
            Tail = tail;
        }
    }
    public static class Stream
    {
        /*
            Your first task is to define a utility function which constructs a
            Stream given a head and a function returning a tail.
        */
        public static Stream<T> Cons<T>(T h, Func<Stream<T>> t)
        {
            return new Stream<T>(h, new Lazy<Stream<T>>(t));
        }

        // .------------------------------.
        // | Static constructor functions |
        // '------------------------------'

        // Construct a stream by repeating a value.
        public static Stream<T> Repeat<T>(T x)
        {
            return Cons(x, () => Repeat<T>(x));
        }

        // Construct a stream by repeatedly applying a function.
        public static Stream<T> Iterate<T>(Func<T, T> f, T x)
        {
            return Cons(x, () => Iterate<T>(f, f(x)));
        }

        //// Construct a stream by repeating an enumeration forever.
        //public static Stream<T> Cycle<T>(IEnumerable<T> a)
        //{
        //    var index = 0;
        //    return Iterate<T>(t => { return a.ElementAt(++index % a.Count()); }, a.ElementAt(0));
        //}

        // Construct a stream by repeating an enumeration forever.
        public static Stream<T> Cycle<T>(IEnumerable<T> a)
        {
            var b = a.RepeatIndefinitely();
            return Cons(b.First(), () => Cycle(b.Skip(1)));
        }

        private static IEnumerable<T> RepeatIndefinitely<T>(this IEnumerable<T> source)
        {
            while (true)
            {
                foreach (var item in source) yield return item;
            }
        }

        // Construct a stream by counting numbers starting from a given one.
        public static Stream<int> From(int x)
        {
            return Iterate<int>(i => { return i + 1; }, x);
        }

        // Same as From but count with a given step width.
        public static Stream<int> FromThen(int x, int d)
        {
            return Iterate<int>(i => { return i + d; }, x);
        }

        // .------------------------------------------.
        // | Stream reduction and modification (pure) |
        // '------------------------------------------'

        /*
            Being applied to a stream (x1, x2, x3, ...), Foldr shall return
            f(x1, f(x2, f(x3, ...))). Foldr is a right-associative fold.
            Thus applications of f are nested to the right.
        */
        public static U Foldr<T, U>(this Stream<T> s, Func<T, Func<U>, U> f)
        {
            return f(s.Head, () => s.Tail.Value.Foldr(f));
        }

        // Filter stream with a predicate function.
        public static Stream<T> Filter<T>(this Stream<T> s, Predicate<T> p)
        {
            if (p(s.Head))
                return Cons(s.Head, () => s.Tail.Value.Filter(p));
            return s.Tail.Value.Filter(p);
        }

        // Returns a given amount of elements from the stream.
        public static IEnumerable<T> Take<T>(this Stream<T> s, int n)
        {
            if (n <= 0)
                yield break;
            yield return s.Head;
            foreach (var item in s.Tail.Value.Take(n - 1))
            {
                yield return item;
            }
        }

        // Drop a given amount of elements from the stream.
        public static Stream<T> Drop<T>(this Stream<T> s, int n)
        {
            if (n <= 0)
                return s;
            return s.Tail.Value.Drop(n - 1);
        }

        // Combine 2 streams with a function.
        public static Stream<R> ZipWith<T, U, R>(this Stream<T> s, Func<T, U, R> f, Stream<U> other)
        {
            return Cons(f(s.Head, other.Head), () => s.Tail.Value.ZipWith(f, other.Tail.Value));
        }

        // Map every value of the stream with a function, returning a new stream.
        public static Stream<U> FMap<T, U>(this Stream<T> s, Func<T, U> f)
        {
            return Cons(f(s.Head), () => s.Tail.Value.FMap(f));
        }

        // Return the stream of all fibonacci numbers.
        public static Stream<int> Fib()
        {
            return Cons(0, () => Cons(1, () => Fib(0, 1)));
        }

        private static Stream<int> Fib(int n, int n1)
        {
            return Cons(n + n1, () => Fib(n1, n + n1));
        }

        // Return the stream of all prime numbers.
        public static Stream<int> Primes()
        {
            return From(2).Filter(n => IsPrime(n));
        }

        private static bool IsPrime(int n)
        {
            if (n > 3)
            {
                if (n % 2 == 0)
                    return false;
                int sqrt = (int)Math.Sqrt(n);
                for (int i = 3; i <= sqrt; i += 2)
                {
                    if (n % i == 0)
                        return false;
                }
            }
            return true;
        }
    }
}
