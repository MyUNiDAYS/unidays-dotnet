#!/bin/bash

PACKAGE_VERSION=$1
NUGET_API_KEY=$2

nuget push src/Unidays.Client/bin/Release/Unidays.Client.$PACKAGE_VERSION.nupkg -ApiKey $NUGET_API_KEY
