using System.Threading.Tasks;

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
}