# Rhino.Scripting.FSharp

[![Rhino.Scripting.FSharp on nuget.org](https://img.shields.io/nuget/v/Rhino.Scripting.FSharp.svg)](https://nuget.org/packages/Rhino.Scripting.FSharp)
[![Rhino.Scripting.FSharp on fuget.org](https://www.fuget.org/packages/Rhino.Scripting.FSharp/badge.svg)](https://www.fuget.org/packages/Rhino.Scripting.FSharp)
![code size](https://img.shields.io/github/languages/code-size/goswinr/Rhino.Scripting.FSharp.svg)
[![license](https://img.shields.io/github/license/goswinr/Rhino.Scripting.FSharp)](LICENSE)


![logo](https://raw.githubusercontent.com/goswinr/Rhino.Scripting.FSharp/main/Doc/logo400.png)

### What is Rhino.Scripting.FSharp?

Rhino.Scripting.FSharp is a set of useful extensions to the [Rhino.Scripting](https://github.com/goswinr/Rhino.Scripting) library.
This includes type extension for pretty printing of Rhino objects as well as implementations of commonly used functions in curried form for use with F#.

This library allows you to compose RhinoScript functions with pipelines:

Get started by opening the Rhino.Scripting namespaces:

```fsharp
open Rhino.Scripting
open Rhino.Scripting.FSharp // opening this will extend RhinoScriptSyntax and some Rhino.Geometry types with additional static and member functions.
type rs = RhinoScriptSyntax
```

```fsharp
rs.AddPoint( 1. , 2.,  3.)
|>! rs.setLayer "my points"
|>! rs.setUserText "id" "point123"
|>  rs.setName "123"
```

instead of

```fsharp
let guid = rs.AddPoint( 1. , 2.,  3.)
rs.ObjectLayer (guid, "my points")
rs.SetUserText (guid, "id", "point123")
rs.ObjectName (guid, "123")
```

The `|>!` operator is part of Rhino.Scripting via the [FsEx](https://github.com/goswinr/FsEx) library.
It passes it's input on as output. See [definition](https://github.com/goswinr/FsEx/blob/dd993e737fa70878f8a10e5357e8331dd68857a6/Src/TopLevelFunctions.fs#L126).

### License
[MIT](https://raw.githubusercontent.com/goswinr/FsEx/main/LICENSE.txt)

### Change Log

`0.8.1`
- align Plane API with Euclid library

`0.8.0`
- align Line, Point3d and Vector3d API with Euclid library
- referencing Rhino.Scripting 0.8.0

`0.5.0`

- first public release
- referencing Rhino.Scripting 0.5.0
