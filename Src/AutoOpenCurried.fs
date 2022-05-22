namespace Rhino.Scripting.Extension


open FsEx
open System
open Rhino
open Rhino.Geometry
//open Rhino.NiceString
//open FsEx.UtilMath
//open Rhino.Scripting
//open System.Xml.Schema




[<AutoOpen>] 
/// This module provides curried F# functions for easy use with pipeline operator |>
/// This module is automatically opened when Rhino.Scripting.Extension namespace is opened.
module AutoOpenCurried = 

  type Scripting with

    ///<summary>Modifies the layer of an object, creates layer if not yet existing.</summary>
    ///<param name="layer">(string) Name of layer or empty string for current layer</param>
    ///<param name="objectId">(Guid) The identifier of the object</param>
    ///<returns>(unit) void, nothing.</returns>
    static member setLayer (layer:string) (objectId:Guid) : unit = 
        Scripting.ObjectLayer(objectId, layer, createLayerIfMissing=true)

    ///<summary>Modifies the layer of several objects, creates layer if not yet existing.</summary>
    ///<param name="layer">(string) Name of layer or empty string for current layer</param>
    ///<param name="objectIds">(Guid seq) The identifiers of several objects</param>
    ///<returns>(unit) void, nothing.</returns>
    static member setLayers (layer:string) (objectIds:seq<Guid>) : unit = 
        Scripting.ObjectLayer(objectIds, layer, createLayerIfMissing=true)

