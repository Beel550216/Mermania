using UnityEngine;

public class WalkState : BaseState<PlayerStateMachine.PlayerState>
{
    public WalkState(PlayerStateMachine.PlayerState key) : base(key)
    {
    }

    public override void EnterState()
    {
        Debug.Log("ENTERED WALK STATE");
    }
    public override void UpdateState()
    {
        Debug.Log("UPDATE WALK STATE");
    }
    public override void ExitState()
    {
        Debug.Log("EXIT WALK STATE");
    }

    public override PlayerStateMachine.PlayerState GetNextState()
    {
        Debug.Log("Getting next state");
        Debug.Log(StateKey);
        return StateKey;
    }

    public override PlayerStateMachine.PlayerState GetState()
    {
        //Debug.Log("Getting " + StateKey + " state");
        //Debug.Log("STATE: " + StateKey);
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
