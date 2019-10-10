using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;

#pragma warning disable 1998

namespace FunctionalLink
{
    // ----------------------------------------------------
    // RESULT
    // ----------------------------------------------------

    [DataContract]
    public partial class Result<T, TFailure>
    {
        protected Result(SuccessCase<T> success) => (IsSuccess, SuccessValue) = (true, success.Value);
        protected Result(FailureCase<TFailure> failure) => (IsSuccess, FailureValue) = (false, failure.Value);

        [DataMember] public readonly bool IsSuccess;
        [DataMember] public readonly T SuccessValue;
        [DataMember] public readonly TFailure FailureValue;

        public T Value =>
            IsSuccess
                ? SuccessValue
                : throw new InvalidOperationException("Cannot access value of 'Failure'.  Always check 'IsSuccess' first.");

        public static implicit operator Result<T, TFailure>(SuccessCase<T> success) =>
            new Result<T, TFailure>(success);

        public static implicit operator Result<T, TFailure>(FailureCase<TFailure> failure) =>
            new Result<T, TFailure>(failure);

        public static implicit operator bool(Result<T, TFailure> result) =>
            result.Match(success => true, failure => false);
    }

    // ----------------------------------------------------
    // RESULT AS IENUMERABLE
    // ----------------------------------------------------

    public partial class Result<T, TFailure>
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
        public static Result<T, TFailure> Success<T, TFailure>(this T t) =>
            new SuccessCase<T>(t);

        public static Result<T, TFailure> Failure<T, TFailure>(this TFailure failure) =>
            new FailureCase<TFailure>(failure);

        // IS SUCCESS -----------------------------------------------

        public static bool IsSuccess<T, TFailure>(this Result<T, TFailure> r) =>
            r.IsSuccess;

        public static async Task<bool> IsSuccess<T, TFailure>(this Task<Result<T, TFailure>> r) =>
            (await r).IsSuccess();

        // IS FAILURE -----------------------------------------------

        public static bool IsFailure<T, TFailure>(this Result<T, TFailure> r) =>
            !r.IsSuccess;

        public static async Task<bool> IsFailure<T, TFailure>(this Task<Result<T, TFailure>> r) =>
            (await r).IsFailure();

        // MAP -----------------------------------------------

        public static Result<B, TFailure> Map<A, B, TFailure>(this Result<A, TFailure> r, Func<A, B> f) =>
            r.Bind(t => Success<B, TFailure>(f(t)));

        public static async Task<Result<B, TFailure>> Map<A, B, TFailure>(this Result<A, TFailure> r, Func<A, Task<B>> f) =>
            await r.Bind<A, B, TFailure>(async t => Success<B, TFailure>(await f(t)));

        public static async Task<Result<B, TFailure>> Map<A, B, TFailure>(this Task<Result<A, TFailure>> r, Func<A, Task<B>> f) =>
            await (await r).Map(f);

        public static async Task<Result<B, TFailure>> Map<A, B, TFailure>(this Task<Result<A, TFailure>> r, Func<A, B> f) =>
            (await r).Map(f);

        // VOID -----------------------------------------------

        public static Result<T, TFailure> Void<T, TFailure>(this Result<T, TFailure> r, Action<T> f) =>
            r.Bind<T, T, TFailure>(t => { f(t); return r; });

        public static async Task<Result<T, TFailure>> Void<T, TFailure>(this Result<T, TFailure> r, Func<T, Task> f) =>
            await r.Bind<T, T, TFailure>(async t => { await f(t); return r; });

        public static async Task<Result<T, TFailure>> Void<T, TFailure>(this Task<Result<T, TFailure>> r, Func<T, Task> f) =>
            await (await r).Void(f);

        public static async Task<Result<T, TFailure>> Void<T, TFailure>(this Task<Result<T, TFailure>> r, Action<T> f) =>
            (await r).Void(f);

        // BIND -----------------------------------------------

        public static Result<B, TFailure> Bind<A, B, TFailure>(this Result<A, TFailure> r, Func<A, Result<B, TFailure>> f) =>
            r.Match(f, Failure<B, TFailure>);

