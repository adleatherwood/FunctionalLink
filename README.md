# FUNCTIONAL LINK

This library is intented to be a gateway for functional concepts to be applied in C#.

```csharp
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
```


## SELF

Just a few basic extension methods available in this space.

```csharp
T Self.Id(this T t)
void Self.Ignore(this T t) 
```

## UNIONS

Unions are implemented as tagged unions and are serializable.  They are generic with up to 5 different types with compatible implicit conversions and tag matching functions.  

```csharp
var result = new Union<int, string>(1).Match(
    number => number.ToString(), 
    test => text + "more text");
```


## OPTION

Options are implemented as a Union of T or None.  Options are serializable and inherently support tag matching.

```csharp
var o = Option.Some("test");

var actual = o.Match(
    value => true,
    none => false);

Assert.IsTrue(actual);
```

Option support basic monadic functions: Map, Void, Next (bind), ValueOrEffect.
There are async compatible version of these methods that help reduce the number of async keywords in code.

## RESULT

Result is implemented as a Union of T & TFailure.  Result\<T\> is a convenience type implementing Result\<T, string\> as the underlying type.  Results support implicit conversions and inherently support tag matching.

```csharp
var o = Result.Success("test");

var actual = o.Match(
    value => true,
    none => false);

Assert.IsTrue(actual);
```

Result support basic monadic functions: Map, Void, Next (bind), SuccessOrEffect.
There are async compatible version of these methods that help reduce the number of async keywords in code.

## ENUMERABLES

There are a handful of enumerable extension methods that support passing in dependencies to mapping functions without having to resort to lambda expressions.

```csharp
IEnumerable<TResult> Select(this IEnumerable<T> e, Func<T,A,B,C,TResult> f, a, b, c)
IEnumerable<TResult> Select(this IEnumerable<Tuple<A, B>>, Func<A,B,TResult> f)
IEnumerable<T> Iterate(this IEnumerable<T> e)
void EvaluateAndIgnore(IEnumerable<T> e)
```

## INITIALIZERS

There a few helper methods that can be used to initialize collections with less ceremony.  Using a static import, like so:

```csharp
using static FunctionalLink.Init;
```

Gives you the ability to initialize collections like so:

```csharp
var list1 = list (1, 2, 3, 4, 5)
var list2 = lst (1, 2, 3, 4, 5) 

var array1 = array (1, 2, 3, 4, 5)
var array2 = arr (1, 2, 3, 4, 5) 

var dict1 = dict ((1,"one"), (2,"two"), (3,"three"))
var dict2 = dict (Tuple.Create(1,"one"), Tuple.Create(2,"two"), Tuple.Create(3,"three"))
```
