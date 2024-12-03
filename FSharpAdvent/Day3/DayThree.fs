module FSharpAdvent.DayThree.DayThree

open System.IO
open System.Text.RegularExpressions

    
module DayThree =
    type Mul = {
        A: int
        B: int
    }
    
    let toMul (m: Match) = {
        A = int m.Groups.[1].Value
        B = int m.Groups.[2].Value
    }
    
    let multiply (m: Mul) = m.A * m.B
    
    let run =
        printfn "Day Three"
        
        let input = File.ReadAllText "3.input"
        
        let part1 = Regex.Matches(input, @"mul\((\d{1,3}),(\d{1,3})\)")
                    |> Seq.map toMul
                    |> Seq.map multiply
                    |> Seq.sum
        
        let part2 = Regex.Matches(input, @"mul\((\d{1,3}),(\d{1,3})\)|don't\(\)|do\(\)")
                    |> Seq.fold (fun (acc, skip) m ->
                        match m.Value with
                        | "don't()" when not skip -> (acc, true)
                        | "do()" when skip -> (acc, false)
                        | "do()" when not skip -> (acc, skip)
                        | _ when not skip -> (m :: acc, skip)
                        | _ -> (acc, skip))
                        ([], false)
                    |> fst
                    |> Seq.map toMul
                    |> Seq.map multiply
                    |> Seq.sum
        
        printfn $"Part 1 sum is {part1}"
        printfn $"Part 2 sum is {part2}"