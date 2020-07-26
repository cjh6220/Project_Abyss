using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FsmClass<TFsm_Type>
{
    protected SortedDictionary<TFsm_Type, FsmState<TFsm_Type>> mfsmStateList = new SortedDictionary<TFsm_Type, FsmState<TFsm_Type>>();

    protected FsmState<TFsm_Type> mcurState = null;
    protected FsmState<TFsm_Type> mnextState = null;
    protected FsmState<TFsm_Type> mpreState = null;

    #region - get

    public FsmState<TFsm_Type> getCurstate
    {
        get
        {
            return mcurState;
        }
    }

    public TFsm_Type getcurStateType
    {
        get
        {
            if (null == mcurState)
                return default(TFsm_Type);

            return mcurState.getsateType;
        }
    }

    public FsmState<TFsm_Type> getNextstate
    {
        get
        {
            return mnextState;
        }
    }

    public TFsm_Type getNextStateType
    {
        get
        {
            if (null == mnextState)
                return default(TFsm_Type);

            return mnextState.getsateType;
        }
    }
    #endregion

    public virtual void AddFsm(FsmState<TFsm_Type> _state)
    {   
        if (mfsmStateList.ContainsKey(_state.getsateType))
        {
            Debug.LogError("FsmClass::AddFsm  exist state key " + _state.getsateType);
            return;
        }

        mfsmStateList.Add(_state.getsateType, _state);
    }

    public virtual void SetState(TFsm_Type _state)
    {
#if UNITY_EDITOR
        if (false == mfsmStateList.ContainsKey(_state))
        {
            Debug.LogError("FsmClass::SetState  Can't find state " + _state);
        }
#endif
        mnextState = mfsmStateList[_state];
    }

    public virtual void Update()
    {
        if (mnextState != null)
        {
            if (mcurState != null)
                mcurState.End();

            mcurState = mnextState;
            mcurState.Enter();
            mnextState = null;
        }
        if (null != mcurState)
            mcurState.Update();
    }

    public virtual void FixedUpdate()
    {
        if (null != mcurState)
        {
            mcurState.FixedUpdate();
        }
    }
}
