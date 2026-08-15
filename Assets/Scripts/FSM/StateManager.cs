using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState,BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
    protected BaseState<EState> CurrentState;
    protected bool IsTransitioningState = false;

    public virtual void Start()
    {
        CurrentState.EnterState();
    }
    public virtual void Update()
    {
        EState nextStateKey = CurrentState.GetNextState();
        if(!IsTransitioningState && nextStateKey.Equals(CurrentState.StateKey))
        {
            CurrentState.UpdateState();
        }
        else if (!IsTransitioningState)
        {
            TransitionToState(nextStateKey);
        }
    }
    public void TransitionToState(EState stateKey) 
    {
        IsTransitioningState = true;
        CurrentState.ExitState();
        CurrentState = States[stateKey];
        CurrentState.EnterState();
        IsTransitioningState = false;
    }
}
