using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalLink
{
    public static class Tuples
    {
        public static IEnumerable<Tuple<A, B>> Couple<A, B>(this IEnumerable<A> e, Func<A, B> builder) =>
            e.Select((a) => Tuple.Create(a, builder(a)));

        public static IEnumerable<Tuple<A, B, C>> Couple<A, B, C>(this IEnumerable<Tuple<A, B>> e, Func<A, B, C> builder) =>
            e.Select((t) => Tuple.Create(t.Item1, t.Item2, builder(t.Item1, t.Item2)));

        public static IEnumerable<TResult> Select<A, B, TResult>(this IEnumerable<Tuple<A, B>> e, Func<A, B, TResult> deconstruct) =>
            e.Select((t) => deconstruct(t.Item1, t.Item2));

        public static IEnumerable<TResult> Select<A, B, TResult>(this IEnumerable<(A a, B b)> e, Func<A, B, TResult> deconstruct) =>
            e.Select((t) => deconstruct(t.Item1, t.Item2));

        public static IEnumerable<TResult> Select<A, B, C, TResult>(this IEnumerable<Tuple<A, B, C>> e, Func<A, B, C, TResult> deconstruct) =>
            e.Select((t) => deconstruct(t.Item1, t.Item2, t.Item3));

        public static IEnumerable<TResult> Select<A, B, C, TResult>(this IEnumerable<(A a, B b, C c)> e, Func<A, B, C, TResult> deconstruct) =>
            e.Select((t) => deconstruct(t.Item1, t.Item2, t.Item3));
    }
}