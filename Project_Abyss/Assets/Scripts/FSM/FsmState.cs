using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class FsmState<TFsm_Type>
{
    private TFsm_Type mstateType;

    public FsmState(TFsm_Type _stateIdx)
    {
        mstateType = _stateIdx;
    }

    public TFsm_Type getsateType
    {
        get
        {
            return mstateType;
        }
    }
    //가상함수에요 
    #region - virtual
    public virtual void Enter()
    {

    }

    public virtual void Update()
    {

    }
    public virtual void FixedUpdate()
    {

    }

    public virtual void End()
    {

    }

    public virtual void DrawGizmo()
    {
    }

    #endregion

}
