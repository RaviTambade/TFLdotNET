
function Remove-CSharpComments {
    param([string]$FilePath)
    
    $content = [System.IO.File]::ReadAllText($FilePath, [System.Text.Encoding]::UTF8)
    $originalSize = $content.Length
    
    # Remove multi-line comments /* ... */
    $content = [System.Text.RegularExpressions.Regex]::Replace($content, '/\*.*?\*/', '', [System.Text.RegularExpressions.RegexOptions]::Singleline)
    
    # Process line by line for single-line comments
    $lines = $content -split [System.Environment]::NewLine
    $newLines = @()
    
    foreach ($line in $lines) {
        if ($line -match '//') {
            $inString = $false
            $inChar = $false
            $commentPos = -1
            
            for ($i = 0; $i -lt $line.Length - 1; $i++) {
                $char = $line[$i]
                $nextChar = $line[$i + 1]
                
                # Simple check (doesn't handle escaped quotes perfectly but good enough)
                if ($char -eq '"') { $inString = -not $inString }
                if ($char -eq "'") { $inChar = -not $inChar }
                
                # Find comment start
                if (-not $inString -and -not $inChar -and $char -eq '/' -and $nextChar -eq '/') {
                    $commentPos = $i
                    break
                }
            }
            
            if ($commentPos -ge 0) {
                $line = $line.Substring(0, $commentPos).TrimEnd()
            }
        }
        
        # Only add non-empty lines
        if ($line.Trim()) {
            $newLines += $line
        }
    }
    
    $content = $newLines -join [System.Environment]::NewLine
    
    # Remove excess blank lines
    while ($content -match [System.Environment]::NewLine + [System.Environment]::NewLine + [System.Environment]::NewLine) {
        $content = $content -replace ([System.Environment]::NewLine + [System.Environment]::NewLine + [System.Environment]::NewLine), ([System.Environment]::NewLine + [System.Environment]::NewLine)
    }
    
    $content = $content.Trim()
    if ($content) {
        $content += [System.Environment]::NewLine
    }
    
    $newSize = $content.Length
    $bytesRemoved = $originalSize - $newSize
    
    return @{
        'Content' = $content
        'BytesRemoved' = $bytesRemoved
        'HasComments' = $bytesRemoved -gt 0
    }
}

# Get all files
$files = @(Get-ChildItem -Path "d:\Ravi\TAP\TAP\TFLdotNET\Days" -Filter "*.cs" -Recurse -File)
$totalFiles = $files.Count
Write-Host "Total files: $totalFiles"

$filesWithComments = 0
$totalBytesRemoved = 0

# Process files in batches of 20
$batchSize = 20
$batches = [Math]::Ceiling($totalFiles / $batchSize)

for ($b = 0; $b -lt $batches; $b++) {
    $start = $b * $batchSize
    $end = [Math]::Min($start + $batchSize, $totalFiles)
    $batchFiles = $files[$start..($end - 1)]
    
    Write-Host "Processing batch $($b + 1)/$batches (files $($start + 1)-$end)"
    
    foreach ($file in $batchFiles) {
        $result = Remove-CSharpComments -FilePath $file.FullName
        
        if ($result.HasComments) {
            [System.IO.File]::WriteAllText($file.FullName, $result.Content, [System.Text.Encoding]::UTF8)
            $filesWithComments++
            $totalBytesRemoved += $result.BytesRemoved
            Write-Host "  Cleaned: $($file.Name) (removed $($result.BytesRemoved) bytes)"
        }
    }
}

Write-Host ""
Write-Host "Summary:"
Write-Host "=========="
Write-Host "Total files processed: $totalFiles"
Write-Host "Files with comments removed: $filesWithComments"
Write-Host "Total bytes removed: $totalBytesRemoved"
