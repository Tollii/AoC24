open System.Diagnostics
open FSharpAdvent.Day1.DayOne
open FSharpAdvent.Day2.DayTwo
open FSharpAdvent.Day3.DayThree
open FSharpAdvent.Day4.DayFour

printfn "Hello from F#"

let stopwatch = Stopwatch.StartNew()
// DayOne.run
// DayTwo.run
// DayThree.run
DayFour.run

stopwatch.Stop()
printfn $"Elapsed milliseconds: {stopwatch.Elapsed.Milliseconds}"