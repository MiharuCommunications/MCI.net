# MCI.net

[![Build status](https://ci.appveyor.com/api/projects/status/pp9ffxtg1mvbc2ub/branch/develop?svg=true)](https://ci.appveyor.com/project/inahata/mci-net/branch/develop)
[![Build Status](https://travis-ci.org/MiharuCommunications/MCI.net.svg?branch=develop)](https://travis-ci.org/MiharuCommunications/MCI.net)
[![Coverage Status](https://coveralls.io/repos/github/MiharuCommunications/MCI.net/badge.svg?branch=develop)](https://coveralls.io/github/MiharuCommunications/MCI.net?branch=develop)

[![codecov](https://codecov.io/gh/MiharuCommunications/MCI.net/branch/develop/graph/badge.svg)](https://codecov.io/gh/MiharuCommunications/MCI.net)

| Channel       | MCI.Core | MCI.Standard |
| :------------ | :------: | :----------: |
| .NET Standard |   1.1    |     2.0      |

## Table of Contents

- [MCI.net](#mcinet)
  - [Table of Contents](#table-of-contents)
  - [Installation](#installation)
  - [Usage](#usage)
  - [Contribute](#contribute)
    - [Set UP](#set-up)
    - [Build](#build)
    - [Cover](#cover)
  - [License](#license)

## Installation

This project uses .net Framework and NuGet.

```text
PM> Install-Package MCI.Core
```

## Usage

- Functional programming
  - [Monad](./doc/fp/monad.md)

## Contribute

### Set UP

```
make install
```

### Build

```
make build
```

### Cover

```
make cover
```

## License

Code copyright 1955 - 2024 Miharu Communications Inc.  
Code released under [the MIT license](./LICENSE).
