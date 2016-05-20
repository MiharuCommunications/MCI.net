.PHONY: nuget



nuget:
	@.nuget/nuget.exe pack MCI.net/MCI.net.csproj -Prop Configuration=Release -Symbol -OutputDirectory nupkg
	@.nuget/nuget.exe pack MCI.Reactive/MCI.Reactive.csproj -Prop Configuration=Release -Symbol -OutputDirectory nupkg
