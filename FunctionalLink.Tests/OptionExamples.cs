using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FunctionalLink;
using System.Threading.Tasks;

using static FunctionalLink.GlobalLink;

namespace FunctionalLink.Tests
{
    [TestClass]
    public class OptionExamples
    {
        [TestMethod]
        public void OptionExampleUsageOfBind()
        {
            Option<string> NotEmpty(string email) =>
                !String.IsNullOrWhiteSpace(email) ? Some(email) : None;

            Option<string> IsEmail(string email) =>
                email.Contains("@") ? Some(email) : None;

            var actual = Some("tony@gmail")
                .Bind(NotEmpty)
                .Bind(IsEmail)
                .Match(some => true, none => false);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void OptionExampleUsageOfWhere()
        {
            bool NotEmpty(string email) =>
                !String.IsNullOrWhiteSpace(email);

            bool IsEmail(string email) =>
                email.Contains("@");

            var actual = Some("tony@gmail")
                .Filter(NotEmpty)
                .Filter(IsEmail)
                .Match(some => true, none => false);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void OptionExampleUsageOfNullableConversion()
        {
            Option<int> DivideInto(int numerator, int denominator) =>
                denominator > 0
                    ? Some(numerator / denominator)
                    : None;

            int? denominator = null;

            var actual = Some(denominator)
                .Bind(DivideInto, 9)
                .Match(some => true, none => false);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void OptionExampleUsageOfVoid()
        {
            Option<int> DivideInto(int denominator, int numerator) =>
                denominator > 0
                    ? Some(numerator / denominator)
                    : None;

            void LogResult(int value) { Console.Write("Result = " + value); }

            int denominator = 3;

            var actual = Some(denominator)
                .Bind(DivideInto, 12)
                .Void(LogResult)
                .Match(some => some, none => -1);

            Assert.AreEqual(4, actual);
        }

        [TestMethod]
        public async Task OptionExampleUsageOfAsyncVoid()
        {
            Option<int> DivideInto(int denominator, int numerator) =>
                denominator > 0
                    ? Some(numerator / denominator)
                    : None;

            Task LogResult(int value) => Task.FromResult(0);

            int denominator = 3;

            var actual = await Some(denominator)
                .Bind(DivideInto, 12)
                .Void(LogResult)   // <-- this call is seamlessly async
                .Match(some => some, none => -1);

            Assert.AreEqual(4, actual);
        }

        [TestMethod]
        public void OptionExampleUsageOfEnumerableMethod()
        {
            int MultiplyBy(int a, int b) =>
                a * b;

            int number = 3;

            var actual = Some(number)
                .Where(d => d > 0)
                .Select(MultiplyBy, 4)
                .SingleOrDefault();

            Assert.AreEqual(12, actual);
        }

        [TestMethod]
        public void OptionExampleUsageOfEnumerableQuery()
        {
            Option<double> LookupCost(string itemId) => Some(10.00);
            Option<double> LookupTax(string areaId) => Some(0.06);

            var actual =  // <-- options translate to 0 or 1 records
                from cost in LookupCost("123")
                from tax in LookupTax("CA")
                select cost + (cost * tax);

            Assert.AreEqual(10.60, actual.SingleOrDefault());
        }

        [TestMethod]
        public void OptionExampleUsageOfEnumerableMethodWithTupleDeconstructor()
        {
            Option<double> LookupCost(string itemId) => Some(10.00);
            Option<double> LookupTax(string areaId) => Some(0.06);

            var actual = LookupCost("123")
                .Zip(LookupTax("CA"))            // <-- this tuple is deconstructed
                .Select((cost, tax) => cost + (cost * tax)) // <-- and pass to this
                .SingleOrDefault();

            Assert.AreEqual(10.60, actual);
        }
    }
}
