namespace FSharpAdvent.DayOne

module LocationIdPairModule = 
    type LocationIdPair = {
        Left: int
        Right: int
    }
    
    let toLocationPair (parts: string array) = {
        Left = int parts.[0]
        Right = int parts.[1]
    }