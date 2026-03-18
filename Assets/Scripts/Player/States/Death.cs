using UnityEngine;

public class DeathState : BaseState<PlayerStateMachine.PlayerState>
{
    public DeathState(PlayerStateMachine.PlayerState key) : base(key)
    {
    }

    public override void EnterState()
    {
        //currentPlayerModel = playerModelPrefab[2];  //2 = 3

        Debug.Log("ENTERED DEATH STATE");
    }
    public override void UpdateState()
    {
        Debug.Log("UPDATE DEATH STATE");
    }
    public override void ExitState()
    {
        Debug.Log("EXIT DEATH STATE");
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
