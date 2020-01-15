using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static FunctionalLink.GlobalLink;

namespace FunctionalLink.Tests
{
    [TestClass]
    public class ObjectsTests
    {
        [TestMethod]
        public void ToOptionReturnsSomeForNonNull()
        {
            var isSome = new object()
                .ToOption()
                .Match(some => true, none => false);

            Assert.IsTrue(isSome);
        }

        [TestMethod]
        public void ToOptionReturnsNoneForNonNull()
        {
            var isNone = ((Object)null)
                .ToOption()
                .Match(some => false, none => true);

            Assert.IsTrue(isNone);
        }
    }
}