# Windows build script. Run from the repo root.
$ErrorActionPreference = "Stop"
Set-Location (Join-Path $PSScriptRoot "..")
dotnet build ProjectSystemsSmoke.slnx @args
dotnet test --solution ProjectSystemsSmoke.slnx
