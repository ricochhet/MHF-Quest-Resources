@echo off
echo PLEASE MAKE A BACKUP OF FILES BEFOREHAND
set /p ReFrontier=ReFrontierExePath:
set /p MHFDATBIN=MHFDATBINPath:

%ReFrontier% %MHFDATBIN% -compress 4,100 -close
%ReFrontier% %MHFDATBIN% -encrypt -close

pause