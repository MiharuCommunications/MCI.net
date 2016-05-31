# MCI.net

[![Build status](https://ci.appveyor.com/api/projects/status/pp9ffxtg1mvbc2ub/branch/develop?svg=true)](https://ci.appveyor.com/project/inahata/mci-net/branch/develop)



## 目的
* 様々なパラダイムの設計方法を取り入れる



## インストール
MCI.net のインストールは、NuGet から行えます。

```
PM> Install-Package MCI.Core
```



## 使い方
* 関数型プログラミングの機能
    * [モナド](./docs/Functional/Monad.md)
        * [Option](./docs/Functional/Monad.md#option)
        * [Either](./docs/Functional/Monad.md#either)
        * [Try](./docs/Functional/Monad.md#try)
        * [Future](./docs/Functional/Monad.md#future)
    * [コレクション操作](./docs/Functional/Collection.md)
* [Reactive Extensions](./docs/Rx/README.md)



## Copyright and license
Code copyright 1955 - 2016 Miharu Communications Inc.  
Code released under [the MIT license](https://github.com/MiharuCommunications/MCI.net/blob/master/LICENSE).
