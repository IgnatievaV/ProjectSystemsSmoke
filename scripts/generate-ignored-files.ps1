# Generates a large git-ignored folder (local-cache/) to check indexing and exclusion in Repo view.
#   pwsh scripts/generate-ignored-files.ps1 -Count 5000
param([int]$Count = 2000)
$root = Resolve-Path (Join-Path $PSScriptRoot "..")
$dir = Join-Path $root "local-cache"
New-Item -ItemType Directory -Force $dir | Out-Null
1..$Count | ForEach-Object {
    $sub = Join-Path $dir ("chunk-" + ($_ % 50))
    if (-not (Test-Path $sub)) { New-Item -ItemType Directory $sub | Out-Null }
    Set-Content -Path (Join-Path $sub "file-$_.json") -Value "{ `"id`": $_, `"text`": `"ignored file $_`" }"
}
Write-Host "Generated $Count files in $dir"
