@echo off

set PROJECT_DIR=%CD%\..\..
set TOOLS_DIR=%PROJECT_DIR%\Tools

set OUTPUT_DIR=%CD%
set EXCEL_2_BINARY=%TOOLS_DIR%\Excel2J\Release\ExcelForUnity.exe

copy ProductTable.txt + BallTable.txt + StageTable.txt + PaymentTable.txt + GameTimeLimitTable.txt + PatternTable.txt + SplitTable.txt + SparePinTable.txt + ManagerModeTable.txt AllTable.txt

pause