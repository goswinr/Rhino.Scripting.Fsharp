# Rhino.ScriptingFsharp

[![Rhino.ScriptingFsharp on nuget.org](https://img.shields.io/nuget/v/Rhino.ScriptingFsharp.svg)](https://nuget.org/packages/Rhino.ScriptingFsharp) 
[![Rhino.ScriptingFsharp on fuget.org](https://www.fuget.org/packages/Rhino.ScriptingFsharp/badge.svg)](https://www.fuget.org/packages/Rhino.ScriptingFsharp)
![code size](https://img.shields.io/github/languages/code-size/goswinr/Rhino.ScriptingFsharp.svg) 
[![license](https://img.shields.io/github/license/goswinr/Rhino.ScriptingFsharp)](LICENSE)


![logo](https://raw.githubusercontent.com/goswinr/Rhino.ScriptingFsharp/main/Doc/logo400.png)

### What is Rhino.ScriptingFsharp?

Rhino.ScriptingFsharp is a set of useful extensions to the [Rhino.Scripting](https://github.com/goswinr/Rhino.Scripting) library. This includes type extension for pretty printing of Rhino objects as well as implementations of commonly used functions in curried form for use with F#.

This library allows you to compose RhinoScript functions with pipelines:

```fsharp
rs.AddPoint( 1. , 2.,  3.)
|>! rs.setLayer "my points"
|>! rs.setrUserText "id" "point123"
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
It passes it's input on as output. See [definition](https://github.com/goswinr/FsEx/blob/5e52a5a0be15cdcd6d48b43666031755bfd5d251/Src/TopLevelFunctions.fs#L131https://github.com/goswinr/FsEx/blob/5e52a5a0be15cdcd6d48b43666031755bfd5d251/Src/TopLevelFunctions.fs#L131).

### License
[MIT](https://raw.githubusercontent.com/goswinr/FsEx/main/LICENSE.txt)

### Change Log

`0.5.0`

- first public release
- referencing Rhino.Scripting 0.5.0
