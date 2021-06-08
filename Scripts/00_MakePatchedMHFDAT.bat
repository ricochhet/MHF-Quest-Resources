@echo off
call ./01_DecompMHFDAT.bat
call ./04_ApplyModsToMHFDAT.bat
call ./02_RecompMHFDAT.bat
call ./03_EncryptMHFDAT.bat