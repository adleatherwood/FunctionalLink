using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;

#pragma warning disable 1998

namespace FunctionalLink
{
    // ----------------------------------------------------
    // SUCCESS
    // ----------------------------------------------------

    [DataContract]
    public class SuccessCase<T>
    {
        internal SuccessCase(T value) => Value = value;

        [DataMember] public readonly T Value;
    }

    // ----------------------------------------------------
    // FAILURE
    // ----------------------------------------------------

    [DataContract]
    public class FailureCase<T>
    {
        internal FailureCase(T value) => Value = value;

        [DataMember] public readonly T Value;
    }

    // ----------------------------------------------------
    // RESULT
    // ----------------------------------------------------

    [DataContract]
    public partial class Result<T>
    {
        protected Result(SuccessCase<T> success) => (IsSuccess, SuccessValue) = (true, success.Value);
        protected Result(FailureCase<string> failure) => (IsSuccess, FailureValue) = (false, failure.Value);

        [DataMember] public readonly bool IsSuccess;
        [DataMember] public readonly T SuccessValue;
        [DataMember] public readonly string FailureValue;

        public static implicit operator Result<T>(SuccessCase<T> success) =>
            new Result<T>(success);

        public static implicit operator Result<T>(FailureCase<string> failure) =>
            new Result<T>(failure);
    }

    // ----------------------------------------------------
    // RESULT AS IENUMERABLE
    // ----------------------------------------------------

    public partial class Result<T>
        : IEnumerable<T>
    {
        public IEnumerator<T> GetEnumerator() =>
            this.IsSuccess
                ? new List<T> { this.SuccessValue }.GetEnumerator()
                : new List<T>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }

    // ----------------------------------------------------
    // RESULT FUNCTIONS
    // ----------------------------------------------------

    public static partial class Result
    {
        public static Result<T> Success<T>(this T t) =>
            new SuccessCase<T>(t);

        public static Result<T> Failure<T>(this string failure) =>
            new FailureCase<string>(failure);

        // IS SUCCESS -----------------------------------------------

        public static bool IsSuccess<T>(this Result<T> r) =>
            r.IsSuccess;

        public static async Task<bool> IsSuccess<T>(this Task<Result<T>> r) =>
            (await r).IsSuccess();

        // IS FAILURE -----------------------------------------------

        public static bool IsFailure<T>(this Result<T> r) =>
            !r.IsSuccess;

        public static async Task<bool> IsFailure<T>(this Task<Result<T>> r) =>
            (await r).IsFailure();

        // MAP -----------------------------------------------

        public static Result<B> Map<A, B>(this Result<A> r, Func<A, B> f) =>
            r.Bind(t => Success<B>(f(t)));

        public static async Task<Result<B>> Map<A, B>(this Result<A> r, Func<A, Task<B>> f) =>
            await r.Bind<A, B>(async t => Success<B>(await f(t)));

        public static async Task<Result<B>> Map<A, B>(this Task<Result<A>> r, Func<A, Task<B>> f) =>
            await (await r).Map(f);

        public static async Task<Result<B>> Map<A, B>(this Task<Result<A>> r, Func<A, B> f) =>
            (await r).Map(f);

        // VOID -----------------------------------------------

        public static Result<T> Void<T>(this Result<T> r, Action<T> f) =>
            r.Bind(t => { f(t); return r; });

        public static async Task<Result<T>> Void<T>(this Result<T> r, Func<T, Task> f) =>
            await r.Bind<T, T>(async t => { await f(t); return r; });

        public static async Task<Result<T>> Void<T>(this Task<Result<T>> r, Func<T, Task> f) =>
            await (await r).Void(f);

        public static async Task<Result<T>> Void<T>(this Task<Result<T>> r, Action<T> f) =>
            (await r).Void(f);

        // BIND -----------------------------------------------

        public static Result<B> Bind<A, B>(this Result<A> r, Func<A, Result<B>> f) =>
            r.Match(f, Failure<B>);

        public static async Task<Result<B>> Bind<A, B>(this Result<A> r, Func<A, Task<Result<B>>> f) =>
            await r.Match(
                async success => await f(success),
                async failure => Failure<B>(failure));

        public static async Task<Result<B>> Bind<A, B>(this Task<Result<A>> r, Func<A, Task<Result<B>>> f) =>
            await (await r).Bind(f);

        public static async Task<Result<B>> Bind<A, B>(this Task<Result<A>> r, Func<A, Result<B>> f) =>
            (await r).Bind(f);

        // MATCH -----------------------------------------------

        public static B Match<A, B>(this Result<A> r, Func<A, B> fsuccess, Func<string, B> ffailure) =>
            r.IsSuccess
                ? fsuccess(r.SuccessValue)
                : ffailure(r.FailureValue);

        public static async Task<B> Match<A, B>(this Result<A> r, Func<A, Task<B>> fsuccess, Func<string, Task<B>> ffailure) =>
            r.IsSuccess
                ? await fsuccess(r.SuccessValue)
                : await ffailure(r.FailureValue);

        public static async Task<B> Match<A, B>(this Task<Result<A>> r, Func<A, Task<B>> fsuccess, Func<string, Task<B>> ffailure) =>
            await (await r).Match(fsuccess, ffailure);

        public static async Task<B> Match<A, B>(this Task<Result<A>> r, Func<A, B> fsuccess, Func<string, B> ffailure) =>
            (await r).Match(fsuccess, ffailure);
    }

    // ----------------------------------------------------
    // RESULT EXTRAS
    // ----------------------------------------------------

    public static partial class ResultExtras
    {
        // NO LAMBDA --------------------------------------------

        public static Result<TResult> Map<T, A, TResult>(this Result<T> r, Func<T, A, TResult> f, A a) =>
            r.Map(t => f(t, a));

        public static Result<TResult> Map<T, A, B, TResult>(this Result<T> r, Func<T, A, B, TResult> f, A a, B b) =>
            r.Map(t => f(t, a, b));

        public static Result<TResult> Map<T, A, B, C, TResult>(this Result<T> r, Func<T, A, B, C, TResult> f, A a, B b, C c) =>
            r.Map(t => f(t, a, b, c));

        public static Result<T> Void<T, A>(this Result<T> r, Action<T, A> f, A a) =>
            r.Void(t => f(t, a));

        public static Result<T> Void<T, A, B>(this Result<T> r, Action<T, A, B> f, A a, B b) =>
            r.Void(t => f(t, a, b));

        public static Result<T> Void<T, A, B, C>(this Result<T> r, Action<T, A, B, C> f, A a, B b, C c) =>
            r.Void(t => f(t, a, b, c));

        public static Result<TResult> Bind<T, A, TResult>(this Result<T> r, Func<T, A, Result<TResult>> f, A a) =>
            r.Bind(t => f(t, a));

        public static Result<TResult> Bind<T, A, B, TResult>(this Result<T> r, Func<T, A, B, Result<TResult>> f, A a, B b) =>
            r.Bind(t => f(t, a, b));

        public static Result<TResult> Bind<T, A, B, C, TResult>(this Result<T> r, Func<T, A, B, C, Result<TResult>> f, A a, B b, C c) =>
            r.Bind(t => f(t, a, b, c));
    }
}

#pragma warning restore 1998
