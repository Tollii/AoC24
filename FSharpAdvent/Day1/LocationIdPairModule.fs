namespace FSharpAdvent.DayOne

open System

module LocationIdPairModule = 
    type LocationIdPair = {
        Left: int
        Right: int
    }
    
    let toLocationPair (parts: string array) = {
        Left = int parts.[0]
        Right = int parts.[1]
    }
    
    
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
