#!/usr/bin/env dotnet
// File-based app (.NET 11). Run from this folder: dotnet app.cs   (or from the root: dotnet run --file file-based/app.cs)
#:include helpers.cs
#:include models/customer.cs
#:property AssemblyTitle=FileBasedSmoke
#:project ../src/Smoke.Library/Smoke.Library.csproj

Console.WriteLine(Helpers.FormatOutput(new Customer("Rider")));
Console.WriteLine(Smoke.Library.Greeter.Hello("file-based app"));
