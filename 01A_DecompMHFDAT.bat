@echo off
echo PLEASE MAKE A BACKUP OF FILES BEFOREHAND
set /p ReFrontier=ReFrontierExePath:
set /p MHFDATBIN=MHFDATBINPath:

%ReFrontier% %MHFDATBIN% -log -close

pause