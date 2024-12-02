module FSharpAdvent.DayOne.DistanceProcessor

open System
open FSharpAdvent.DayOne.LocationIdPairModule

module DistanceProcessor =

    let calculateTotalDistance (locationIds: seq<LocationIdPair>) =
        let calculateDistance (left: int, right: int) = Math.Abs (left - right)
        
        let leftIds = locationIds
                      |> Seq.map _.Left
                      |> Seq.sort
                      
        let rightIds = locationIds
                       |> Seq.map _.Right
                       |> Seq.sort
                     
        leftIds
        |> Seq.zip rightIds
        |> Seq.sumBy calculateDistance