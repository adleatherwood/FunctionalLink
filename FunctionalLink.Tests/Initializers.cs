using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static FunctionalLink.GlobalLink;

namespace FunctionalLink.Tests
{
    [TestClass]
    public class InitTests
    {
        [TestMethod]
        public void DictInitializesAllValues()
        {
            var d = dict((1, "one"), (2, "two"));

            Assert.AreEqual("one", d[1]);
            Assert.AreEqual("two", d[2]);
        }

        [TestMethod]
        public void DictInitializesAllValuesWithTuples()
        {
            var d = dict(Tuple.Create(1, "one"), Tuple.Create(2, "two"));

            Assert.AreEqual("one", d[1]);
            Assert.AreEqual("two", d[2]);
        }

        [TestMethod]
        public void ListInitializesAllValues()
        {
            var l = list(1, 2, 3);

            Assert.AreEqual(1, l[0]);
            Assert.AreEqual(2, l[1]);
            Assert.AreEqual(3, l[2]);
        }

        [TestMethod]
        public void ArrayInitializesAllValues()
        {
            var l = array(1, 2, 3);

            Assert.AreEqual(1, l[0]);
            Assert.AreEqual(2, l[1]);
            Assert.AreEqual(3, l[2]);
        }
    }
}
