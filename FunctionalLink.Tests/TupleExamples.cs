using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static FunctionalLink.GlobalLink;

namespace FunctionalLink.Tests
{
    [TestClass]
    public class TuplesExamples
    {
        [TestMethod]
        public void TuplesExtendExample()
        {
            var listA = list(1,1,1);
            var listB = list(2,2);
            var listC = list(3);

            var actual = listA
                .Zip(listB, Tuple.Create)
                .Zip(listC, Tuples.Extend)
                .Select((a,b,c) => $"{a}{b}{c}")
                .Single();

            Assert.AreEqual("123", actual);
        }

        [TestMethod]
        public void TuplesExtendNoResultExample()
        {
            var listA = list(1,1);
            var listB = list(2);
            var listC = list<int>();

            var actual = listA
                .Zip(listB, Tuple.Create)
                .Zip(listC, Tuples.Extend)
                .Select((a,b,c) => $"{a}{b}{c}")
                .SingleOrDefault();

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void ValueTuplesExtendExample()
        {
            var listA = list(1);
            var listB = list(2);
            var listC = list(3);
            var listD = list(4);
            var listE = list(5);
            var listF = list(6);
            var listG = list(7);

            var actual = listA
                .Zip(listB, (a,b) => (a,b))
                .Zip(listC, Tuples.Extend)
                .Zip(listD, Tuples.Extend)
                .Zip(listE, Tuples.Extend)
                .Zip(listF, Tuples.Extend)
                .Zip(listG, Tuples.Extend)
                .Select((a,b,c,d,e,f,g) => $"{a}{b}{c}{d}{e}{f}{g}")
                .Single();

            Assert.AreEqual("1234567", actual);
        }
    }
}