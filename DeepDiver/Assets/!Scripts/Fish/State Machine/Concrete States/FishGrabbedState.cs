using UnityEngine;
using UnityEngine.EventSystems;

public class FishGrabbedState : FishState
{
    private Transform _playerTransform;
    private Vector3 moveDirection;
    private Vector3 movePosition;
    private float positionMult = 6;
    private float speedMult = 3;

    public FishGrabbedState(Fish fish, FishStateMachine fishStateMachine) : base(fish, fishStateMachine)
    {

    }

    public override void AnimationTriggerEvent(Fish.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        movePosition = (fish.transform.position - _playerTransform.position).normalized * positionMult;
        moveDirection = (fish.transform.position - _playerTransform.position).normalized;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        fish.MoveAndRotateFish(movePosition, moveDirection, fish.Speed * speedMult);

        if (!fish.IsGrabbed)
        {
            fish.StateMachine.ChangeState(fish.ScaredState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}