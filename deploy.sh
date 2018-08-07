PACKAGE_VERSION=$1
NUGET_API_KEY=$2

dotnet nuget push src/Unidays.Client/bin/Release/Unidays.Client.$PACKAGE_VERSION.nupkg --api-key $NUGET_API_KEY --source https://api.nuget.org/v3/index.json
