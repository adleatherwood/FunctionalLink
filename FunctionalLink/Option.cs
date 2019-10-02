using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;

#pragma warning disable 1998

namespace FunctionalLink
{
    // ----------------------------------------------------
    // SOME
    // ----------------------------------------------------

    [DataContract]
    public class SomeCase<T>
    {
        internal SomeCase(T value) => Value = value;

        [DataMember] public readonly T Value;
    }

    // ----------------------------------------------------
    // NONE
    // ----------------------------------------------------

    [DataContract]
    public class NoneCase
    {
        private NoneCase() { }

        public static readonly NoneCase Value = new NoneCase();
    }

    // ----------------------------------------------------
    // OPTION
    // ----------------------------------------------------

    [DataContract]
    public partial class Option<T>
    {
        private Option(SomeCase<T> some) => (IsSome, SomeValue) = (true, some.Value);
        private Option(NoneCase none) => (IsSome, NoneValue) = (false, none);

        [DataMember] public readonly bool IsSome;
        [DataMember] public readonly T SomeValue;
        [DataMember] public readonly NoneCase NoneValue;

        public static implicit operator Option<T>(SomeCase<T> some) =>
            some.Value != null
                ? new Option<T>(some)
                : new Option<T>(GlobalLink.None);

        public static implicit operator Option<T>(NoneCase none) =>
            new Option<T>(none);
    }

    // ----------------------------------------------------
    // OPTION AS IENUMERABLE
    // ----------------------------------------------------

    public partial class Option<T>
        : IEnumerable<T>
    {
        public IEnumerator<T> GetEnumerator() =>
            this.IsSome()
                ? new List<T> { this.SomeValue }.GetEnumerator()
                : new List<T>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }

    // ----------------------------------------------------
    // OPTION FUNCTIONS
    // ----------------------------------------------------

    public static partial class Option
    {
        public static Option<T> From<T>(this T a) =>
            new SomeCase<T>(a);

        public static Option<T> Some<T>(this T a) =>
            new SomeCase<T>(a);

        public static Option<T> None<T>() =>
            FunctionalLink.NoneCase.Value;

        // IS SOME -----------------------------------------------

        public static bool IsSome<T>(this Option<T> o) =>
            o.IsSome;

        public static async Task<bool> IsSome<T>(this Task<Option<T>> o) =>
            (await o).IsSome();

        // IS NONE -----------------------------------------------

        public static bool IsNone<T>(this Option<T> o) =>
            !o.IsSome;

        public static async Task<bool> IsNone<T>(this Task<Option<T>> o) =>
            (await o).IsNone();

        // MAP -----------------------------------------------

        public static Option<B> Map<A, B>(this Option<A> o, Func<A, B> f) =>
            o.Bind(t => Some(f(t)));

        public static async Task<Option<B>> Map<A, B>(this Option<A> o, Func<A, Task<B>> f) =>
            await o.Bind(async t => Some(await f(t)));

        public static async Task<Option<B>> Map<A, B>(this Task<Option<A>> o, Func<A, Task<B>> f) =>
            await (await o).Map(f);

        public static async Task<Option<B>> Map<A, B>(this Task<Option<A>> o, Func<A, B> f) =>
            (await o).Map(f);

        // VOID -----------------------------------------------

        public static Option<T> Void<T>(this Option<T> o, Action<T> f) =>
            o.Bind<T, T>(t => { f(t); return o; });

        public static async Task<Option<T>> Void<T>(this Option<T> o, Func<T, Task> f) =>
            await o.Bind<T, T>(async t => { await f(t); return o; });

        public static async Task<Option<T>> Void<T>(this Task<Option<T>> o, Func<T, Task> f) =>
            await (await o).Void(f);

        public static async Task<Option<T>> Void<T>(this Task<Option<T>> o, Action<T> f) =>
            (await o).Void(f);

        // BIND -----------------------------------------------

        public static Option<B> Bind<A, B>(this Option<A> o, Func<A, Option<B>> f) =>
            o.Match(f, none => none);

        public static async Task<Option<B>> Bind<A, B>(this Option<A> o, Func<A, Task<Option<B>>> f) =>
            await o.Match<A, Option<B>>( // TODO: why are the types required here?
                async some => await f(some),
                async none => none);

