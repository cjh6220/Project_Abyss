
[ 2013/01/18 ]
 - "-head" 헤더 추가 인자 변경
 - "-indexer" 인덱서 사용여부로 변경

[ 2012/12/05 ]

 - "useheader" 인자 추가
 - 첫줄 필드명을 데이터에 포함할지 여부


[ 2012/10/01 ]

 - 0번째 시트에 대해서 바이너리 데이터 생성

 - 셀의 데이터를 String타입으로 저장

 - Excel 2003~2010 호환

 - 셀의 제일 첫줄은 필드명으로 처리하여 읽지 않음

예) 
 - Excel2Binary 엑셀파일 경로 / Output파일 경로 / -head(첫줄 필드명 포함여부) / -indexer(인덱서 사용여부) / 암호화 키값(현재 사용안함)

 - Excel2Binary test.xlsx test.txt 
 - Excel2Binary test.xlsx test.dat -head
 - Excel2Binary test.xlsx test.dat -head -indexer
 - Excel2Binary test.xlsx test.fuck useheader @1rfajsahrrasr

