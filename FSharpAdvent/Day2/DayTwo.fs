module FSharpAdvent.DayTwo.DayTwo

open System.IO
open System.Text.RegularExpressions

    
module DayTwo =
    
    let isSafe (levels: int list) =
        if List.length levels < 2 then
            true
        else
            let diffs = List.pairwise levels |> List.map (fun (a, b) -> b - a)
            let isIncrementing = List.forall ((<) 0) diffs
            let isDecrementing = List.forall ((>) 0) diffs
            let diffsInRange = List.forall (fun d -> abs d >= 1 && abs d <= 3) diffs
            (isIncrementing || isDecrementing) && diffsInRange
    
    let canBeMadeSafe (levels: int list) =
        let len = List.length levels
        let indices = [0..len - 1]
        indices
        |> List.exists (fun idx ->
            let modifiedLevels =  List.take idx levels @ List.skip (idx + 1) levels
            isSafe modifiedLevels
        )
    
    let run =
        printfn "Day Two"
        
        let reports =
            File.ReadLines "2.input"
            |> Seq.map (fun line -> Regex.Split(line, @"\s+"))
            |> Seq.map (fun parts -> (parts
                                      |> List.ofSeq
                                      |> List.map int)
            )
            |> List.ofSeq
        
        let safeReportsCount =
            reports
            |> List.filter isSafe
            |> List.length
        
        let safeErrorReportsWithToleranceCount =
            reports
            |> List.filter (fun report -> isSafe report || canBeMadeSafe report)
            |> List.length
        
        printfn $"Number of safe reports: {safeReportsCount}"
        printfn $"Number of safe reports with one error tolerance: {safeErrorReportsWithToleranceCount}"
        
