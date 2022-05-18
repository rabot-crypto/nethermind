#!/bin/bash

# This script is intended to be called in ../

set -e

BINDING_DIR="binding"
FUNCTIONAL_DIR="functional"
ARTIFACTS_DIR="artifacts"

CONFIGURATION=Release

BINDING_TAG=$(dotnet vernuntii --config-path "$BINDING_DIR" --duplicate-version-fails)
echo $BINDING_TAG

# Pack projects for only binding purposes e.g. writing plugins
dotnet pack -c $CONFIGURATION "$BINDING_DIR/RabotCrypto.Nethermind.Api"
dotnet pack -c $CONFIGURATION "$BINDING_DIR/RabotCrypto.Nethermind.Db.Rocks"

FUNCTIONAL_TAG=$(dotnet vernuntii --config-path "$FUNCTIONAL_DIR" --empty-caches --duplicate-version-fails)
echo $FUNCTIONAL_TAG

# Pack fully functional projects
dotnet msbuild "$FUNCTIONAL_DIR/RecursivePacker" -p:Configuration=$CONFIGURATION

# Push created packages
find $ARTIFACTS_DIR -name "*.nupkg" -exec dotnet nuget push {} --source "$NUGET_SOURCE" \;
echo "Packages have been published."

git tag "$BINDING_TAG"
git tag "$FUNCTIONAL_TAG"
git push --tags
echo "Git tags have been pushed"