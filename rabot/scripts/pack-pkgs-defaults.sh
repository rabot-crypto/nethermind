set -e

# Set variables

VERSION_CORE="${VERSION_CORE:-0.1.0}"
VERSION_PRE_RELEASE_SUFFIX="${VERSION_PRE_RELEASE_SUFFIX:-0}"
CONFIGURATION="Release"

# Inject "Directory.Build.targets" to Nethermind-projects without actually creating such file

CustomBeforeMicrosoftCommonTargets="../Nethermind.Directory.Build.targets"
CustomBeforeMicrosoftCommonTargets=$(readlink -f $CustomBeforeMicrosoftCommonTargets)

# A small Cygwin fix :D
if [ -x "$(command -v cygpath)" ]
then
CustomBeforeMicrosoftCommonTargets="$(cygpath -aw $CustomBeforeMicrosoftCommonTargets)"
fi

export CustomBeforeMicrosoftCommonTargets
