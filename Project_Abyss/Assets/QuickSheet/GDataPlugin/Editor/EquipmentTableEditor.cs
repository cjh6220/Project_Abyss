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
[CustomEditor(typeof(EquipmentTable))]
public class EquipmentTableEditor : BaseGoogleEditor<EquipmentTable>
{	    
    public override bool Load()
    {        
        EquipmentTable targetData = target as EquipmentTable;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<EquipmentTableData>(targetData.WorksheetName) ?? db.CreateTable<EquipmentTableData>(targetData.WorksheetName);
        
        List<EquipmentTableData> myDataList = new List<EquipmentTableData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            EquipmentTableData data = new EquipmentTableData();
            
            data = Cloner.DeepCopy<EquipmentTableData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
