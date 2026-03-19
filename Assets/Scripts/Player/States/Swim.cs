using UnityEngine;

public class SwimState : BaseState<PlayerStateMachine.PlayerState>
{
    public SwimState(PlayerStateMachine.PlayerState key) : base(key)
    {
    }

    public override void EnterState()
    {
        Debug.Log("ENTERED SWIM STATE");
    }
    public override void UpdateState()
    {
        Debug.Log("UPDATE SWIM STATE");
    }
    public override void ExitState()
    {
        Debug.Log("EXIT SWIM STATE");
    }

    public override PlayerStateMachine.PlayerState GetNextState()
    {
        Debug.Log("Getting next state");
        Debug.Log(StateKey);
        return StateKey;
    }

    public override void CheckForRun()
    {

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
