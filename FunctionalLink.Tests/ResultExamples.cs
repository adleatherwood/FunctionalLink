using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FunctionalLink;
using System.Threading.Tasks;

using static FunctionalLink.GlobalLink;

namespace FunctionalLink.Tests
{
    [TestClass]
    public class ResultExamples
    {
        [TestMethod]
        public void ResultExampleUsageOfImplicitBool()
        {
            Result<double> LookupCost(string itemId) => Success(10.00);
            Result<double> LookupTax(string areaId) => Success(0.06);

            var cost = LookupCost("123");
            var tax  = LookupTax("CA");

            var actual = cost && tax
                ? cost.Value + (cost.Value * tax.Value)
                : 0;

            Assert.AreEqual(10.60, actual);
        }

        [TestMethod]
        public void ResultExampleUsageOfImplicitBoolNegative()
        {
            Result<double> LookupCost(string itemId) => Success(10.00);
            Result<double> LookupTax(string areaId) => Failure("Area not found");

            var cost = LookupCost("123");
            var tax  = LookupTax("CA");

            var actual = cost && tax
                ? cost.Value + (cost.Value * tax.Value)
                : 0;

            Assert.AreEqual(0, actual);
        }
    }
}