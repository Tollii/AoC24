module FSharpAdvent.DayOne.InputProcessor

open System.IO
open System.Text.RegularExpressions

module InputProcessor = 

    type LocationIdPair = {
        Left: int
        Right: int
    }

    let readAndProcessInput filePath =
        let input filePath = File.ReadLines(filePath)
        
        let splitOnSpaces lines = Seq.map (fun line -> Regex.Split(line, @"\s+")) lines
        
        let toLocationSet (parts: string array) = {
            Left = int parts.[0]
            Right = int parts.[1]
        }

        let processInput (lines: seq<string>)=
            lines
            |> splitOnSpaces
            |> Seq.map toLocationSet
            
        input filePath
        |> processInput
