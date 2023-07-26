using UnityEngine;

public interface IFishMovable
{
    Rigidbody RB { get; set; }

    void MoveAndRotateFish(Vector3 _targetPosition, Vector3 _targetDirection, float speed);
}