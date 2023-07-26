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
            Debug.Log("Fish Got Scared.");
        }
        if (fish.IsGrabbed)
        {
            fish.StateMachine.ChangeState(fish.GrabbedState);
        }
        Vector3 targetDirection = (fish.targetPosition - fish.transform.position).normalized;
        fish.MoveAndRotateFish(fish.targetPosition, targetDirection, fish.speed);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}