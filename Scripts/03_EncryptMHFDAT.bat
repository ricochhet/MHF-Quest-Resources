@echo off
echo.
echo ========================================
echo ENCRYPT MHFDAT.BIN
echo PLEASE MAKE A BACKUP OF FILES BEFOREHAND
echo ========================================
echo.

set ReFrontier="C:\VSCode-Workspace\MHF-Resources\ReFrontier\ReFrontier-Debug\ReFrontier\bin\Debug\net4.7.2\ReFrontier.exe"
set MHFDATMETA="C:\VSCode-Workspace\MHF-Resources\Quest-Data\mhfdat.bin.meta"
set OUTPUTPATH="C:\VSCode-Workspace\MHF-Resources\Scripts\output\"
set MHFDATBIN="C:\VSCode-Workspace\MHF-Resources\Scripts\output\mhfdat.bin"

copy %MHFDATMETA% %OUTPUTPATH%

%ReFrontier% %MHFDATBIN% -encrypt -close

pause