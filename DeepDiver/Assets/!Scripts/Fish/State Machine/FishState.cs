using UnityEngine;

public class FishState
{
    protected Fish fish;
    protected FishStateMachine fishStateMachine;

    public FishState(Fish fish, FishStateMachine fishStateMachine)
    {
        this.fish = fish;
        this.fishStateMachine = fishStateMachine;
    }

    public virtual void EnterState() { }

    public virtual void ExitState() { }
    
    public virtual void FrameUpdate() { }
    
    public virtual void PhysicsUpdate() { }

    public virtual void AnimationTriggerEvent(Fish.AnimationTriggerType triggerType) { }
}