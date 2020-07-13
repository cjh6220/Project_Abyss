@echo off

set PROJECT_DIR=%CD%\..\..
set TOOLS_DIR=%PROJECT_DIR%\Tools
set VERIFY_DATA=%TOOLS_DIR%\verifyData\release\verifyData.exe

%VERIFY_DATA% %PROJECT_DIR%\Assets\Resources\Table

pause