using UnityEngine;

public class IdleState : BaseState<PlayerStateMachine.PlayerState>
{
    public IdleState(PlayerStateMachine.PlayerState key) : base(key)
    {

    }

    public override void EnterState()
    {
       
    }
    public override void ExitState()
    {
       
    }
    public override void UpdateState()
    {
       
    }
   /* public override void BaseState<PlayerStateMachine.PlayerState> GetNextState()   //EState, not void
    {
        return StateKey;
    }*/
   
    /*public override void OnTriggerEnter(Collider other)()
    {
       
    }
    public override void OnTriggerExit(Collider other)()
    {
       
    }
    public override void OnTriggerEnter(Collider other)()
    {

    }*/
}
