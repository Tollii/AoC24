module FSharpAdvent.DayTwo.DayTwo

open FSharpAdvent.DayOne.InputProcessor
open FSharpAdvent.DayTwo.ReportModule

    
module DayTwo =
    let run =
        printfn "Day Two"
        
        let reports = InputProcessor.readAndParseInput "2.test" fromPartsToReport |> List.ofSeq
        
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
        
