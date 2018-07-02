using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FunctionalLink;
using static FunctionalLink.Init;

namespace Glue2U.Tests
{
    [TestClass]
    public class SelfTests
    {
        [TestMethod]
        public void IdReturnsSameInstance()
        {
            var expected = new Test();
            var actual = Self.Id(expected);

            Assert.AreSame(expected, actual);
        }

        [TestMethod]
        public void IgnoreExpectsNoFurtherAction()
        {
            new Test().Ignore();
        }

        [TestMethod]
        public void EvaluateAndIgnoreForcesEvaluation()
        {
            var list = lst(1, 2, 3, 4);
            var actual = lst<int>();

            list.Where(i => i % 2 == 0)
                .Select(i => {actual.Add(i); return i; })
                .EvaluateAndIgnore();

            Assert.AreEqual(2, actual[0]);
            Assert.AreEqual(4, actual[1]);
        }

        private class Test
        {
        }
    }
}
