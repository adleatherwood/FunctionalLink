using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FunctionalLink;

namespace FunctionalLink.Tests
{
    [TestClass]
    public class UnionTests
    {
        [TestMethod]
        public void Union1ConstructsProperly()
        {
            var actual = new Union<int>(1);

            Assert.AreEqual(0, actual.Tag);
            Assert.AreEqual(1, actual.Value);
        }

        [TestMethod]
        public void Match1EvaluatesCorrectResults()
        {
            var actual = new Union<int>(1)
                .Match(i => i.ToString());

            Assert.AreEqual("1", actual);
        }

        [TestMethod]
        public void Union2ConstructsProperly()
        {
            var actualA = new Union<int, string>(1);

            Assert.AreEqual(0, actualA.Tag);
            Assert.AreEqual(1, actualA.Value);

            var actualB = new Union<int, string>("one");

            Assert.AreEqual(1, actualB.Tag);
            Assert.AreEqual("one", actualB.ValueB);
        }

        [TestMethod]
        public void Match2EvaluatesCorrectResults()
        {
            var actual1 = new Union<int, string>(1)
                .Match(i => i.ToString(), s => s + "x");

            Assert.AreEqual("1", actual1);

            var actual2 = new Union<int, string>("1")
                .Match(i => i.ToString(), s => s + "x");

            Assert.AreEqual("1x", actual2);
        }

        [TestMethod]
        public void Union3ConstructsProperly()
        {
            var actualA = new Union<int, string, double>(1);

            Assert.AreEqual(0, actualA.Tag);
            Assert.AreEqual(1, actualA.Value);

            var actualB = new Union<int, string, double>("one");

            Assert.AreEqual(1, actualB.Tag);
            Assert.AreEqual("one", actualB.ValueB);

            var actualC = new Union<int, string, double>(1.1);

            Assert.AreEqual(2, actualC.Tag);
            Assert.AreEqual(1.1, actualC.ValueC);
        }

        [TestMethod]
        public void Match3EvaluatesCorrectResults()
        {
            var actual1 = new Union<int, string, double>(1)
                .Match(i => i.ToString(), s => s + "x", d => d.ToString(CultureInfo.InvariantCulture));

            Assert.AreEqual("1", actual1);

            var actual2 = new Union<int, string, double>("1")
                .Match(i => i.ToString(), s => s + "x", d => d.ToString(CultureInfo.InvariantCulture));

            Assert.AreEqual("1x", actual2);

            var actual3 = new Union<int, string, double>(1.1)
                .Match(i => i.ToString(), s => s + "x", d => d.ToString(CultureInfo.InvariantCulture));

            Assert.AreEqual("1.1", actual3);
        }

        [TestMethod]
        public void ImplicitOperators2InitializeCorrectly()
        {
            Union<int, string> actual1 = 1;
            Union<int, string> actual2 = "one";
            
            Assert.AreEqual(1, actual1.Value);
            Assert.AreEqual("one", actual2.ValueB);            
        }

        [TestMethod]
        public void ImplicitOperators3InitializeCorrectly()
        {
            Union<int, string, double> actual1 = 1;
            Union<int, string, double> actual2 = "one";
            Union<int, string, double> actual3 = 1.1;
            
            Assert.AreEqual(1, actual1.Value);
            Assert.AreEqual("one", actual2.ValueB);
            Assert.AreEqual(1.1, actual3.ValueC);
        }

        [TestMethod]
        public void SingleCaseUnionsLookAgreableInUse()
        {
            var x = new Union<PatientId, string>("");
            var y = new Union<PatientId, string>(new PatientId(""));
            var z = y.Match(
                p => p.Value,
                s => s);
        }

        private class PatientId : Union<string>
        {
            public PatientId(string value) : base(value) { }
        }
    }
}
