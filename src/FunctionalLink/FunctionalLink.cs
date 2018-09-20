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
    #region self

    public static class Self
    {
        [DebuggerStepThrough]
        public static T Id<T>(T t) => t;

        [DebuggerStepThrough]
        public static void Ignore<T>(this T t) { }
    }

    #endregion
    #region unions

    [DataContract]
    public class Union<T>
    {
        [DebuggerStepThrough] public Union(T value) => (Value, Tag) = (value, 0);

        [DataMember] public readonly T Value;
        [DataMember] public readonly int Tag;
    }

    [DataContract]
    public class Union<TA, TB>
    {
        [DebuggerStepThrough] public Union(TA value) => (Value, Tag) = (value, 0);
        [DebuggerStepThrough] public Union(TB valueB) => (ValueB, Tag) = (valueB, 1);

        [DataMember] public readonly TA Value;
        [DataMember] public readonly TB ValueB;
        [DataMember] public readonly int Tag;

        [DebuggerStepThrough]
        public static implicit operator Union<TA, TB>(TA value) =>
            new Union<TA, TB>(value);

        [DebuggerStepThrough]
        public static implicit operator Union<TA, TB>(TB valueB) =>
            new Union<TA, TB>(valueB);
    }

    [DataContract]
    public class Union<TA, TB, TC>
    {
        [DebuggerStepThrough] public Union(TA value) => (Value, Tag) = (value, 0);
        [DebuggerStepThrough] public Union(TB valueB) => (ValueB, Tag) = (valueB, 1);
        [DebuggerStepThrough] public Union(TC valueC) => (ValueC, Tag) = (valueC, 2);

        [DataMember] public readonly TA Value;
        [DataMember] public readonly TB ValueB;
        [DataMember] public readonly TC ValueC;
        [DataMember] public readonly int Tag;

        [DebuggerStepThrough]
        public static implicit operator Union<TA, TB, TC>(TA value) =>
            new Union<TA, TB, TC>(value);

        [DebuggerStepThrough]
        public static implicit operator Union<TA, TB, TC>(TB valueB) =>
            new Union<TA, TB, TC>(valueB);

        [DebuggerStepThrough]
        public static implicit operator Union<TA, TB, TC>(TC valueC) =>
            new Union<TA, TB, TC>(valueC);
    }

    [DataContract]
    public class Union<TA, TB, TC, TD>
    {
        [DebuggerStepThrough] public Union(TA value) => (Value, Tag) = (value, 0);
        [DebuggerStepThrough] public Union(TB valueB) => (ValueB, Tag) = (valueB, 1);
        [DebuggerStepThrough] public Union(TC valueC) => (ValueC, Tag) = (valueC, 2);
        [DebuggerStepThrough] public Union(TD valueD) => (ValueD, Tag) = (valueD, 3);

        [DataMember] public readonly TA Value;
        [DataMember] public readonly TB ValueB;
        [DataMember] public readonly TC ValueC;
        [DataMember] public readonly TD ValueD;
        [DataMember] public readonly int Tag;

        [DebuggerStepThrough]
        public static implicit operator Union<TA, TB, TC, TD>(TA value) =>
            new Union<TA, TB, TC, TD>(value);

        [DebuggerStepThrough]
        public static implicit operator Union<TA, TB, TC, TD>(TB valueB) =>
            new Union<TA, TB, TC, TD>(valueB);

        [DebuggerStepThrough]
        public static implicit operator Union<TA, TB, TC, TD>(TC valueC) =>
            new Union<TA, TB, TC, TD>(valueC);

        [DebuggerStepThrough]
        public static implicit operator Union<TA, TB, TC, TD>(TD valueD) =>
            new Union<TA, TB, TC, TD>(valueD);
    }

    public class Union<TA, TB, TC, TD, TE>
    {
        [DebuggerStepThrough] public Union(TA value) => (Value, Tag) = (value, 0);
        [DebuggerStepThrough] public Union(TB valueB) => (ValueB, Tag) = (valueB, 1);
        [DebuggerStepThrough] public Union(TC valueC) => (ValueC, Tag) = (valueC, 2);
        [DebuggerStepThrough] public Union(TD valueD) => (ValueD, Tag) = (valueD, 3);
        [DebuggerStepThrough] public Union(TE valueE) => (ValueE, Tag) = (valueE, 4);

        [DataMember] public readonly TA Value;
        [DataMember] public readonly TB ValueB;
        [DataMember] public readonly TC ValueC;
        [DataMember] public readonly TD ValueD;
        [DataMember] public readonly TE ValueE;
        [DataMember] public readonly int Tag;

        [DebuggerStepThrough]
        public static implicit operator Union<TA, TB, TC, TD, TE>(TA value) =>
            new Union<TA, TB, TC, TD, TE>(value);

        [DebuggerStepThrough]
        public static implicit operator Union<TA, TB, TC, TD, TE>(TB valueB) =>
            new Union<TA, TB, TC, TD, TE>(valueB);

        [DebuggerStepThrough]
        public static implicit operator Union<TA, TB, TC, TD, TE>(TC valueC) =>
            new Union<TA, TB, TC, TD, TE>(valueC);

        [DebuggerStepThrough]
        public static implicit operator Union<TA, TB, TC, TD, TE>(TD valueD) =>
            new Union<TA, TB, TC, TD, TE>(valueD);

        [DebuggerStepThrough]
        public static implicit operator Union<TA, TB, TC, TD, TE>(TE valueE) =>
            new Union<TA, TB, TC, TD, TE>(valueE);
    }

    public static class Union
    {
        [DebuggerStepThrough]
        public static TResult Match<T, TResult>(this Union<T> u, Func<T, TResult> fa) =>
            fa(u.Value);

        [DebuggerStepThrough]
        public static TResult Match<T, B, TResult>(this Union<T, B> u, Func<T, TResult> fa, Func<B, TResult> fb) =>
            u.Tag == 0
                ? fa(u.Value)
                : fb(u.ValueB);

        [DebuggerStepThrough]
        public static TResult Match<T, B, C, TResult>(this Union<T, B, C> u, Func<T, TResult> fa, Func<B, TResult> fb, Func<C, TResult> fc) =>
            u.Tag == 0
                ? fa(u.Value)
                : u.Tag == 1
                    ? fb(u.ValueB)
                    : fc(u.ValueC);

        [DebuggerStepThrough]
        public static TResult Match<T, B, C, D, TResult>(this Union<T, B, C, D> u, Func<T, TResult> fa, Func<B, TResult> fb, Func<C, TResult> fc, Func<D, TResult> fd) =>
            u.Tag == 0
                ? fa(u.Value)
                : u.Tag == 1
                    ? fb(u.ValueB)
                    : u.Tag == 2
                        ? fc(u.ValueC)
                        : fd(u.ValueD);

        [DebuggerStepThrough]
        public static TResult Match<T, B, C, D, E, TResult>(this Union<T, B, C, D, E> u, Func<T, TResult> fa, Func<B, TResult> fb, Func<C, TResult> fc, Func<D, TResult> fd, Func<E, TResult> fe) =>
            u.Tag == 0
                ? fa(u.Value)
                : u.Tag == 1
                    ? fb(u.ValueB)
                    : u.Tag == 2
                        ? fc(u.ValueC)
                        : u.Tag == 3
                            ? fd(u.ValueD)
                            : fe(u.ValueE);
    }

    public static class UnionTask
    {
        [DebuggerStepThrough]
        public static async Task<TResult> Match<TA, TResult>(this Task<Union<TA>> u, Func<TA, TResult> fa) =>
            (await u).Match(fa);

        [DebuggerStepThrough]
        public static async Task<TResult> Match<TA, TResult>(this Task<Union<TA>> u, Func<TA, Task<TResult>> fa) =>
            await fa((await u).Value);

        [DebuggerStepThrough]
        public static async Task<TResult> Match<TA, TB, TResult>(this Task<Union<TA, TB>> u, Func<TA, TResult> fa, Func<TB, TResult> fb) =>
            (await u).Match(fa, fb);

        [DebuggerStepThrough]
        public static async Task<TResult> Match<TA, TB, TResult>(this Task<Union<TA, TB>> u, Func<TA, Task<TResult>> fa, Func<TB, Task<TResult>> fb) =>
            await (await u).Match(
                async a => await fa(a),
                async b => await fb(b));

        [DebuggerStepThrough]
        public static async Task<TResult> Match<TA, TB, TC, TResult>(this Task<Union<TA, TB, TC>> u, Func<TA, TResult> fa, Func<TB, TResult> fb, Func<TC, TResult> fc) =>
            (await u).Match(fa, fb, fc);

        [DebuggerStepThrough]
        public static async Task<TResult> Match<TA, TB, TC, TResult>(this Task<Union<TA, TB, TC>> u, Func<TA, Task<TResult>> fa, Func<TB, Task<TResult>> fb, Func<TC, Task<TResult>> fc) =>
            await (await u).Match(
                async a => await fa(a),
                async b => await fb(b),
                async c => await fc(c));

        [DebuggerStepThrough]
        public static async Task<TResult> Match<TA, TB, TC, TD, TResult>(this Task<Union<TA, TB, TC, TD>> u, Func<TA, TResult> fa, Func<TB, TResult> fb, Func<TC, TResult> fc, Func<TD, TResult> fd) =>
            (await u).Match(fa, fb, fc, fd);

        [DebuggerStepThrough]
        public static async Task<TResult> Match<TA, TB, TC, TD, TResult>(this Task<Union<TA, TB, TC, TD>> u, Func<TA, Task<TResult>> fa, Func<TB, Task<TResult>> fb, Func<TC, Task<TResult>> fc, Func<TD, Task<TResult>> fd) =>
            await (await u).Match(
                async a => await fa(a),
                async b => await fb(b),
                async c => await fc(c),
                async d => await fd(d));

        [DebuggerStepThrough]
        public static async Task<TResult> Match<TA, TB, TC, TD, TE, TResult>(this Task<Union<TA, TB, TC, TD, TE>> u, Func<TA, TResult> fa, Func<TB, TResult> fb, Func<TC, TResult> fc, Func<TD, TResult> fd, Func<TE, TResult> fe) =>
            (await u).Match(fa, fb, fc, fd, fe);

        [DebuggerStepThrough]
        public static async Task<TResult> Match<TA, TB, TC, TD, TE, TResult>(this Task<Union<TA, TB, TC, TD, TE>> u, Func<TA, Task<TResult>> fa, Func<TB, Task<TResult>> fb, Func<TC, Task<TResult>> fc, Func<TD, Task<TResult>> fd, Func<TE, Task<TResult>> fe) =>
            await (await u).Match(
                async a => await fa(a),
                async b => await fb(b),
                async c => await fc(c),
                async d => await fd(d),
                async e => await fe(e));
    }

    #endregion
    #region option

    [DataContract]
    public sealed class None
    {
        [DebuggerStepThrough]
        internal None() { }
    }

    [DataContract]
    public class Option<T>
        : Union<T, None>
    {
        [DebuggerStepThrough] public Option(T item) : base(item) { }

        [DebuggerStepThrough] public Option(None item) : base(item) { }

        [DebuggerStepThrough]
        public static implicit operator Option<T>(T t) =>
            new Option<T>(t);

        [DebuggerStepThrough]
        public static implicit operator Option<T>(None n) =>
            new Option<T>(n);
    }

    public static class Option
    {    
        [DebuggerStepThrough]
        public static Option<T> From<T>(this T t) =>
            t != null ? Some(t) : None;

        [DebuggerStepThrough]
        public static Option<T> Some<T>(this T t) =>
            new Option<T>(t);

        public static readonly None None = new None();
        
        [DebuggerStepThrough]
        public static bool IsSome<T>(this Option<T> o) =>
            o.Match(
                some => true,
                none => false);

        [DebuggerStepThrough]
        public static bool IsNone<T>(this Option<T> o) =>
            o.Match(
                some => false,
                none => true);

        [DebuggerStepThrough]
        public static Option<TResult> Map<T, TResult>(this Option<T> o, Func<T, TResult> f) =>
            o.Next(t => Some(f(t)));

        [DebuggerStepThrough]
        public static Option<T> Void<T>(this Option<T> o, Action<T> f) =>
            o.Next<T, T>(t => { f(t); return t; });

        [DebuggerStepThrough]
        public static Option<TResult> Next<T, TResult>(this Option<T> o, Func<T, Option<TResult>> f) =>
            o.Match(
                f,
                none => None);

        [DebuggerStepThrough]
        public static T ValueOrEffect<T>(this Option<T> o, Action f)
        {
            if (o.IsNone())
                f();
            return o.Value;
        }
    }

    public static class OptionTask
    {
        [DebuggerStepThrough]
        public static async Task<Option<TResult>> Map<T, TResult>(this Task<Option<T>> o, Func<T, TResult> f) =>
            await o.Next(t => Option.Some(f(t)));

        [DebuggerStepThrough]
        public static async Task<Option<TResult>> Map<T, TResult>(this Task<Option<T>> o, Func<T, Task<TResult>> f) =>
            await o.Next(async t => Option.Some(await f(t)));

        [DebuggerStepThrough]
        public static async Task<Option<T>> Void<T>(this Task<Option<T>> o, Action<T> f) =>
            await o.Next(t => { f(t); return Option.Some(t); });

        [DebuggerStepThrough]
        public static async Task<Option<T>> Void<T>(this Task<Option<T>> o, Func<T, Task> f) =>
            await o.Next(async t => { await f(t); return Option.Some(t); });

        [DebuggerStepThrough]
        public static async Task<Option<TResult>> Next<T, TResult>(this Task<Option<T>> o, Func<T, Option<TResult>> f) =>
            (await o).Next(f);

        [DebuggerStepThrough]
        public static async Task<Option<TResult>> Next<T, TResult>(this Task<Option<T>> o, Func<T, Task<Option<TResult>>> f) =>
            await (await o).Match(
                async some => await f(some),
#pragma warning disable 1998
                async none => new Option<TResult>(Option.None));
#pragma warning restore 1998

        [DebuggerStepThrough]
        public static async Task<T> ValueOrEffect<T>(this Task<Option<T>> o, Action f) =>
            (await o).ValueOrEffect(f);

        [DebuggerStepThrough]
        public static async Task<T> ValueOrEffect<T>(this Task<Option<T>> o, Func<Task> f)
        {
            var awaited = await o;
            if (awaited.IsNone())
                await f();
            return awaited.Value;
        }
    }

    public static class OptionNoLamda
    {
        [DebuggerStepThrough]
        public static Option<TResult> Map<T, A, TResult>(this Option<T> o, Func<T, A, TResult> f, A a) =>
            o.Map(t => f(t, a));

        [DebuggerStepThrough]
        public static Option<TResult> Map<T, A, B, TResult>(this Option<T> o, Func<T, A, B, TResult> f, A a, B b) =>
            o.Map(t => f(t, a, b));

        [DebuggerStepThrough]
        public static Option<TResult> Map<T, A, B, C, TResult>(this Option<T> o, Func<T, A, B, C, TResult> f, A a, B b, C c) =>
            o.Map(t => f(t, a, b, c));

        [DebuggerStepThrough]
        public static Option<T> Void<T, A>(this Option<T> o, Action<T, A> f, A a) =>
            o.Void(t => f(t, a));

        [DebuggerStepThrough]
        public static Option<T> Void<T, A, B>(this Option<T> o, Action<T, A, B> f, A a, B b) =>
            o.Void(t => f(t, a, b));

        [DebuggerStepThrough]
        public static Option<T> Void<T, A, B, C>(this Option<T> o, Action<T, A, B, C> f, A a, B b, C c) =>
            o.Void(t => f(t, a, b, c));

        [DebuggerStepThrough]
        public static Option<TResult> Next<T, A, TResult>(this Option<T> o, Func<T, A, Option<TResult>> f, A a) =>
            o.Next(t => f(t, a));

        [DebuggerStepThrough]
        public static Option<TResult> Next<T, A, B, TResult>(this Option<T> o, Func<T, A, B, Option<TResult>> f, A a, B b) =>
            o.Next(t => f(t, a, b));

        [DebuggerStepThrough]
        public static Option<TResult> Next<T, A, B, C, TResult>(this Option<T> o, Func<T, A, B, C, Option<TResult>> f, A a, B b, C c) =>
            o.Next(t => f(t, a, b, c));
    }

    #endregion
    #region result

    [DataContract]
    public class Result<T>
        : Result<T, string>        
    {
        [DebuggerStepThrough] internal Result(T success) : base(success) { }
        [DebuggerStepThrough] internal Result(string failure) : base(failure) { }

        [DebuggerStepThrough]
        public static implicit operator Result<T>(T t) =>
            new Result<T>(t);

        [DebuggerStepThrough]
        public static implicit operator Result<T>(string message) =>
            new Result<T>(message);
    }

    [DataContract]
    public class Result<T, TFailure>
        : Union<T, TFailure>        
    {
        [DebuggerStepThrough] internal Result(T success) : base(success) { }

        [DebuggerStepThrough] internal Result(TFailure failure) : base(failure) { }

        [DataMember] public T Success => Value;
        [DataMember] public TFailure Failure => ValueB;

        [DebuggerStepThrough]
        public static implicit operator Result<T, TFailure>(T t) =>
            new Result<T, TFailure>(t);

        [DebuggerStepThrough]
        public static implicit operator Result<T, TFailure>(TFailure failure) =>
            new Result<T, TFailure>(failure);
    }

    public static class Result
    {
        [DebuggerStepThrough]
        public static Result<T> Success<T>(this T t) =>
            new Result<T>(t);

        [DebuggerStepThrough]
        public static Result<T> Failure<T>(this string message) =>
            new Result<T>(message);

        [DebuggerStepThrough]
        public static Result<T, TFailure> Success<T, TFailure>(this T t) =>
            new Result<T, TFailure>(t);

        [DebuggerStepThrough]
        public static Result<T, TFailure> Failure<T, TFailure>(this TFailure failure) =>
            new Result<T, TFailure>(failure);

        [DebuggerStepThrough]
        public static bool IsSuccess<T, TFailure>(this Result<T, TFailure> r) =>
            r.Match(
                success => true,
                failure => false);

        [DebuggerStepThrough]
        public static bool IsFailure<T, TFailure>(this Result<T, TFailure> r) =>
            r.Match(
                success => false,
                failure => true);

        [DebuggerStepThrough]
        public static Result<TResult, TFailure> Map<T, TResult, TFailure>(this Result<T, TFailure> r, Func<T, TResult> f) =>
            r.Next<T, TResult, TFailure>(t => f(t));

        [DebuggerStepThrough]
        public static Result<T, TFailure> Void<T, TFailure>(this Result<T, TFailure> r, Action<T> f) =>
            r.Next<T, T, TFailure>(t => { f(t); return t; });

        [DebuggerStepThrough]
        public static Result<TResult, TFailure> Next<T, TResult, TFailure>(this Result<T, TFailure> r, Func<T, Result<TResult, TFailure>> f) =>
            r.Match(
                f,
                Failure<TResult, TFailure>);

        [DebuggerStepThrough]
        public static T SuccessOrEffect<T, TFailure>(this Result<T, TFailure> r, Action<TFailure> f)
        {
            if (r.IsFailure())
                f(r.Failure);
            return r.Success;
        }
    }

    public static class ResultTask
    {
        [DebuggerStepThrough]
        public static async Task<Result<TResult, TFailure>> Map<T, TResult, TFailure>(this Task<Result<T, TFailure>> r, Func<T, TResult> f) =>
            (await r).Map(f);

        [DebuggerStepThrough]
        public static async Task<Result<TResult, TFailure>> Map<T, TResult, TFailure>(this Task<Result<T, TFailure>> r, Func<T, Task<TResult>> f) =>
            await r.Next(async t => Result.Success<TResult, TFailure>(await f(t)));

        [DebuggerStepThrough]
        public static async Task<Result<T, TFailure>> Void<T, TFailure>(this Task<Result<T, TFailure>> r, Action<T> f) =>
            (await r).Void(f);

        [DebuggerStepThrough]
        public static async Task<Result<T, TFailure>> Void<T, TFailure>(this Task<Result<T, TFailure>> r, Func<T, Task> f) =>
            await r.Next(async t => { await f(t); return Result.Success<T, TFailure>(t); });

        [DebuggerStepThrough]
        public static async Task<Result<TResult, TFailure>> Next<T, TResult, TFailure>(this Task<Result<T, TFailure>> r, Func<T, Result<TResult, TFailure>> f) =>
            (await r).Next(f);

        [DebuggerStepThrough]
        public static async Task<Result<TResult, TFailure>> Next<T, TResult, TFailure>(this Task<Result<T, TFailure>> r, Func<T, Task<Result<TResult, TFailure>>> f) =>
            await (await r).Match(
                async success => await f(success),
#pragma warning disable 1998
                async failure => Result.Failure<TResult, TFailure>(failure));
#pragma warning restore 1998

        [DebuggerStepThrough]
        public static async Task<T> SuccessOrEffect<T, TFailure>(this Task<Result<T, TFailure>> r, Action<TFailure> f) =>
            (await r).SuccessOrEffect(f);

        [DebuggerStepThrough]
        public static async Task<T> SuccessOrEffect<T, TFailure>(this Task<Result<T, TFailure>> r, Func<TFailure, Task> f)
        {
            var awaited = await r;
            if (awaited.IsFailure())
                await f(awaited.Failure);
            return awaited.Success;
        }
    }

    public static class ResultNoLamda
    {
        [DebuggerStepThrough]
        public static Result<TResult, TFailure> Map<T, A, TResult, TFailure>(this Result<T, TFailure> r, Func<T, A, TResult> f, A a) =>
            r.Map(t => f(t, a));

        [DebuggerStepThrough]
        public static Result<TResult, TFailure> Map<T, A, B, TResult, TFailure>(this Result<T, TFailure> r, Func<T, A, B, TResult> f, A a, B b) =>
            r.Map(t => f(t, a, b));

        [DebuggerStepThrough]
        public static Result<TResult, TFailure> Map<T, A, B, C, TResult, TFailure>(this Result<T, TFailure> r, Func<T, A, B, C, TResult> f, A a, B b, C c) =>
            r.Map(t => f(t, a, b, c));

        [DebuggerStepThrough]
        public static Result<T, TFailure> Void<T, A, TFailure>(this Result<T, TFailure> r, Action<T, A> f, A a) =>
            r.Void(t => f(t, a));

        [DebuggerStepThrough]
        public static Result<T, TFailure> Void<T, A, B, TFailure>(this Result<T, TFailure> r, Action<T, A, B> f, A a, B b) =>
            r.Void(t => f(t, a, b));

        [DebuggerStepThrough]
        public static Result<T, TFailure> Void<T, A, B, C, TFailure>(this Result<T, TFailure> r, Action<T, A, B, C> f, A a, B b, C c) =>
            r.Void(t => f(t, a, b, c));

        [DebuggerStepThrough]
        public static Result<TResult, TFailure> Next<T, A, TResult, TFailure>(this Result<T, TFailure> r, Func<T, A, Result<TResult, TFailure>> f, A a) =>
            r.Next(t => f(t, a));

        [DebuggerStepThrough]
        public static Result<TResult, TFailure> Next<T, A, B, TResult, TFailure>(this Result<T, TFailure> r, Func<T, A, B, Result<TResult, TFailure>> f, A a, B b) =>
            r.Next(t => f(t, a, b));

        [DebuggerStepThrough]
        public static Result<TResult, TFailure> Next<T, A, B, C, TResult, TFailure>(this Result<T, TFailure> r, Func<T, A, B, C, Result<TResult, TFailure>> f, A a, B b, C c) =>
            r.Next(t => f(t, a, b, c));

        [DebuggerStepThrough]
        public static T SuccessOrEffect<T, A, TFailure>(this Result<T, TFailure> r, Action<TFailure, A> f, A a) =>
           r.SuccessOrEffect(s => f(s, a));

        [DebuggerStepThrough]
        public static T SuccessOrEffect<T, A, B, TFailure>(this Result<T, TFailure> r, Action<TFailure, A, B> f, A a, B b) =>
           r.SuccessOrEffect(s => f(s, a, b));

        [DebuggerStepThrough]
        public static T SuccessOrEffect<T, A, B, C, TFailure>(this Result<T, TFailure> r, Action<TFailure, A, B, C> f, A a, B b, C c) =>
           r.SuccessOrEffect(s => f(s, a, b, c));
    }

    #endregion
    #region lasync

    //public class Lasync<T>
    //{
    //    private readonly Func<Task<T>> _factory;
    //    private Lazy<Task<T>> _value;

    //    public Task<T> Task => _value.Value;

    //    [DebuggerStepThrough]
    //    public Lasync(Func<Task<T>> factory) 
    //    {
    //        _factory = factory;
    //        _value = new Lazy<Task<T>>(factory, true);
    //    }

    //    [DebuggerStepThrough]
    //    public void Reset() =>        
    //        _value = new Lazy<Task<T>>(_factory, true);
        
    //    [DebuggerStepThrough]
    //    public TaskAwaiter<T> GetAwaiter() => 
    //        _value.Value.GetAwaiter();        
    //}

    //public static class Lasync
    //{
    //    [DebuggerStepThrough]
    //    public static Lasync<T> Create<T>(Func<Task<T>> f) =>        
    //        new Lasync<T>(f);        
    //}


    #endregion
    
}
#pragma warning restore IDE1006 // Naming Styles