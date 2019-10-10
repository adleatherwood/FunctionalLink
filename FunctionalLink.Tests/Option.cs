using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System;

using static FunctionalLink.GlobalLink;
using static FunctionalLink.Tests.GlobalTest;

namespace FunctionalLink.Tests
{
    [TestClass]
    public class OptionTests
    {
        [TestMethod]
        public void ImplicitConversionFromSome()
        {
            Option<string> option = Some("");

            var actual = option.Match(some => true, none => false);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ImplicitConversionFromSomeNull()
        {
            Option<string> option = Some(NullString);

            var actual = option.Match(some => true, none => false);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ImplicitConversionFromNone()
        {
            Option<string> option = None;

            var actual = option.Match(some => true, none => false);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ExplicitConversionFrom()
        {
            var actual = Option.From("")
                .Match(some => true, none => false);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ExplicitConversionFromNull()
        {
            var actual = Option.From(NullString)
                .Match(some => true, none => false);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ExplicitConversionFromSome()
        {
            var actual = Option.Some("")
                .Match(some => true, none => false);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ExplicitConversionFromSomeNull()
        {
            var actual = Option.Some(NullString)
                .Match(some => true, none => false);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ExplicitConversionFromNone()
        {
            var actual = Option.None<string>()
                .Match(some => true, none => false);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void EnumerateSome()
        {
            var actual = Option.Some(1)
                .Single();

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void EnumerateNone()
        {
            var actual = Option.None<int>()
                .Any();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsSomePositive()
        {
            var actual = Option.Some("")
                .IsSome();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsSomeNegative()
        {
            var actual = Option.None<string>()
                .IsSome();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsSomeNegativeNull()
        {
            var actual = Option.Some(NullString)
                .IsSome();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public async Task IsSomeAsyncPositive()
        {
            var actual = await Option.Some("").ToTask()
                .IsSome();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public async Task IsSomeAsyncPositiveNull()
        {
            var actual = await Option.Some(NullString).ToTask()
                .IsSome();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public async Task IsSomeAsyncNegative()
        {
            var actual = await Option.None<string>().ToTask()
                .IsSome();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsNonePositive()
        {
            var actual = Option.Some(NullString)
                .IsNone();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsNoneNegative()
        {
            var actual = Option.Some("")
                .IsNone();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsNoneNegativeNull()
        {
            var actual = Option.Some(NullString)
                .IsNone();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public async Task IsNoneAsyncPositive()
        {
            var actual = await Option.None<string>().ToTask()
                .IsNone();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void FilterPositive()
        {
            bool helper(int i) => i == 1;

            var actual = Option.Some(1)
                .Filter(helper)
                .ValueOrDefault();

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void FilterNegative()
        {
            bool helper(int i) => i == 0;

            var actual = Option.Some(1)
                .Filter(helper)
                .ValueOrDefault();

            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public async Task FilterAsyncPositiveAsync()
        {
            Task<bool> helper(int i) => Async(i == 1);

            var actual = await Option.Some(1).ToTask()
                .Filter(helper)
                .ValueOrDefault();

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public async Task FilterAsyncPositive()
        {
            bool helper(int i) => i == 1;

            var actual = await Option.Some(1).ToTask()
                .Filter(helper)
                .ValueOrDefault();

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public async Task IsNoneAsyncPositiveNull()
        {
            var actual = await Option.Some(NullString).ToTask()
                .IsNone();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public async Task IsNoneAsyncNegative()
        {
            var actual = await Option.None<string>().ToTask()
                .IsNone();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void MapSome()
        {
            var actual = Option.Some("1")
                .Map(int.Parse)
                .ValueOrDefault();

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public async Task MapAsyncSome()
        {
            Task<int> helper(string value) => Task.FromResult(Int32.Parse(value));

            var actual = await Option.Some("1")
                .Map(helper)
                .ValueOrDefault();

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public async Task MapAsyncSomeAsync()
        {
            Task<int> helper(string value) => Async(Int32.Parse(value));

            var actual = await Option.Some("1").ToTask()
                .Map(helper)
                .ValueOrDefault();

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public async Task MapSomeAsync()
        {
            var actual = await Option.Some("1").ToTask()
                .Map(Int32.Parse)
                .ValueOrDefault();

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void MapNone()
        {
            var actual = Option.None<string>()
                .Map(int.Parse)
                .ValueOrDefault();

            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public async Task MapAsyncNone()
        {
            Task<int> helper(string value) => Async(Int32.Parse(value));

            var actual = await Option.None<string>()
                .Map(helper)
                .ValueOrDefault();

            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public async Task MapAsyncNoneAsync()
        {
            Task<int> helper(string value) => Async(Int32.Parse(value));

            var actual = await Option.None<string>().ToTask()
                .Map(helper)
                .ValueOrDefault();

            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public async Task MapNoneAsync()
        {
            int helper(string value) => Int32.Parse(value);

            var actual = await Option.None<string>().ToTask()
                .Map(helper)
                .ValueOrDefault();

            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void VoidSome()
        {
            var actual = "";

            void helper(string value) { actual = value; }

            Option.Some("1")
                .Void(helper);

            Assert.AreEqual("1", actual);
        }

        [TestMethod]
        public async Task VoidAsyncSome()
        {
            var actual = "";

            Task helper(string value) => Async(actual = value);

            await Option.Some("1")
                .Void(helper);

            Assert.AreEqual("1", actual);
        }

        [TestMethod]
        public async Task VoidAsyncSomeAsync()
        {
            var actual = "";

            Task helper(string value) => Async(actual = value);

            await Option.Some("1").ToTask()
                .Void(helper);

            Assert.AreEqual("1", actual);
        }

        [TestMethod]
        public async Task VoidSomeAsync()
        {
            var actual = "";

            void helper(string value) { actual = value; }

            await Option.Some("1").ToTask()
                .Void(helper);

            Assert.AreEqual("1", actual);
        }

        // [TestMethod]
        // public void OrPositive()
        // {
        //     var actual = Option.Some(1)
        //         .Or(Some(2))
        //         .ValueOrDefault();

        //     Assert.AreEqual(1, actual);
        // }

        // [TestMethod]
        // public void OrNegative()
        // {
        //     var actual = Option.None<int>()
        //         .Or(Some(2))
        //         .ValueOrDefault();

        //     Assert.AreEqual(2, actual);
        // }

        // [TestMethod]
        // public async Task OrAsyncNegative()
        // {
        //     var actual = await Option.None<int>().ToTask()
        //         .Or(Some(2))
        //         .ValueOrDefault();

        //     Assert.AreEqual(2, actual);
        // }

        // [TestMethod]
        // public async Task OrAsyncNegativeAsync()
        // {
        //     var actual = await Option.None<int>().ToTask()
        //         .Or(Option.Some(2).ToTask())
        //         .ValueOrDefault();

        //     Assert.AreEqual(2, actual);
        // }

        // [TestMethod]
        // public async Task OrAsyncNegativeFetch()
        // {
        //     Task<Option<int>> helper() => Async(Option.Some(2));

        //     var actual = await Option.None<int>()
        //         .OrFetch(helper)
        //         .ValueOrDefault();

        //     Assert.AreEqual(2, actual);
        // }

        // [TestMethod]
        // public async Task OrAsyncNegativeFetchAsync()
        // {
        //     Task<Option<int>> helper() => Async(Option.Some(2));

        //     var actual = await Option.None<int>().ToTask()
        //         .OrFetch(helper)
        //         .ValueOrDefault();

        //     Assert.AreEqual(2, actual);
        // }

        // [TestMethod]
        // public async Task OrNegativeFetchAsync()
        // {
        //     Option<int> helper() => Option.Some(2);

        //     var actual = await Option.None<int>().ToTask()
        //         .OrFetch(helper)
        //         .ValueOrDefault();

        //     Assert.AreEqual(2, actual);
        // }

        // [TestMethod]
        // public async Task ValueAsyncOrAsyncPositive()
        // {
        //     var actual = await Option.Some(1).ToTask()
        //         .ValueOr(Async(2));

        //     Assert.AreEqual(1, actual);
        // }

        // [TestMethod]
        // public async Task ValueAsyncOrAsyncNegative()
        // {
        //     var actual = await Option.None<int>().ToTask()
        //         .ValueOr(Async(2));

        //     Assert.AreEqual(2, actual);
        // }

        // [TestMethod]
        // public async Task ValueOrAsyncPositive()
        // {
        //     var actual = await Option.Some(1)
        //         .ValueOr(Async(2));

        //     Assert.AreEqual(1, actual);
        // }

        // [TestMethod]
        // public async Task ValueOrAsyncNegative()
        // {
        //     var actual = await Option.None<int>()
        //         .ValueOr(Async(2));

        //     Assert.AreEqual(2, actual);
        // }

        [TestMethod]
        public async Task ValueOrDefaultPositive()
        {
            var actual = await Option.Some(1).ToTask()
                .ValueOrDefault();

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public async Task ValueOrDefaultNegative()
        {
            var actual = await Option.None<int>().ToTask()
                .ValueOrDefault();

            Assert.AreEqual(0, actual);
        }

        // [TestMethod]
        // public async Task ValueOrFetchAsyncPositiveAsync()
        // {
        //     Task<int> helper() => Async(2);

        //     var actual = await Option.Some(1).ToTask()
        //         .ValueOrFetch(helper);

        //     Assert.AreEqual(1, actual);
        // }

        // [TestMethod]
        // public async Task ValueOrFetchAsyncNegativeAsync()
        // {
        //     Task<int> helper() => Async(2);

        //     var actual = await Option.None<int>().ToTask()
        //         .ValueOrFetch(helper);

        //     Assert.AreEqual(2, actual);
        // }

        [TestMethod]
        public void MapA()
        {
            int helper(int x, int a) => x + a;

            var actual = Option.Some(1)
                .Map(helper, 1)
                .ValueOrDefault();

            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        public void MapAB()
        {
            int helper(int x, int a, int b) => x + a + b;

            var actual = Option.Some(1)
                .Map(helper, 1, 1)
                .ValueOrDefault();

            Assert.AreEqual(3, actual);
        }

        [TestMethod]
        public void MapABC()
        {
            int helper(int x, int a, int b, int c) => x + a + b + c;

            var actual = Option.Some(1)
                .Map(helper, 1, 1, 1)
                .ValueOrDefault();

            Assert.AreEqual(4, actual);
        }

        [TestMethod]
        public void VoidAPositive()
        {
            var effect = 0;

            void helper(int x, int a) => effect = x + a;

            var actual = Option.Some(1)
                .Void(helper, 1)
                .ValueOrDefault();

            Assert.AreEqual(1, actual);
            Assert.AreEqual(2, effect);
        }

        [TestMethod]
        public void VoidABPositive()
        {
            var effect = 0;

            void helper(int x, int a, int b) => effect = x + a + b;

            var actual = Option.Some(1)
                .Void(helper, 1, 1)
                .ValueOrDefault();

            Assert.AreEqual(1, actual);
            Assert.AreEqual(3, effect);
        }

        [TestMethod]
        public void VoidABCPositive()
        {
            var effect = 0;

            void helper(int x, int a, int b, int c) => effect = x + a + b + c;

            var actual = Option.Some(1)
                .Void(helper, 1, 1, 1)
                .ValueOrDefault();

            Assert.AreEqual(1, actual);
            Assert.AreEqual(4, effect);
        }

        [TestMethod]
        public void VoidANegative()
        {
            var effect = 0;

            void helper(int x, int a) => effect = x + a;

            var actual = Option.None<int>()
                .Void(helper, 1)
                .ValueOrDefault();

            Assert.AreEqual(0, actual);
            Assert.AreEqual(0, effect);
        }

        [TestMethod]
        public void BindAPositive()
        {
            Option<int> helper(int x, int a) => Some(x + a);

            var actual = Option.Some(1)
                .Bind(helper, 1)
                .ValueOrDefault();

            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        public void BindABPositive()
        {
            Option<int> helper(int x, int a, int b) => Some(x + a + b);

            var actual = Option.Some(1)
                .Bind(helper, 1, 1)
                .ValueOrDefault();

            Assert.AreEqual(3, actual);
        }

        [TestMethod]
        public void BindABCPositive()
        {
            Option<int> helper(int x, int a, int b, int c) => Some(x + a + b + c);

            var actual = Option.Some(1)
                .Bind(helper, 1, 1, 1)
                .ValueOrDefault();

            Assert.AreEqual(4, actual);
        }

        [TestMethod]
        public void BindANegative()
        {
            Option<int> helper(int x, int a) => Some(x + a);

            var actual = Option.None<int>()
                .Bind(helper, 1)
                .ValueOrDefault();

            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void ImplicitBooolPositive()
        {
            var actual = Option.Some(1);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ImplicitBooolNegative()
        {
            var actual = Option.None<int>();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ValuePropertyPositive()
        {
            var actual = Option.Some(1);

            Assert.AreEqual(1, actual.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ValuePropertyNegative()
        {
            var actual = Option.None<int>();

            Assert.AreEqual(1, actual.Value);
        }
    }
}
