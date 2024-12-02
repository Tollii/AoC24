module FSharpAdvent.DayOne.SimilarityScoreProcessor

open FSharpAdvent.DayOne.LocationIdPairModule

module SimilarityScoreProcessor =
    
    let calculateSimilarityScore (locationIds: seq<LocationIdPair>) =
        let calculateSimilarityScore (id: int) (count: int) = id * count
        
        let leftIds = locationIds
                      |> Seq.map _.Left
                       
        leftIds
        |> Seq.map (fun leftId -> (leftId, locationIds
                                           |> Seq.map _.Right
                                           |> Seq.filter (fun rightId -> leftId = rightId)
                                           |> Seq.length))
        |> Seq.sumBy (fun (leftId, count) -> calculateSimilarityScore leftId count)
