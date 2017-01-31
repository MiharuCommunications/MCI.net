# Monad

## Option<T>

```cs
using Miharu;

Option<int> opt = Option<int>.Return(10);
Option<int> opt = Option<int>.Fail();
```

## Either<L, R>

```cs
using Miharu;

Either<string, int> e = new Right<string, int>(100);
Either<string, int> e = new Left<string, int>("Error !!");
```

## Try<T>

```cs
using Miharu;

Try<int> t = Try<int>.Success(10);
Try<int> t = Try<int>.Fail(new NotImplementedException());
Try<int> t = Try<int>.Execute(() =>
{
    return int.Parse("11")
});
```

## Future<T>

## with LINQ

```cs
var result = from v1 in GetIntAsOption("1")
             from v2 in GetIntAsOption("2")
             select v1 + v2;
// result == Some<int>(3)

var result = from v1 in GetIntAsOption("1")
             from v2 in GetIntAsOption("z")
             select v1 + v2;
// result == None<int>()
```
