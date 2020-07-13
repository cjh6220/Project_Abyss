

set PROJECT_DIR=%CD%\..\..
set TOOLS_DIR=%PROJECT_DIR%\Tools

set OUTPUT_DIR=%CD%
set EXCEL_2_BINARY=%TOOLS_DIR%\Excel2J\Release\ExcelForUnity.exe

%EXCEL_2_BINARY% MonsterTable.xlsx %OUTPUT_DIR%\MonsterTable.txt -head

pause