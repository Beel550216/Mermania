using UnityEngine;

public class PlayerStateMachine : StateManager<PlayerStateMachine.PlayerState>
{
    public enum PlayerState
    {
        IdleState,
        WalkState,
        RunState,
        SwimState,
        DeathState
    }

    private void Awake()
    {
        InitializeStates();                              //must add states to dictionary first (otherwise CurrentState wont have a state to access)
        CurrentState = States[PlayerState.IdleState];
        Debug.Log(States);
    }

    private void InitializeStates()
    {
        // Add states to dictionary
        States.Add(PlayerState.IdleState, new IdleState(PlayerState.IdleState));
        States.Add(PlayerState.RunState, new RunState(PlayerState.RunState));
        States.Add(PlayerState.WalkState, new WalkState(PlayerState.WalkState));
        States.Add(PlayerState.SwimState, new SwimState(PlayerState.SwimState));
        States.Add(PlayerState.DeathState, new DeathState(PlayerState.DeathState));
        CurrentState = States[PlayerState.IdleState]; //change this to Reset
    }
}
