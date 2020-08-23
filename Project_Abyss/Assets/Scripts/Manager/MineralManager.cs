using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralManager : MonoBehaviour
{
    MineralTable m_MineralTableData;
    private static MineralManager instance = null;

    public static MineralManager Instance
    {
        get
        {
            if (null == instance)
                instance = new GameObject("MineralManager").AddComponent<MineralManager>();

            return instance;
        }
    }
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadMineralTable();
    }

    void LoadMineralTable()
    {
        if (m_MineralTableData != null)
            return;

        m_MineralTableData = Resources.Load("Table/MineralTable") as MineralTable;
    }

    public MineralTableData GetMineralData(string name)
    {
        MineralTableData data = null;
        if(m_MineralTableData == null)
        {
            LoadMineralTable();
        }
        for(int i = 0; i < m_MineralTableData.dataArray.Length; i++)
        {
            if (m_MineralTableData.dataArray[i].Name == name)
            {
                data = m_MineralTableData.dataArray[i];
                return data;
            }
        }
        return null;
    }
}
