using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FunctionalLink;
using static FunctionalLink.Init;

namespace FunctionalLink.Tests
{
    [TestClass]
    public class Examples
    {
        [TestMethod]
        public void Example()
        {
            var actual = TryParse("1")
                .Map(Add, 1)
                .Void(SideEffect)
                .Next(TryGuid)
                .Next(TryInt)
                //.With(M<other>, Func<this,other,M<this>>  <-- if this and other then fold else this.error || other.error
                //.When(i > 3, Map/Void/Next) <-- too many possibilities.  overrides won't work out
                //.NextWhen(Func<T,bool>, Func<T,M<TResult>) <-- bind/next
                //.MapWhen(Func<T,bool>, Func<T,TResult) <-- lame
                //.VoidWhen(Func<T,bool>, Action<T>) <-- lame
                // need a not version for error results
                //.Fail(???) .Else???
                .SuccessOrEffect(SideEffect2);
            // When | Unless (more like if/then than filter) // filter // guard // ensure
            // FlatMap // selectMany
            // fold (like "And" or "With" or "Plus") ::: 
            // "Map" to "Lift"?
            Assert.AreEqual(2, actual);
        }


        [TestMethod]
        public void EnumerableExample()
        {
            var list1 = list ( "1", "b", "3" );
            var list2 = list ( "a", "2", "9" );

            var actual = list1.Zip(list2, Tuple.Create)
                .Select(Pivot)
                .SelectMany(Self.Id)
                .Select(TryParse)                
                .Select(r => r.Map(Add, 1))
                .Select(r => r.Void(SideEffect))
                .Select(r => r.Next(TryGuid))
                .Select(r => r.Next(TryInt))
                .Select(AsString)
                .Iterate(SideEffect2)
                .ToArray();

            Assert.AreEqual(actual[0], "2");
            Assert.AreEqual(actual[1], "Failed to parse.");
            Assert.AreEqual(actual[2], "Failed to parse.");
            Assert.AreEqual(actual[3], "3");
            Assert.AreEqual(actual[4], "4");
            Assert.AreEqual(actual[5], "Failed to Guid.");
        }
        
        private static IEnumerable<string> Pivot(string value1, string value2)
        {
            yield return value1;
            yield return value2;
        }

        private static Result<int> TryParse(string value)
        {
            return int.TryParse(value, out var result)
                ? Result.Success(result)
                : "Failed to parse.";
        }

        private static Result<Guid, string> TryGuid(int value)
        {
            return Guid.TryParse("00000000-0000-0000-0000-00000000000" + value, out var result)
                ? Result.Success<Guid, string>(result)
                : "Failed to Guid.";             
        }

        private static Result<int, string> TryInt(Guid value)
        {
            return TryParse(value.ToString().Last() + "");
        }

        private static int Add(int number, int increment)
        {
            return number + increment;
        }
       
        private static string AsString(Result<int, string> result)
        {
            return result.Match(
                success => success.ToString(),
                failure => failure);
        }

        private static void SideEffect(int value)
        {
            //
        }

        private static void SideEffect2(string value)
        {
            //
        }
    }
}
