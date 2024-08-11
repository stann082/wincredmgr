$rootDir = ".\"
$foldersToDelete = @("bin", "obj", "build", "pub")

function Remove-Folders {
    param (
        [string]$rootDir,
        [string[]]$foldersToDelete
    )

    foreach ($folder in $foldersToDelete) {
        Get-ChildItem -Path $rootDir -Recurse -Directory -Force -ErrorAction SilentlyContinue | 
            Where-Object { $_.Name -eq $folder } | 
            ForEach-Object {
                Write-Host "Deleting folder: $($_.FullName)"
                Remove-Item -Recurse -Force -Path $_.FullName
            }
    }
}

Remove-Folders -rootDir $rootDir -foldersToDelete $foldersToDelete
Write-Host "Cleanup complete."
