@echo off
echo.
echo ========================================
echo DECOMPILE QUEST BIN
echo PLEASE MAKE A BACKUP OF FILES BEFOREHAND
echo ========================================
echo.

set ReFrontier="C:\VSCode-Workspace\MHF-Resources\ReFrontier\ReFrontier-Debug\ReFrontier\bin\Debug\net4.7.2\ReFrontier.exe"
set QUESTBINPATH="C:\VSCode-Workspace\MHF-Resources\Quest-Data\quests\21978d0.bin"

%ReFrontier% %QUESTBINPATH% -log -close

pause