.PHONY: install build cover


install:
	nuget restore src/MCI.net.sln

build:
	msbuild src/MCI.net.sln /p:Configuration=Release /t:Clean;Build

cover:
	powershell -NoProfile -ExecutionPolicy Unrestricted ./cover.ps1
