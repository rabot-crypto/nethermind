#!/bin/bash

# This script is intended to be called in /rabot/

set -e

VERSION=$(vernuntii)
CONFIGURATION=Release

dotnet pack -c $CONFIGURATION "src/RabotCrypto.Nethermind.Api"
dotnet pack -c $CONFIGURATION "src/RabotCrypto.Nethermind.Db.Rocks"

dotnet nuget push "src/RabotCrypto.Nethermind.Api/bin/Release/RabotCrypto.Nethermind.Api.$VERSION.nupkg" --source "$NUGET_SOURCE"
dotnet nuget push "src/RabotCrypto.Nethermind.Db.Rocks/bin/Release/RabotCrypto.Nethermind.Db.Rocks.$VERSION.nupkg" --source "$NUGET_SOURCE"

git tag "$VERSION"
git push --tags

echo "The packages has been published."