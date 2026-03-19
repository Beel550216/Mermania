using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();  //e.g. idlestate (enum), BaseState<IdleState> (gets the seperate state script)

    //Dictionaries store values behind keys --> each state is being stored //similar to a list
    // BaseState = type //protected as only those that inherit will need to access this

    public BaseState<EState> CurrentState;

    protected bool IsTransitioningState = false;

    void Start()
    {
        CurrentState.EnterState();
    }

    void Update()
    {
        EState nextStateKey = CurrentState.GetNextState(); //change to GetState()

        CurrentState.CheckForRun();



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

    public void CheckForRun()
    {
        //TransitionToState(PlayerState.WalkState);
        Debug.Log("CHECKING RUN");//nextStateKey = WalkState;
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            if (direction.magnitude >= 0.1f) // key held down
            {
               // CurrentState = States[WalkState];
                return;
            }

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
