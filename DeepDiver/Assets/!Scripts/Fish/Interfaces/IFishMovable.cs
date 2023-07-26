using UnityEngine;

public interface IFishMovable
{
    Rigidbody RB { get; set; }

    void MoveFish(Vector3 _targetPosition, float speed);
}