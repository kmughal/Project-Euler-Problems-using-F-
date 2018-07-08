module ProjectEuler

open System
open System.Linq

open System.IO


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


//4. Find the largest palindrome made from the product of two 3-digit numbers.

let toCharArray (x:int):char[] = x.ToString().ToCharArray()
let revCharArray (ch:char[]):char[] = ch |> Array.rev
let isPalindrome item =
    let (_,_,res) = item
    let numberArray = toCharArray res
    let revNumberArray = revCharArray numberArray  
    revNumberArray.SequenceEqual(numberArray)

let generateThreeDigitsSeq = seq { for i in 100 .. 999 do for j in 100 .. 999 do yield (i,j,i*j) } 
printfn "%A" (Seq.max ((Seq.filter isPalindrome) generateThreeDigitsSeq))

// 5. smallest positive number that is evenly divisible by 1..20
let stop =  21
let divideByAll (n:int) = 
  [1..20] |> Seq.forall(fun x -> n % x = 0) 
let generateNaturalNumbers = 
    Seq.unfold(fun x -> Some(x,x+1)) stop 
    
generateNaturalNumbers
|> Seq.filter(fun y -> divideByAll(y))
|> Seq.head
|> printfn "%A"

// 6. Find difference between sum of square of 100 numbers with square of sum.
let square a = a * a
(square (seq { 1..100} |> Seq.sum)) - (seq { for i in 1..100 do yield (square i)} |> Seq.sum) |> printfn "Difference: %A"

//7. what is 10001 prime number?
let findFactorsOf n = 
        let ub = int32(sqrt (double(n)))
        [2..ub] |> Seq.filter(fun x -> n % x = 0 )
let isPrimeNumber n = findFactorsOf n |> Seq.isEmpty
let show n = match isPrimeNumber(n) with | true -> printfn "%d is prime" n | false -> printfn "%d is not prime" n
[1..10] |> Seq.iter show
Seq.unfold(fun n -> Some(n,n+1)) 2 |> Seq.filter isPrimeNumber |> Seq.item(1000) |> printfn "1001 prime number :%A"

// 8. Find the greatest product of five consecutive digits in the 1000-digit number.
let calc n =   
        let mul x y = x * y
        n |> Seq.fold mul 1
File.ReadAllLines("big-num.txt") 
|> Seq.concat 
|> Seq.map(fun c-> int32(c.ToString()))
|> Seq.windowed(5)
|> Seq.map calc
|> Seq.max
|> printfn "val:%A"

// 9. A Pythagorean triplet is a set of three natural numbers, a < b < c, for which, a*a + b*b = c*c
let generateTiplet = seq { for i in 1 .. 10 do for j in 1..1000 do for k in 1..1000 do yield [i;j;k]}
    
let isPythagoreanTriplet (n:int list) =
        match List.sort(n) with | [a;b;c;] -> a*a + b*b = c*c | _ -> false

generateTiplet |> Seq.filter isPythagoreanTriplet |> Seq.head |> Seq.fold(fun acc n -> acc * n) 1 |>printfn "%A"

//10. sum of 2 million prime numbers
let findFactorsOfLong n = 
        let ub = int64(sqrt (double(n)))
        [2L..ub] |> Seq.filter(fun x -> n % x = 0L )

let isPrimeNumberLong n = findFactorsOfLong n |> Seq.isEmpty

seq {for n in 2L..2000000L do if isPrimeNumberLong(n) then yield n } |> Seq.sum |> printfn "Sum:%A"