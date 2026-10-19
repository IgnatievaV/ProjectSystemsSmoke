#if NET11_0_OR_GREATER
namespace Smoke.Library.Extensions;

// C# 15: extension indexers.
public static class SequenceIndexer
{
    extension(IEnumerable<int> sequence)
    {
        public int this[int index] => sequence.ElementAt(index);
    }
}
#endif
