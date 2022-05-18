#!/bin/bash

# This script is intended to be called in ../

set -e

BINDING_DIR="binding"
OFFICALS_DIR="officials"
ARTIFACTS_DIR="artifacts"

CONFIGURATION=Release

# Pack projects for only binding purposes e.g. writing plugins
dotnet pack -c $CONFIGURATION "$BINDING_DIR/RabotCrypto.Nethermind.Api"
dotnet pack -c $CONFIGURATION "$BINDING_DIR/RabotCrypto.Nethermind.Db.Rocks"

# Pack fully functional projects
dotnet msbuild "$OFFICALS_DIR/RecursivePacker" -p:Configuration=$CONFIGURATION

# Push created packages
find $ARTIFACTS_DIR -name "*.nupkg" -exec dotnet nuget push {} --source "$NUGET_SOURCE" \;

echo "The packages have been published."