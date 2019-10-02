# FUNCTIONAL LINK

This library is intented to be a gateway for functional concepts to be applied in C#.

[![pipeline status](https://gitlab.com/adleatherwood/FunctionalLink/badges/develop/pipeline.svg)](https://gitlab.com/adleatherwood/FunctionalLink/commits/develop)
[![coverage report](https://gitlab.com/adleatherwood/FunctionalLink/badges/develop/coverage.svg)](https://gitlab.com/adleatherwood/FunctionalLink/commits/develop)

```csharp
[TestMethod]
public void OptionExampleUsageOfBind()
{
    Option<string> NotEmpty(string email) =>
        !String.IsNullOrWhiteSpace(email) ? Some(email) : None;

    Option<string> IsEmail(string email) =>
        email.Contains("@") ? Some(email) : None;

    var actual = Some("me@gmail")
        .Bind(NotEmpty)
        .Bind(IsEmail)
        .Match(some => true, none => false);

    Assert.IsTrue(actual);
}
```

Seamless async function composition.

```csharp
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
```

Supports IEnumerable query syntax.

```csharp
[TestMethod]
public void OptionExampleUsageOfEnumerableQuery()
{
    Option<double> LookupCost(string itemId) => Some(10.00);
    Option<double> LookupTax(string areaId) => Some(0.06);

    var actual =  // <-- options translate to 0 or 1 records
        from cost in LookupCost("123")
        from tax  in LookupTax("CA")
        select cost + (cost * tax);

    Assert.AreEqual(10.60, actual.SingleOrDefault());
}
```

Also includes a Result monad with the same features.  Examples to come...

## INITIALIZERS

There a few helper methods that can be used to initialize collections with less ceremony.  Using a static import, like so:

```csharp
using static FunctionalLink.Init;
```

Gives you the ability to initialize collections like so:

```csharp
var list1 = list (1, 2, 3, 4, 5)

var array1 = array (1, 2, 3, 4, 5)

var dict1 = dict ((1,"one"), (2,"two"), (3,"three"))
var dict2 = dict (Tuple.Create(1,"one"), Tuple.Create(2,"two"), Tuple.Create(3,"three"))
```

[Icon Source](http://www.iconarchive.com/show/macaron-icons-by-goescat.html)