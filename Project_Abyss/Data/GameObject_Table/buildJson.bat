
set PROJECT_DIR=%CD%
set TOOLS_DIR=%PROJECT_DIR%\Tools

set OUTPUT_DIR=%CD%
set EXCEL_2_BINARY=%TOOLS_DIR%\Excel2J\Release\ExcelForUnity.exe

%EXCEL_2_BINARY% ProductTable.xlsx %OUTPUT_DIR%\ProductTable.txt -head
%EXCEL_2_BINARY% BallTable.xlsx %OUTPUT_DIR%\BallTable.txt -head
%EXCEL_2_BINARY% StageTable.xlsx %OUTPUT_DIR%\StageTable.txt -head
%EXCEL_2_BINARY% PaymentTable.xlsx %OUTPUT_DIR%\PaymentTable.txt -head
%EXCEL_2_BINARY% GameTimeLimitTable.xlsx %OUTPUT_DIR%\GameTimeLimitTable.txt -head
%EXCEL_2_BINARY% PatternTable.xlsx %OUTPUT_DIR%\PatternTable.txt -head
%EXCEL_2_BINARY% SplitTable.xlsx %OUTPUT_DIR%\SplitTable.txt -head
%EXCEL_2_BINARY% SparePinTable.xlsx %OUTPUT_DIR%\SparePinTable.txt -head
%EXCEL_2_BINARY% ManagerModeTable.xlsx %OUTPUT_DIR%\ManagerModeTable.txt -head

pause