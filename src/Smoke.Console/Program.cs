using Smoke.Library;
using Smoke.Library.Extensions;
using Smoke.Library.Unions;

Console.WriteLine(Greeter.Hello("Rider"));

// Command-line args: with the "MSBuildArgs" launch profile .NET 11 expands $(AssemblyName) / $(TargetFramework).
Console.WriteLine($"Args: {(args.Length == 0 ? "<none>" : string.Join(' ', args))}");

// Environment: set via launch profile or `dotnet run -e SMOKE_ENV=value` (new in .NET 11).
Console.WriteLine($"SMOKE_PROFILE = {Environment.GetEnvironmentVariable("SMOKE_PROFILE") ?? "<not set>"}");
Console.WriteLine($"SMOKE_ENV     = {Environment.GetEnvironmentVariable("SMOKE_ENV") ?? "<not set>"}");

// C# 15 features from Smoke.Library.
Pet pet = new Dog("Rex");
Console.WriteLine($"Union:              {PetInfo.NameOf(pet)}");
Console.WriteLine($"Closed hierarchy:   {Gate.Describe(new Open(42))}");

IEnumerable<int> numbers = Enumerable.Range(1, 10);
Console.WriteLine($"Extension indexer:  numbers[2] = {numbers[2]}");

char[,] grid = { { '.', '#', 'G' }, { '.', 'G', '.' } };
Console.WriteLine($"Labeled break:      goal at {Grid.FindGoal(grid)}");
Console.WriteLine($"Collection args:    {Grid.UniqueIgnoreCase("Hello", "HELLO", "hello").Count} unique item(s)");

// Project model: OS-conditional compile item, source generator, shared project.
Console.WriteLine($"Platform file:      {Smoke.Console.PlatformInfo.Name} ({Environment.OSVersion.Platform})");
Console.WriteLine($"Source generator:   {Smoke.Generated.HelloGenerated.Message}");
Console.WriteLine($"Shared project:     {Smoke.Shared.SharedConstants.Origin}");
