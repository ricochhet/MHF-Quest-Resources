@echo off
echo PLEASE MAKE A BACKUP OF FILES BEFOREHAND
set /p FrontierDataTools=FrontierDataToolsExePath:
set /p MHFDATBIN=MHFDATBINPath:

%FrontierDataTools% modshop %MHFDATBIN%

pause