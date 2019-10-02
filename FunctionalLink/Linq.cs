using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalLink
{
    public static class Nolambda
    {
        public static IEnumerable<TResult> Select<T, A, TResult>(this IEnumerable<T> e, Func<T, A, TResult> map, A a) =>
            e.Select((i) => map(i, a));

        public static IEnumerable<TResult> Select<T, A, B, TResult>(this IEnumerable<T> e, Func<T, A, B, TResult> map, A a, B b) =>
            e.Select((i) => map(i, a, b));

        public static IEnumerable<TResult> Select<T, A, B, C, TResult>(this IEnumerable<T> e, Func<T, A, B, C, TResult> map, A a, B b, C c) =>
            e.Select((i) => map(i, a, b, c));

        public static IEnumerable<TResult> SelectMany<T, A, TResult>(this IEnumerable<T> enumerable, Func<T, A, IEnumerable<TResult>> f, A a) =>
            enumerable
                .SelectMany(i => f(i, a));

        public static IEnumerable<TResult> SelectMany<T, A, B, TResult>(this IEnumerable<T> enumerable, Func<T, A, B, IEnumerable<TResult>> f, A a, B b) =>
            enumerable
                .SelectMany(i => f(i, a, b));

        public static IEnumerable<TResult> SelectMany<T, A, B, C, TResult>(this IEnumerable<T> enumerable, Func<T, A, B, C, IEnumerable<TResult>> f, A a, B b, C c) =>
            enumerable
                .SelectMany(i => f(i, a, b, c));

        public static IEnumerable<T> Where<T, A>(this IEnumerable<T> e, Func<T, A, bool> f, A a) =>
            e.Where(i => f(i, a));

        public static IEnumerable<T> Where<T, A, B>(this IEnumerable<T> e, Func<T, A, B, bool> f, A a, B b) =>
            e.Where(i => f(i, a, b));

        public static IEnumerable<T> Where<T, A, B, C>(this IEnumerable<T> e, Func<T, A, B, C, bool> f, A a, B b, C c) =>
            e.Where(i => f(i, a, b, c));

        public static IEnumerable<T> Iterate<T>(this IEnumerable<T> e, Action<T> f) =>
            e.Select(i => { f(i); return i; });

        public static IEnumerable<T> Iterate<T, A>(this IEnumerable<T> e, Action<T, A> f, A a) =>
            e.Select(i => { f(i, a); return i; });

        public static IEnumerable<T> Iterate<T, A, B>(this IEnumerable<T> e, Action<T, A, B> f, A a, B b) =>
            e.Select(i => { f(i, a, b); return i; });

        public static IEnumerable<T> Iterate<T, A, B, C>(this IEnumerable<T> e, Action<T, A, B, C> f, A a, B b, C c) =>
            e.Select(i => { f(i, a, b, c); return i; });

        public static IReadOnlyCollection<T> Singleton<T>(this T t) =>
            new[] { t };

        public static IReadOnlyCollection<T> Evaluate<T>(this IEnumerable<T> e) =>
            e.ToList();

        public static void EvaluateAndIgnore<T>(this IEnumerable<T> e) =>
            e.ToList();
    }
}
