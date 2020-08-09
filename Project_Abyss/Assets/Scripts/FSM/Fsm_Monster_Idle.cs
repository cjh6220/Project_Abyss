using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fsm_Idle_Monster : FsmState<eMonster_State>
{
    MonsterBase mMonster = null;
    Animator mAnimator = null;
    Rigidbody2D mRigidbody2D = null;
    BoxCollider2D mBoxCollider2D = null;
    float moveAniTime = 0;
    int curMoveVec = 0;
   
    public Fsm_Idle_Monster(MonsterBase monster) : base(eMonster_State.eState_Idle)
    {
        mMonster = monster;
        mAnimator = mMonster.getAnimator;
        mRigidbody2D = mMonster.getRigidbody;
        moveAniTime = mMonster.getFsmTime;
        mBoxCollider2D = mMonster.GetComponent<BoxCollider2D>();
    }
    public override void Enter()
    {
        ChangeMoveVec();
        base.Enter();
        Debug.Log("Fsm_Idle_Npc - Enter");
    }
    public override void Update()
    {
        //MoveMonster();

        base.Update();
    }
    public override void FixedUpdate()
    {
        MoveMonster();

        base.FixedUpdate();
    }
    public void Seraching_Target()
    {

    }

    void MoveMonster()
    {
        CheckObstacle();
        ChangeMoveVec();        
        mRigidbody2D.velocity = new Vector2(mMonster.getMoveSpd * curMoveVec, mRigidbody2D.velocity.y);
        
    }

    void ChangeMoveVec()
    {
        if(moveAniTime >= mMonster.getFsmTime)
        {
            switch (curMoveVec) //이동 방향을 랜덤으로 돌림
            {
                case -1:
                    curMoveVec = Random.Range(0, 2);
                    break;

                case 0:                    
                    while(true)
                    {
                        curMoveVec = Random.Range(-1, 2);
                        if (curMoveVec != 0)
                            break;
                    }
                    break;

                case 1:
                    curMoveVec = Random.Range(-1, 1);
                    break;

            }
            
            switch(curMoveVec) // 해당 방향에 맞도록 오브젝트 로테이션과 애니메이션을 바꿔줌
            {
                case -1:
                    mMonster.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    mAnimator.SetBool("IsMove", true);
                    break;

                case 0:
                    mAnimator.SetBool("IsMove", false);
                    break;

                case 1:
                    mMonster.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    mAnimator.SetBool("IsMove", true);
                    break;
            }
            moveAniTime = 0;
        }
        else
        {
            moveAniTime += Time.deltaTime;
        }
    }

    void CheckObstacle()
    {
        RaycastHit2D raySideHit;
        RaycastHit2D rayBottomHit;
        Vector3 forwardPos;
        switch (curMoveVec)
        {
            case -1:
                forwardPos = new Vector3(mRigidbody2D.position.x - 0.6f, mRigidbody2D.position.y);
                Debug.DrawRay(mRigidbody2D.position, Vector3.left * mBoxCollider2D.size.x * 0.6f, Color.red);
                Debug.DrawRay(forwardPos, Vector3.down * mBoxCollider2D.size.y, Color.red);
                raySideHit = Physics2D.Raycast(mRigidbody2D.position, Vector3.left, mBoxCollider2D.size.x * 0.6f, LayerMask.GetMask("Ground"));
                rayBottomHit = Physics2D.Raycast(forwardPos, Vector3.down, mBoxCollider2D.size.y, LayerMask.GetMask("Ground"));
                //RaycastHit2D rayHit = Physics2D.Raycast(mRigidbody2D.position, Vector3.left * mBoxCollider2D.size * 0.6f);
                if (raySideHit.collider != null || rayBottomHit.collider == null)
                {
                    //if(raySideHit.distance < mBoxCollider2D.size.x * 0.6f)
                    //{
                        Debug.Log("벽이다");
                        curMoveVec = Random.Range(0, 2);
                        if(curMoveVec == 1)
                        {
                            mMonster.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                            mAnimator.SetBool("IsMove", true);
                        }
                        else
                        {
                            mAnimator.SetBool("IsMove", false);
                        }   
                        moveAniTime = 0;
                    //}
                }
                //if(rayBottomHit.collider == null)
                //{
                //    Debug.Log("바닥없음");
                //}
                break;

            case 0:
                mAnimator.SetBool("IsMove", false);
                break;

            case 1:
                forwardPos = new Vector3(mRigidbody2D.position.x + 0.6f, mRigidbody2D.position.y);
                Debug.DrawRay(mRigidbody2D.position, Vector3.right * mBoxCollider2D.size.x * 0.6f, Color.red);
                Debug.DrawRay(forwardPos, Vector3.down * mBoxCollider2D.size.y, Color.red);
                raySideHit = Physics2D.Raycast(mRigidbody2D.position, Vector3.right, mBoxCollider2D.size.x * 0.6f, LayerMask.GetMask("Ground"));
                rayBottomHit = Physics2D.Raycast(forwardPos, Vector3.down, mBoxCollider2D.size.y, LayerMask.GetMask("Ground"));
                if (raySideHit.collider != null || rayBottomHit.collider == null)
                {
                    //if (raySideHit.distance < mBoxCollider2D.size.x * 0.6f)
                    //{
                        Debug.Log("벽이다");
                        curMoveVec = Random.Range(-1, 1);
                        if (curMoveVec == -1)
                        {
                            mMonster.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                            mAnimator.SetBool("IsMove", true);
                        }
                        else
                        {
                            mAnimator.SetBool("IsMove", false);
                        }
                        moveAniTime = 0;
                    //}
                }
                //if (rayBottomHit.collider == null)
                //{
                //    Debug.Log("바닥없음");
                //}
                break;
        }
        
    }

    public override void End()
    {
        Debug.Log("Fsm_Idle_Npc - End");
        base.End();
    }
}
