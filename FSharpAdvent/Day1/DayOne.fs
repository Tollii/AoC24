module FSharpAdvent.DayOne.DayOne

open System.IO
open System.Text.RegularExpressions



module DayOne =
    
    let calculateTotalDistance locationIds =
        let calculateDistance (left: int, right: int) = abs (left - right)
        
        let leftIds = locationIds
                      |> Seq.map fst
                      |> Seq.sort
                      
        let rightIds = locationIds
                       |> Seq.map snd
                       |> Seq.sort
                     
        leftIds
        |> Seq.zip rightIds
        |> Seq.sumBy calculateDistance
        
        
    let calculateSimilarityScore (locationIds) =
        let calculateSimilarityScore (id: int) (count: int) = id * count
        
        let leftIds = locationIds
                      |> Seq.map fst
                       
        leftIds
        |> Seq.map (fun leftId -> (leftId, locationIds
                                           |> Seq.map snd
                                           |> Seq.filter (fun rightId -> leftId = rightId)
                                           |> Seq.length))
        |> Seq.sumBy (fun (leftId, count) -> calculateSimilarityScore leftId count)
    
    let run =
        printfn "Day One"
        
        let toTuple (parts: string array) = int parts.[0], int parts.[1]
        let input =
            File.ReadLines "1.input"
            |> Seq.map (fun line -> Regex.Split(line, @"\s+"))
            |> Seq.map toTuple
        
        let totalDistance = calculateTotalDistance input
        let similarityScore = calculateSimilarityScore input
        
        printfn $"Total distance: {totalDistance}"
        printfn $"Similarity score: {similarityScore}"