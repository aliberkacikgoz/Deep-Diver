using TMPro;
using UnityEngine;

public class FishIdleState : FishState
{
    private bool startedFindingPositions = false;

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

        if (startedFindingPositions) return;
        fish.StartChangeTargetPosition();
        startedFindingPositions = true;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (fish.IsScared)
        {
            fish.StateMachine.ChangeState(fish.ScaredState);
        }
        if (fish.IsGrabbed)
        {
            fish.StateMachine.ChangeState(fish.GrabbedState);
        }
        //Vector3 targetDirection = (fish.TargetPosition - fish.transform.position).normalized;
        //fish.MoveAndRotateFish(fish.TargetPosition, targetDirection, fish.Speed);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Vector3 targetDirection = (fish.TargetPosition - fish.transform.position).normalized;
        fish.MoveAndRotateFish(fish.TargetPosition, targetDirection, fish.Speed);
    }
}