Add-Type -AssemblyName System.IO.Compression.FileSystem
$zip = [System.IO.Compression.ZipFile]::OpenRead('c:\EESTN6\Nueva carpeta\olimpiadas 5to.docx')
$xmlEntry = $zip.Entries | Where-Object { $_.FullName -eq 'word/document.xml' }
$stream = $xmlEntry.Open()
$reader = New-Object System.IO.StreamReader($stream)
$xmlStr = $reader.ReadToEnd()
$reader.Close()
$stream.Close()
$zip.Dispose()
$xmlStr -replace '<[^>]+>', ' ' -replace '\s+', ' '
