namespace Smoke.FSharp

type Shape =
    | Circle of radius: float
    | Rectangle of width: float * height: float

module Shapes =
    let area shape =
        match shape with
        | Circle r -> System.Math.PI * r * r
        | Rectangle (w, h) -> w * h
