/**************************************************************************************
 * Original Author : Anthony Leatherwood (adleatherwood@gmail.com)                              
 * Source Location : https://gitlab.com/adleatherwood/FunctionalLink
 *  
 * This source is subject to the GNU GPL3 License.
 * Link: https://gitlab.com/adleatherwood/FunctionalLink/blob/develop/LICENSE
 *  
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
 * EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
 *
 * Version: <--version--> 
 **************************************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InvokeAsExtensionMethod
// ReSharper disable UnusedMember.Global
// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming

#pragma warning disable IDE1006 // Naming Styles

namespace FunctionalLink
{
    #region enumerables

    public static class nolambda
    {
        [DebuggerStepThrough]
        public static IEnumerable<TResult> Select<T, A, TResult>(this IEnumerable<T> e, Func<T, A, TResult> map, A a) =>
            e.Select((i) => map(i, a));

        [DebuggerStepThrough]
        public static IEnumerable<TResult> Select<T, A, B, TResult>(this IEnumerable<T> e, Func<T, A, B, TResult> map, A a, B b) =>
            e.Select((i) => map(i, a, b));

        [DebuggerStepThrough]
        public static IEnumerable<TResult> Select<T, A, B, C, TResult>(this IEnumerable<T> e, Func<T, A, B, C, TResult> map, A a, B b, C c) =>
            e.Select((i) => map(i, a, b, c));

        [DebuggerStepThrough]
        public static IEnumerable<TResult> SelectMany<T, A, TResult>(this IEnumerable<T> enumerable, Func<T, A, IEnumerable<TResult>> f, A a) =>
            enumerable
                .SelectMany(i => f(i, a));

        [DebuggerStepThrough]
        public static IEnumerable<TResult> SelectMany<T, A, B, TResult>(this IEnumerable<T> enumerable, Func<T, A, B, IEnumerable<TResult>> f, A a, B b) =>
            enumerable
                .SelectMany(i => f(i, a, b));            

        [DebuggerStepThrough]
        public static IEnumerable<TResult> SelectMany<T, A, B, C, TResult>(this IEnumerable<T> enumerable, Func<T, A, B, C, IEnumerable<TResult>> f, A a, B b, C c) =>
            enumerable
                .SelectMany(i => f(i, a, b, c));            

        [DebuggerStepThrough]
        public static IEnumerable<T> Where<T, A>(this IEnumerable<T> e, Func<T, A, bool> f, A a) =>
            e.Where(i => f(i, a));

        [DebuggerStepThrough]
        public static IEnumerable<T> Where<T, A, B>(this IEnumerable<T> e, Func<T, A, B, bool> f, A a, B b) =>
            e.Where(i => f(i, a, b));

        [DebuggerStepThrough]
        public static IEnumerable<T> Where<T, A, B, C>(this IEnumerable<T> e, Func<T, A, B, C, bool> f, A a, B b, C c) =>
            e.Where(i => f(i, a, b, c));

        [DebuggerStepThrough]
        public static IEnumerable<T> Iterate<T>(this IEnumerable<T> e, Action<T> f) =>
            e.Select(i => { f(i); return i; });

        [DebuggerStepThrough]
        public static IEnumerable<T> Iterate<T, A>(this IEnumerable<T> e, Action<T, A> f, A a) =>
            e.Select(i => { f(i, a); return i; });

        [DebuggerStepThrough]
        public static IEnumerable<T> Iterate<T, A, B>(this IEnumerable<T> e, Action<T, A, B> f, A a, B b) =>
            e.Select(i => { f(i, a, b); return i; });

        [DebuggerStepThrough]
        public static IEnumerable<T> Iterate<T, A, B, C>(this IEnumerable<T> e, Action<T, A, B, C> f, A a, B b, C c) =>
            e.Select(i => { f(i, a, b, c); return i; });

        [DebuggerStepThrough]
        public static IReadOnlyCollection<T> Singleton<T>(this T t) =>
            new[] { t };

        [DebuggerStepThrough]
        public static IReadOnlyCollection<T> Evaluate<T>(this IEnumerable<T> e) =>
            e.ToList();

        [DebuggerStepThrough]
        public static void EvaluateAndIgnore<T>(this IEnumerable<T> e) =>
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            e.ToList();
    }

    #endregion
    #region tuples

    public static class Tuples
    {
        [DebuggerStepThrough]
        public static IEnumerable<Tuple<TA, TResult>> Build<TA, TResult>(this IEnumerable<TA> e, Func<TA, TResult> builder) =>
            e.Select((a) => Tuple.Create(a, builder(a)));

        [DebuggerStepThrough]
        public static IEnumerable<Tuple<TA, TB, TResult>> Build<TA, TB, TResult>(this IEnumerable<Tuple<TA, TB>> e, Func<TA, TB, TResult> builder) =>
            e.Select((t) => Tuple.Create(t.Item1, t.Item2, builder(t.Item1, t.Item2)));
    
        [DebuggerStepThrough]
        public static IEnumerable<TResult> Select<TA, TB, TResult>(this IEnumerable<Tuple<TA, TB>> e, Func<TA, TB, TResult> deconstruct) =>
            e.Select((t) => deconstruct(t.Item1, t.Item2));

        [DebuggerStepThrough]
        public static IEnumerable<TResult> Select<TA, TB, TC, TResult>(this IEnumerable<Tuple<TA, TB, TC>> e, Func<TA, TB, TC, TResult> deconstruct) =>
            e.Select((t) => deconstruct(t.Item1, t.Item2, t.Item3));
    }

    #endregion
    #region initializers

    public static class Init
    {        
        public static List<T> list<T>(params T[] values) =>
            values.ToList();

        public static List<T> lst<T>(params T[] values) =>
            values.ToList();

        public static T[] array<T>(params T[] values) =>
            values;

        public static T[] arr<T>(params T[] values) =>
            values;

        public static Dictionary<K, V> dict<K, V>(params Tuple<K, V>[] records) =>
            records.ToDictionary(t => t.Item1, t => t.Item2);

        public static Dictionary<K, V> dict<K, V>(params ValueTuple<K, V>[] records) =>
            records.ToDictionary(t => t.Item1, t => t.Item2);
    }

    #endregion        
}

#pragma warning restore IDE1006 // Naming Styles