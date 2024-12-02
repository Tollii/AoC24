module FSharpAdvent.DayOne.InputProcessor

open System.IO
open System.Text.RegularExpressions

module InputProcessor = 

    type Report = {
        Levels: int list
    }

    let parseInput filePath mapping =
        let input filePath = File.ReadLines(filePath)
        
        let splitOnSpaces lines = Seq.map (fun line -> Regex.Split(line, @"\s+")) lines

        let processInput (lines: seq<string>)=
            lines
            |> splitOnSpaces
            |> Seq.map mapping
            
        input filePath
        |> processInput
