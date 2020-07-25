using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public const int MONSTER_TABLE = 0;
    public const int LAST_TABLE = 1;

    public const int SHEET_TOTAL_COUNT = LAST_TABLE;

    private static string[] tableName =
    {
        "MonsterTable"
    };

    private static DataSheet[] arrDataSheet = new DataSheet[LAST_TABLE];

    public static bool isLoadedAllData = false;
    [SerializeField] private MonsterStat m_StatData;    
    // Start is called before the first frame update
    void Start()
    {
        GetTable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region ResMgr Singleton
    private static TableManager instance = null;
    public static TableManager Instance
    {
        get
        {
            if (null == instance)
                instance = new GameObject("TableManager").AddComponent<TableManager>();

            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }
    void OnApplicationQuit()
    {
        instance = null;
    }

    void OnDestroy()
    {
        instance = null;
    }
    #endregion

    public void Init()
    {

    }

    public virtual void InitDataSheet()
    {

    }

    //public void LoadAllTable()
    //{
    //    if (isLoadedAllData) return;

    //    string path = "";
    //    path = "Table/";

    //    for(int i = 0; i < SHEET_TOTAL_COUNT; i++)
    //    {
    //        if (null == arrDataSheet[i])
    //            arrDataSheet[i] = new DataSheet();

    //        LoadDataSheet(path + tableName[i], tableName[i], arrDataSheet[i]);
    //    }

    //    isLoadedAllData = true;

    //    Debug.Log("[TableManager] LoadAllDataTable Complete !!");
    //}

    //public static void LoadDataSheet(string filePath, string sheetName, DataSheet ds)
    //{
    //    //string path = filePath;

    //    //Object prefabobj = null;
        
    //}
    
    public void GetTable()
    {
        //string path = "Table/" + tableName.ToString();
        //m_StatData = Resources.Load(path) as MonsterStat;
        //Debug.Log("m_StatData 길이 =" + m_StatData.dataArray.Length);

    }
}
public class DataSheet
{
    public string sheetName;
    public int rowCount;
    public int columCount;

    public Dictionary<string, Dictionary<string, string>> dataTable = new Dictionary<string, Dictionary<string, string>>();

    public void Clear()
    {
        sheetName = null;
        rowCount = columCount = 0;

        dataTable.Clear();
    }
}
