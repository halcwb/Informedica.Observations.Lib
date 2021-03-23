﻿namespace Informedica.Observations.Lib


module Observation =

    open Types


    let create name type' length sources collapseFn =
        {
            Name = name
            Type = type'
            Length = length
            Sources = sources
            Collapse = collapseFn
        }


    let createSource id name convertFn =
        {
            Id = id
            Name = name
            Convert = convertFn
        }


    let mapToObservations definitions =
        definitions 
        |> List.map (fun (name, type', length, collapseFn, sources) ->
            let sources =
                sources 
                |> List.map (fun (id, name, convertFn) -> createSource id name convertFn)
            create name type' length sources collapseFn
        )

    let signalBelongsToSource (signal : Signal) (source : Source) =
        if signal.Id.IsSome then signal.Id.Value = source.Id
        else
            signal.Name.Trim().ToLower() = source.Name.Trim().ToLower()

