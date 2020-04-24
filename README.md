# FUNCTIONAL LINK

This library is intented to be a gateway for functional concepts to be applied in C#.

[![pipeline status](https://gitlab.com/adleatherwood/FunctionalLink/badges/master/pipeline.svg)](https://gitlab.com/adleatherwood/FunctionalLink/commits/master)
[![coverage report](https://gitlab.com/adleatherwood/FunctionalLink/badges/master/coverage.svg)](https://gitlab.com/adleatherwood/FunctionalLink/commits/master)
[![NuGet](https://img.shields.io/nuget/v/FunctionalLink.svg?style=flat)](https://www.nuget.org/packages/FunctionalLink/)

## Functions & Types

#### Option\<T>
Options are useful when you want to return a possible non-value without using null.  This make for clearer & more composable function signatures.
```cs
Option<int> DivideInto(int denominator, int numerator) =>
    denominator > 0
        ? Some(numerator / denominator)
        : None;
```
You can turn an ordinary nullable class instance into an Option like so:
```cs
var option = Option.From(default(string));
```
#### Result\<T> & Result\<T, TFailure>
Results are useful when you want to return either a value or an error to the consumer.  With this signature, the caller doesn't have to be concerned with an exception occurring.

Result\<T> assumes a string for the failure type.

Result\<T, TFailure> lets you specify a custom failure type.
```cs
Result<int> DivideInto(int denominator, int numerator) =>
    denominator > 0
        ? Success(numerator / denominator)
        : Failure("Denominator is zero");
```
#### Monad Functions

Via extension methods, there are several primary functions available for composing other functions together.  They are basically the same between the Option & the Result types.  There are async & other overloads for each of these methods.

* Filter: Takes a value and returns a bool.
* Bind: Takes a value and returning an Option or Result.
* Map: Takes a value and transforming it.
* Void: Takes a value and returns nothing.  This usually denotes a side-effect is occurring (reading or writing).

This isn't a practical example, but attempts to be familiar in order to show the different signatures working together.
```cs
class User {
    public string Name;
    // ...
}

// filter (like: Where)
bool NotEmpty(string value) =>
    !String.IsNullOrWhitespace(value);

// bind (like: SelectMany)
// this function may not be able to return a value!
Option<string> ExtractName(string email) =>
    value.Split('@').Length == 2
        ? Some(value.Split('@').First())
        : None;

// map (like: Select)
User ToUser(string name) =>
    new User { Name=name };

Option<Name> TryCreateUser(string email) =>
    Option.From(value)
        .Filter(NotEmpty)
        .Bind(ExtractName)
        .Map(ToUser);
```
With a few variations, this could also be represented with a Result.

```cs
// ...

Result<string> ExtractName(string email) =>
    value.Split('@').Length == 2
        ? Success(value.Split('@').First())
        : Failure("Invalid email format");

//...

Result<Name> TryCreateUser(string email) =>
    Result.Success(value)
        .Filter(NotEmpty)
        .Bind(ExtractName)
        .Map(ToUser);
```
In this way, the caller would not only know the user creation didn't work, but why as well.

The caller can then use the result like so:
```cs
var result = TryCreateUser("elconquistador@gmail.com")
    .Match(
        success => WriteDb(success),
        failure => throw new Exception(failure));
```

#### Global Functions

```cs
using static FunctionalLink.GlobalLink;
```
Some & None: Functions for creating a Options in a more succinct manner.
```cs
Option<T> IsAnswer(bool answer) =>
    answer == true
        ? Some(answer)
        : None;
```
Success & Failure: Functions for creating Results in a more succinct manner.
```cs
Result<T> ValidateString(string value) =>
    String.IsNullOrWhitespace(value)
        ? Failure("Value is empty)
        : Success(value);
```
There are multiple functions available to initialize collections like so:
```csharp
var numbers = list (1, 2, 3, 4, 5);

var numbers = array (1, 2, 3, 4, 5);

var numbers = dict ((1,"one"), (2,"two"), (3,"three"));

var numbers = dict (Tuple.Create(1,"one"), Tuple.Create(2,"two"), Tuple.Create(3,"three"));
```
Singleton: A function for turning single items into an enumerable with a single item.
```cs
bool ValidateString(string value) =>
    singleton(value)
        .Where(v => v != null)
        .Where(v => v.Length > 0)
        .Any();
```

## Examples

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

    var actual =  // <-- options translate to 0 or 1 enumerable records
        from cost in LookupCost("123")
        from tax  in LookupTax("CA")
        select cost + (cost * tax);

    Assert.AreEqual(10.60, actual.SingleOrDefault());
}
```

Supports JS-like evaluations if that's what you're into.

```cs
[TestMethod]
public void OptionExampleUsageOfImplicitBool()
{
    Option<double> LookupCost(string itemId) => Some(10.00);
    Option<double> LookupTax(string areaId) => Some(0.06);

    var cost = LookupCost("123");
    var tax  = LookupTax("CA");

    var actual = cost && tax
        ? cost.Value + (cost.Value * tax.Value)
        : 0;

    Assert.AreEqual(10.60, actual);
}
```

Also includes a Result monad with the same features.  Examples to come...

[Icon Source](http://www.iconarchive.com/show/macaron-icons-by-goescat.html)
