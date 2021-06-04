@echo off
echo PLEASE MAKE A BACKUP OF FILES BEFOREHAND
REM set /p FrontierDataTools=FrontierDataToolsExePath:
REM set /p MHFDATBIN=MHFDATBINPath:

set FrontierDataTools="C:\VSCode-Workspace\MHF-Resources\ReFrontier\ReFrontier-Debug\FrontierDataTool\bin\Debug\net4.7.2\FrontierDataTool.exe"
set MHFDATBIN="C:\VSCode-Workspace\MHF-Resources\QuestData\mhfdat.bin"

%FrontierDataTools% modshop %MHFDATBIN%

pause