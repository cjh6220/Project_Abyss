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
[CustomEditor(typeof(MonsterStat))]
public class MonsterStatEditor : BaseGoogleEditor<MonsterStat>
{	    
    public override bool Load()
    {        
        MonsterStat targetData = target as MonsterStat;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<MonsterStatData>(targetData.WorksheetName) ?? db.CreateTable<MonsterStatData>(targetData.WorksheetName);
        
        List<MonsterStatData> myDataList = new List<MonsterStatData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            MonsterStatData data = new MonsterStatData();
            
            data = Cloner.DeepCopy<MonsterStatData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
