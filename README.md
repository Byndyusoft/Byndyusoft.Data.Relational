[![(License)](https://img.shields.io/github/license/Byndyusoft/Byndyusoft.Data.Relational.Abstractions.svg)](LICENSE.txt)


| [Byndyusoft.Data.Relational.Abstractions](https://www.nuget.org/packages/Byndyusoft.Data.Relational.Abstractions/) | [![Nuget](https://img.shields.io/nuget/v/Byndyusoft.Data.Relational.Abstractions.svg)](https://www.nuget.org/packages/Byndyusoft.Data.Relational.Abstractions/) | [![Downloads](https://img.shields.io/nuget/dt/Byndyusoft.Data.Relational.Abstractions.svg)](https://www.nuget.org/packages/Byndyusoft.Data.Relational.Abstractions/) |
|------- | ------------ | --------- |
| [**Byndyusoft.Data.Relational**](https://www.nuget.org/packages/Byndyusoft.Data.Relational/) | [![Nuget](https://img.shields.io/nuget/v/Byndyusoft.Data.Relational.svg)](https://www.nuget.org/packages/Byndyusoft.Data.Relational/) | [![Downloads](https://img.shields.io/nuget/dt/Byndyusoft.Data.Relational.svg)](https://www.nuget.org/packages/Byndyusoft.Data.Relational/) |
| [**Byndyusoft.Data.Relational.OpenTelemetry**](https://www.nuget.org/packages/Byndyusoft.Data.Relational.OpenTelemetry/) | [![Nuget](https://img.shields.io/nuget/v/Byndyusoft.Data.Relational.OpenTelemetry.svg)](https://www.nuget.org/packages/Byndyusoft.Data.Relational.OpenTelemetry/) | [![Downloads](https://img.shields.io/nuget/dt/Byndyusoft.Data.Relational.OpenTelemetry.svg)](https://www.nuget.org/packages/Byndyusoft.Data.Relational.OpenTelemetry/) |

# Byndyusoft.Data.Relational.Abstractions
Relational abstractions for Byndyusoft.Data.Relational.

## Installing

```shell
dotnet add package Byndyusoft.Data.Relational.Abstractions
```

# Byndyusoft.Data.Relational
Relational database default implementation for Byndyusoft.Data.Relational

## Installing

```shell
dotnet add package Byndyusoft.Data.Relational
```

## Usage

TBDL

### Using Json Type Handler

```csharp
SqlMapper.AddTypeHandler(new JsonTypeHandler<User>());
```

**Note:** 'null' and null will be queried from db as null.

# Contributing

To contribute, you will need to setup your local environment, see [prerequisites](#prerequisites). For the contribution and workflow guide, see [package development lifecycle](#package-development-lifecycle).

A detailed overview on how to contribute can be found in the [contributing guide](CONTRIBUTING.md).

## Prerequisites

Make sure you have installed all of the following prerequisites on your development machine:

- Git - [Download & Install Git](https://git-scm.com/downloads). OSX and Linux machines typically have this already installed.
- .NET Core (version 6.0 or higher) - [Download & Install .NET Core](https://dotnet.microsoft.com/download/dotnet/6.0).

## General folders layout

### src
- source code

### tests

- unit-tests

### example

- example console application

## Package development lifecycle

- Implement package logic in `src`
- Add or addapt unit-tests (prefer before and simultaneously with coding) in `tests`
- Add or change the documentation as needed
- Open pull request in the correct branch. Target the project's `master` branch

# Maintainers

[github.maintain@byndyusoft.com](mailto:github.maintain@byndyusoft.com)
