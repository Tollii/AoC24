module FSharpAdvent.Day5.DayFive

open System
open System.IO

module DayFive =
        
    let parseRule (ruleLine: string) =
        match ruleLine.Split('|') with
        | [| page; beBefore |] -> (int page, int beBefore)
        | _ -> failwith "Invalid format"
    
    let getRules (input: string list) =
        input
        |> List.takeWhile (String.IsNullOrWhiteSpace >> not)
        |> List.map parseRule
        
    let getChanges (input: string list) =
        input
        |> List.skipWhile (String.IsNullOrWhiteSpace >> not)
        |> List.skip 1
        |> List.map _.Split(',')
        |> List.map List.ofArray
        |> List.map (List.map int)
        
    let isUpdateValid (rules: (int * int) list) (update: int list)=
        let pageIndexMap = update |> List.mapi (fun idx page -> page, idx) |> Map.ofList
        
        rules
        |> List.forall (fun (x, y) ->
            if Map.containsKey x pageIndexMap && Map.containsKey y pageIndexMap then
                pageIndexMap.[x] < pageIndexMap.[y]
            else
                true) 
        
    let middlePage (update: int list) = update.[(update.Length - 1) / 2]
    
    let run =
        printfn "Day Five"
        
        let input = File.ReadLines "5.input" |> List.ofSeq
        
        let rules = getRules input
        let updates = getChanges input
        
        let (validUpdates, invalidUpdates) =
            updates
            |> List.partition (isUpdateValid rules)
        
        let validMiddlePages =
            validUpdates
            |> List.map middlePage
            
        let validSum = List.sum validMiddlePages
        
        printfn $"Sum of middle pages: {validSum}"
                    