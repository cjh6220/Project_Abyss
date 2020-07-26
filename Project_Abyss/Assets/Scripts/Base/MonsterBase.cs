using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    protected FsmClass<eMonster_State> mStateMgr = new FsmClass<eMonster_State>();

    [SerializeField]protected string mMonsterName;
    [SerializeField] protected int mMonsterHP;
    [SerializeField] protected int mMonsterIdx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void InitMonster(string name)
    {
        MonsterStatData stat = MonsterManager.Instance.GetMonsterStat(name);

        mMonsterName = stat.Name;
        mMonsterHP = stat.HP;
        mMonsterIdx = stat.Idx;
    }
}
