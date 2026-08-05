remove-item .\pub\ -recurse -erroraction silentlycontinue
dotnet publish .\src\cli\cli.csproj -c Release -o .\pub

move-item '.\pub\cli.exe' '.\pub\credmgr.exe' -verbose
remove-item $env:APPDATA\utils\cli_1_0_0_0 -recurse -erroraction silentlycontinue
copy-item '.\pub' "$env:APPDATA\utils\cli_1_0_0_0" -recurse -verbose

$batchScriptPath = "$env:APPDATA\utils\credmgr.bat"
if (Test-Path $batchScriptPath) {
    Remove-Item $batchScriptPath -Force
    Write-Output "Existing batch script deleted at $batchScriptPath"
}

Set-Content -Path $batchScriptPath -Value $batchScript
Write-Output "Batch script created at $batchScriptPath"
