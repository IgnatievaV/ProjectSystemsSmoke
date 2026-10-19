using Smoke.Library;
using Smoke.Library.Extensions;
using Smoke.Library.Unions;

namespace Smoke.Tests;

public class CSharp15Tests
{
    [Fact]
    public void Greeter_UsesNet11Target() => Assert.Equal("Hello, Rider! (net11.0)", Greeter.Hello("Rider"));

    [Theory]
    [InlineData("Rex")]
    [InlineData("Tom")]
    public void Union_ReturnsCaseName(string name)
    {
        Pet pet = new Cat(name);
        Assert.Equal(name, PetInfo.NameOf(pet));
    }

    [Fact]
    public void ClosedHierarchy_IsExhaustive()
    {
        Assert.Equal("closed", Gate.Describe(new Closed()));
        Assert.Equal("50% open", Gate.Describe(new Open(50)));
    }

    [Fact]
    public void ExtensionIndexer_ReturnsElement()
    {
        IEnumerable<int> numbers = Enumerable.Range(1, 10);
        Assert.Equal(3, numbers[2]);
    }

    [Fact]
    public void LabeledBreak_FindsFirstReachableGoal()
    {
        char[,] grid = { { '.', '#', 'G' }, { '.', 'G', '.' } };
        Assert.Equal((1, 1), Grid.FindGoal(grid));
    }

    [Fact]
    public void CollectionExpressionArguments_UseComparer() =>
        Assert.Single(Grid.UniqueIgnoreCase("Hello", "HELLO", "hello"));
}
