using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwimmingMovement : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed;

    [SerializeField]
    private MovementListenerSO _movementListener;

    private Vector2 _movementAmount;
    private Rigidbody _rigidbody;

    private void OnEnable()
    {
        _movementListener.OnMovement += Move;
    }

    private void OnDisable()
    {
        _movementListener.OnMovement -= Move;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //if its normal walk it will be in x and z axis
        Vector3 scaledMovement = _playerSpeed * Time.fixedDeltaTime * new Vector3(
            _movementAmount.x,
            _movementAmount.y,
            0
        );

        if ( scaledMovement != Vector3.zero ) {
            Vector3 heading = scaledMovement.normalized;
            transform.rotation = Quaternion.LookRotation(Vector3.forward * -1, heading);

        }

        //Rigidbody moves the player
        _rigidbody.MovePosition(transform.position + scaledMovement);
    }

    public void Move(Vector2 moveVector)
    {
        _movementAmount = moveVector;
    }
}
