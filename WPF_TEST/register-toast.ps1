# WPF 앱을 Windows Toast 알림 시스템에 등록하는 PowerShell 스크립트
# 관리자 권한으로 실행하세요

$appPath = "C:\Sources\WPF_TEST\WPF_TEST\bin\Debug\net10.0-windows10.0.26100.0\win-x64\WPF_TEST.exe"
$appName = "WPF_TEST"
$aumid = "WPF_TEST.$appName"

# 레지스트리에 앱 등록
$registryPath = "HKCU:\SOFTWARE\Classes\AppUserModelId\$aumid"
New-Item -Path $registryPath -Force | Out-Null
Set-ItemProperty -Path $registryPath -Name "DisplayName" -Value $appName
Set-ItemProperty -Path $registryPath -Name "IconUri" -Value $appPath

Write-Host "앱이 레지스트리에 등록되었습니다: $aumid"
Write-Host "이제 Windows 설정 > 시스템 > 알림에서 '$appName' 앱을 찾아 알림을 활성화하세요."

