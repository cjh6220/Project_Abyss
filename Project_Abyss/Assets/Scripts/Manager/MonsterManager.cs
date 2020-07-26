using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    MonsterStat m_MonsterStatData;
    private static MonsterManager instance = null;

    public static MonsterManager Instance
    {
        get
        {
            if (null == instance)
                instance = new GameObject("MonsterManager").AddComponent<MonsterManager>();

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
        LoadMonsterTable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadMonsterTable()
    {
        if (m_MonsterStatData != null)
            return;

        m_MonsterStatData = Resources.Load("Table/MonsterStat") as MonsterStat;
    }

    public MonsterStatData GetMonsterStat(string name)
    {
        MonsterStatData stat = null;
        if(m_MonsterStatData == null)
        {
            LoadMonsterTable();
        }
        for(int i = 0; i < m_MonsterStatData.dataArray.Length; i ++)
        {
            if(m_MonsterStatData.dataArray[i].Name == name)
            {
                stat = m_MonsterStatData.dataArray[i];
                return stat;
            }
        }
        return stat;
    }
}
