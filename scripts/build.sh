#!/usr/bin/env bash
# Cross-platform build script. Run from the repo root.
set -euo pipefail
cd "$(dirname "$0")/.."
dotnet build ProjectSystemsSmoke.slnx "$@"
dotnet test --solution ProjectSystemsSmoke.slnx
