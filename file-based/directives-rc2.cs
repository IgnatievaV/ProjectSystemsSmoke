#!/usr/bin/env dotnet
// Requires .NET 11 RC2+: quoted #:property values and Name=Value item metadata on #:package.
// Run from this folder: dotnet directives-rc2.cs
#:property Company="Contoso Tools"
#:property AssemblyTitle="Contoso Directive Demo"
#:package System.CommandLine@2.0.1 Aliases=CommandLine

extern alias CommandLine;

using System.Reflection;

Console.WriteLine($"Company: {Assembly.GetEntryAssembly()!.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company}");
Console.WriteLine($"Alias:   {typeof(CommandLine::System.CommandLine.RootCommand).Assembly.GetName().Name}");
