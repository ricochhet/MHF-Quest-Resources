@echo off
echo PLEASE MAKE A BACKUP OF FILES BEFOREHAND
REM set /p ReFrontier=ReFrontierExePath:
REM set /p MHFDATBIN=MHFDATBINPath:

set ReFrontier="C:\VSCode-Workspace\MHF-Resources\ReFrontier\ReFrontier-Debug\ReFrontier\bin\Debug\net4.7.2\ReFrontier.exe"
set MHFDATBIN="C:\VSCode-Workspace\MHF-Resources\QuestData\mhfdat.bin"

%ReFrontier% %MHFDATBIN% -log -close

pause