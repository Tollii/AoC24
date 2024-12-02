module FSharpAdvent.DayOne.DayOne

open DistanceProcessor.DistanceProcessor
open FSharpAdvent.DayOne.LocationIdPairModule
open FSharpAdvent.DayOne.SimilarityScoreProcessor.SimilarityScoreProcessor
open InputProcessor.InputProcessor

module DayOne =
    let run =
        printfn "Day One"
        
        let input = parseInput "1.input" toLocationPair
        
        let totalDistance = calculateTotalDistance input
        let similarityScore = calculateSimilarityScore input
        
        printfn $"Total distance: {totalDistance}"
        printfn $"Similarity score: {similarityScore}"