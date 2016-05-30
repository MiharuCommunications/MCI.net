# モナド
* `Scala` で使用されているものを元に移植しています。



## Option
* `NullReferenceException` を回避するために使用します。
* `Scala` では `Option` 、 `Haskell` では `Maybe` として用意されています。

```cs
using Miharu;

Option<int> opt = Option<int>.Return(10);
Option<int> opt = Option<int>.Fail();
```



## Either
* `L, R` の2つの型の値を返したい場合に利用します。
    * 結果と、エラーコードを同時に返したい場合
```cs
using Miharu;

Either<string, int> e = new Right<string, int>(100);
Either<string, int> e = new Left<string, int>("Error !!");
```

* `C#` に多い以下のコードを解決する事ができます。
```cs
int result = 0;
if (int.TryParse("1", out result))
{
    // Hoge Hoge
}
```



## Try
* 値もしくは例外クラスを返したい場合に利用します。

```cs
using Miharu;

Try<int> t = Try<int>.Success(10);
Try<int> t = Try<int>.Fail(new NotImplementedException());
Try<int> t = Try<int>.Execute(() =>
{
    return int.Parse("11")
});
```



## Future
* `C#` の `Task` を拡張したものです。

```cs
using Miharu;
```



## LINQ
* 各モナドには `LINQ` 用の拡張メソッドが定義されているので `LINQ` を利用できます。

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



## 参考資料
* Scala メモ
    * [Scala Option（Some・None）](http://www.ne.jp/asahi/hishidama/home/tech/scala/option.html)
    * [Scala Future](http://www.ne.jp/asahi/hishidama/home/tech/scala/future.html)
