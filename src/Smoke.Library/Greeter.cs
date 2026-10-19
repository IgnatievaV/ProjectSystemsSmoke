using System;

namespace Smoke.Library
{
    /// <summary>Compiled for every target framework.</summary>
    public static class Greeter
    {
        public static string Hello(string name) => $"Hello, {name}! ({TargetInfo.Framework})";
    }

    public static class TargetInfo
    {
#if NET11_0_OR_GREATER
        public const string Framework = "net11.0";
#elif NET10_0
        public const string Framework = "net10.0";
#elif NET48
        public const string Framework = "net48";
#else
        public const string Framework = "netstandard2.1";
#endif
    }
}
