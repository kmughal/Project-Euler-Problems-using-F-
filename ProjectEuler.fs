

// Recursion example
let IsPrime n x = 
        x = n || x % n <> 0

let rec RemoveMultiples (list1:list<int>) (list2:list<int>) =
    match list1 with 
    | head::tail -> RemoveMultiples tail (List.filter (IsPrime head) list2)
    | [] -> list2
       
let GetPrimeNumber n = 
    let max = int(sqrt(float n))
    let list1 = [2 ..max ]
    let list2 = [ 1..n ]
    printfn "list1: %A, list2: %A" list1 list2
    RemoveMultiples list1 list2
printfn "%d : %A" 1000 (GetPrimeNumber 1000)


// 1. Determin sum of first 1000 number which are multiple of 3 or 5
List.sum(List.filter(fun f-> f%3 = 0 || f% 5 = 0)[1..1000]) |> printfn "%A"

// 2. Find the sum of 4 million even values Fibonacci sequence
Seq.unfold(fun(cur,nxt) -> Some(cur,(nxt,(cur+nxt)))) (0, 1)
    |> Seq.takeWhile(fun n -> n < 400000)
    |> Seq.filter(fun v -> v % 2 = 0)
    |> Seq.sum
    |> printfn "Sum:%A"

// 3. What is the largest prime factor of the number 600851475143 ?
let sqroot v = int64(sqrt(double(v)))
let isPrime n =  not(Seq.exists(fun x ->  n % x = 0L ) [2L..(sqroot n)])
let canDivide (x:int64) (y:int64) = x % y = 0L
let getLargestPrimeFactor (n:int64) = [2L..(sqroot n)] |> Seq.filter(fun (x:int64) -> (canDivide n x)) |> Seq.filter isPrime |> Seq.max
getLargestPrimeFactor 600851475143L |> printfn "%A"

