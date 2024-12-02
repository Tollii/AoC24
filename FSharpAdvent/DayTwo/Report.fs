namespace FSharpAdvent.DayTwo

module ReportModule =

    type Report = {
        Levels: int seq
    }
    
    let toReport (parts: string seq) = {
        Levels = parts |> Seq.map int
    }
    
    let private isWithinDiffLimit previous current = abs (previous - current) <= 3 && abs (previous - current) >= 1
    let private incrementValidator (previous, current) = previous < current && isWithinDiffLimit previous current
    let private decrementValidator (previous, current) = previous > current && isWithinDiffLimit previous current
    
    let private areLevelsValid (levels: int list) =
        match levels with 
        | [] -> true
        | [_] -> true
        | lst ->
            let pairs = List.pairwise lst
            let isValidIncreasing = List.forall incrementValidator pairs
            let isValidDecreasing = List.forall decrementValidator pairs
            isValidIncreasing || isValidDecreasing
            
    let private areLevelsValidWithOneError (levels: int list) =
        match levels with
        | [] -> true
        | [_] -> true
        | lst ->
            let removeCurrentElement pairwise = 
                match pairwise with
                | [] -> []
                | (first, _) :: tail -> first :: (List.map snd tail)
                
            let pairs = List.pairwise lst
            
            if incrementValidator pairs.Head then
                let untilFirstError = pairs |> List.skipWhile incrementValidator
                let modifiedLevels =
                    match untilFirstError with
                    | [] -> levels
                    | lst -> removeCurrentElement lst
                    
                areLevelsValid modifiedLevels
            else
                let untilFirstError = pairs |> List.skipWhile decrementValidator
                let modifiedLevels =
                    match untilFirstError with
                    | [] -> []
                    | lst -> removeCurrentElement lst
                    
                areLevelsValid modifiedLevels
        
    let isValid (report: Report) =
        areLevelsValid (report.Levels |> List.ofSeq)
        
    let isValidWithProblemDampener (report: Report) =
        areLevelsValidWithOneError (report.Levels |> List.ofSeq)
