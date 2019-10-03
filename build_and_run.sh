#!/bin/bash

dotnet build
dotnet publish -r osx-x64
dotnet bin/Debug/netcoreapp2.2/cosmosdb-deleter.dll "$@"