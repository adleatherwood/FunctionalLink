using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FunctionalLink;
using static FunctionalLink.Init;

namespace FunctionalLink.Tests
{
    [TestClass]
    public class EnumerablesTests
    {
        [TestMethod]
        public void Select1EvaluatesProperly()
        {
            var list = lst (1, 2, 3);
            var actual = list
                .Select((i, a) => i + a, 1)
                .ToArray();
            Assert.AreEqual(2, actual[0]);
            Assert.AreEqual(3, actual[1]);
            Assert.AreEqual(4, actual[2]);
        }

        [TestMethod]
        public void Select2EvaluatesProperly()
        {
            var list = lst (1, 2, 3);
            var actual = list
                .Select((i, a, b) => i + a + b, 1, 2)
                .ToArray();
            Assert.AreEqual(4, actual[0]);
            Assert.AreEqual(5, actual[1]);
            Assert.AreEqual(6, actual[2]);
        }

        [TestMethod]
        public void Select3EvaluatesProperly()
        {
            var list = lst (1, 2, 3);
            var actual = list
                .Select((i, a, b, c) => i + a + b + c, 1, 2, 3)
                .ToArray();
            Assert.AreEqual(7, actual[0]);
            Assert.AreEqual(8, actual[1]);
            Assert.AreEqual(9, actual[2]);
        }

        [TestMethod]
        public void IterateEvaluatesProperly()
        {            
            var actual = new List<int>();
            lst (1, 2, 3)
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
            lst (1, 2, 3)
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
            lst (1, 2, 3)
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
            lst (1, 2, 3)
                .Iterate((i, a, b, c) => actual.Add(i + a + b + c), 1, 2, 3)
                .EvaluateAndIgnore();

            Assert.AreEqual(7, actual[0]);
            Assert.AreEqual(8, actual[1]);
            Assert.AreEqual(9, actual[2]);
        }
    }
}