#!/bin/bash

cd "$(dirname "${BASH_SOURCE[0]}")"
. pack-pkgs-defaults.sh

BINDING_DIR="../binding"

BINDING_TAG=$VERSION_CORE-binding.$VERSION_PRE_RELEASE_SUFFIX
echo $BINDING_TAG

# Restores tools
(cd .. && dotnet tool restore)

# Pack projects for only binding purposes e.g. writing plugins
dotnet pack -c $CONFIGURATION "$BINDING_DIR/RabotCrypto.Nethermind.Api" -p:PackageVersion="$BINDING_TAG"
dotnet pack -c $CONFIGURATION "$BINDING_DIR/RabotCrypto.Nethermind.Db.Rocks" -p:PackageVersion="$BINDING_TAG"
