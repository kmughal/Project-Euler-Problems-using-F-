
module MarsRover

open Microsoft.FSharp.Reflection

type Location = N | S | W | E | Default
let fromString<'a> (s:string) =
    match FSharpType.GetUnionCases typeof<'a> |> Array.filter (fun case -> case.Name = s) with
    |[|case|] -> Some(FSharpValue.MakeUnion(case,[||]) :?> 'a)
    |_ -> None

let moveLeft direction:Location = 
    match direction with | N -> Location.W | W -> Location.S | S -> Location.E | E -> Location.N | _ -> invalidOp "wrong direction"

let moveRight direction:Location = 
    match direction with | N -> Location.E | E -> Location.S | S -> Location.W | W -> Location.N | _ -> invalidOp "wrong direction"

let move (x:int,y:int, direction:Location) = 
    match direction with
    | N -> (x,y+1,direction)| E -> (x+1,y,direction) | S -> (x,y-1,direction) | W ->(x-1,y,direction) | _ -> invalidOp "wrong direction"

let parse (x:int,y:int,direction:Location,command:string) =
  match command with 
  | "l" | "L" -> (x,y,moveLeft direction) 
  | "r" | "R" -> (x,y,moveRight direction)
  | "m" |"M" ->  move(x,y,direction)
  | _ -> invalidOp "invalid operation"


let rec visit (arr:list<char>,x:int,y:int,loc:Location) = 
        
        match arr with 
        | [] -> printfn "end of list"
        | v -> 
        let (x,y,loc) = parse (x,y ,loc, v.Head.ToString())
        printfn "x:%d, y:%d, Location:%s" x y (loc.ToString())
        visit(v.Tail, x ,y ,loc )

type Position = {x:int;y:int;dir:Location}

type Command = {command:string}

type Instruction = 
    | Inst1 of Position 
    | Inst2 of Command

let parsePosition (command:string) =
    let v = command.Split(' ')
    ((v.[0]|> int),(v.[1] |> int) ,(fromString<Location> (v.[2])))
     
let getCommandOrPosition (c:string,i:int) = 
    match (i%2) = 0 with 
    | true -> 
    let (x,y,dir) = parsePosition(c)
    Inst1{x=x;y=y;dir=dir.Value}
    | false ->Inst2{command=c}
let mutable counter = -1
let getInstructions (y:list<string>):list<Instruction> = 
    y |> List.map(fun f -> counter <- counter+1 ;getCommandOrPosition(f,counter))

let rec ShowPosition r:list<Instruction> =
        match r with 
        | head::tail-> 
          match head with 
          | Inst1 c1 -> 
            match tail.Head with 
            | Inst2 c2 -> 
                visit((c2.command |> Seq.toList),c1.x,c1.y,c1.dir)
            | _ -> ()
          | _ -> ()
          ShowPosition tail.Tail
        | _ -> []
    