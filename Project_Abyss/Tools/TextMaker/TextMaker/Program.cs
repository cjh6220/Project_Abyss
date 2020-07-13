using System;
using System.IO;
using System.Text;
using System.Data;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.CodeDom;
using Microsoft.CSharp;

namespace TextMaker
{
    class Program
    {
        // file name
        static List<string> targetFiles = new List<string>();
        // Table ID(Key)
        static List<string> textKeys = new List<string>();
        static Dictionary<string, int> verifyKeys = new Dictionary<string, int>();
        
        // Table Values
        static List<List<string>> textStrings = new List<List<string>>();                
        
        // Multi-language File Count
        static int fileCnt = 0;

        // Row Key Count
        static int stringRowCount = 0;
        // Verify flag
        static bool isUsedSameKey = false;

        static void Main(string[] args)
        {
#if DEBUG
            Console.WriteLine("\n----------명령줄 인수----------\n");
            for (int i = 0; i < args.Length; i++) Console.WriteLine(args[i]);
#endif

            string strSrc = args[0];

            fileCnt = args.Length - 1;

            // Save Language File Name         
            for (int i = 0; i < fileCnt; i++)
            {
                targetFiles.Add( Path.GetFileNameWithoutExtension(args[i + 1]) );

            }
            //Path.GetFileNameWithoutExtension(strDest);

            // excel -> dataset
            DataSet ds = LiAsExcelDB.OpenExcelDB(strSrc, true);

            // Set List Data
            loadExelData(ds);

            // Verifing...
            if (isUsedSameKey) return;

            // file write
            for (int i = 0; i < fileCnt; i++)
            {
                saveBinary(args[i + 1], textStrings[i]);
            }
        }

        private static void loadExelData(DataSet dataSet)
        {
            int sheetCnt = 0;
            int nextColum = 0;

            string tempKey = "";

            // Initialize List
            textKeys.Clear();
            textStrings.Clear();

            for (int i = 0; i < fileCnt; i++)
            {
                List<string> list = new List<string>();
                textStrings.Add(list);
            }

            try
            {             
                // For each table in the DataSet, print the row values.
                foreach (DataTable table in dataSet.Tables)
                {
                    // 데이터 시트 2개 이상일때 처리
                    //if (sheetCnt > 0) break;

                    nextColum = 0;                    
                    foreach (DataColumn column in table.Columns)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            if (nextColum > fileCnt) break;

                            if (nextColum < 1)
                            {
                                tempKey = row[column].ToString();

                                textKeys.Add(tempKey);

                                if (verifyKeys.ContainsKey(tempKey))
                                {                                    
                                    throw new ArgumentException("An element with Key < " + tempKey + " > already exists.\n");                                    
                                }                                
                                verifyKeys.Add(tempKey, stringRowCount++);  
                            }
                            else
                            {
                                textStrings[nextColum - 1].Add(row[column].ToString());
                            }
                        }
                        nextColum++;
                    }
                    sheetCnt++;

                    Console.WriteLine("----------------\n sheet name: " + table.TableName);                      
                }
                Console.WriteLine("\n---------------------\n Sheet Count:" + sheetCnt);   
            }
            catch (Exception e)
            {                
                Console.WriteLine(e.ToString());

                isUsedSameKey = true;           
            }
#if DEBUG
            //Console.WriteLine("\n----------------\n 시트 갯수 : " + sheetCnt + "\n---------------------");            

            /*           
            Console.WriteLine("\n----------keys----------\n");
            for(int i = 0; i < textKeys.Count; i++)
                Console.WriteLine(textKeys[i]);

            Console.WriteLine("\n----------values----------\n");
            foreach (List<string> list in textStrings)
            {
                foreach(string str in list)
                    Console.WriteLine(str);

                Console.WriteLine("\n");
            }
            */
#endif
        }

        private static void saveBinary(string fileName, List<string> list)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            //Create the file.
            try
            {
                int rowCnt = textKeys.Count;

                // row count
                bw.Write(rowCnt.ToString());
                // row data
                for (int i = 0; i < rowCnt; i++)
                {                  
                    bw.Write(textKeys[i].ToString());
                    bw.Write(list[i].ToString());
                }
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
            Console.WriteLine("\n----------DEBUG----------\n");

            BinaryReader br = new BinaryReader(File.Open(fileName, FileMode.Open));

            try
            {
                int testRow = Convert.ToInt32(br.ReadString()) * 2;

                Console.WriteLine("Row:: " + testRow);

                for (int i = 0; i < testRow; i++)
                {
                    Console.WriteLine(br.ReadString());
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
    }    
}
