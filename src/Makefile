.PHONY: nuget



nuget:
	@.nuget/nuget.exe pack MCI.Core/MCI.Core.csproj -Prop Configuration=Release -Symbol -OutputDirectory nupkg
	@.nuget/nuget.exe pack MCI.Converters/MCI.Converters.csproj -Prop Configuration=Release -Symbol -OutputDirectory nupkg
	@.nuget/nuget.exe pack MCI.Reactive/MCI.Reactive.csproj -Prop Configuration=Release -Symbol -OutputDirectory nupkg
