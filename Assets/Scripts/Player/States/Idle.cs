using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Build;
using UnityEngine;
using static PlayerStateMachine;
//using static Unity.Cinemachine.InputAxisControllerBase<T>;

public class IdleState : BaseState<PlayerStateMachine.PlayerState>
{
    public IdleState(PlayerStateMachine.PlayerState key) : base(key)
    {
    }

    public override void EnterState()
    {
        Debug.Log("ENTERED IDLE STATE");
    }
    public override void UpdateState()
    {
        Debug.Log("UPDATE IDLE STATE");

        /*if (direction.magnitude >= 0.1f)
        {
            ExitState();
        }*/

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            Debug.Log("Walking");
            GetState();
            //ExitState();

        }
    }
    public override void ExitState()
    {
        Debug.Log("EXIT IDLE STATE");
    }

    public override PlayerStateMachine.PlayerState GetNextState()
    {
        Debug.Log("Getting next state");
        Debug.Log(StateKey);
        return StateKey;
    }

    public override PlayerStateMachine.PlayerState GetState()
    {
        //PlayerState.IdleState;
        //EState nextStateKey = CurrentState.GetNextState();
        Debug.Log("Getting " + StateKey + " state");
        Debug.Log("STATE: " + StateKey);
        return StateKey;
    }


    public override void OnTriggerEnter(Collider other)
    {
       
    }
    public override void OnTriggerStay(Collider other)
    {

    }
    public override void OnTriggerExit(Collider other)
    {
       
    }
}