        public static async Task<Option<B>> Bind<A, B>(this Task<Option<A>> o, Func<A, Task<Option<B>>> f) =>
            await (await o).Bind(f);

        public static async Task<Option<B>> Bind<A, B>(this Task<Option<A>> o, Func<A, Option<B>> f) =>
            (await o).Bind(f);

        // MATCH -----------------------------------------------

        public static B Match<A, B>(this Option<A> o, Func<A, B> fsome, Func<NoneCase, B> fnone) =>
            o.IsSome
                ? fsome(o.SomeValue)
                : fnone(o.NoneValue);

        public static async Task<B> Match<A, B>(this Option<A> o, Func<A, Task<B>> fsome, Func<NoneCase, Task<B>> fnone) =>
            o.IsSome
                ? await fsome(o.SomeValue)
                : await fnone(o.NoneValue);

        public static async Task<B> Match<A, B>(this Task<Option<A>> o, Func<A, Task<B>> fsome, Func<NoneCase, Task<B>> fnone) =>
            await (await o).Match(fsome, fnone);

        public static async Task<B> Match<A, B>(this Task<Option<A>> o, Func<A, B> fsome, Func<NoneCase, B> fnone) =>
            (await o).Match(fsome, fnone);
    }

    // ----------------------------------------------------
    // OPTION EXTRAS
    // ----------------------------------------------------

    public static partial class OptionExtras
    {
        // VALUE OR DEFAULT -----------------------------------------------

        public static T ValueOrDefault<T>(this Option<T> o) =>
            o.Match(
                some => some,
                none => default);

        public static async Task<T> ValueOrDefault<T>(this Task<Option<T>> o) =>
            (await o).ValueOrDefault();

        // FILTER -----------------------------------------------

        public static Option<T> Filter<T>(this Option<T> o, Func<T, bool> f) =>
            o.Bind(t => f(t) ? o : GlobalLink.None);

        public static async Task<Option<T>> Filter<T>(this Option<T> o, Func<T, Task<bool>> f) =>
            await o.Bind(async t => await f(t) ? o : GlobalLink.None);

        public static async Task<Option<T>> Filter<T>(this Task<Option<T>> o, Func<T, Task<bool>> f) =>
            await (await o).Filter(f);

        public static async Task<Option<T>> Filter<T>(this Task<Option<T>> o, Func<T, bool> f) =>
            (await o).Filter(f);

        // NO LAMBDA --------------------------------------------

        public static Option<TResult> Map<T, A, TResult>(this Option<T> o, Func<T, A, TResult> f, A a) =>
            o.Map(t => f(t, a));

        public static Option<TResult> Map<T, A, B, TResult>(this Option<T> o, Func<T, A, B, TResult> f, A a, B b) =>
            o.Map(t => f(t, a, b));

        public static Option<TResult> Map<T, A, B, C, TResult>(this Option<T> o, Func<T, A, B, C, TResult> f, A a, B b, C c) =>
            o.Map(t => f(t, a, b, c));

        public static Option<T> Void<T, A>(this Option<T> o, Action<T, A> f, A a) =>
            o.Void(t => f(t, a));

        public static Option<T> Void<T, A, B>(this Option<T> o, Action<T, A, B> f, A a, B b) =>
            o.Void(t => f(t, a, b));

        public static Option<T> Void<T, A, B, C>(this Option<T> o, Action<T, A, B, C> f, A a, B b, C c) =>
            o.Void(t => f(t, a, b, c));

        public static Option<TResult> Bind<T, A, TResult>(this Option<T> o, Func<T, A, Option<TResult>> f, A a) =>
            o.Bind(t => f(t, a));

        public static Option<TResult> Bind<T, A, B, TResult>(this Option<T> o, Func<T, A, B, Option<TResult>> f, A a, B b) =>
            o.Bind(t => f(t, a, b));

        public static Option<TResult> Bind<T, A, B, C, TResult>(this Option<T> o, Func<T, A, B, C, Option<TResult>> f, A a, B b, C c) =>
            o.Bind(t => f(t, a, b, c));
    }
}

#pragma warning restore 1998