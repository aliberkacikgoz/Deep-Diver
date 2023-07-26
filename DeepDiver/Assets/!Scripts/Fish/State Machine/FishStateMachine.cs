using UnityEngine;

public class FishStateMachine
{
    public FishState CurrentFishState;

    public void Initialize(FishState startingState)
    {
        CurrentFishState = startingState;
        CurrentFishState.EnterState();
    }

    public void ChangeState(FishState newState)
    {
        CurrentFishState.ExitState();
        CurrentFishState = newState;
        CurrentFishState.EnterState();
    }
}