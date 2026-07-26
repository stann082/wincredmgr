remove-item .\pub\ -recurse -erroraction silentlycontinue
MSBuild .\src\cli\cli.csproj /p:Configuration=Release /p:OutputPath="..\..\build\release\" `
    /p:Platform="Any CPU" /p:PublishDir="..\..\pub" /t:Publish /v:minimal

move-item '.\pub\cli.exe' '.\pub\CredManager.exe' -verbose
remove-item $env:APPDATA\utils\cli_1_0_0_0 -recurse -erroraction silentlycontinue
copy-item '.\pub' "$env:APPDATA\utils\cli_1_0_0_0" -recurse -verbose

$batchScript = @'
@echo off

set exePath=%APPDATA%\utils\cli_1_0_0_0\CredManager.exe
if not exist "%exePath%" (
    echo CredManager.exe not found at %exePath%
    exit /b 1
)

"%exePath%" %*
exit /b %ERRORLEVEL%
'@

$batchScriptPath = "$env:APPDATA\utils\credmgr.bat"
if (Test-Path $batchScriptPath) {
    Remove-Item $batchScriptPath -Force
    Write-Output "Existing batch script deleted at $batchScriptPath"
}

Set-Content -Path $batchScriptPath -Value $batchScript
Write-Output "Batch script created at $batchScriptPath"
