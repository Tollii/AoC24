module FSharpAdvent.DayOne.DayOne

open FSharpAdvent.DayOne.LocationIdPairModule
open FSharpAdvent.Util.InputProcessor.InputProcessor

module DayOne =
    let run =
        printfn "Day One"
        
        let input = readAndParseInput "1.input" toLocationPair
        
        let totalDistance = calculateTotalDistance input
        let similarityScore = calculateSimilarityScore input
        
        printfn $"Total distance: {totalDistance}"
        printfn $"Similarity score: {similarityScore}"