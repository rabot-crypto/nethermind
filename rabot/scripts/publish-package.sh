#!/bin/bash

cd "$(dirname "${BASH_SOURCE[0]}")"
. pack-pkgs-defaults.sh

VERSION_CORE=1.14.5
VERSION_PRE_RELEASE_SUFFIX=$(git rev-list --count HEAD)
ARTIFACTS_DIR="../artifacts"

. pack-binding-pkgs.sh
. pack-functional-pkgs.sh

if [ "$DISABLE_PUBLISH" != "true" ]
then
# Push created packages
find $ARTIFACTS_DIR -name "*.nupkg" -exec dotnet nuget push {} --source "$NUGET_SOURCE" \;
echo "Packages have been published."

git tag "$BINDING_TAG"
git tag "$FUNCTIONAL_TAG"
git push --tags
echo "Git tags have been pushed"
fi
