using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/MonsterStat")]
    public static void CreateMonsterStatAssetFile()
    {
        MonsterStat asset = CustomAssetUtility.CreateAsset<MonsterStat>();
        asset.SheetName = "AbyssMonsterTable";
        asset.WorksheetName = "MonsterStat";
        EditorUtility.SetDirty(asset);        
    }
    
}