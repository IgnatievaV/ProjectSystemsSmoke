using Microsoft.CodeAnalysis;

namespace Smoke.Generators;

[Generator]
public sealed class HelloGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource("HelloGenerated.g.cs",
            """
            namespace Smoke.Generated;

            public static class HelloGenerated
            {
                public const string Message = "Hello from the source generator";
            }
            """));
    }
}
