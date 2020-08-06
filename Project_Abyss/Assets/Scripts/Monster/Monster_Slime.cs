using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Slime : MonsterBase
{
    void Awake()
    {
        InitAwake();
        InitMonster("Slime");

        mStateMgr.AddFsm(new Fsm_Idle_Monster(this));
        Debug.Log("fsm 추가했음");

        mStateMgr.SetState(eMonster_State.eState_Idle);
        Debug.Log("fsm set 했음");

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mStateMgr.Update();
    }

    private void FixedUpdate()
    {
        mStateMgr.FixedUpdate();
    }

    public override void InitMonster(string name)
    {
        base.InitMonster(name);
    }
}
