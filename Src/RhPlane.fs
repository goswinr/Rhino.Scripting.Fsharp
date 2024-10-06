namespace Rhino.Scripting.Fsharp

open FsEx
open System
open Rhino
open Rhino.Geometry
open FsEx.UtilMath
open Rhino.Scripting



/// When Rhino.Scripting.Fsharp is opened this module will be auto-opened.
/// It only contains extension members for type Plane.
[<AutoOpen>]
module AutoOpenVector3d =

  type Plane  with

    /// WorldXY rotated 180 degrees round Z Axis
    static member WorldMinusXMinusY =
        Plane(Point3d.Origin, -Vector3d.XAxis, -Vector3d.YAxis)

    /// WorldXY rotated 90 degrees round Z Axis counter clockwise from top
    static member WorldYMinusX =
        Plane(Point3d.Origin, Vector3d.YAxis, -Vector3d.XAxis)

    /// WorldXY rotated 270 degrees round Z Axis counter clockwise from top
    static member WorldMinusYX =
        Plane(Point3d.Origin, -Vector3d.YAxis, Vector3d.XAxis)

    /// WorldXY rotated 180 degrees round X Axis, Z points down now
    static member WorldXMinusY =
        Plane(Point3d.Origin, Vector3d.XAxis, -Vector3d.YAxis)


    /// Return a new plane with given Origin
    static member inline setOrig (pt:Point3d) (pl:Plane) =
        let mutable p = pl.Clone()
        p.Origin <- pt
        p

    /// Return a new plane with given Origin X value changed.
    static member inline setOrigX (x:float) (pl:Plane) =
        let mutable p = pl.Clone()
        p.OriginX <- x
        p

    /// Return a new plane with given Origin Y value changed.
    static member inline setOrigY (y:float) (pl:Plane) =
        let mutable p = pl.Clone()
        p.OriginY <- y
        p

    /// Return a new plane with given Origin Z value changed.
    static member inline setOrigZ (z:float) (pl:Plane) =
        let mutable p = pl.Clone()
        p.OriginZ <- z
        p

    /// Return a new plane with Origin translated by Vector3d.
    static member inline translateBy (v:Vector3d) (pl:Plane) =
        let mutable p = pl.Clone()
        p.Origin <- p.Origin + v
        p

    /// Return a new plane with Origin translated in World X direction.
    static member inline translateByWorldX (x:float) (pl:Plane) =
        let mutable p = pl.Clone()
        p.OriginX <- p.OriginX + x
        p

    /// Return a new plane with Origin translated in World Y direction.
    static member inline translateByWorldY (y:float) (pl:Plane) =
        let mutable p = pl.Clone()
        p.OriginY <- p.OriginY + y
        p

    /// Return a new plane with Origin translated in World Z direction.
    static member inline translateByWorldZ (z:float) (pl:Plane) =
        let mutable p = pl.Clone()
        p.OriginZ <- p.OriginZ + z
        p


    /// Rotate about Z axis by angle in degree.
    /// Counter clockwise in top view (for WorldXY Plane).
    static member inline rotateZ (angDegree:float) (pl:Plane) =
        let mutable p = pl.Clone()
        if not <| p.Rotate(UtilMath.toRadians angDegree, Vector3d.ZAxis) then
            RhinoScriptingFsharpException.Raise "Rhino.Scripting.Fsharp: RhinoScriptSyntax.RhPlane.rotateZ by %s for %s" angDegree.ToNiceString pl.ToNiceString
        p