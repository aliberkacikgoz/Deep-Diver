using UnityEngine;

public class FishIdleState : FishState
{
    public FishIdleState(Fish fish, FishStateMachine fishStateMachine) : base(fish, fishStateMachine)
    {

    }

    public override void AnimationTriggerEvent(Fish.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        fish.StartChangeTargetPosition();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        fish.MoveFish(fish.targetPosition, fish.speed);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}