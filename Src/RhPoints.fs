namespace Rhino.Scripting.Fsharp

open System
open Rhino
open Rhino.Geometry
open FsEx.SaveIgnore
open FsEx
open UtilRHinoScriptingFsharp
open Rhino.Scripting

/// This module provides curried functions to manipulate Rhino Point3d
/// It is NOT automatically opened.
[<RequireQualifiedAccess>]
module RhPoints =


    /// returns the closest point index form a Point list  to a given Point
    let closestPointIdx (pt:Point3d) (pts:Rarr<Point3d>) : int =
        if pts.Count = 0 then RhinoScriptingFsharpException.Raise "RhPnt.closestPoint empty List of Points: pts"
        let mutable mi = -1
        let mutable mid = Double.MaxValue
        for i=0 to pts.LastIndex do
            let p = pts.[i]
            let d = Point3d.distanceSq p pt
            if d < mid then
                mid <- d
                mi <- i
        mi

    /// returns the closest point form a Point list to a given Point
    let closestPoint (pt:Point3d) (pts:Rarr<Point3d>) : Point3d=
        pts.[closestPointIdx pt pts]

    /// returns the indices of the points that are closest to each other
    let closestPointsIdx (xs:Rarr<Point3d>) (ys:Rarr<Point3d>) =
        if xs.Count = 0 then RhinoScriptingFsharpException.Raise "RhPnt.closestPointsIdx empty List of Points: xs"
        if ys.Count = 0 then RhinoScriptingFsharpException.Raise "RhPnt.closestPointsIdx empty List of Points: ys"
        let mutable xi = -1
        let mutable yj = -1
        let mutable mid = Double.MaxValue
        for i=0 to xs.LastIndex do
            let pt = xs.[i]
            for j=0 to ys.LastIndex do
                let d = Point3d.distanceSq pt ys.[j]
                if d < mid then
                    mid <- d
                    xi <- i
                    yj <- j
        xi,yj

    /// returns the smallest Distance between Point Sets
    let minDistBetweenPointSets (xs:Rarr<Point3d>) (ys:Rarr<Point3d>) =
        if xs.Count = 0 then RhinoScriptingFsharpException.Raise "RhPnt.minDistBetweenPointSets empty List of Points: xs"
        if ys.Count = 0 then RhinoScriptingFsharpException.Raise "RhPnt.minDistBetweenPointSets empty List of Points: ys"
        let (i,j) = closestPointsIdx xs ys
        Point3d.distance xs.[i]  ys.[j]

    /// find the index of the point that has the biggest distance to any point from the other set
    /// basically the mos lonely point in 'findPointFrom' list with respect to 'checkAgainst' list
    /// returns findPointFromIdx * checkAgainstIdx
    let mostDistantPointIdx (findPointFrom:Rarr<Point3d>) (checkAgainst:Rarr<Point3d>) : int*int=
        if findPointFrom.Count = 0 then RhinoScriptingFsharpException.Raise "RhPnt.mostDistantPoint empty List of Points: findPointFrom"
        if checkAgainst.Count = 0 then RhinoScriptingFsharpException.Raise "RhPnt.mostDistantPoint empty List of Points: checkAgainst"
        let mutable maxd = Double.MinValue
        let mutable findPointFromIdx = -1
        let mutable checkAgainstTempIdx = -1
        let mutable checkAgainstIdx = -1
        for i=0 to findPointFrom.LastIndex do
            let pt = findPointFrom.[i]
            let mutable mind = Double.MaxValue
            for j=0 to checkAgainst.LastIndex do
                let d = Point3d.distanceSq pt checkAgainst.[j]
                if d < mind then
                    mind <- d
                    checkAgainstTempIdx <-j
            if mind > maxd then
                maxd <- mind
                findPointFromIdx <-i
                checkAgainstIdx <-checkAgainstTempIdx
        findPointFromIdx, checkAgainstIdx

    /// find the point that has the biggest distance to any point from another set
    let mostDistantPoint (findPointFrom:Rarr<Point3d>) (checkAgainst:Rarr<Point3d>) =
        let i,_ = mostDistantPointIdx findPointFrom checkAgainst
        findPointFrom.[i]



    /// Culls points if they are to close to previous or next item
    /// Last and first points stay the same
    let cullDuplicatePointsInSeq (tolerance) (pts:Rarr<Point3d>)  =
        if pts.Count = 0 then RhinoScriptingFsharpException.Raise "RhPnt.cullDuplicatePointsInSeq empty List of Points: pts"
        if pts.Count = 1 then
            pts
        else
            let tolSq = tolerance*tolerance
            let res  =  Rarr(pts.Count)
            let mutable last  = pts.[0]
            res.Add last
            let iLast = pts.LastIndex
            for i = 1  to iLast do
                let pt = pts.[i]
                if Point3d.distanceSq last pt > tolSq then
                    last <- pt
                    res.Add last
                elif i=iLast then // to ensure last point stays the same
                    res.Pop() |> ignore
                    res.Add pt
            res

    /// Similar to Join Polylines this tries to find continuous sequences of points.
    /// 'tolGap' is the maximum allowable gap between the start and the endpoint of to segments.
    /// Search starts from the segment with the most points.
    /// Both start and end point of each point list is checked for adjacency
    let findContinuousPoints (tolGap:float)  (ptss: Rarr<Rarr<Point3d>>)  =
        let i =  ptss |> Rarr.maxIndBy Rarr.length
        let res = ptss.Pop(i)
        let mutable loop = true
        while loop && ptss.Count > 0 do
            //first try to append to end
            let ende = res.Last
            let si = ptss |> Rarr.minIndBy ( fun ps -> Point3d.distanceSq ende ps.First)
            let ei = ptss |> Rarr.minIndBy ( fun ps -> Point3d.distanceSq ende ps.Last)
            let sd = Point3d.distance ende ptss.[si].First
            let ed = Point3d.distance ende ptss.[ei].Last
            if   sd < tolGap && sd < ed then  res.AddRange(         ptss.Pop(si))
            elif ed < tolGap && ed < sd then  res.AddRange(Rarr.rev(ptss.Pop(ei)))
            else
                //search from start
                let start = res.First
                let si = ptss |> Rarr.minIndBy ( fun ps -> Point3d.distanceSq start ps.First)
                let ei = ptss |> Rarr.minIndBy ( fun ps -> Point3d.distanceSq start ps.Last)
                let sd = Point3d.distance start ptss.[si].First
                let ed = Point3d.distance start ptss.[ei].Last
                if   sd < tolGap && sd < ed then res.InsertRange(0, Rarr.rev(ptss.Pop(si)))
                elif ed < tolGap && ed < sd then res.InsertRange(0,          ptss.Pop(ei))
                else
                    loop <- false
        res

