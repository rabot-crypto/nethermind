#!/bin/bash

# This script is intended to be called in ../

set -e

BINDING_DIR="binding"
FUNCTIONAL_DIR="functional"
ARTIFACTS_DIR="artifacts"

VERSION_CORE=1.13.1
TIMESTAMP=$(git rev-list --count HEAD)

CONFIGURATION=Release

BINDING_TAG=$VERSION_CORE-binding.$TIMESTAMP
echo $BINDING_TAG

# Pack projects for only binding purposes e.g. writing plugins
dotnet pack -c $CONFIGURATION "$BINDING_DIR/RabotCrypto.Nethermind.Api" -p:PackageVersion="$BINDING_TAG"
dotnet pack -c $CONFIGURATION "$BINDING_DIR/RabotCrypto.Nethermind.Db.Rocks" -p:PackageVersion="$BINDING_TAG"

FUNCTIONAL_TAG=$VERSION_CORE-functional.$TIMESTAMP
echo $FUNCTIONAL_TAG

# Pack fully functional projects
dotnet msbuild "$FUNCTIONAL_DIR/RecursivePacker" -p:Configuration=$CONFIGURATION -p:PackageVersion="$FUNCTIONAL_TAG"

# Push created packages
find $ARTIFACTS_DIR -name "*.nupkg" -exec dotnet nuget push {} --source "$NUGET_SOURCE" \;
echo "Packages have been published."

git tag "$BINDING_TAG"
git tag "$FUNCTIONAL_TAG"
git push --tags
echo "Git tags have been pushed"