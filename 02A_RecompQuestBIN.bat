@echo off
echo PLEASE MAKE A BACKUP OF FILES BEFOREHAND
set /p ReFrontier=ReFrontierExePath:
set /p QUESTBINPATH=QUESTBINPath:

%ReFrontier% %QUESTBINPATH% -compress 3,100 -close
%ReFrontier% %QUESTBINPATH% -encrypt -close

pause