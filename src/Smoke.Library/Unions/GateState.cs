#if NET11_0_OR_GREATER
namespace Smoke.Library.Unions;

// C# 15: closed hierarchies. GateState can only be derived from inside this assembly.
public closed record class GateState;
public record class Closed : GateState;
public record class Open(float Percent) : GateState;

public static class Gate
{
    public static string Describe(GateState state) => state switch
    {
        Closed => "closed",
        Open(var percent) => $"{percent}% open",
        // No warning: every direct descendant of 'GateState' is handled.
    };
}
#endif
