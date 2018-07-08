open MarsRover
open System
open System.Linq
open System.IO
[<EntryPoint>]
let main argv =
    [
        "1 2 N"
        "LMLMLMLMM"
        "3 3 E"
        "MMRMMRMRRM"
        "5 5 W"
        "LLMMMRRRMM"
    ] |> getInstructions |> ShowPosition |> ignore

    let sd = DateTime.Now
    let stopAfter2Seconds (d:DateTime) = 
        int(d.Subtract(sd).TotalSeconds) > 2

    printfn "Started:%A" sd
    Seq.unfold(fun s-> Some(s, DateTime.Now)) DateTime.Now |> Seq.filter stopAfter2Seconds |> Seq.head |> printfn "Stopped:%A"
    seq {1.. 20} |> Seq.filter(fun f-> f% 2 = 0) |> Seq.head |> printfn "%A"

    
    
    0 // return an integer exit code


 
  
  