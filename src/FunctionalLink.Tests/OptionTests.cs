using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FunctionalLink;

namespace FunctionalLink.Tests
{
    [TestClass]
    public class OptionTests
    {
        [TestMethod]
        public void SomeCaseInitializesAndEvaluatesProperly()
        {
            Option<string> o = "test";

            var actual = o.Match(
                value => true,
                none => false);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void NoneCaseInitializesAndEvaluatesProperly()
        {
            Option<string> o = Option.None;
            
            var actual = o.Match(
                value => false,
                none => true);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void OptionMapEvaluatesProperlyWithSome()
        {
            var actual = Option.Some("t")
                .Map((s,a,b,c) => s + a + b + c, "e", "s", "t")
                .Value;

            Assert.AreEqual("test", actual);
        }

        [TestMethod]
        public void OptionMapEvaluatesProperlyWithNone()
        {
            var actual = new Option<string>(Option.None)
                .Map((s, a, b, c) => s + a + b + c, "e", "s", "t")
                .Value;

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void OptionIsSomeEnumeratesProperly()
        {
            var list = new List<Option<string>>() { "test" };

            var actual = list
                .Where(Option.IsSome)
                .Count();

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void OptionIsSomeEnumeratesProperlyWithNone()
        {
            var list = new List<Option<string>>() { Option.None };

            var actual = list
                .Where(Option.IsSome)
                .Count();

            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void OptionIsNoneEnumeratesProperly()
        {
            var list = new List<Option<string>>() { "test" };

            var actual = list
                .Where(Option.IsNone)
                .Count();

            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void OptionIsNoneEnumeratesProperlyWithNone()
        {
            var list = new List<Option<string>>() { Option.None };

            var actual = list
                .Where(Option.IsNone)
                .Count();

            Assert.AreEqual(1, actual);
        }

        //[TestMethod]
        //public void OptionValueOrDefaultEnumeratesProperly()
        //{
        //    var list = new List<Option<string>>() { "test" };

        //    var actual = list
        //        .Select(o => o.s("x"))
        //        .Single();

        //    Assert.AreEqual("test", actual);
        //}

        //[TestMethod]
        //public void OptionValueOrDefaultEnumeratesProperlyWhenNone()
        //{
        //    var list = new List<Option<string>>() { Option.None };

        //    var actual = list
        //        .Select(o => o.Su("x"))
        //        .Single();

        //    Assert.AreEqual("x", actual);
        //}

        [TestMethod]
        public void OptionValueOrEffectEnumeratesProperly()
        {
            var list = new List<Option<string>>() { "test" };
            var actual = false;
            list
                .Select(o => o.ValueOrEffect(() => actual = true))
                .Single()
                .Ignore();

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void OptionValueOrEffectEnumeratesProperlyWhenNone()
        {
            var list = new List<Option<string>>() { Option.None };
            var actual = false;
            list
                .Select(o => o.ValueOrEffect(() => actual = true))
                .Single()
                .Ignore();

            Assert.AreEqual(true, actual);
        }
    }
}
