using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunctionalLink.Tests
{
    [TestClass]
    public class TuplesTests
    {
        [TestMethod]
        public void TuplesExtend3EvaluatesProperly()
        {
            var tuple = Tuple.Create(1,2);
            var actual = Tuples.Extend(tuple, 3);

            Assert.AreEqual(1, actual.Item1);
            Assert.AreEqual(2, actual.Item2);
            Assert.AreEqual(3, actual.Item3);
        }

        [TestMethod]
        public void ValueTuplesExtend3EvaluatesProperly()
        {
            var tuple = (1,2);
            var actual = Tuples.Extend(tuple, 3);

            Assert.AreEqual(1, actual.Item1);
            Assert.AreEqual(2, actual.Item2);
            Assert.AreEqual(3, actual.Item3);
        }

        [TestMethod]
        public void TuplesExtend4EvaluatesProperly()
        {
            var tuple = Tuple.Create(1,2,3);
            var actual = Tuples.Extend(tuple, 4);

            Assert.AreEqual(1, actual.Item1);
            Assert.AreEqual(2, actual.Item2);
            Assert.AreEqual(3, actual.Item3);
            Assert.AreEqual(4, actual.Item4);
        }

        [TestMethod]
        public void ValueTuplesExtend4EvaluatesProperly()
        {
            var tuple = (1,2,3);
            var actual = Tuples.Extend(tuple, 4);

            Assert.AreEqual(1, actual.Item1);
            Assert.AreEqual(2, actual.Item2);
            Assert.AreEqual(3, actual.Item3);
            Assert.AreEqual(4, actual.Item4);
        }

        [TestMethod]
        public void TuplesExtend5EvaluatesProperly()
        {
            var tuple = Tuple.Create(1,2,3,4);
            var actual = Tuples.Extend(tuple, 5);

            Assert.AreEqual(1, actual.Item1);
            Assert.AreEqual(2, actual.Item2);
            Assert.AreEqual(3, actual.Item3);
            Assert.AreEqual(4, actual.Item4);
            Assert.AreEqual(5, actual.Item5);
        }

        [TestMethod]
        public void ValueTuplesExtend5EvaluatesProperly()
        {
            var tuple = (1,2,3,4);
            var actual = Tuples.Extend(tuple, 5);

            Assert.AreEqual(1, actual.Item1);
            Assert.AreEqual(2, actual.Item2);
            Assert.AreEqual(3, actual.Item3);
            Assert.AreEqual(4, actual.Item4);
            Assert.AreEqual(5, actual.Item5);
        }

        [TestMethod]
        public void TuplesExtend6EvaluatesProperly()
        {
            var tuple = Tuple.Create(1,2,3,4,5);
            var actual = Tuples.Extend(tuple, 6);

            Assert.AreEqual(1, actual.Item1);
            Assert.AreEqual(2, actual.Item2);
            Assert.AreEqual(3, actual.Item3);
            Assert.AreEqual(4, actual.Item4);
            Assert.AreEqual(5, actual.Item5);
            Assert.AreEqual(6, actual.Item6);
        }

        [TestMethod]
        public void ValueTuplesExtend6EvaluatesProperly()
        {
            var tuple = (1,2,3,4,5);
            var actual = Tuples.Extend(tuple, 6);

            Assert.AreEqual(1, actual.Item1);
            Assert.AreEqual(2, actual.Item2);
            Assert.AreEqual(3, actual.Item3);
            Assert.AreEqual(4, actual.Item4);
            Assert.AreEqual(5, actual.Item5);
            Assert.AreEqual(6, actual.Item6);
        }

        [TestMethod]
        public void TuplesExtend7EvaluatesProperly()
        {
            var tuple = Tuple.Create(1,2,3,4,5,6);
            var actual = Tuples.Extend(tuple, 7);

            Assert.AreEqual(1, actual.Item1);
            Assert.AreEqual(2, actual.Item2);
            Assert.AreEqual(3, actual.Item3);
            Assert.AreEqual(4, actual.Item4);
            Assert.AreEqual(5, actual.Item5);
            Assert.AreEqual(6, actual.Item6);
            Assert.AreEqual(7, actual.Item7);
        }

        [TestMethod]
        public void ValueTuplesExtend7EvaluatesProperly()
        {
            var tuple = (1,2,3,4,5,6);
            var actual = Tuples.Extend(tuple, 7);

            Assert.AreEqual(1, actual.Item1);
            Assert.AreEqual(2, actual.Item2);
            Assert.AreEqual(3, actual.Item3);
            Assert.AreEqual(4, actual.Item4);
            Assert.AreEqual(5, actual.Item5);
            Assert.AreEqual(6, actual.Item6);
            Assert.AreEqual(7, actual.Item7);
        }

        [TestMethod]
        public void SelectDeconstructsTuple2()
        {
            var tuples = Tuple.Create(1,2)
                .Singleton();

            var actual = tuples.Select((a,b) => $"{a}{b}")
                .Single();

            Assert.AreEqual("12", actual);
        }

        [TestMethod]
        public void SelectDeconstructsTuple3()
        {
            var tuples = Tuple.Create(1,2,3)
                .Singleton();

            var actual = tuples.Select((a,b,c) => $"{a}{b}{c}")
                .Single();

            Assert.AreEqual("123", actual);
        }

        [TestMethod]
        public void SelectDeconstructsTuple4()
        {
            var tuples = Tuple.Create(1,2,3,4)
                .Singleton();

            var actual = tuples.Select((a,b,c,d) => $"{a}{b}{c}{d}")
                .Single();

            Assert.AreEqual("1234", actual);
        }

        [TestMethod]
        public void SelectDeconstructsTuple5()
        {
            var tuples = Tuple.Create(1,2,3,4,5)
                .Singleton();

            var actual = tuples.Select((a,b,c,d,e) => $"{a}{b}{c}{d}{e}")
                .Single();

            Assert.AreEqual("12345", actual);
        }

        [TestMethod]
        public void SelectDeconstructsTuple6()
        {
            var tuples = Tuple.Create(1,2,3,4,5,6)
                .Singleton();

            var actual = tuples.Select((a,b,c,d,e,f) => $"{a}{b}{c}{d}{e}{f}")
                .Single();

            Assert.AreEqual("123456", actual);
        }

        [TestMethod]
        public void SelectDeconstructsValueTuple6()
        {
            var tuples = (1,2,3,4,5,6)
                .Singleton();

            var actual = tuples.Select((a,b,c,d,e,f) => $"{a}{b}{c}{d}{e}{f}")
                .Single();

            Assert.AreEqual("123456", actual);
        }

        [TestMethod]
        public void SelectDeconstructsValueTuple5()
        {
            var tuples = (1,2,3,4,5)
                .Singleton();

            var actual = tuples.Select((a,b,c,d,e) => $"{a}{b}{c}{d}{e}")
                .Single();

            Assert.AreEqual("12345", actual);
        }

        [TestMethod]
        public void SelectDeconstructsValueTuple4()
        {
            var tuples = (1,2,3,4)
                .Singleton();

            var actual = tuples.Select((a,b,c,d) => $"{a}{b}{c}{d}")
                .Single();

            Assert.AreEqual("1234", actual);
        }

        [TestMethod]
        public void SelectDeconstructsValueTuple3()
        {
            var tuples = (1,2,3)
                .Singleton();

            var actual = tuples.Select((a,b,c) => $"{a}{b}{c}")
                .Single();

            Assert.AreEqual("123", actual);
        }

        [TestMethod]
        public void SelectDeconstructsValueTuple2()
        {
            var tuples = (1,2)
                .Singleton();

            var actual = tuples.Select((a,b) => $"{a}{b}")
                .Single();

            Assert.AreEqual("12", actual);
        }

        [TestMethod]
        public void CoupleConstructsTuple2()
        {
            var values = "1"
                .Singleton();

            var (text, number) = values.Couple(int.Parse)
                .Single();

            Assert.AreEqual("1", text);
            Assert.AreEqual(1, number);
        }

        [TestMethod]
        public void CoupleConstructsTuple3()
        {
            var values = Tuple.Create("1",2)
                .Singleton();

            var (text, number, answer) = values.Couple((s,i) => int.Parse(s) + i)
                .Single();

            Assert.AreEqual("1", text);
            Assert.AreEqual(2, number);
            Assert.AreEqual(3, answer);
        }
    }
}