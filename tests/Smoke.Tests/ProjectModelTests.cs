namespace Smoke.Tests;

public class ProjectModelTests
{
    [Fact]
    public void SharedProject_IsCompiledIntoTests() =>
        Assert.Equal("Smoke.Shared (.shproj)", Smoke.Shared.SharedConstants.Origin);
}
