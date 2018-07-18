using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FunctionalLink;

namespace FunctionalLink.Tests
{
    [TestClass]
    public class ResultTests
    {
        [TestMethod]
        public void SuccessCaseInitializesAndEvaluatesProperly()
        {
            var actual = Result.Success(1);

            Assert.AreEqual(1, actual.Success);
        }

        [TestMethod]
        public void FailureCaseInitializesAndEvaluatesProperly()
        {
            var actual = Result.Failure<int>("x");

            Assert.AreEqual("x", actual.Failure);
        }

        [TestMethod]
        public void IsSuccessEvaluatesTrueProperly()
        {
            var actual = Result.Success(1);

            Assert.IsTrue(actual.IsSuccess());
        }

        [TestMethod]
        public void IsSuccessEvaluatesFalseProperly()
        {
            var actual = Result.Failure<int>("");

            Assert.IsFalse(actual.IsSuccess());
        }

        [TestMethod]
        public void IsFailureEvaluatesFalseProperly()
        {
            var actual = Result.Success(1);

            Assert.IsFalse(actual.IsFailure());
        }

        [TestMethod]
        public void IsFailureEvaluatesTrueProperly()
        {
            var actual = Result.Failure<int>("");

            Assert.IsTrue(actual.IsFailure());
        }

        [TestMethod]
        public void MapEvaluatesProperly()
        {
            var actual = Result.Success(1)
                .Map(i => i + 1);

            Assert.AreEqual(2, actual.Success);
        }

        [TestMethod]
        public void Map1EvaluatesProperly()
        {
            var actual = Result.Success(1)
                .Map((i, a) => i + a, 1);

            Assert.AreEqual(2, actual.Success);
        }

        [TestMethod]
        public void Map2EvaluatesProperly()
        {
            var actual = Result.Success(1)
                .Map((i, a, b) => i + a + b, 1, 2);

            Assert.AreEqual(4, actual.Success);
        }

        [TestMethod]
        public void Map3EvaluatesProperly()
        {
            var actual = Result.Success(1)
                .Map((i, a, b, c) => i + a + b + c, 1, 2, 3);

            Assert.AreEqual(7, actual.Success);
        }

        [TestMethod]
        public void NextEvaluatesProperly()
        {
            var actual = Result.Success(1)
                .Next((i) => Result.Success(i + 1));

            Assert.AreEqual(2, actual.Success);
        }

        [TestMethod]
        public void Next1EvaluatesProperly()
        {
            var actual = Result.Success(1)
                .Next((i, a) => Result.Success(i + a), 1);

            Assert.AreEqual(2, actual.Success);
        }

        [TestMethod]
        public void Next2EvaluatesProperly()
        {
            var actual = Result.Success(1)
                .Next((i, a, b) => Result.Success(i + a + b), 1, 2);

            Assert.AreEqual(4, actual.Success);
        }

        [TestMethod]
        public void Next3EvaluatesProperly()
        {
            var actual = Result.Success(1)
                .Next((i, a, b, c) => Result.Success(i + a + b + c), 1, 2, 3);

            Assert.AreEqual(7, actual.Success);
        }

        [TestMethod]
        public void VoidEvaluatesProperly()
        {
            var actual = Result.Success(1)
                .Void((i) => {});

            Assert.AreEqual(1, actual.Success);
        }

        [TestMethod]
        public void Void1EvaluatesProperly()
        {
            var actual = Result.Success(1)
                .Void((i, a) => {}, 1);

            Assert.AreEqual(1, actual.Success);
        }

        [TestMethod]
        public void Void2EvaluatesProperly()
        {
            var actual = Result.Success(1)
                .Void((i, a, b) => {}, 1, 2);

            Assert.AreEqual(1, actual.Success);
        }

        [TestMethod]
        public void Void3EvaluatesProperly()
        {
            var actual = Result.Success(1)
                .Void((i, a, b, c) => {}, 1, 2, 3);

            Assert.AreEqual(1, actual.Success);
        }
    } 
}