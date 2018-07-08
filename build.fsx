#r "paket:
open Fake.DotNet
nuget Fake.IO.FileSystem
nuget Fake.DotNet.MSBuild
nuget Fake.Core.Target //"
#load "./.fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.IO
open Fake.IO.Globbing.Operators
open Fake.DotNet

let bin = "./bin/"

Target.create "Clean" (fun _ -> 
    Shell.cleanDir bin 
)

Target.create "Build" (fun _ -> 
    !! "*.fsproj" |> MSBuild.runRelease id bin "Build" |> Trace.logItems "Build-ouput:"
)

Target.create "Default" (fun _ -> Trace.trace "Hello world")

open Fake.Core.TargetOperators
"Clean" 
    ==> "Build"
    ==> "Default"

Target.runOrDefault "Default"