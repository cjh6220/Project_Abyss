using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/EquipmentTable")]
    public static void CreateEquipmentTableAssetFile()
    {
        EquipmentTable asset = CustomAssetUtility.CreateAsset<EquipmentTable>();
        asset.SheetName = "AbyssMonsterTable";
        asset.WorksheetName = "EquipmentTable";
        EditorUtility.SetDirty(asset);        
    }
    
}