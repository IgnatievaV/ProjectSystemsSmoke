# Installs node_modules for the standalone frontend and the .esproj (needs Node.js 20+).
$ErrorActionPreference = "Stop"
$root = Resolve-Path (Join-Path $PSScriptRoot "..")
Push-Location (Join-Path $root "frontend/vite-react"); npm install; Pop-Location
Push-Location (Join-Path $root "src/Smoke.Esproj"); npm install; Pop-Location
