using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();

    //Dictionaries store values behind keys --> each state is being stored //similar to a list
    // BaseState = type //protected as only those that inherit will need to access this

    protected BaseState<EState> CurrentState;

    protected bool IsTransitioningState = false;
    
    void Start()
    {
        CurrentState.EnterState();
    }

    void Update()
    {
        EState nextStateKey = CurrentState.GetNextState();

        if (!IsTransitioningState && nextStateKey.Equals(CurrentState.StateKey))
        {
            CurrentState.UpdateState();
        }
        else if (!IsTransitioningState)  //transitions when IsTransitioningState == false
        {
            TransitionToState(nextStateKey);
        }


    }

    public void TransitionToState(EState stateKey)  //changes to the state (key) by searching for it in the dictionary
    {
        IsTransitioningState = true;               //avoids being called multiple times
        CurrentState.ExitState();
        CurrentState = States[stateKey];
        CurrentState.EnterState();
        IsTransitioningState = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        CurrentState.OnTriggerEnter(other);
    }

    private void OnTriggerStay(Collider other)
    {
        CurrentState.OnTriggerStay(other);
    }

    private void OnTriggetExit(Collider other)
    {
        CurrentState.OnTriggerExit(other);
    }


}
