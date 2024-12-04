module FSharpAdvent.Day4.DayFour

open System.IO

module DayFour =
    
    let countXShapes (rows: string array) =
        let numRows = rows.Length
        let numCols = rows.[0].Length
        let matrix = Array2D.init numRows numCols (fun row col -> rows.[row].[col])

        let directions = [
            (-1, -1);  // Up left 
            (-1,  1);  // Up right
            ( 1, -1);  // Down left 
            ( 1,  1);  // Down right 
        ]

        let getValidChar (row, col) (matrix: char [,]) =
            if row >= 0 && row < numRows && col >= 0 && col < numCols
            then Some matrix.[row, col] else None
            
        let applyDirections (row, col) (dirRow, dirCol) =
            (row + dirRow, col + dirCol), (row - dirRow, col - dirCol)

        // Check if an arm forms "MAS" or "SAM"
        let isValidArm pos dir =
            let pos1, pos2 = applyDirections pos dir
            match getValidChar pos1 matrix, getValidChar pos2 matrix with
            | Some 'M', Some 'S'
            | Some 'S', Some 'M' -> true
            | _ -> false
                
        let allCoordinates =
            [ for row in 0 .. numRows - 1 do
                for col in 0 .. numCols - 1 -> row, col ]

        allCoordinates
        |> List.filter (fun (row, col) -> matrix.[row, col] = 'A')
        |> List.filter (fun pos -> directions |> List.forall (isValidArm pos))
        |> List.length
    
    let occurrencesInGrid (wordToFind: string) (grid: string array) =
        let wordVector = wordToFind.ToCharArray()
        let wordLength = wordVector.Length
        let numRows = grid.Length
        let numCols = grid.[0].Length
        let matrix = Array2D.init numRows numCols (fun row col -> grid.[row].[col])

        let directions = [
            (-1, -1);  // Up-left
            (-1,  0);  // Up
            (-1,  1);  // Up-right
            ( 0, -1);  // Left
            ( 0,  1);  // Right
            ( 1, -1);  // Down-left
            ( 1,  0);  // Down
            ( 1,  1);  // Down-right
        ]
            
        let getPositions (row, col) (dirRow, dirCol) =
            [ for i in 0..wordLength - 1 -> row + i * dirRow, col + i * dirCol ]
            
        let isValidPosition (row, col) =
            row >= 0 && row < numRows && col >= 0 && col < numCols

        let wordMatches positions =
            positions |> List.forall isValidPosition
            && positions |> List.mapi (fun idx (r, c) -> matrix.[r, c] = wordVector.[idx])
                         |> List.forall id

        let countOccurrencesAt pos =
            directions
            |> List.sumBy (fun dir ->
                if wordMatches (getPositions pos dir) then 1 else 0
            )

        [ for row in 0..numRows - 1 do
            for col in 0..numCols - 1 -> countOccurrencesAt (row, col) ]
        |> List.sum
    
    let run =
        printfn "Day Four"
        
        let input =
            File.ReadLines "4.input"
            |> Array.ofSeq

        let part1 = input |> occurrencesInGrid "XMAS"
        let part2 = input |> countXShapes 
            
        printfn $"XMAS Count: {part1}"
        printfn $"X-MAS Count: {part2}"
