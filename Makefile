.PHONY: install build cover nuget


install:
	nuget restore src/MCI.net.sln

build:
	msbuild src/MCI.net.sln /p:Configuration=Release /t:Clean;Build

cover:
	powershell -NoProfile -ExecutionPolicy Unrestricted ./cover.ps1


nuget:
	@src/.nuget/nuget.exe pack src/MCI.Core/MCI.Core.csproj -Prop Configuration=Release -Symbol -OutputDirectory src/nupkg
	@src/.nuget/nuget.exe pack src/MCI.Reactive/MCI.Reactive.csproj -Prop Configuration=Release -Symbol -OutputDirectory src/nupkg
