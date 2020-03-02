#!/bin/bash

mkdir -p Builds

UNITY_PATH=$(find-unity)

echo "Building with ${UNITY_PATH}"

"${UNITY_PATH}/Contents/MacOS/Unity" \
    -batchmode \
    -nographics \
    -silent-crashes \
    -stackTraceLogType Full \
    -logFile - \
    -projectPath "$(pwd)/" \
    -exportPackage "Assets/Plugins" \
    "$(pwd)/Builds/A11YTK.unitypackage" \
    -quit

echo "Done"
