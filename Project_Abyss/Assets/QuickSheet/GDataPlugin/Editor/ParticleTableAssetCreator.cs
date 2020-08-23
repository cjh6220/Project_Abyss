using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/ParticleTable")]
    public static void CreateParticleTableAssetFile()
    {
        ParticleTable asset = CustomAssetUtility.CreateAsset<ParticleTable>();
        asset.SheetName = "AbyssMonsterTable";
        asset.WorksheetName = "ParticleTable";
        EditorUtility.SetDirty(asset);        
    }
    
}