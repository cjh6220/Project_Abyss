using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fsm_BeforeSpawn : FsmState<eMonster_State>
{
    protected MonsterBase mMonster = null;
    public Fsm_BeforeSpawn(MonsterBase monster) : base(eMonster_State.eState_Before_Spawn)
    {
        mMonster = monster;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
