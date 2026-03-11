using UnityEngine;

public class PlayerStateMachine : StateManager<PlayerStateMachine.PlayerState>
{
    public enum PlayerState
    {
        Idle,
        Walk,
        Run,
        Swim,
        Die
    }

    private void Awake()
    {
        CurrentState = States[PlayerState.Idle];
    }
}
