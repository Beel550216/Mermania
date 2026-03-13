using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Build;
using UnityEngine;

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
