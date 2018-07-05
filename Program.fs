open MarsRover
[<EntryPoint>]
let main argv =
    [
        "1 2 N"
        "LMLMLMLMM"
        "3 3 E"
        "MMRMMRMRRM"
    ] |> getInstructions |> ShowPosition |> ignore
    0 // return an integer exit code
