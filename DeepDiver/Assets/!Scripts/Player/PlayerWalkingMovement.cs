using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerWalkingMovement : MonoBehaviour
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
            0,
            _movementAmount.y
        );

        transform.LookAt(transform.position + scaledMovement, Vector3.up);

        //Rigidbody ile hareket iþini burda ya
        _rigidbody.MovePosition(transform.position + scaledMovement);
    }

    public void Move(Vector2 moveVector)
    {
        _movementAmount = moveVector;
    }
}
