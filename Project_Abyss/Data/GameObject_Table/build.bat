
set PROJECT_DIR=%CD%\..\..
set TOOLS_DIR=%PROJECT_DIR%\Tools

set OUTPUT_DIR=%CD%\output
set EXCEL_2_BINARY=%TOOLS_DIR%\Excel2Binary\Release\Excel2Binary.exe

rd /s /q %OUTPUT_DIR%
md %OUTPUT_DIR%

%EXCEL_2_BINARY% CharTable.xlsx %OUTPUT_DIR%\CharTable.bytes -head -indexer
%EXCEL_2_BINARY% ItemTable.xlsx %OUTPUT_DIR%\ItemTable.bytes -head
%EXCEL_2_BINARY% DummySensorTable.xlsx %OUTPUT_DIR%\DummySensorTable.bytes -head
%EXCEL_2_BINARY% PinInfoTable.xlsx %OUTPUT_DIR%\PinInfoTable.bytes -head
%EXCEL_2_BINARY% SoundTable.xlsx %OUTPUT_DIR%\SoundTable.bytes -head
%EXCEL_2_BINARY% LineWidthTable.xlsx %OUTPUT_DIR%\LineWidthTable.bytes -head
%EXCEL_2_BINARY% StageTable.xlsx %OUTPUT_DIR%\StageTable.bytes -head
%EXCEL_2_BINARY% BallTable.xlsx %OUTPUT_DIR%\BallTable.bytes -head
%EXCEL_2_BINARY% TutorialTable.xlsx %OUTPUT_DIR%\TutorialTable.bytes -head
%EXCEL_2_BINARY% ZoneTable.xlsx %OUTPUT_DIR%\ZoneTable.bytes -head
%EXCEL_2_BINARY% PatternTable.xlsx %OUTPUT_DIR%\PatternTable.bytes -head
%EXCEL_2_BINARY% UITable.xlsx %OUTPUT_DIR%\UITable.bytes -head
%EXCEL_2_BINARY% SplitTable.xlsx %OUTPUT_DIR%\SplitTable.bytes -head
%EXCEL_2_BINARY% BoardTable.xlsx %OUTPUT_DIR%\BoardTable.bytes -head
%EXCEL_2_BINARY% SparePinTable.xlsx %OUTPUT_DIR%\SparePinTable.bytes -head
%EXCEL_2_BINARY% ModeTable.xlsx %OUTPUT_DIR%\ModeTable.bytes -head
%EXCEL_2_BINARY% EmotionTable.xlsx %OUTPUT_DIR%\EmotionTable.bytes -head
%EXCEL_2_BINARY% EmotionMenuTable.xlsx %OUTPUT_DIR%\EmotionMenuTable.bytes -head
%EXCEL_2_BINARY% ColorPinTable.xlsx %OUTPUT_DIR%\ColorPinTable.bytes -head
%EXCEL_2_BINARY% RPMTable.xlsx %OUTPUT_DIR%\RPMTable.bytes -head
%EXCEL_2_BINARY% AdBoardTable.xlsx %OUTPUT_DIR%\AdBoardTable.bytes -head
%EXCEL_2_BINARY% GuideTable.xlsx %OUTPUT_DIR%\GuideTable.bytes -head
%EXCEL_2_BINARY% ObstacleTable.xlsx %OUTPUT_DIR%\ObstacleTable.bytes -head
%EXCEL_2_BINARY% ObsPropertyTable.xlsx %OUTPUT_DIR%\ObsPropertyTable.bytes -head
%EXCEL_2_BINARY% ObsInfoTable.xlsx %OUTPUT_DIR%\ObsInfoTable.bytes -head
%EXCEL_2_BINARY% ObsTutorialTable.xlsx %OUTPUT_DIR%\ObsTutorialTable.bytes -head
%EXCEL_2_BINARY% RouletteModeTable.xlsx %OUTPUT_DIR%\RouletteModeTable.bytes -head
%EXCEL_2_BINARY% RoulettePerTable.xlsx %OUTPUT_DIR%\RoulettePerTable.bytes -head
%EXCEL_2_BINARY% KioskInfoItemTable.xlsx %OUTPUT_DIR%\KioskInfoItemTable.bytes -head
%EXCEL_2_BINARY% KioskMenuItemTable.xlsx %OUTPUT_DIR%\KioskMenuItemTable.bytes -head
%EXCEL_2_BINARY% EventObjTable.xlsx %OUTPUT_DIR%\EventObjTable.bytes -head
%EXCEL_2_BINARY% BallInfoTable.xlsx %OUTPUT_DIR%\BallInfoTable.bytes -head
%EXCEL_2_BINARY% ParticleTable.xlsx %OUTPUT_DIR%\ParticleTable.bytes -head
%EXCEL_2_BINARY% ScoreBoardTable.xlsx %OUTPUT_DIR%\ScoreBoardTable.bytes -head
%EXCEL_2_BINARY% ScoreBoardInfoTable.xlsx %OUTPUT_DIR%\ScoreBoardInfoTable.bytes -head
%EXCEL_2_BINARY% PinTable.xlsx %OUTPUT_DIR%\PinTable.bytes -head
%EXCEL_2_BINARY% ReplayCameraTable.xlsx %OUTPUT_DIR%\ReplayCameraTable.bytes -head


cd %OUTPUT_DIR%

pause