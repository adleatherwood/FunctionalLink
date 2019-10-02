using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static FunctionalLink.GlobalLink;
using static FunctionalLink.Tests.GlobalTest;

namespace FunctionalLink.Tests
{
    [TestClass]
    public class ResultTests
    {
        [TestMethod]
        public void SuccessConstructorInitializesProperty()
        {
            Result<string> result = Success("");

            var actual = result.Match(success => true, failure => false);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void FailureConstructorInitializesProperty()
        {
            Result<string> result = Failure("");

            var actual = result.Match(success => true, failure => false);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void SuccessEnumeratesToOneRecord()
        {
            Result<string> result = Success("test");

            var actual = result.Single();

            Assert.AreEqual("test", actual);
        }

        [TestMethod]
        public void FailureEnumeratesToZeroRecords()
        {
            Result<string> result = Failure("");

            var actual = result.SingleOrDefault();

            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task IsSuccessPositive()
        {
            Result<string> result = Success("");

            var actual = await result.ToTask()
                .IsSuccess();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public async Task IsSuccessNegative()
        {
            Result<string> result = Failure("");

            var actual = await result.ToTask()
                .IsSuccess();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public async Task IsFailurePositive()
        {
            Result<string> result = Failure("");

            var actual = await result.ToTask()
                .IsFailure();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public async Task IsFailureNegative()
        {
            Result<string> result = Success("");

            var actual = await result.ToTask()
                .IsFailure();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public async Task MapAsync()
        {
            int helper(string value) =>
                int.Parse(value);

            Result<string> result = Success("1");

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

            Result<string> result = Success("1");

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

            Result<string> result = Success("1");

            await result.ToTask()
                .Void(helper);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public async Task VoidAsync2()
        {
            var actual = false;

            Task helper(string value) => Async(actual = true);

            Result<string> result = Success("1");

            await result.ToTask()
                .Void(helper);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public async Task BindAsync()
        {
            Result<int> helper(string value) =>
                int.TryParse(value, out var result)
                    ? Success(result)
                    : Failure("Incorrect format");

            Result<string> result = Success("1");

            var actual = await result.ToTask()
                .Bind(helper)
                .Match(success => success, failure => -1);

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public async Task BindAsync2()
        {
            Task<Result<int>> helper(string value) =>
                (int.TryParse(value, out var result)
                    ? Success(result)
                    : Failure("Incorrect format")).ToTask();

            Result<string> result = Success("1");

            var actual = await result.ToTask()
                .Bind(helper)
                .Match(success => success, failure => -1);

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public async Task MatchAsync()
        {
            Result<string> result = Success("1");

            var actual = await result.ToTask()
                .Match(success => success, failure => "");

            Assert.AreEqual("1", actual);
        }

        [TestMethod]
        public async Task MatchAsync2()
        {
            Result<string> result = Success("1");

            var actual = await result.ToTask()
                .Match(success => Async(success), failure => Async(""));

            Assert.AreEqual("1", actual);
        }
    }
}