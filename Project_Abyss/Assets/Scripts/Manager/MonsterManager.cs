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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadMonsterTable()
    {
        m_MonsterStatData = Resources.Load("Table/MonsterStat") as MonsterStat;
    }
}
