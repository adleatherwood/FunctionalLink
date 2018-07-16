﻿using System.Globalization;
using System.Threading.Tasks;
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
        public void Union4ConstructsProperly()
        {
            var actualA = new Union<int, string, double, char>(1);

            Assert.AreEqual(0, actualA.Tag);
            Assert.AreEqual(1, actualA.Value);

            var actualB = new Union<int, string, double, char>("one");

            Assert.AreEqual(1, actualB.Tag);
            Assert.AreEqual("one", actualB.ValueB);

            var actualC = new Union<int, string, double, char>(1.1);

            Assert.AreEqual(2, actualC.Tag);
            Assert.AreEqual(1.1, actualC.ValueC);

            var actualD = new Union<int, string, double, char>('x');

            Assert.AreEqual(3, actualD.Tag);
            Assert.AreEqual('x', actualD.ValueD);
        }

        [TestMethod]
        public void Match4EvaluatesCorrectResults()
        {
            var actual1 = new Union<int, string, double, char>(1)
                .Match(
                        i => i.ToString(), 
                        s => s + "x", 
                        d => d.ToString(CultureInfo.InvariantCulture),
                        c => c.ToString());

            Assert.AreEqual("1", actual1);

            var actual2 = new Union<int, string, double, char>("1")
                .Match(
                    i => i.ToString(), 
                    s => s + "x", 
                    d => d.ToString(CultureInfo.InvariantCulture),
                    c => c.ToString());

            Assert.AreEqual("1x", actual2);

            var actual3 = new Union<int, string, double, char>(1.1)
                .Match(
                    i => i.ToString(), 
                    s => s + "x", 
                    d => d.ToString(CultureInfo.InvariantCulture),
                    c => c.ToString());

            Assert.AreEqual("1.1", actual3);

            var actual4 = new Union<int, string, double, char>('x')
                .Match(
                    i => i.ToString(), 
                    s => s + "x", 
                    d => d.ToString(CultureInfo.InvariantCulture),
                    c => c.ToString());

            Assert.AreEqual("x", actual4);
        }     

        [TestMethod]
        public void Union5ConstructsProperly()
        {
            var actualA = new Union<int, string, double, char, long>(1);

            Assert.AreEqual(0, actualA.Tag);
            Assert.AreEqual(1, actualA.Value);

            var actualB = new Union<int, string, double, char, long>("one");

            Assert.AreEqual(1, actualB.Tag);
            Assert.AreEqual("one", actualB.ValueB);

            var actualC = new Union<int, string, double, char, long>(1.1);

            Assert.AreEqual(2, actualC.Tag);
            Assert.AreEqual(1.1, actualC.ValueC);

            var actualD = new Union<int, string, double, char, long>('x');

            Assert.AreEqual(3, actualD.Tag);
            Assert.AreEqual('x', actualD.ValueD);

            var actualE = new Union<int, string, double, char, long>(2L);

            Assert.AreEqual(4, actualE.Tag);
            Assert.AreEqual(2L, actualE.ValueE);
        }  

        [TestMethod]
        public void Match5EvaluatesCorrectResults()
        {
            var actual1 = new Union<int, string, double, char, long>(1)
                .Match(
                    i => i.ToString(), 
                    s => s + "x", 
                    d => d.ToString(CultureInfo.InvariantCulture),
                    c => c.ToString(),
                    l => l.ToString());

            Assert.AreEqual("1", actual1);

            var actual2 = new Union<int, string, double, char, long>("1")
                .Match(
                    i => i.ToString(), 
                    s => s + "x", 
                    d => d.ToString(CultureInfo.InvariantCulture),
                    c => c.ToString(),
                    l => l.ToString());

            Assert.AreEqual("1x", actual2);

            var actual3 = new Union<int, string, double, char, long>(1.1)
                .Match(
                    i => i.ToString(), 
                    s => s + "x", 
                    d => d.ToString(CultureInfo.InvariantCulture),
                    c => c.ToString(),
                    l => l.ToString());

            Assert.AreEqual("1.1", actual3);

            var actual4 = new Union<int, string, double, char, long>('x')
                .Match(
                    i => i.ToString(), 
                    s => s + "x", 
                    d => d.ToString(CultureInfo.InvariantCulture),
                    c => c.ToString(),
                    l => l.ToString());

            Assert.AreEqual("x", actual4);

            var actual5 = new Union<int, string, double, char, long>(2L)
                .Match(
                    i => i.ToString(), 
                    s => s + "x", 
                    d => d.ToString(CultureInfo.InvariantCulture),
                    c => c.ToString(),
                    l => l.ToString());

            Assert.AreEqual("2", actual5);
        }      

        [TestMethod]
        public async Task MatchAsync1EvaluatesCorrectResults()
        {
            var actual1 = await new Union<int>(1)
                .Match(
                    i => Task.Run(() => i.ToString()));

            Assert.AreEqual("1", actual1);
        }

        [TestMethod]
        public async Task MatchAsync2EvaluatesCorrectResults()
        {
            var actual1 = await new Union<int, string>(1)
                .Match(
                    i => Task.Run(() => i.ToString()), 
                    s => Task.Run(() => s + "x"));

            Assert.AreEqual("1", actual1);

            var actual2 = await new Union<int, string>("1")
                .Match(
                    i => Task.Run(() => i.ToString()), 
                    s => Task.Run(() => s + "x"));

            Assert.AreEqual("1x", actual2);
        }

        [TestMethod]
        public async Task MatchAsync3EvaluatesCorrectResults()
        {
            var actual1 = await new Union<int, string, double>(1)
                .Match(
                    i => Task.Run(() => i.ToString()), 
                    s => Task.Run(() => s + "x"), 
                    d => Task.Run(() => d.ToString(CultureInfo.InvariantCulture)));

            Assert.AreEqual("1", actual1);

            var actual2 = await new Union<int, string, double>("1")
                .Match(
                    i => Task.Run(() => i.ToString()), 
                    s => Task.Run(() => s + "x"), 
                    d => Task.Run(() => d.ToString(CultureInfo.InvariantCulture)));

            Assert.AreEqual("1x", actual2);

            var actual3 = await new Union<int, string, double>(1.1)
                .Match(
                    i => Task.Run(() => i.ToString()), 
                    s => Task.Run(() => s + "x"), 
                    d => Task.Run(() => d.ToString(CultureInfo.InvariantCulture)));

            Assert.AreEqual("1.1", actual3);
        }

        [TestMethod]
        public async Task MatchAsync4EvaluatesCorrectResults()
        {
            var actual1 = await new Union<int, string, double, char>(1)
                .Match(
                    i => Task.Run(() => i.ToString()), 
                    s => Task.Run(() => s + "x"), 
                    d => Task.Run(() => d.ToString(CultureInfo.InvariantCulture)),
                    c => Task.Run(() => c.ToString()));

            Assert.AreEqual("1", actual1);

            var actual2 = await new Union<int, string, double, char>("1")
                .Match(
                    i => Task.Run(() => i.ToString()), 
                    s => Task.Run(() => s + "x"), 
                    d => Task.Run(() => d.ToString(CultureInfo.InvariantCulture)),
                    c => Task.Run(() => c.ToString()));

            Assert.AreEqual("1x", actual2);

            var actual3 = await new Union<int, string, double, char>(1.1)
                .Match(
                    i => Task.Run(() => i.ToString()), 
                    s => Task.Run(() => s + "x"), 
                    d => Task.Run(() => d.ToString(CultureInfo.InvariantCulture)),
                    c => Task.Run(() => c.ToString()));

            Assert.AreEqual("1.1", actual3);

            var actual4 = await new Union<int, string, double, char>('x')
                .Match(
                    i => Task.Run(() => i.ToString()), 
                    s => Task.Run(() => s + "x"), 
                    d => Task.Run(() => d.ToString(CultureInfo.InvariantCulture)),
                    c => Task.Run(() => c.ToString()));

            Assert.AreEqual("x", actual4);
        }

        [TestMethod]
        public async Task MatchAsync5EvaluatesCorrectResults()
        {
            var actual1 = await new Union<int, string, double, char, long>(1)
                .Match(
                    i => Task.Run(() => i.ToString()), 
                    s => Task.Run(() => s + "x"), 
                    d => Task.Run(() => d.ToString(CultureInfo.InvariantCulture)),
                    c => Task.Run(() => c.ToString()),
                    l => Task.Run(() => l.ToString()));

            Assert.AreEqual("1", actual1);

            var actual2 = await new Union<int, string, double, char, long>("1")
                .Match(
                    i => Task.Run(() => i.ToString()), 
                    s => Task.Run(() => s + "x"), 
                    d => Task.Run(() => d.ToString(CultureInfo.InvariantCulture)),
                    c => Task.Run(() => c.ToString()),
                    l => Task.Run(() => l.ToString()));

            Assert.AreEqual("1x", actual2);

            var actual3 = await new Union<int, string, double, char, long>(1.1)
                .Match(
                    i => Task.Run(() => i.ToString()), 
                    s => Task.Run(() => s + "x"), 
                    d => Task.Run(() => d.ToString(CultureInfo.InvariantCulture)),
                    c => Task.Run(() => c.ToString()),
                    l => Task.Run(() => l.ToString()));

            Assert.AreEqual("1.1", actual3);

            var actual4 = await new Union<int, string, double, char, long>('x')
                .Match(
                    i => Task.Run(() => i.ToString()), 
                    s => Task.Run(() => s + "x"), 
                    d => Task.Run(() => d.ToString(CultureInfo.InvariantCulture)),
                    c => Task.Run(() => c.ToString()),
                    l => Task.Run(() => l.ToString()));

            Assert.AreEqual("x", actual4);

            var actual5 = await new Union<int, string, double, char, long>(2L)
                .Match(
                    i => Task.Run(() => i.ToString()), 
                    s => Task.Run(() => s + "x"), 
                    d => Task.Run(() => d.ToString(CultureInfo.InvariantCulture)),
                    c => Task.Run(() => c.ToString()),
                    l => Task.Run(() => l.ToString()));

            Assert.AreEqual("2", actual5);
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
        public void ImplicitOperators4InitializeCorrectly()
        {
            Union<int, string, double, char> actual1 = 1;
            Union<int, string, double, char> actual2 = "one";
            Union<int, string, double, char> actual3 = 1.1;
            Union<int, string, double, char> actual4 = 'x';
            
            Assert.AreEqual(1, actual1.Value);
            Assert.AreEqual("one", actual2.ValueB);
            Assert.AreEqual(1.1, actual3.ValueC);
            Assert.AreEqual('x', actual4.ValueD);
        }

        [TestMethod]
        public void ImplicitOperators5InitializeCorrectly()
        {
            Union<int, string, double, char, long> actual1 = 1;
            Union<int, string, double, char, long> actual2 = "one";
            Union<int, string, double, char, long> actual3 = 1.1;
            Union<int, string, double, char, long> actual4 = 'x';
            Union<int, string, double, char, long> actual5 = 2L;
            
            Assert.AreEqual(1, actual1.Value);
            Assert.AreEqual("one", actual2.ValueB);
            Assert.AreEqual(1.1, actual3.ValueC);
            Assert.AreEqual('x', actual4.ValueD);
            Assert.AreEqual(2L, actual5.ValueE);
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
