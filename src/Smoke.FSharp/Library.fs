namespace Smoke.FSharp

module Say =
    /// Calls into the C# multi-targeted library (cross-language reference).
    let hello name = Smoke.Library.Greeter.Hello name

    let describe shape = sprintf "%A has area %.2f" shape (Shapes.area shape)
