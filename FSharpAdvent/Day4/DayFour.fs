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

        let isValidPosition (row, col) =
            row >= 0 && row < numRows && col >= 0 && col < numCols

        // Check if an arm forms "MAS" or "SAM"
        let isValidArm (centerRow, centerCol) (deltaRow, deltaCol) =
            let pos1 = (centerRow + deltaRow, centerCol + deltaCol) // Check left and right
            let pos2 = (centerRow - deltaRow, centerCol - deltaCol) // CHeck up and down
            if isValidPosition pos1 && isValidPosition pos2 then
                let char1 = matrix.[fst pos1, snd pos1]
                let char2 =  matrix.[fst pos2, snd pos2]
                (char1 = 'M' && char2 = 'S') || (char1 = 'S' && char2 = 'M')
            else
                false
                
        let allCoordinates =
            [ for row in 0 .. numRows - 1 do
                for col in 0 .. numCols - 1 -> row, col ]
        

        allCoordinates
        |> List.filter (fun (row, col) -> matrix.[row, col] = 'A')
        |> List.filter (fun (row, col) -> directions |> List.forall (isValidArm (row, col)))
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
            
        let getPositions row col dirRow dirCol =
            [ for i in 0..wordLength - 1 -> row + i * dirRow, col + i * dirCol ]
            
        let isValidPosition (row, col) =
            row >= 0 && row < numRows && col >= 0 && col < numCols

        let wordMatches positions =
            positions |> List.forall isValidPosition
            && positions |> List.mapi (fun idx (r, c) -> matrix.[r, c] = wordVector.[idx])
                         |> List.forall id

        let countOccurrencesAt row col =
            directions
            |> List.sumBy (fun (dirRow, dirCol) ->
                if wordMatches (getPositions row col dirRow dirCol) then 1 else 0
            )

        [ for row in 0..numRows - 1 do
            for col in 0..numCols - 1 -> countOccurrencesAt row col ]
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
