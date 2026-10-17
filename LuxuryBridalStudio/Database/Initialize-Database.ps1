[CmdletBinding()]
param(
    [string]$ServerInstance = '.',
    [switch]$SeedDemo
)

$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Data
$builder = New-Object System.Data.SqlClient.SqlConnectionStringBuilder
$builder['Data Source'] = $ServerInstance
$builder['Initial Catalog'] = 'master'
$builder['Integrated Security'] = $true
$builder['Encrypt'] = $true
# Development connection to the local SQL Server's self-signed certificate.
$builder['TrustServerCertificate'] = $true
$builder['Connect Timeout'] = 10
$connection = New-Object System.Data.SqlClient.SqlConnection $builder.ConnectionString
try {
    $connection.Open()
    $scripts = @('001_CreateDatabase.sql', '002_Schema.sql')
    if ($SeedDemo) { $scripts += '003_SeedDemoCatalog.sql' }
    foreach ($script in $scripts) {
        $sql = [IO.File]::ReadAllText((Join-Path $PSScriptRoot $script), [Text.Encoding]::UTF8)
        foreach ($batch in [regex]::Split($sql, '(?im)^\s*GO\s*\r?$')) {
            if ([string]::IsNullOrWhiteSpace($batch)) { continue }
            $command = $connection.CreateCommand()
            try {
                $command.CommandTimeout = 60
                $command.CommandText = $batch
                [void]$command.ExecuteNonQuery()
            } finally { $command.Dispose() }
        }
        Write-Host "Applied $script"
    }
    Write-Host "Database LuxuryBridalStudio is ready on server '$ServerInstance'."
} finally { $connection.Dispose() }
