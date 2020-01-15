using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalLink
{
    public static partial class Tuples
    {
        // questionable
        public static IEnumerable<Tuple<A, B>> Couple<A, B>(this IEnumerable<A> e, Func<A, B> builder) =>
            e.Select((a) => Tuple.Create(a, builder(a)));

        // questionable
        public static IEnumerable<Tuple<A, B, C>> Couple<A, B, C>(this IEnumerable<Tuple<A, B>> e, Func<A, B, C> builder) =>
            e.Select((t) => Tuple.Create(t.Item1, t.Item2, builder(t.Item1, t.Item2)));
    }

    public static partial class Tuples
    {
        public static IEnumerable<TResult> Select<A, B, TResult>(this IEnumerable<Tuple<A, B>> e, Func<A, B, TResult> map) =>
            e.Select((t) => map(t.Item1, t.Item2));

        public static IEnumerable<TResult> Select<A, B, C, TResult>(this IEnumerable<Tuple<A, B, C>> e, Func<A, B, C, TResult> map) =>
            e.Select((t) => map(t.Item1, t.Item2, t.Item3));

        public static IEnumerable<TResult> Select<A, B, C, D, TResult>(this IEnumerable<Tuple<A, B, C, D>> e, Func<A, B, C, D, TResult> map) =>
            e.Select((t) => map(t.Item1, t.Item2, t.Item3, t.Item4));

        public static IEnumerable<TResult> Select<A, B, C, D, E, TResult>(this IEnumerable<Tuple<A, B, C, D, E>> e, Func<A, B, C, D, E, TResult> map) =>
            e.Select((t) => map(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5));

        public static IEnumerable<TResult> Select<A, B, C, D, E, F, TResult>(this IEnumerable<Tuple<A, B, C, D, E, F>> e, Func<A, B, C, D, E, F, TResult> map) =>
            e.Select((t) => map(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5, t.Item6));

        public static IEnumerable<TResult> Select<A, B, C, D, E, F, G, TResult>(this IEnumerable<Tuple<A, B, C, D, E, F, G>> e, Func<A, B, C, D, E, F, G, TResult> map) =>
            e.Select((t) => map(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5, t.Item6, t.Item7));
    }

    public static partial class Tuples
    {
        public static IEnumerable<TResult> Select<A, B, TResult>(this IEnumerable<(A, B)> e, Func<A, B, TResult> map) =>
            e.Select((t) => map(t.Item1, t.Item2));

        public static IEnumerable<TResult> Select<A, B, C, TResult>(this IEnumerable<(A, B, C)> e, Func<A, B, C, TResult> map) =>
            e.Select((t) => map(t.Item1, t.Item2, t.Item3));

        public static IEnumerable<TResult> Select<A, B, C, D, TResult>(this IEnumerable<(A, B, C, D)> e, Func<A, B, C, D, TResult> map) =>
            e.Select((t) => map(t.Item1, t.Item2, t.Item3, t.Item4));

        public static IEnumerable<TResult> Select<A, B, C, D, E, TResult>(this IEnumerable<(A, B, C, D, E)> e, Func<A, B, C, D, E, TResult> map) =>
            e.Select((t) => map(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5));

        public static IEnumerable<TResult> Select<A, B, C, D, E, F, TResult>(this IEnumerable<(A, B, C, D, E, F)> e, Func<A, B, C, D, E, F, TResult> map) =>
            e.Select((t) => map(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5, t.Item6));

        public static IEnumerable<TResult> Select<A, B, C, D, E, F, G, TResult>(this IEnumerable<(A, B, C, D, E, F, G)> e, Func<A, B, C, D, E, F, G, TResult> map) =>
            e.Select((t) => map(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5, t.Item6, t.Item7));
    }

    public static partial class Tuples
    {
        public static Tuple<A,B,C> Extend<A,B,C>(this Tuple<A,B> t, C c) =>
            Tuple.Create(t.Item1, t.Item2, c);

        public static Tuple<A,B,C,D> Extend<A,B,C,D>(this Tuple<A,B,C> t, D d) =>
            Tuple.Create(t.Item1, t.Item2, t.Item3, d);

        public static Tuple<A,B,C,D,E> Extend<A,B,C,D,E>(this Tuple<A,B,C,D> t, E e) =>
            Tuple.Create(t.Item1, t.Item2, t.Item3, t.Item4, e);

        public static Tuple<A,B,C,D,E,F> Extend<A,B,C,D,E,F>(this Tuple<A,B,C,D,E> t, F f) =>
            Tuple.Create(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5, f);

        public static Tuple<A,B,C,D,E,F,G> Extend<A,B,C,D,E,F,G>(this Tuple<A,B,C,D,E,F> t, G g) =>
            Tuple.Create(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5, t.Item6, g);
    }

    public static partial class Tuples
    {
        public static (A,B,C) Extend<A,B,C>(this (A,B) t, C c) =>
            (t.Item1, t.Item2, c);

        public static (A,B,C,D) Extend<A,B,C,D>(this (A,B,C) t, D d) =>
            (t.Item1, t.Item2, t.Item3, d);

        public static (A,B,C,D,E) Extend<A,B,C,D,E>(this (A,B,C,D) t, E e) =>
            (t.Item1, t.Item2, t.Item3, t.Item4, e);

        public static (A,B,C,D,E,F) Extend<A,B,C,D,E,F>(this (A,B,C,D,E) t, F f) =>
            (t.Item1, t.Item2, t.Item3, t.Item4, t.Item5, f);

        public static (A,B,C,D,E,F,G) Extend<A,B,C,D,E,F,G>(this (A,B,C,D,E,F) t, G g) =>
            (t.Item1, t.Item2, t.Item3, t.Item4, t.Item5, t.Item6, g);
    }
}
