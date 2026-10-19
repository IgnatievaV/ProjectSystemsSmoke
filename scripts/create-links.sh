#!/usr/bin/env bash
# Creates symbolic links and a hard link used by the "Linked / symbolic links" checks (Linux/macOS).
set -euo pipefail
root="$(cd "$(dirname "$0")/.." && pwd)"
target="$root/src/Smoke.ProjectModel/Links"
mkdir -p "$target"
ln -sfn "$root/shared/SharedCode.cs" "$target/SymlinkToSharedCode.cs"
ln -sfn "$root/shared/assets" "$target/SymlinkToAssets"
ln -f "$root/shared/assets/asset1.txt" "$target/HardlinkToAsset1.txt"
echo "Links created in $target"
