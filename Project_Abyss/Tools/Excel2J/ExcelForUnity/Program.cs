using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using ExcelDataReader;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;

namespace ExcelForUnity
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        //[STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new Form1());
        //}
        static List<string> nameList = new List<string>();
        static string fileName;
        static string dirName;

        static FileStream fileStream;
        static DataSet result;
        static Dictionary<int, Type[,]> cellTypes;
        static string tablePath;//테이블의 위치 경로.

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                MessageBox.Show("args가 널");
                Console.WriteLine("args가 널");
                return;
            }

            string strSrc = args[0];
            string strDest = args[1];

            fileName = strSrc;
            dirName = strDest;
            tablePath = Path.GetDirectoryName(strDest);

            //MessageBox.Show("fileName : " + fileName + " / " + "dirName : " + dirName + " / tablePath : " + tablePath);

            string fName = string.Format("{0}/{1}", tablePath, fileName);
            try
            {
                fileStream = File.Open(fName, FileMode.Open, FileAccess.Read);
            }
            catch (Exception ex)
            {
                MessageBox.Show("예외 : " + ex.Message);
                return;
            }

            IExcelDataReader reader = ExcelReaderFactory.CreateReader(fileStream);
            result = reader.AsDataSet();

            cellTypes = new Dictionary<int, Type[,]>();

            for (int workSheetIndex = 0; workSheetIndex < reader.ResultsCount; workSheetIndex++)
            {
                Type[,] types = new Type[reader.RowCount, reader.FieldCount];
                int rowIndex = 0;
                while (reader.Read())
                {
                    for (int columnIndex = 0; columnIndex <= reader.FieldCount - 1; columnIndex++)
                    {
                        types[rowIndex, columnIndex] = reader.GetFieldType(columnIndex);
                    }
                    rowIndex++;
                }
                cellTypes.Add(workSheetIndex, types);
                reader.NextResult();
            }

            reader.Close();
            reader.Dispose();
            reader = null;

            //Convert....
            string TEXTJSON = "{\r\n";

            string comma;
            List<string> headers;
            string items = "";

            for (int tableIndex = 0; tableIndex < result.Tables.Count; tableIndex++)
            {
                TEXTJSON += "\t\"" + result.Tables[tableIndex].TableName + "\": [\r\n";

                var table = result.Tables[tableIndex];

                headers = new List<string>();

                for (int columnIndex = 0; columnIndex < table.Columns.Count; columnIndex++)
                {
                    headers.Add("\"" + table.Rows[0][columnIndex] + "\": ");
                }

                items = "";

                for (int rowIndex = 1; rowIndex < table.Rows.Count; rowIndex++)
                {
                    items += "\t\t{\r\n";
                    for (int columnIndex = 0; columnIndex < table.Columns.Count; columnIndex++)
                    {
                        comma = "";
                        if (columnIndex < table.Columns.Count - 1)
                        {
                            comma += ",";
                        }
                        string text = "\t\t\t" + headers[columnIndex] + GetFixedType(tableIndex, table, rowIndex, columnIndex) + comma + "\r\n";
                        items += text;
                    }
                    comma = "";
                    if (rowIndex < table.Rows.Count - 1)
                    {
                        comma += ",";
                    }
                    items += "\t\t}" + comma + "\r\n";
                }

                comma = "";
                if (tableIndex < result.Tables.Count - 1)
                {
                    comma += ",";
                }
                TEXTJSON += items + "\t]" + comma + "\r\n";
            }

            TEXTJSON += "}";

            string tableName = dirName;

            try
            {
                FileStream fs = new FileStream(tableName, FileMode.Create, FileAccess.Write);
                fs.Close();
                File.AppendAllText(tableName, Environment.NewLine + TEXTJSON);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        static string GetFixedType(int tableIndex, DataTable table, int rowIndex, int columnIndex)
        {
            string text = "";
            Type type = cellTypes[tableIndex][rowIndex, columnIndex];

            string target = "" + table.Rows[rowIndex][columnIndex];

            if (type == null) return "";

            if (type.Equals(typeof(System.String)))
            {
                if (target.Substring(target.Length - 1).Equals("f"))
                {
                    float v = 0;
                    if (float.TryParse(target.Substring(0, target.Length - 1), out v))
                        text = target.Substring(0, target.Length - 1);
                    else
                        text = "\"" + table.Rows[rowIndex][columnIndex] + "\"";
                }
                else
                    text = "\"" + table.Rows[rowIndex][columnIndex] + "\"";
            }
            if (type.Equals(typeof(System.Double)))
                text = "" + table.Rows[rowIndex][columnIndex];
            if (type.Equals(typeof(System.DateTime)))
                text = "" + table.Rows[rowIndex][columnIndex];

            return text;
        }
    }
}
