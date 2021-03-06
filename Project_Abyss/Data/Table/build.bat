@echo off

set PROJECT_DIR=%CD%\..\..
set TOOLS_DIR=%PROJECT_DIR%\Tools

set OUTPUT_DIR=%CD%\output
set EXCEL_2_BINARY=%TOOLS_DIR%\Excel2Binary\Release\Excel2Binary.exe

rd /s /q %OUTPUT_DIR%
md %OUTPUT_DIR%

%EXCEL_2_BINARY% MonsterTable.xlsx %OUTPUT_DIR%\MonsterTable.bytes -head


cd %OUTPUT_DIR%
copy *.bytes %PROJECT_DIR%\Assets\Resources\Table\*.bytes

pause