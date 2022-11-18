$psCred = Get-StoredCredential -Target "192.168.147.51"
Invoke-Command -ComputerName 192.168.147.51 -Credential $psCred -ScriptBlock {Stop-Website -Name 86port}

dotnet publish -c Release -o publish
$session = New-PSSession -ComputerName 192.168.147.51 -Credential $psCred
Copy-Item -Path "publish\*" -ToSession $session -Destination "C:\Users\su\Desktop\86port" -Recurse -Force

Invoke-Command -ComputerName 192.168.147.51 -Credential $psCred -ScriptBlock {Start-Website -Name 86port}
$shell = New-Object -ComObject Wscript.Shell
$shell.popup("Ура все получилось",0,"Результат" , 64)