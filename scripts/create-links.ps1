# Creates symbolic links and a hard link used by the "Linked / symbolic links" checks.
# Needs Developer Mode or an elevated prompt on Windows. Run from the repo root:
#   pwsh scripts/create-links.ps1
$ErrorActionPreference = "Stop"
$root = Resolve-Path (Join-Path $PSScriptRoot "..")
$target = Join-Path $root "src/Smoke.ProjectModel/Links"
New-Item -ItemType Directory -Force $target | Out-Null

New-Item -ItemType SymbolicLink -Force -Path (Join-Path $target "SymlinkToSharedCode.cs") -Target (Join-Path $root "shared/SharedCode.cs") | Out-Null
New-Item -ItemType SymbolicLink -Force -Path (Join-Path $target "SymlinkToAssets") -Target (Join-Path $root "shared/assets") | Out-Null
New-Item -ItemType HardLink -Force -Path (Join-Path $target "HardlinkToAsset1.txt") -Target (Join-Path $root "shared/assets/asset1.txt") | Out-Null
Write-Host "Links created in $target"
