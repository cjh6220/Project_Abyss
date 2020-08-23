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
[CustomEditor(typeof(ParticleTable))]
public class ParticleTableEditor : BaseGoogleEditor<ParticleTable>
{	    
    public override bool Load()
    {        
        ParticleTable targetData = target as ParticleTable;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<ParticleTableData>(targetData.WorksheetName) ?? db.CreateTable<ParticleTableData>(targetData.WorksheetName);
        
        List<ParticleTableData> myDataList = new List<ParticleTableData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            ParticleTableData data = new ParticleTableData();
            
            data = Cloner.DeepCopy<ParticleTableData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
