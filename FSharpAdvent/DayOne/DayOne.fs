module FSharpAdvent.DayOne.DayOne

open DistanceProcessor.DistanceProcessor
open FSharpAdvent.DayOne.SimilarityScoreProcessor.SimilarityScoreProcessor
open InputProcessor.InputProcessor

module DayOne =
    let run =
        printfn "Day One"
        
        let input = readAndProcessInput "1.input"
        
        let totalDistance = calculateTotalDistance input
        let similarityScore = calculateSimilarityScore input
        
        printfn $"Total distance: {totalDistance}"
        printfn $"Similarity score: {similarityScore}"