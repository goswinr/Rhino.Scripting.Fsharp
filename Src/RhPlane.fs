namespace Rhino.ScriptingFsharp

open FsEx
open System
open Rhino
open Rhino.Geometry
open FsEx.UtilMath


/// This module provides curried functions to manipulate Rhino Plane structs
/// It is NOT automatically opened.
[<RequireQualifiedAccess>]
module RhPlane = 
    
    /// Return a new plane with given Origin
    let inline setOrig (pt:Point3d) (pl:Plane) =   
        let mutable p = pl.Clone()
        p.Origin <- pt
        p 
    
    /// Return a new plane with given Origin X value changed.
    let inline setOrigX (x:float) (pl:Plane) =   
        let mutable p = pl.Clone()
        p.OriginX <- x
        p      
    
    /// Return a new plane with given Origin Y value changed.
    let inline setOrigY (y:float) (pl:Plane) =   
        let mutable p = pl.Clone()
        p.OriginY <- y
        p      
    
    /// Return a new plane with given Origin Z value changed.
    let inline setOrigZ (z:float) (pl:Plane) =   
        let mutable p = pl.Clone()
        p.OriginZ <- z
        p           

    /// Return a new plane with Origin translated by Vector3d.
    let inline translateBy (v:Vector3d) (pl:Plane) =   
        let mutable p = pl.Clone()
        p.Origin <- p.Origin + v
        p 
    
    /// Return a new plane with Origin translated in World X direction.
    let inline translateByWorldX (x:float) (pl:Plane) =   
        let mutable p = pl.Clone()
        p.OriginX <- p.OriginX + x
        p      
    
    /// Return a new plane with Origin translated in World Y direction.
    let inline translateByWorldY (y:float) (pl:Plane) =   
        let mutable p = pl.Clone()
        p.OriginY <- p.OriginY + y
        p      
    
    /// Return a new plane with Origin translated in World Z direction.
    let inline translateByWorldZ (z:float) (pl:Plane) =   
        let mutable p = pl.Clone()
        p.OriginZ <- p.OriginZ + z
        p

    
    /// Rotate about Z axis by angle in degree.  
    /// Counter clockwise in top view (for WorldXY Plane).
    let inline rotateZ (angDegree:float) (pl:Plane) =   
        let mutable p = pl.Clone()
        if not <| p.Rotate(UtilMath.toRadians angDegree, Vector3d.ZAxis) then 
            RhinoScriptingFsharpException.Raise "Rhino.ScriptingFsharp.RhPlane.rotateZ by %s for %s" angDegree.ToNiceString pl.ToNiceString
        p 