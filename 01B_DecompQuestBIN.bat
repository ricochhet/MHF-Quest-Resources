@echo off
echo PLEASE MAKE A BACKUP OF FILES BEFOREHAND
REM set /p ReFrontier=ReFrontierExePath:
REM set /p QUESTBINPATH=QUESTBINPath:

set ReFrontier="C:\VSCode-Workspace\MHF-Resources\ReFrontier\ReFrontier-Debug\ReFrontier\bin\Debug\net4.7.2\ReFrontier.exe"
set QUESTBINPATH="C:\VSCode-Workspace\MHF-Resources\QuestData\quests\55799d0.bin"

%ReFrontier% %QUESTBINPATH% -log -close

pause