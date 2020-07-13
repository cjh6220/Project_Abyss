using System;
using System.IO;
using System.Text;
using System.Data;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.CodeDom;
using Microsoft.CSharp;

namespace Excel2Binary
{
    class Program
    {
        //added JJ
        static List<string> nameList = new List<string>();
        static string fileName;
        static string dirName;

        static void Main(string[] args)
        {                        
            string strSrc = args[0];//Path.GetFileNameWithoutExtension(args[0]);
            string strDest = args[1];

            //added JJ
            fileName = Path.GetFileNameWithoutExtension(strSrc).ToUpper();
            dirName = Path.GetDirectoryName(strDest);
                   
            // 0번째 라인 헤더 포함 여부
            bool skipHeader = true;            
            // 인덱서 사용 여부
            bool useSaveIndexer = false;
            // 암호화 키 세팅
            string strKey = null;
            
            // arg[0]: read exel file, arg[[1]: create binary file
            if (args.Length > 2){              
                // 헤더 사용여부
                if (args[2].Equals("-head"))
                    skipHeader = false;
            }
            if (args.Length > 3){
                // 인덱서 사용여부
                if (args[3].Equals("-indexer"))
                    useSaveIndexer = true;
            }
            if (args.Length > 4)
            {
                // 암호화 키값
                strKey = args[3];
            }

#if DEBUG
            Console.WriteLine("\n----------명령줄 인수----------\n");
            for (int i = 0; i < args.Length; i++) Console.WriteLine(args[i]);
#endif
            
            string strFileName = Path.GetFileNameWithoutExtension(strDest);

            // Delete the file if it exists.
            if (File.Exists(strDest))
            {
                File.Delete(strDest);
            }

            // excel -> dataset
            DataSet ds = LiAsExcelDB.OpenExcelDB(strSrc, skipHeader);
            
            // dataset -> excel
            //LiAsExcelDB.SaveExcelDB("C:\\Temp_save.dat",DS);

            // file write
            saveBinary(strDest, ds, skipHeader, strKey);

            Console.WriteLine("Create File - [" + strFileName + "] is Done.....");

            // use Indexer
            if (useSaveIndexer)
            {
                //SaveIndexer(nameList);
            }
        }

        private static void saveBinary(string fileName, DataSet dataSet, bool skipHeader, string strKey)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
           
            int checkCnt, rowCnt, columnCnt;
            int tableCnt = dataSet.Tables.Count;

            // init value
            checkCnt = rowCnt = columnCnt = 0;

            string columnData = "";
            
            //Create the file.
            try
            {
                // For each table in the DataSet, print the row values.
                foreach (DataTable table in dataSet.Tables)
                {
                    // 데이터 시트 2개 이상일때 처리
                    if (checkCnt > 0) break;

                    rowCnt = table.Rows.Count;
                    columnCnt = table.Columns.Count;

                    // row / colunm 갯수 헤더값
                    if (!skipHeader)
                    {
                        bw.Write(rowCnt.ToString());
                        bw.Write(columnCnt.ToString());
                    }

                    //added JJ
                    int i = 0;
                    int line = 0;

                    foreach (DataRow row in table.Rows)
                    {
                        i = 0;
                        foreach (DataColumn column in table.Columns)
                        {
                            columnData = row[column].ToString().Trim('*');

                            //bw.Write(row[column].ToString().Length);
                            bw.Write(columnData);

                            //added JJ
                            if (i == 1 && line > 0)
                            {
                                nameList.Add(columnData);
                            }

                            i++;
#if DEBUG
                            //Console.WriteLine(row[column]);                            
#endif
                        }

                        line++;
                    }

                    checkCnt++;
                }

                // Dataset으로부터 xml 저장
                //ds.WriteXml(Path.GetFileNameWithoutExtension(strFileName) + ".xml");

#if DEBUG
                Console.WriteLine("\n엑셀 테이블 시트 갯수: " + tableCnt + "\n");
#endif
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                fs.Close();
                bw.Close();

                // Gabage Collect
                GC.Collect();
            }

            

#if DEBUG
            BinaryReader br = new BinaryReader(File.Open(fileName, FileMode.Open));

            try
            {
                int getRow, getCol, offset;
                string[] header;

                if (!skipHeader)
                {
                    getRow = Convert.ToInt32(br.ReadString()) - 1; // remove header row
                    getCol = Convert.ToInt32(br.ReadString());
                    offset = 0;

                    header = new string[getCol];
                    for (int k = 0; k < getCol; k++)
                    {
                        header[k] = br.ReadString();
                    }

                    Console.WriteLine("Row Count: " + getRow);
                    Console.WriteLine("Column Count: " + getCol);
                    for( int k = 0; k < getCol; k++ )
                        Console.WriteLine(header[k]);
                }
                else
                {
                    getRow = rowCnt;
                    getCol = columnCnt;
                    offset = 0;
                }

                for (int j = offset; j < getRow; j++)
                {
                    Console.WriteLine("-------colunm : " + j + "---------");

                    for (int i = 0; i < getCol; i++)
                    {
                        Console.WriteLine(br.ReadString());
                    }
                }
            }
            catch (EndOfStreamException e)
            {
                Console.WriteLine("{0} caught and ignored. " + "Using default values.", e.GetType().Name);
            }
            finally
            {
                br.Close();
            }
#endif
        }

        //added JJ
        static void SaveIndexer(List<string> indexerList)
        {
            string constVar = "";
            string caseStr = "";
            string temp = "";
            int i = 0;
            foreach (string name in nameList)
            {
                temp = name.ToUpper();   // 변수명
                constVar += "   public const string " + temp + " = \"" + name + "\";\n";   //상수 라인
                caseStr += "       case " + temp + ": return " + i + ";\n"; // case 문..
                i++;
            }

            string literalCode;
            literalCode = "\n" +
                "// It was auto generated...\n" +
                "// Do not modify this class!!!!!!!!!!!!!!!!!!!!!!!!!\n"+
                "public class " + fileName + "\n" +
                "{\n" +
                "   static " + fileName + " instance;\n" +
                "   public static " + fileName + " Instance {\n" +
		        "       get {\n"+
                "           if(instance == null) { instance =  new " + fileName + "(); }\n" +
			    "           return instance;\n"+
		        "       }\n"+
		        "	    set { instance = value; }\n"+
	            "   }\n\n"+

                constVar+

                "\n"+
                "   public int this[string propertyName] {\n" +
                "   get {\n" +
                "       switch(propertyName) {\n"+
                caseStr +
                "       default: throw new System.ArgumentException(propertyName);\n"+
                "       }\n"+
                "   }\n" +
                "   }\n" +
                "}";

            CodeSnippetCompileUnit csu = new CodeSnippetCompileUnit(literalCode);

            // Write the code to a file.
            using (var fileStream = new StreamWriter(File.Create(@dirName+"/"+@fileName+".cs")))
            {
                var provider = new CSharpCodeProvider();
                provider.GenerateCodeFromCompileUnit(csu, fileStream, null);
            }
        }

    }    
}
