using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    protected FsmClass<eMonster_State> mStateMgr = new FsmClass<eMonster_State>();

    [SerializeField] protected string mMonsterName;
    [SerializeField] protected int mMonsterHP;
    [SerializeField] protected int mMonsterIdx;
    [SerializeField] protected float mMoveSpd;
    [SerializeField] protected float mFsmTime;
    [SerializeField] protected Animator mAnimator = null;
    [SerializeField] protected Rigidbody2D mRigidbody = null;
    //[SerializeField] protected BoxCollider2D

    private void Awake()
    {
        InitAwake();
    }

    public virtual void InitMonster(string name)
    {
        MonsterStatData stat = MonsterManager.Instance.GetMonsterStat(name);

        mMonsterName = stat.Name;
        mMonsterHP = stat.HP;
        mMonsterIdx = stat.Idx;
        mMoveSpd = stat.Movespd;
        mFsmTime = stat.Fsmtime;

        SetState(eMonster_State.eState_Before_Spawn);
    }

    protected void InitAwake()
    {
        mAnimator = gameObject.GetComponent<Animator>();
        mRigidbody = gameObject.GetComponent<Rigidbody2D>();

        mStateMgr.AddFsm(new Fsm_BeforeSpawn(this));
    }

    public void SetState(eMonster_State estate)
    {
        mStateMgr.SetState(estate);
    }

    public Animator getAnimator
    {
        get { return mAnimator; }
    }

    public Rigidbody2D getRigidbody
    {
        get { return mRigidbody; }
    }

    public float getMoveSpd
    {
        get { return mMoveSpd; }
    }

    public float getFsmTime
    {
        get { return mFsmTime; }
    }
    private void FixedUpdate()
    {
        mStateMgr.FixedUpdate();
    }
}
