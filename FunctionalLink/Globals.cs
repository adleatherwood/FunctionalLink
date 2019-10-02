
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalLink
{
    public static class GlobalLink
    {
        public static T Self<T>(T t) =>
            t;
        public static UnitType Unit =>
            UnitType.Value;

        public static Option<T> Some<T>(T t) =>
            new SomeCase<T>(t);

        public static Option<T> Some<T>(Nullable<T> t) where T : struct =>
            t.HasValue
                ? (Option<T>)new SomeCase<T>(t.Value)
                : NoneCase.Value;

        public static NoneCase None =>
            NoneCase.Value;

        public static Result<T> Success<T>(T t) =>
            new SuccessCase<T>(t);

        public static Result<T, F> Success<T, F>(T t) =>
            new SuccessCase<T>(t);

        public static FailureCase<T> Failure<T>(T message) =>
            new FailureCase<T>(message);

        public static List<T> list<T>(params T[] values) =>
            values.ToList();

        public static T[] array<T>(params T[] values) =>
            values;

        public static Dictionary<K, V> dict<K, V>(params Tuple<K, V>[] records) =>
            records.ToDictionary(t => t.Item1, t => t.Item2);

        public static Dictionary<K, V> dict<K, V>(params ValueTuple<K, V>[] records) =>
            records.ToDictionary(t => t.Item1, t => t.Item2);

        public static List<T> singleton<T>(T t) =>
            new List<T>() { t };
    }
}
