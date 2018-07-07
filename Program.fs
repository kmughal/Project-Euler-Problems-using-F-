open MarsRover

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

    ["khurram";"shahzad"]
    |> List.fold(fun y x-> y + "-" + x) "" |>ignore

    ["khurram";"shahzad"] |>
    (fun f -> 
    let rec show x = match x with | [] -> printfn "end" | head::tail -> printfn "%s" head; show tail;
    show f)
    
    
    [100..999]
    |> (fun f -> printfn "%A" f)
     
    
     

    
    
    0 // return an integer exit code


 
  
  