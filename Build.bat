@echo off

rem Needed to update variable in loop
setlocal enabledelayedexpansion

for %%i in (.\bin\AnyCPU\*.dll) do (set files=!files!%%~i )

echo Merging CHAOS .Net SDK

tools\ILMerge\ILMerge.exe /out:build\AnyCPU\CHAOS.Portal.Client.dll /lib:C:\Windows\Microsoft.NET\Framework64\v4.0.30319 /targetplatform:v4,C:\Windows\Microsoft.NET\Framework64\v4.0.30319 /lib:lib\AnyCPU\ %files%

echo Done