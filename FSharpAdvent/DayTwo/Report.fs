namespace FSharpAdvent.DayTwo

module ReportModule =

    type Report = {
        Levels: int seq
    }
    
    type LevelDirection =
        | Increment 
        | Decrement
    
    let toReport (parts: string seq) = {
        Levels = parts |> Seq.map int
    }
    
    let private isWithinDiffLimit previous current = abs (previous - current) <= 3 && abs (previous - current) >= 1
    let private incrementValidator (previous, current) = previous < current && isWithinDiffLimit previous current
    let private decrementValidator (previous, current) = previous > current && isWithinDiffLimit previous current
    let private direction (pairs: (int * int) list) = if incrementValidator pairs.Head then Increment else Decrement
    let private areLevelsValidIncr (levels: int list) = levels|> List.pairwise|> List.forall incrementValidator
    let private areLevelsValidDecr (levels: int list) = levels|> List.pairwise|> List.forall decrementValidator
    
    let private areLevelsValid (levels: int list) =
        let pairs = List.pairwise levels
        let isValidIncreasing = List.forall incrementValidator pairs
        let isValidDecreasing = List.forall decrementValidator pairs
        isValidIncreasing || isValidDecreasing
            
    let private areLevelsValidWithOneError (levels: int list) =
        let removeCurrentLevel pairwise = 
            match pairwise with
            | [] -> []
            | (first, second) :: tail ->
                printfn $"Removing {first}, next element is {second}"
                second :: (List.map snd tail)
            
        let pairs = List.pairwise levels
        match direction pairs with
        | Increment -> 
            pairs
            |> List.skipWhile incrementValidator
            |> removeCurrentLevel
            |> areLevelsValid
        | Decrement ->
            pairs
            |> List.skipWhile decrementValidator
            |> removeCurrentLevel
            |> areLevelsValid
    let isValid (report: Report) =
        areLevelsValid (report.Levels |> List.ofSeq)
        
    let isValidWithProblemDampener (report: Report) =
        areLevelsValidWithOneError (report.Levels |> List.ofSeq)
