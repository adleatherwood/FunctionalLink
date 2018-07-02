using Microsoft.VisualStudio.TestTools.UnitTesting;
using static FunctionalLink.Init;

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
    }
}
