@ECHO OFF

IF [%1] EQU [] GOTO failed

nuget pack Aubio.NET.Desktop.nuspec -Version %1
nuget pack Aubio.NET.Desktop.Native.nuspec -Version %1

nuget pack Aubio.NET.UWP.nuspec -Version %1
nuget pack Aubio.NET.UWP.Native.nuspec -Version %1

GOTO :eof

:failed
ECHO Syntax: %~nx0 ^<version^>