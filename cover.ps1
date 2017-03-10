$opencover = (Resolve-Path "src/packages/OpenCover.*/tools/OpenCover.Console.exe").ToString()
$runner = (Resolve-Path "src/packages/xunit.runner.console.*/tools/xunit.console.x86.exe").ToString()
$coveralls = (Resolve-Path "src/packages/coveralls.net.*/tools/csmacnz.coveralls.exe").ToString()
$targetdir = (Resolve-Path "src/MCI.Core.Tests/bin/Release").ToString()
$targetargs = """MCI.Core.Tests.dll"" -noshadow -appveyor"
$filter = " +[MCI.Core*]* -[MCI.Core.Tests*]*"


& $opencover -register:user -target:$runner "-targetargs:$targetargs" -targetdir:$targetdir "-filter:$filter" -returntargetcode -hideskipped:All -output:result.xml


$report = (Resolve-Path "src/packages/ReportGenerator.*/tools/ReportGenerator.exe").ToString()

& $report result.xml html
