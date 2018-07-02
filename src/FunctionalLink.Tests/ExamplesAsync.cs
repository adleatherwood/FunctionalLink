using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FunctionalLink;
using static FunctionalLink.Init;

#pragma warning disable 1998

namespace FunctionalLink.Tests
{
    [TestClass]
    public class ExamplesAsync
    {
        [TestMethod]
        public async Task ExampleAsync()
        {
            var actual = await TryParse("1")
                .Map(i => Add(i, 1))                
                .Void(SideEffect)
                .Next(TryGuid)
                .Next(TryInt)
                .SuccessOrEffect(SideEffect2);

            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        public async Task EnumerableExample()
        {
            var list1 = list ( "1", "b", "3" );
            var list2 = list ( "a", "2", "9" );

            var actual = list1.Zip(list2, Tuple.Create)
                .Select(Pivot)
                .SelectMany(Self.Id)
                .Select(TryParse)
                .Select(r => r.Map(i => Add(i, 1)))
                .Select(r => r.Void(SideEffect))
                .Select(r => r.Next(TryGuid))
                .Select(r => r.Next(TryInt))
                .Select(AsString)
                .Iterate(SideEffect2)
                .ToArray();

            Assert.AreEqual(await actual[0], "2");
            Assert.AreEqual(await actual[1], "Failed to parse.");
            Assert.AreEqual(await actual[2], "Failed to parse.");
            Assert.AreEqual(await actual[3], "3");
            Assert.AreEqual(await actual[4], "4");
            Assert.AreEqual(await actual[5], "Failed to Guid.");
        }

        private static IEnumerable<string> Pivot(string value1, string value2)
        {
            yield return value1;
            yield return value2;
        }

        private static async Task<Result<int, string>> TryParse(string value)
        {
            return int.TryParse(value, out var result)
                ? Result.Success<int, string>(result) 
                : "Failed to parse.";
        }

        private static Result<Guid, string> TryGuid(int value)
        {
            return Guid.TryParse("00000000-0000-0000-0000-00000000000" + value, out var result)
                ? Result.Success<Guid, string>(result)
                : "Failed to Guid.";             
        }

        private static Task<Result<int, string>> TryInt(Guid value)
        {
            return TryParse(value.ToString().Last() + "");
        }

        private static int Add(int number, int increment)
        {
            return number + increment;
        }
       
        private static async Task<string> AsString(Task<Result<int, string>> result)
        {
            return (await result).Match(
                success => success.ToString(),
                failure => failure);
        }

        private static Task SideEffect(int value)
        {
            return Task.CompletedTask;
        }
        
        private static async Task SideEffect2(string value)
        {
            // noop;
        }

        private static async void SideEffect2(Task<string> value)
        {
            await value;
        }
    }
}
