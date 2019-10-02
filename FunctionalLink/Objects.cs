namespace FunctionalLink
{
    public static class Objects
    {
        public static Option<T> ToOption<T>(this T t) =>
            new SomeCase<T>(t);

        public static void Ignore<T>(this T t) { }
    }
}