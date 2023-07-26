using UnityEngine;

public class FishScaredState : FishState
{
    private Transform _playerTransform;
    private Vector3 moveDirection;
    private Vector3 movePosition;
    private float positionMult = 6;
    private float speedMult = 4;
    public bool startedEscaping = false;

    public FishScaredState(Fish fish, FishStateMachine fishStateMachine) : base(fish, fishStateMachine)
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void AnimationTriggerEvent(Fish.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        moveDirection = (fish.transform.position - _playerTransform.position).normalized;
        movePosition = fish.transform.position + moveDirection * positionMult;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        fish.MoveAndRotateFish(movePosition, moveDirection, fish.speed * speedMult);

        if (fish.IsGrabbed)
        {
            fish.StateMachine.ChangeState(fish.GrabbedState);
        }
        if (!fish.IsScared)
        {
            if (startedEscaping) return;
            fish.StartFishIsEscaping();
            startedEscaping = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}