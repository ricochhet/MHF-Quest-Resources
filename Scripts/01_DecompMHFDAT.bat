@echo off
echo.
echo ========================================
echo DECOMPILE MHFDAT.BIN
echo PLEASE MAKE A BACKUP OF FILES BEFOREHAND
echo ========================================
echo.

set ReFrontier="C:\VSCode-Workspace\MHF-Resources\ReFrontier\ReFrontier-Debug\ReFrontier\bin\Debug\net4.7.2\ReFrontier.exe"
set MHFDATBIN="C:\VSCode-Workspace\MHF-Resources\Quest-Data\mhfdat.bin"

%ReFrontier% %MHFDATBIN% -log -close

pause