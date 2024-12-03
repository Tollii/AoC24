module FSharpAdvent.Util.InputProcessor

open System.IO
open System.Text.RegularExpressions

module InputProcessor =
    
    let readAndParseInput filePath mapping =
        File.ReadLines filePath
        |> Seq.map (fun line -> Regex.Split(line, @"\s+"))
        |> Seq.map mapping
