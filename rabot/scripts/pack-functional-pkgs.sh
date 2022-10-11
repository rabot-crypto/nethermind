#!/bin/bash

cd "$(dirname "${BASH_SOURCE[0]}")"
. pack-pkgs-defaults.sh

FUNCTIONAL_DIR="../functional"

FUNCTIONAL_TAG=$VERSION_CORE-functional.$VERSION_PRE_RELEASE_SUFFIX
echo $FUNCTIONAL_TAG

# Pack fully functional projects
dotnet msbuild "$FUNCTIONAL_DIR/RecursivePacker" -p:Configuration=$CONFIGURATION -p:PackageVersion="$FUNCTIONAL_TAG"
