using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CSharp.DayOne;

public static class DayOne
{
    private static readonly Regex MultiSpaceRegex = new(@"\s+");
    
    public static void Run()
    {
        Console.WriteLine();
        Console.WriteLine("Day One\n");
        
        var locationIds = ReadAndProcessInput()
            .ToImmutableList();
        
        Console.WriteLine("Part 1");
        var totalDistance = CalculateTotalDistance(locationIds);
        Console.WriteLine($"Total distance: {totalDistance}\n");
        
        
        Console.WriteLine("Part 2");
        var similarityScore = CalculateSimilarityScore(locationIds);
        Console.WriteLine($"Similarity score: {similarityScore}\n");
    }

    private static int CalculateTotalDistance(IReadOnlyCollection<LocationPair> locations)
    {
        var leftLocationIds = locations
            .Select(x => x.Left)
            .OrderBy(x => x);
        
        var rightLocationIds = locations
            .Select(x => x.Right)
            .OrderBy(x => x);
            
        return leftLocationIds
            .Zip(rightLocationIds, (left, right) => new { Left = left, Right = right })
            .Aggregate(0, (totalDistances, ids) => totalDistances + CalculateDistance(ids.Left, ids.Right));
    }
    
    private static int CalculateSimilarityScore(IReadOnlyCollection<LocationPair> locations)
        => locations
            .Select(locationIds => locationIds.Left)
            .Select(leftId => new { LocationId = leftId, SimilarityScore = leftId * locations
                .Count(locationId => leftId == locationId.Right) })
            .Sum(locationIdToCount => locationIdToCount.SimilarityScore);

    private static int CalculateDistance(int x, int y) => Math.Abs(x - y);

    private static IEnumerable<LocationPair> ReadAndProcessInput() 
        => File
            .ReadLines("DayOne/input_1.txt")
            .Select(line => MultiSpaceRegex.Split(line))
            .Select(parts => new LocationPair(int.Parse(parts[0]), int.Parse(parts[1])));
}

public record LocationPair(int Left, int Right);