        public static async Task<Result<B, TFailure>> Bind<A, B, TFailure>(this Result<A, TFailure> r, Func<A, Task<Result<B, TFailure>>> f) =>
            await r.Match(
                async success => await f(success),
                async failure => Failure<B, TFailure>(failure));

        public static async Task<Result<B, TFailure>> Bind<A, B, TFailure>(this Task<Result<A, TFailure>> r, Func<A, Task<Result<B, TFailure>>> f) =>
            await (await r).Bind(f);

        public static async Task<Result<B, TFailure>> Bind<A, B, TFailure>(this Task<Result<A, TFailure>> r, Func<A, Result<B, TFailure>> f) =>
            (await r).Bind(f);

        // MATCH -----------------------------------------------

        public static B Match<A, TFailure, B>(this Result<A, TFailure> r, Func<A, B> fsuccess, Func<TFailure, B> ffailure) =>
            r.IsSuccess
                ? fsuccess(r.SuccessValue)
                : ffailure(r.FailureValue);

        public static async Task<B> Match<A, TFailure, B>(this Result<A, TFailure> r, Func<A, Task<B>> fsuccess, Func<TFailure, Task<B>> ffailure) =>
            r.IsSuccess
                ? await fsuccess(r.SuccessValue)
                : await ffailure(r.FailureValue);

        public static async Task<B> Match<A, TFailure, B>(this Task<Result<A, TFailure>> r, Func<A, Task<B>> fsuccess, Func<TFailure, Task<B>> ffailure) =>
            await (await r).Match(fsuccess, ffailure);

        public static async Task<B> Match<A, TFailure, B>(this Task<Result<A, TFailure>> r, Func<A, B> fsuccess, Func<TFailure, B> ffailure) =>
            (await r).Match(fsuccess, ffailure);
    }

    // ----------------------------------------------------
    // RESULT EXTRAS
    // ----------------------------------------------------

    public static partial class ResultExtras
    {
        // NO LAMBDA --------------------------------------------

        public static Result<TResult, TFailure> Map<T, A, TResult, TFailure>(this Result<T, TFailure> r, Func<T, A, TResult> f, A a) =>
            r.Map(t => f(t, a));

        public static Result<TResult, TFailure> Map<T, A, B, TResult, TFailure>(this Result<T, TFailure> r, Func<T, A, B, TResult> f, A a, B b) =>
            r.Map(t => f(t, a, b));

        public static Result<TResult, TFailure> Map<T, A, B, C, TResult, TFailure>(this Result<T, TFailure> r, Func<T, A, B, C, TResult> f, A a, B b, C c) =>
            r.Map(t => f(t, a, b, c));

        public static Result<T, TFailure> Void<T, A, TFailure>(this Result<T, TFailure> r, Action<T, A> f, A a) =>
            r.Void(t => f(t, a));

        public static Result<T, TFailure> Void<T, A, B, TFailure>(this Result<T, TFailure> r, Action<T, A, B> f, A a, B b) =>
            r.Void(t => f(t, a, b));

        public static Result<T, TFailure> Void<T, A, B, C, TFailure>(this Result<T, TFailure> r, Action<T, A, B, C> f, A a, B b, C c) =>
            r.Void(t => f(t, a, b, c));

        public static Result<TResult, TFailure> Bind<T, A, TResult, TFailure>(this Result<T, TFailure> r, Func<T, A, Result<TResult, TFailure>> f, A a) =>
            r.Bind(t => f(t, a));

        public static Result<TResult, TFailure> Bind<T, A, B, TResult, TFailure>(this Result<T, TFailure> r, Func<T, A, B, Result<TResult, TFailure>> f, A a, B b) =>
            r.Bind(t => f(t, a, b));

        public static Result<TResult, TFailure> Bind<T, A, B, C, TResult, TFailure>(this Result<T, TFailure> r, Func<T, A, B, C, Result<TResult, TFailure>> f, A a, B b, C c) =>
            r.Bind(t => f(t, a, b, c));
    }
}

#pragma warning restore 1998
