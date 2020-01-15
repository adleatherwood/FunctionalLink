using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static FunctionalLink.GlobalLink;
using static FunctionalLink.Tests.GlobalTest;

namespace FunctionalLink.Tests
{
    [TestClass]
    public class Result2Tests
    {
        [TestMethod]
        public void SuccessConstructorInitializesProperty()
        {
            Result<string, int> result = Success<string, int>("");

            var actual = result.Match(success => true, failure => false);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void FailureConstructorInitializesProperty()
        {
            Result<string, int> result = Failure(1);

            var actual = result.Match(success => true, failure => false);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void SuccessEnumeratesToOneRecord()
        {
            Result<string, int> result = Success<string, int>("test");

            var actual = result.Single();

            Assert.AreEqual("test", actual);
        }

        [TestMethod]
        public void FailureEnumeratesToZeroRecords()
        {
            Result<string, int> result = Failure(1);

            var actual = result.SingleOrDefault();

            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task IsSuccessPositive()
        {
            Result<string, int> result = Success<string, int>("");

            var actual = await result.ToTask()
                .IsSuccess();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public async Task IsSuccessNegative()
        {
            Result<string, int> result = Failure(1);

            var actual = await result.ToTask()
                .IsSuccess();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public async Task IsFailurePositive()
        {
            Result<string, int> result = Failure(1);

            var actual = await result.ToTask()
                .IsFailure();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public async Task IsFailureNegative()
        {
            Result<string, int> result = Success<string, int>("");

            var actual = await result.ToTask()
                .IsFailure();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public async Task MapAsync()
        {
            int helper(string value) =>
                int.Parse(value);

            Result<string, int> result = Success<string, int>("1");

            var actual = await result.ToTask()
                .Map(helper)
                .Match(success => success, failure => -1);

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public async Task MapAsync2()
        {
            Task<int> helper(string value) =>
                int.Parse(value).ToTask();

            Result<string, int> result = Success<string, int>("1");

            var actual = await result.ToTask()
                .Map(helper)
                .Match(success => success, failure => -1);

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public async Task VoidAsync()
        {
            var actual = false;

            void helper(string value) { actual = true; };

            Result<string, int> result = Success<string, int>("1");

            await result.ToTask()
                .Void(helper);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public async Task VoidAsync2()
        {
            var actual = false;

            Task helper(string value) => Async(actual = true);

            Result<string, int> result = Success<string, int>("1");

            await result.ToTask()
                .Void(helper);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public async Task BindAsync()
        {
            Result<int, string> helper(string value) =>
                int.TryParse(value, out var result)
                    ? Success<int, string>(result)
                    : Failure("Incorrect format");

            Result<string, string> result = Success<string, string>("1");

            var actual = await result.ToTask()
                .Bind(helper)
                .Match(success => success, failure => -1);

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public async Task BindAsync2()
        {
            Task<Result<int, string>> helper(string value) =>
                (int.TryParse(value, out var result)
                    ? Success<int, string>(result)
                    : Failure("Incorrect format")).ToTask();

            Result<string, string> result = Success<string, string>("1");

            var actual = await result.ToTask()
                .Bind(helper)
                .Match(success => success, failure => -1);

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public async Task MatchAsync()
        {
            Result<string, int> result = Success<string, int>("1");

            var actual = await result.ToTask()
                .Match(success => success, failure => "-1");

            Assert.AreEqual("1", actual);
        }

        [TestMethod]
        public async Task MatchAsync2()
        {
            Result<string, int> result = Success<string, int>("1");

            var actual = await result.ToTask()
                .Match(success => Async(success), failure => Async("-1"));

            Assert.AreEqual("1", actual);
        }

        public void ImplicitBooolPositive()
        {
            var actual = Result.Success<int, string>(1);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ImplicitBooolNegative()
        {
            var actual = Result.Failure<int, string>("");

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ValuePropertyPositive()
        {
            var actual = Result.Success<int, string>(1);

            Assert.AreEqual(1, actual.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ValuePropertyNegative()
        {
            var actual = Result.Failure<int, string>("");

            Assert.AreEqual(1, actual.Value);
        }

                [TestMethod]
        public void ImplicitBoolIsTrueWithSuccess()
        {
            var actual = Result.Success<int,string>(1);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ImplicitBoolIsFalseWithFailure()
        {
            var actual = Result.Failure<int,string>("");

            Assert.IsFalse(actual);
        }

        //-----------------------

        [TestMethod]
        public void NoLambdaMapA()
        {
            int add(int a, int b) => a + b;

            var actual = Result.Success<int,string>(1)
                .Map(add, 1);

            Assert.AreEqual(2, actual.Value);
        }

        [TestMethod]
        public void NoLambdaMapAB()
        {
            int add(int a, int b, int c) => a + b + c;

            var actual = Result.Success<int,string>(1)
                .Map(add, 1, 1);

            Assert.AreEqual(3, actual.Value);
        }

        [TestMethod]
        public void NoLambdaMapABC()
        {
            int add(int a, int b, int c, int d) => a + b + c + d;

            var actual = Result.Success<int,string>(1)
                .Map(add, 1, 1, 1);

            Assert.AreEqual(4, actual.Value);
        }

        [TestMethod]
        public void NoLambdaVoidA()
        {
            var actual = 0;

            void callback(int a, int b) => actual = a + b;

            Result.Success<int,string>(1)
                .Void(callback, 1);

            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        public void NoLambdaVoidAB()
        {
            var actual = 0;

            void callback(int a, int b, int c) => actual = a + b + c;

            Result.Success<int,string>(1)
                .Void(callback, 1, 1);

            Assert.AreEqual(3, actual);
        }

        [TestMethod]
        public void NoLambdaVoidABC()
        {
            var actual = 0;

            void callback(int a, int b, int c, int d) => actual = a + b + c + d;

            Result.Success<int,string>(1)
                .Void(callback, 1, 1, 1);

            Assert.AreEqual(4, actual);
        }

        [TestMethod]
        public void NoLambdaBindABC()
        {
            Result<int,string> add(int a, int b, int c, int d) => Success<int,string>(a + b + c + d);

            var actual = Result.Success<int,string>(1)
                .Bind(add, 1, 1, 1);

            Assert.AreEqual(4, actual.Value);
        }

        [TestMethod]
        public void NoLambdaBindAB()
        {
            Result<int,string> add(int a, int b, int c) => Success<int,string>(a + b + c);

            var actual = Result.Success<int,string>(1)
                .Bind(add, 1, 1);

            Assert.AreEqual(3, actual.Value);
        }

        [TestMethod]
        public void NoLambdaBindA()
        {
            Result<int,string> add(int a, int b) => Success<int,string>(a + b);

            var actual = Result.Success<int,string>(1)
                .Bind(add, 1);

            Assert.AreEqual(2, actual.Value);
        }
    }
}