using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using GDataDB;
using GDataDB.Linq;

using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
[CustomEditor(typeof(MineralTable))]
public class MineralTableEditor : BaseGoogleEditor<MineralTable>
{	    
    public override bool Load()
    {        
        MineralTable targetData = target as MineralTable;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<MineralTableData>(targetData.WorksheetName) ?? db.CreateTable<MineralTableData>(targetData.WorksheetName);
        
        List<MineralTableData> myDataList = new List<MineralTableData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            MineralTableData data = new MineralTableData();
            
            data = Cloner.DeepCopy<MineralTableData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
