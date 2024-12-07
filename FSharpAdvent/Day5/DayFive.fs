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
        |> List.groupBy fst
        |> List.map (fun (x, pairs) -> x, pairs |> List.map snd)
        |> Map.ofList
        
    let getChanges (input: string list) =
        input
        |> List.skipWhile (String.IsNullOrWhiteSpace >> not)
        |> List.skip 1
        |> List.map (fun line -> line.Split(','))
        |> List.map Array.toList
        |> List.map (List.map int)

    let isUpdateValid (rules: Map<int, int list>) (update: int list) =
        let pageIndexMap =
            update
            |> List.mapi (fun i p -> p, i)
            |> Map.ofList

        let comesBefore x y =
            match Map.tryFind x pageIndexMap, Map.tryFind y pageIndexMap with
            | Some ix, Some iy -> ix < iy
            | _ -> true  

        rules
        |> Map.forall (fun x ys -> ys |> List.forall (comesBefore x))
        
    let middlePage (update: int list) = update.[(update.Length - 1) / 2]

    let correctUpdate (rules: Map<int, int list>) (update: int list) =
        let pages = List.distinct update
        let total = List.length pages
        let pageToIndex = 
            pages
            |> List.mapi (fun i p -> p, i)
            |> Map.ofList

        let pagesSet = Set.ofList pages

        let adjacency =
            let adj = Array.create total []
            for KeyValue(x, ys) in rules do
                if Map.containsKey x pageToIndex then
                    let xIdx = pageToIndex.[x]
                    let validYs = ys |> List.filter (fun y -> Set.contains y pagesSet)
                    let indexedSuccs = 
                        validYs
                        |> List.choose (fun y ->
                            match Map.tryFind y pageToIndex with
                            | Some yi -> Some yi
                            | None -> None
                        )
                    adj.[xIdx] <- indexedSuccs
            adj

        let incoming =
            let inc = Array.create total 0
            for i in 0 .. total-1 do
                for s in adjacency.[i] do
                    inc.[s] <- inc.[s] + 1
            inc

        let rec backtrack order chosenCount (chosen: bool array) (incoming: int array) =
            if chosenCount = total then
                List.rev order
                |> List.map (fun i -> pages.[i]) // Map back indices to pages
                |> Some
            else
                let candidates =
                    [ for i in 0 .. total-1 do
                        if Array.get incoming i = 0 && not (Array.get chosen i) then
                            yield i ]

                match candidates with
                | [] -> None
                | _ ->
                    candidates
                    |> List.tryPick (fun c ->
                        let chosenCopy = Array.copy chosen
                        chosenCopy.[c] <- true

                        let incomingCopy = Array.copy incoming
                        for succ in adjacency.[c] do
                            incomingCopy.[succ] <- incomingCopy.[succ] - 1

                        backtrack (c::order) (chosenCount+1) chosenCopy incomingCopy
                    )

        let chosen = Array.create total false
        match backtrack [] 0 chosen incoming with
        | Some validPages -> validPages
        | None -> failwith "No valid ordering found."

    let run =
        printfn "Day Five"
        
        let input = File.ReadLines "5.input" |> List.ofSeq
        let rules = getRules input
        let updates = getChanges input
        
        let validUpdates, invalidUpdates =
            updates |> List.partition (isUpdateValid rules)
        
        let validSum =
            validUpdates
            |> List.map middlePage
            |> List.sum
        
        let invalidSum =
            invalidUpdates
            |> List.map (correctUpdate rules)
            |> List.map middlePage
            |> List.sum
        
        printfn $"Sum of valid middle pages: {validSum}"
        printfn $"Sum of invalid (made valid again) middle pages: {invalidSum}"
