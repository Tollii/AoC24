namespace FSharpAdvent.DayTwo

module ReportModule =

    type Report = {
        Levels: int list
    }
    
    let fromPartsToReport (parts: string seq) = {
        Levels = parts |> List.ofSeq |> List.map int
    }
    
    let fromLevelsToReport (levels: int list) = {
        Levels = levels
    }
    
    let isSafe (report: Report) =
        if List.length report.Levels < 2 then
            true
        else
            let diffs = List.pairwise report.Levels |> List.map (fun (a, b) -> b - a)
            let isIncrementing = List.forall ((<) 0) diffs
            let isDecrementing = List.forall ((>) 0) diffs
            let diffsInRange = List.forall (fun d -> abs d >= 1 && abs d <= 3) diffs
            (isIncrementing || isDecrementing) && diffsInRange
    
    let canBeMadeSafe (report: Report) =
        let len = List.length report.Levels
        let indices = [0..len-1]
        indices
        |> List.exists (fun idx ->
            let modifiedReport = fromLevelsToReport (List.take idx report.Levels @ List.skip (idx + 1) report.Levels)
            isSafe modifiedReport
        )
