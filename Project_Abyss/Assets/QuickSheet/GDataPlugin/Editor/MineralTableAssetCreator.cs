using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/MineralTable")]
    public static void CreateMineralTableAssetFile()
    {
        MineralTable asset = CustomAssetUtility.CreateAsset<MineralTable>();
        asset.SheetName = "AbyssMonsterTable";
        asset.WorksheetName = "MineralTable";
        EditorUtility.SetDirty(asset);        
    }
    
}