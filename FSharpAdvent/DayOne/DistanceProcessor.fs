module FSharpAdvent.DayOne.DistanceProcessor

open System
open FSharpAdvent.DayOne.InputProcessor.InputProcessor

module DistanceProcessor =

    let calculateTotalDistance (locationIds: seq<LocationIdPair>) =
        locationIds
        |> Seq.map _.Left
        |> Seq.sort
        |> Seq.zip (locationIds
                    |> Seq.map _.Right
                    |> Seq.sort)
        |> Seq.sumBy (fun (left, right) -> Math.Abs (left - right))
