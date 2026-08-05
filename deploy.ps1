remove-item .\pub\ -recurse -erroraction silentlycontinue
dotnet publish .\src\cli\cli.csproj -c Release -o .\pub

move-item '.\pub\cli.exe' '.\pub\credmgr.exe' -verbose
copy-item '.\pub\credmgr.exe' "$env:APPDATA\utils\" -verbose
