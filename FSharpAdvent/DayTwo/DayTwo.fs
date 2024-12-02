module FSharpAdvent.DayTwo.DayTwo

open FSharpAdvent.DayOne.InputProcessor
open FSharpAdvent.DayTwo.ReportModule

    
module DayTwo =
    let run =
        printfn "Day Two"
        
        let input = InputProcessor.readAndParseInput "2.input" toReport
        
        let part1 =
            input
            |> Seq.filter isValid
            |> Seq.length
        
        printfn $"Number of valid reports: {part1}"
        
        let part2 =
            input
            |> Seq.filter isValidWithProblemDampener
            |> Seq.length
        
        printfn $"Number of valid reports with one error allowed: {part2}"
