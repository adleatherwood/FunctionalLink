using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static FunctionalLink.GlobalLink;

namespace FunctionalLink.Tests
{
    public static class GlobalTest
    {
        public static Task Async() => Task.FromResult(0);
        public static Task<T> Async<T>(T t) => Task.FromResult(t);

        public static readonly string NullString = null;
    }

    public static class Objects
    {
        public static Task<T> ToTask<T>(this T t) => Task.FromResult(t);
    }

    [TestClass]
    public class GlobalLinkTests
    {
        [TestMethod]
        public void SelfReturnsSameReference()
        {
            var o1 = new object();
            var o2 = Self(o1);

            Assert.ReferenceEquals(o1, o2);
        }

        [TestMethod]
        public void UnitValuesAllHaveSameReference()
        {
            var u1 = Unit;
            var u2 = UnitType.Value;

            Assert.ReferenceEquals(u1, u2);
        }

        [TestMethod]
        public void SingletonProvidesASingleItemList()
        {
            var expected = 123;
            var actual = singleton(expected);

            Assert.AreEqual(expected, actual[0]);
        }
    }
}