///<summary>Modifies the layer of an object.</summary>
    ///<param name="layerIndex">(int) Index of layer in layer table</param>
    ///<param name="objectId">(Guid) The identifier of the object</param>
    ///<returns>(unit) void, nothing.</returns>
    static member setLayerIndex (layerIndex:int) (objectId:Guid) : unit = 
        Scripting.ObjectLayer(objectId, layerIndex)

    ///<summary>Modifies the layer of several objects.</summary>
    ///<param name="layerIndex">(int) Index of layer in layer table</param>
    ///<param name="objectIds">(Guid seq) The identifiers of several objects</param>
    ///<returns>(unit) void, nothing.</returns>
    static member setLayersIndex (layerIndex:int) (objectIds:seq<Guid>) : unit = 
        Scripting.ObjectLayer(objectIds, layerIndex)

    ///<summary>Returns the full layer-name of an object.
    /// parent layers are separated by <c>::</c>.</summary>
    ///<param name="objectId">(Guid) The identifier of the object</param>
    ///<returns>(string) The object's current layer.</returns>
    static member getLayer (objectId:Guid) : string = 
        Scripting.ObjectLayer(objectId)

    ///<summary>Returns the short layer of an object.
    ///    Without Parent Layers.</summary>
    ///<param name="objectId">(Guid) The identifier of the object</param>
    ///<returns>(string) The object's current layer.</returns>
    static member getLayerShort (objectId:Guid) : string = 
        Scripting.ObjectLayerShort(objectId)

    ///<summary>Sets the name of an object.</summary>
    ///<param name="name">(string) The new object name.</param>
    ///<param name="objectId">(Guid)Id of object</param>
    ///<returns>(unit) void, nothing.</returns>
    static member setName (name:string) (objectId:Guid) : unit = 
        Scripting.ObjectName(objectId, name)

    ///<summary>Sets the name of several objects.</summary>
    ///<param name="name">(string) The new object name.</param>
    ///<param name="objectIds">(Guid seq)Ids of several objects</param>
    ///<returns>(unit) void, nothing.</returns>
    static member setNames (name:string) (objectIds:seq<Guid>) : unit = 
        Scripting.ObjectName(objectIds, name)


    ///<summary>Returns the name of an object or "" if none given.</summary>
    ///<param name="objectId">(Guid)Id of object</param>
    ///<returns>(string) The current object name, empty string if no name given .</returns>
    static member getName (objectId:Guid) : string = 
        Scripting.ObjectName(objectId)


    ///<summary>Sets the Color of an object.</summary>
    ///<param name="color">(Drawing.Color) The new object color.</param>
    ///<param name="objectId">(Guid)Id of object</param>
    ///<returns>(unit) void, nothing.</returns>
    static member setColor(color:Drawing.Color) (objectId:Guid) : unit = 
        Scripting.ObjectColor(objectId, color)

    ///<summary>Sets the Color of several objects.</summary>
    ///<param name="color">(Drawing.Color) The new object color.</param>
    ///<param name="objectIds">(Guid seq)Id of several objects</param>
    ///<returns>(unit) void, nothing.</returns>
    static member setColors(color:Drawing.Color) (objectIds:seq<Guid>) : unit = 
        Scripting.ObjectColor(objectIds, color)

    ///<summary>Returns the color of an object .</summary>
    ///<param name="objectId">(Guid)Id of object</param>
    ///<returns>(string) The current object color.</returns>
    static member getColor (objectId:Guid) : Drawing.Color = 
        Scripting.ObjectColor(objectId)

    ///<summary>Sets a user text stored on an object.</summary>
    ///<param name="key">(string) The key name to set</param>
    ///<param name="value">(string) The string value to set. Cannot be empty string. use Scripting.DeleteUserText to delete keys</param>
    ///<param name="objectId">(Guid) The object's identifier</param>
    ///<returns>(unit) void, nothing.</returns>
    static member setUserText( key:string) ( value :string) (objectId:Guid) : unit = 
        Scripting.SetUserText(objectId, key, value)

    ///<summary>Sets a user text stored on several objects.</summary>
    ///<param name="key">(string) The key name to set</param>
    ///<param name="value">(string) The string value to set. Cannot be empty string. use Scripting.DeleteUserText to delete keys</param>
    ///<param name="objectIds">(Guid seq) The identifiers of several objects</param>
    ///<returns>(unit) void, nothing.</returns>
    static member setUserTexts( key:string) ( value :string) (objectIds:seq<Guid>) : unit = 
        Scripting.SetUserText(objectIds, key, value)

    ///<summary>Append a string to a possibly already existing user-text value.</summary>
    ///<param name="key">(string) The key name to set</param>
    ///<param name="value">(string) The string value to append. Cannot be empty string. use Scripting.DeleteUserText to delete keys</param>
    ///<param name="objectId">(Guid) The identifier of the objects</param>
    ///<returns>(unit) void, nothing.</returns>
    static member appendtUserText(key:string) (value :string) (objectId:Guid) : unit = 
        if String.IsNullOrWhiteSpace key then
            RhinoScriptingException.Raise "Rhino.Scripting.Extension.appendtUserText key is String.IsNullOrWhiteSpace for value  %s on %s" value (toNiceString objectId)
        if isNull value then
            RhinoScriptingException.Raise "Rhino.Scripting.Extension.appendtUserText value is null  for key %s on %s" key (toNiceString objectId)
        let obj = Scripting.CoerceRhinoObject(objectId)
        let existing = obj.Attributes.GetUserString(key)
        if isNull existing then // only if a value already exist  appending a white space  or empty string is OK too
            if String.IsNullOrWhiteSpace value  then
                RhinoScriptingException.Raise "Rhino.Scripting.Extension.appendtUserText failed on %s for key '%s' but value IsNullOrWhiteSpace" (toNiceString objectId) key
            if not <| obj.Attributes.SetUserString(key, value) then
                RhinoScriptingException.Raise "Rhino.Scripting.Extension.appendtUserText failed on %s for key '%s' and value '%s'" (toNiceString objectId) key value
        else
            if not <| obj.Attributes.SetUserString(key,  existing + value ) then
                RhinoScriptingException.Raise "Rhino.Scripting.Extension.appendtUserText failed on %s for key '%s' and value '%s'" (toNiceString objectId) key value

    ///<summary>Returns user text stored on an object, fails if non existing.</summary>
    ///<param name="key">(string) The key name</param>
    ///<param name="objectId">(Guid) The object's identifies</param>
    ///<returns>(string) if key is specified, the associated value,fails if non existing.</returns>
    static member getUserText( key:string) (objectId:Guid) : string = 
        Scripting.GetUserText(objectId, key)

    ///<summary>Checks if the user text stored on an object matches a given string, fails if non existing.</summary>
    ///<param name="key">(string) The key name</param>
    ///<param name="valueToMatch">(string) The value to check for equality with</param>
    ///<param name="objectId">(Guid) The object's identifies</param>
    ///<returns>(string) if key is specified, the associated value,fails if non existing.</returns>
    static member isUserTextValue( key:string) (valueToMatch:string) (objectId:Guid) : bool = 
        valueToMatch = Scripting.GetUserText(objectId, key)


    ///<summary>Checks if a User Text key is stored on an object.</summary>
    ///<param name="key">(string) The key name</param>
    ///<param name="objectId">(Guid) The object's identifies</param>
    ///<returns>(bool) if key exist true.</returns>
    static member hasUserText( key:string) (objectId:Guid) : bool = 
        Scripting.HasUserText(objectId, key)

    ///<summary>Returns user text stored on an object, returns Option.None if non existing.</summary>
    ///<param name="key">(string) The key name</param>
    ///<param name="objectId">(Guid) The object's identifies</param>
    ///<returns>(string Option) if key is specified, Some(value) else None .</returns>
    static member tryGetUserText( key:string) (objectId:Guid) : string option= 
        Scripting.TryGetUserText(objectId, key)

    ///<summary>Copies all user text keys and values from  one object to another
    ///from both Geometry and Object.Attributes. Existing values are overwritten.</summary>
    ///<param name="sourceId">(Guid) The object to take all keys from </param>
    ///<param name="targetId">(Guid) The object to write  all keys to </param>
    ///<returns>(unit) void, nothing.</returns>
    static member matchAllUserText (sourceId:Guid) (targetId:Guid) : unit= 
        let sc = Scripting.CoerceRhinoObject(sourceId)
        let de = Scripting.CoerceRhinoObject(targetId)
        let usg = sc.Geometry.GetUserStrings()
        for  i = 0 to usg.Count-1 do
            let key = usg.GetKey(i)
            if not <|de.Geometry.SetUserString(key,sc.Geometry.GetUserString(key)) then
                RhinoScriptingException.Raise "Rhino.Scripting.Extension.matchAllUserText: Geometry failed to set key '%s' from %s on %s" key (toNiceString sourceId) (toNiceString targetId)
        let usa = sc.Attributes.GetUserStrings()
        for  i = 0 to usa.Count-1 do
            let key = usa.GetKey(i)
            if not <|de.Attributes.SetUserString(key,sc.Attributes.GetUserString(key))then
                RhinoScriptingException.Raise "Rhino.Scripting.Extension.matchAllUserText: Attributes failed to set key '%s' from %s on %s" key (toNiceString sourceId) (toNiceString targetId)

    ///<summary>Copies the value for a given user text key from a source object to a target object.</summary>
    ///<param name="sourceId">(Guid) The object to take all keys from </param>
    ///<param name="key">(string) The key name to set</param>
    ///<param name="targetId">(Guid) The object to write  all keys to </param>
    ///<returns>(unit) void, nothing.</returns>
    static member matchUserText (sourceId:Guid) ( key:string) (targetId:Guid) : unit= 
        let de = Scripting.CoerceRhinoObject(targetId)
        let v = Scripting.GetUserText(sourceId,key)
        if not <| de.Attributes.SetUserString(key,v) then RhinoScriptingException.Raise "Rhino.Scripting.Extension.matchUserText: failed to set key '%s' to '%s' on %s" key v (toNiceString targetId)

    ///<summary>Copies the object name from a source object to a target object.</summary>
    ///<param name="sourceId">(Guid) The object to take the name from </param>
    ///<param name="targetId">(Guid) The object to write the name to </param>
    ///<returns>(unit) void, nothing.</returns>
    static member matchName (sourceId:Guid) (targetId:Guid) : unit = 
        let sc = Scripting.CoerceRhinoObject(sourceId)
        let de = Scripting.CoerceRhinoObject(targetId)
        let n = sc.Attributes.Name
        if isNull n then
            RhinoScriptingException.Raise "Rhino.Scripting.Extension.matchName: source object %s has no name. Targets name: '%s'" (toNiceString sourceId) de.Attributes.Name
        de.Attributes.Name <- n
        if not <| de.CommitChanges() then
            RhinoScriptingException.Raise "Rhino.Scripting.Extension.matchName failed from %s on %s" (toNiceString sourceId) (toNiceString targetId)

    ///<summary>Puts target object on the same Layer as a source object .</summary>
    ///<param name="sourceId">(Guid) The object to take the layer from </param>
    ///<param name="targetId">(Guid) The object to change the layer</param>
    ///<returns>(unit) void, nothing.</returns>
    static member matchLayer (sourceId:Guid) (targetId:Guid) : unit = 
        let sc = Scripting.CoerceRhinoObject(sourceId)
        let de = Scripting.CoerceRhinoObject(targetId)
        de.Attributes.LayerIndex <- sc.Attributes.LayerIndex
        if not <| de.CommitChanges() then
            RhinoScriptingException.Raise "Rhino.Scripting.Extension.matchLayer failed from %s on %s" (toNiceString sourceId) (toNiceString targetId)


    ///<summary>Matches all properties( layer, name, user text, ....) from a source object to a target object by duplicating attributes.
    /// and copying user strings on geometry. .</summary>
    ///<param name="sourceId">(Guid) The object to take all keys from </param>
    ///<param name="targetId">(Guid) The object to write  all keys to </param>
    ///<returns>(unit) void, nothing.</returns>
    static member matchAllProperties (sourceId:Guid) (targetId:Guid) : unit = 
        let sc = Scripting.CoerceRhinoObject(sourceId)
        let de = Scripting.CoerceRhinoObject(targetId)
        de.Attributes <- sc.Attributes.Duplicate()
        if not <| de.CommitChanges() then
            RhinoScriptingException.Raise "Rhino.Scripting.Extension.matchAllProperties failed from %s on %s" (toNiceString sourceId) (toNiceString targetId)
        let usg = sc.Geometry.GetUserStrings()
        for  i = 0 to usg.Count-1 do
            let key = usg.GetKey(i)
            if not <|de.Geometry.SetUserString(key,sc.Geometry.GetUserString(key)) then
                RhinoScriptingException.Raise "Rhino.Scripting.Extension.matchAllProperties: Geometry failed to set key '%s' from %s on %s" key (toNiceString sourceId) (toNiceString targetId)
    (*
    TODO delete , "draw" should only refer to display pipline
    //<summary>Draws any Geometry object to a given or current layer.</summary>
    //<param name="layer">(string) Name of an layer or empty string for current layer</param>
    //<param name="geo">(GeometryBase) Geometry</param>
    //<returns>(unit) void, nothing.</returns>
    static member draw (layer:string) (geo:'AnyRhinoGeometry) : unit = 
        Scripting.Add(geo,layer)  |> ignore 
    *)


    ///<summary>Moves, scales, or rotates an object given a 4x4 transformation matrix. The matrix acts on the left.</summary>
    ///<param name="matrix">(Transform) The transformation matrix (4x4 array of numbers)</param>
    ///<param name="objectId">(Guid) The identifier of the object</param>
    ///<returns>(unit) void, nothing.</returns>
    static member transform (matrix:Transform) (objectId:Guid) : unit = 
        Scripting.TransformObject(objectId, matrix, copy=false) |> ignore     
    
    
    ///<summary>Moves, scales, or rotates a geometry given a 4x4 transformation matrix. The matrix acts on the left. </summary>
    ///<param name="matrix">(Transform) The transformation matrix (4x4 array of numbers)</param>
    ///<param name="geo">(GeometryBase) Any Geometry derived from GeometryBase</param>
    ///<returns>(unit) void, nothing.</returns>
    static member transformGeo (matrix:Transform) (geo:GeometryBase) : unit = 
        if not <| geo.Transform(matrix) then 
            RhinoScriptingException.Raise "Rhino.Scripting.Extension.scale failed. geo:'%s' matrix:'%s' " (toNiceString geo) matrix.ToNiceString 
        if matrix.SimilarityType = TransformSimilarityType.OrientationReversing then 
            match geo with 
            | :? Brep as g -> if g.IsSolid then g.Flip()
            | :? Mesh as g -> if g.IsClosed then g.Flip(true,true,true)
            // TODO any missing?
            | _ -> ()


    ///<summary>Scales a single object. Uniform scale transformation. Scaling is based on the WorldXY Plane.</summary>
    ///<param name="origin">(Point3d) The origin of the scale transformation</param>
    ///<param name="scale">(float) One numbers that identify the X, Y, and Z axis scale factors to apply</param>
    ///<param name="objectId">(Guid) The identifier of an object</param>
    ///<returns>(unit) void, nothing.</returns>
    static member scale(origin:Point3d) (scale:float) (objectId:Guid) : unit = 
        let mutable plane = Plane.WorldXY
        plane.Origin <- origin
        let xf = Transform.Scale(plane, scale, scale, scale)
        let res = Scripting.Doc.Objects.Transform(objectId, xf, deleteOriginal=true)
        if res = Guid.Empty then
            RhinoScriptingException.Raise "Rhino.Scripting.Extension.scale failed. objectId:'%s' origin:'%s' scale:'%g'" (toNiceString objectId) origin.ToNiceString scale


    ///<summary>Moves a single object.</summary>
    ///<param name="translation">(Vector3d) Vector3d</param>
    ///<param name="objectId">(Guid) The identifier of an object to move</param>
    ///<returns>(unit) void, nothing.</returns>
    static member move (translation:Vector3d)  (objectId:Guid): unit = 
        let xf = Transform.Translation(translation)
        let res = Scripting.Doc.Objects.Transform(objectId, xf, deleteOriginal=true) // TODO test to ensure GUID is the same ?
        if res = Guid.Empty then
            RhinoScriptingException.Raise "Rhino.Scripting.Extension.move to from objectId:'%s' translation:'%A'" (toNiceString objectId) translation

    ///<summary>Moves a single object in X Direction.</summary>
    ///<param name="translationX">(float) movement in X direction</param>
    ///<param name="objectId">(Guid) The identifier of an object to move</param>
    ///<returns>(unit) void, nothing.</returns>
    static member moveX (translationX:float)  (objectId:Guid): unit = 
        let xf = Transform.Translation(Vector3d(translationX, 0.0, 0.0 ))
        let res = Scripting.Doc.Objects.Transform(objectId, xf, deleteOriginal=true) // TODO test to ensure GUID is the same ?
        if res = Guid.Empty then
            RhinoScriptingException.Raise "Rhino.Scripting.Extension.moveX to from objectId:'%s' translation:'%A'" (toNiceString objectId) translationX

    ///<summary>Moves a single object in Y Direction.</summary>
    ///<param name="translationY">(float) movement in Y direction</param>
    ///<param name="objectId">(Guid) The identifier of an object to move</param>
    ///<returns>(unit) void, nothing.</returns>
    static member moveY (translationY:float)  (objectId:Guid): unit = 
        let xf = Transform.Translation(Vector3d(0.0, translationY, 0.0))
        let res = Scripting.Doc.Objects.Transform(objectId, xf, deleteOriginal=true) // TODO test to ensure GUID is the same ?
        if res = Guid.Empty then
            RhinoScriptingException.Raise "Rhino.Scripting.Extension.moveY to from objectId:'%s' translation:'%A'" (toNiceString objectId) translationY

    ///<summary>Moves a single object in Z Direction.</summary>
    ///<param name="translationZ">(float) movement in Z direction</param>
    ///<param name="objectId">(Guid) The identifier of an object to move</param>
    ///<returns>(unit) void, nothing.</returns>
    static member moveZ (translationZ:float)  (objectId:Guid): unit = 
        let xf = Transform.Translation(Vector3d(0.0, 0.0, translationZ))
        let res = Scripting.Doc.Objects.Transform(objectId, xf, deleteOriginal=true) // TODO test to ensure GUID is the same ?
        if res = Guid.Empty then
            RhinoScriptingException.Raise "Rhino.Scripting.Extension.moveZ to from objectId:'%s' translation:'%A'" (toNiceString objectId) translationZ


    ///<summary>Moves a Geometry.</summary>
    ///<param name="translation">(Vector3d) Vector3d</param>
    ///<param name="geo">(GeometryBase) The Geometry to move</param>
    ///<returns>(unit) void, nothing.</returns>
    static member moveGeo (translation:Vector3d)  (geo:GeometryBase): unit = 
        if not <|  geo.Translate translation then
            RhinoScriptingException.Raise "Rhino.Scripting.Extension.moveGeo to from geo:'%A' translation:'%A'"  geo translation

    ///<summary>Moves a Geometry in X Direction.</summary>
    ///<param name="translationX">(float) movement in X direction</param>
    ///<param name="geo">(GeometryBase) The Geometry to move</param>
    ///<returns>(unit) void, nothing.</returns>
    static member moveGeoX (translationX:float)  (geo:GeometryBase): unit = 
        if not <| geo.Translate (Vector3d(translationX, 0.0, 0.0 )) then
            RhinoScriptingException.Raise "Rhino.Scripting.Extension.moveGeoX to from geo:'%A' translation:'%f'"  geo translationX

    ///<summary>Moves a Geometry in Y Direction.</summary>
    ///<param name="translationY">(float) movement in Y direction</param>
    ///<param name="geo">(GeometryBase) The Geometry to move</param>
    ///<returns>(unit) void, nothing.</returns>
    static member moveGeoY (translationY:float)  (geo:GeometryBase): unit = 
        if not <| geo.Translate (Vector3d(0.0, translationY, 0.0)) then
            RhinoScriptingException.Raise "Rhino.Scripting.Extension.moveGeoY to from geo:'%A' translation:'%f'"  geo translationY

    ///<summary>Moves a Geometry in Z Direction.</summary>
    ///<param name="translationZ">(float) movement in Z direction</param>
    ///<param name="geo">(GeometryBase) The Geometry to move</param>
    ///<returns>(unit) void, nothing.</returns>
    static member moveGeoZ (translationZ:float) (geo:GeometryBase): unit = 
        if not <| geo.Translate (Vector3d(0.0, 0.0, translationZ)) then
            RhinoScriptingException.Raise "Rhino.Scripting.Extension.moveGeoZ to from geo:'%A' translation:'%f'"  geo translationZ
    
    ///<summary>Enables or disables a Curve object's annotation arrows.
    /// The size of the arrow cannot be changed. For an adjustable arrow size use a dimension leader object.
    /// Same as Scripting.CurveArrows(curveId,arrowStyle)</summary>
    ///<param name="arrowStyle">(int) The style of annotation arrow to be displayed.
    ///    0 = no arrows
    ///    1 = display arrow at start of Curve
    ///    2 = display arrow at end of Curve
    ///    3 = display arrow at both start and end of Curve</param>
    ///<param name="curveId">(Guid) Identifier of a Curve</param>
    ///<returns>(unit) void, nothing.</returns>
    static member addArrows (arrowStyle:int) (curveId:Guid) : unit = 
        Scripting.CurveArrows(curveId,arrowStyle)
        