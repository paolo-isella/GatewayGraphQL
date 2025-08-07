# Naviga nella cartella Accounts ed esegue i comandi
Write-Host "`n➡️ Eseguo nella cartella Accounts..."
Push-Location ../Accounts

dotnet run -- schema export --output schema.graphql
fusion subgraph pack

Pop-Location

# Naviga nella cartella Reviews ed esegue i comandi
Write-Host "`n➡️ Eseguo nella cartella Reviews..."
Push-Location ../Reviews

dotnet run -- schema export --output schema.graphql
fusion subgraph pack

Pop-Location

# Torna alla cartella iniziale e esegue il compose
Write-Host "`n➡️ Eseguo il compose finale nella cartella corrente..."

fusion compose -p gateway.fgp -s ..\Accounts\
fusion compose -p gateway.fgp -s ..\Reviews\

Write-Host "`n✅ Fatto."