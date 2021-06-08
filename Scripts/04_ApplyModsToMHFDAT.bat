@echo off
echo.
echo ========================================
echo APPLY MOD TO MHFDAT.BIN
echo PLEASE MAKE A BACKUP OF FILES BEFOREHAND
echo ========================================
echo.

set FrontierDataTools="C:\VSCode-Workspace\MHF-Resources\ReFrontier\ReFrontier-Debug\FrontierDataTool\bin\Debug\net4.7.2\FrontierDataTool.exe"
set MHFDATBIN="C:\VSCode-Workspace\MHF-Resources\Quest-Data\mhfdat.bin"

%FrontierDataTools% modshop %MHFDATBIN%

pause