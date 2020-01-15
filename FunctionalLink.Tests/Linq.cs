using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static FunctionalLink.GlobalLink;

namespace FunctionalLink.Tests
{
    [TestClass]
    public class EnumerablesTests
    {
        [TestMethod]
        public void Select1EvaluatesProperly()
        {
            var items = list(1, 2, 3);
            var actual = items
                .Select((i, a) => i + a, 1)
                .ToArray();
            Assert.AreEqual(2, actual[0]);
            Assert.AreEqual(3, actual[1]);
            Assert.AreEqual(4, actual[2]);
        }

        [TestMethod]
        public void Select2EvaluatesProperly()
        {
            var items = list(1, 2, 3);
            var actual = items
                .Select((i, a, b) => i + a + b, 1, 2)
                .ToArray();
            Assert.AreEqual(4, actual[0]);
            Assert.AreEqual(5, actual[1]);
            Assert.AreEqual(6, actual[2]);
        }

        [TestMethod]
        public void Select3EvaluatesProperly()
        {
            var items = list(1, 2, 3);
            var actual = items
                .Select((i, a, b, c) => i + a + b + c, 1, 2, 3)
                .ToArray();
            Assert.AreEqual(7, actual[0]);
            Assert.AreEqual(8, actual[1]);
            Assert.AreEqual(9, actual[2]);
        }

        [TestMethod]
        public void SelectMany1EvaluatesProperly()
        {
            var items = list(1, 2, 3);
            var actual = items
                .SelectMany((i, a) => singleton(i + a), 1)
                .ToArray();
            Assert.AreEqual(2, actual[0]);
            Assert.AreEqual(3, actual[1]);
            Assert.AreEqual(4, actual[2]);
        }

        [TestMethod]
        public void SelectMany2EvaluatesProperly()
        {
            var items = list(1, 2, 3);
            var actual = items
                .SelectMany((i, a, b) => singleton(i + a + b), 1, 2)
                .ToArray();
            Assert.AreEqual(4, actual[0]);
            Assert.AreEqual(5, actual[1]);
            Assert.AreEqual(6, actual[2]);
        }

        [TestMethod]
        public void SelectMany3EvaluatesProperly()
        {
            var items = list(1, 2, 3);
            var actual = items
                .SelectMany((i, a, b, c) => singleton(i + a + b + c), 1, 2, 3)
                .ToArray();
            Assert.AreEqual(7, actual[0]);
            Assert.AreEqual(8, actual[1]);
            Assert.AreEqual(9, actual[2]);
        }

        [TestMethod]
        public void IterateEvaluatesProperly()
        {
            var actual = new List<int>();
            list(1, 2, 3)
                .Iterate((i) => actual.Add(i))
                .EvaluateAndIgnore();

            Assert.AreEqual(1, actual[0]);
            Assert.AreEqual(2, actual[1]);
            Assert.AreEqual(3, actual[2]);
        }

        [TestMethod]
        public void Iterate1EvaluatesProperly()
        {
            var actual = new List<int>();
            list(1, 2, 3)
                .Iterate((i, a) => actual.Add(i + a), 1)
                .EvaluateAndIgnore();

            Assert.AreEqual(2, actual[0]);
            Assert.AreEqual(3, actual[1]);
            Assert.AreEqual(4, actual[2]);
        }

        [TestMethod]
        public void Iterate2EvaluatesProperly()
        {
            var actual = new List<int>();
            list(1, 2, 3)
                .Iterate((i, a, b) => actual.Add(i + a + b), 1, 2)
                .EvaluateAndIgnore();

            Assert.AreEqual(4, actual[0]);
            Assert.AreEqual(5, actual[1]);
            Assert.AreEqual(6, actual[2]);
        }

        [TestMethod]
        public void Iterate3EvaluatesProperly()
        {
            var actual = new List<int>();
            list(1, 2, 3)
                .Iterate((i, a, b, c) => actual.Add(i + a + b + c), 1, 2, 3)
                .EvaluateAndIgnore();

            Assert.AreEqual(7, actual[0]);
            Assert.AreEqual(8, actual[1]);
            Assert.AreEqual(9, actual[2]);
        }

        [TestMethod]
        public void SingletonReturnsASingleItemEnumerable()
        {
            var actual = "test".Singleton();

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual("test", actual.First());
        }

        [TestMethod]
        public void EvaluateForcesTheExecutionOfALazyEnumerableAndReturnsTheResult()
        {
            var expected = 0;

            IEnumerable<int> tick() { for(;;) { yield return ++expected; } }

            var first = tick()
                .Take(3)
                .Evaluate();

            Assert.AreEqual(3, first.Count());
            Assert.AreEqual(3, expected);

            var second = first
                .Select(i => i + 1);

            Assert.AreEqual(3, second.Count());
            Assert.AreEqual(3, expected);
        }

        [TestMethod]
        public void EvaluateAndIgnoreForcesTheExecutionOfALazyEnumerable()
        {
            var expected = 0;

            IEnumerable<int> tick() { for(;;) { yield return ++expected; } }

            tick()
                .Take(3)
                .EvaluateAndIgnore();

            Assert.AreEqual(3, expected);
        }

        [TestMethod]
        public void TupleSelectABEvaluatesProperly()
        {
            var actual = list(
                    Tuple.Create(1, "one"),
                    Tuple.Create(2, "two"),
                    Tuple.Create(3, "three"))
                .Select((a, b) => a + b)
                .ToArray();

            Assert.AreEqual("1one", actual[0]);
            Assert.AreEqual("2two", actual[1]);
            Assert.AreEqual("3three", actual[2]);
        }

        [TestMethod]
        public void TupleSelectABCEvaluatesProperly()
        {
            var actual = list(
                    Tuple.Create(1, "one", true),
                    Tuple.Create(2, "two", false),
                    Tuple.Create(3, "three", true))
                .Select((a, b, c) => a + b + c)
                .ToArray();

            Assert.AreEqual("1oneTrue", actual[0]);
            Assert.AreEqual("2twoFalse", actual[1]);
            Assert.AreEqual("3threeTrue", actual[2]);
        }

        [TestMethod]
        public void EvaluateAndIgnoreForcesEvaluation()
        {
            var items = list(1, 2, 3, 4);
            var actual = list<int>();

            items.Where(i => i % 2 == 0)
                .Select(i => { actual.Add(i); return i; })
                .EvaluateAndIgnore();

            Assert.AreEqual(2, actual[0]);
            Assert.AreEqual(4, actual[1]);
        }
    }
}