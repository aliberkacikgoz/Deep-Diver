using UnityEngine;

public class PlayerSwimmingMovement : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed;

    [SerializeField]
    private MovementListenerSO _movementListener;

    private Vector2 _movementAmount;
    private Rigidbody _rigidbody;

    //private bool activeGrapple;
    //private Vector3 velocityToSet;
    //private bool enableMovementOnNextTouch;
    //private Harpoon _harpoon;

    public bool canNotRotate = false;
    [SerializeField] private bool _disableSwimmingMovement = false;
    public bool DisableSwimmingMovement
    {
        get
        {
            return _disableSwimmingMovement;
        }
        set
        {
            _disableSwimmingMovement = value;
        }
    }

    private Vector3 _scaledMovement;
    public Vector3 ScaledMovement
    {
        get
        {
            return _scaledMovement;
        }
        set
        {
            _scaledMovement = value;
        }
    }

    private void OnEnable()
    {
        _movementListener.OnMovement += Move;
    }

    private void OnDisable()
    {
        _movementListener.OnMovement -= Move;
    }

    //private void Awake()
    //{
    //    _harpoon = GetComponent<Harpoon>();
    //}

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //if (activeGrapple) return;


        //if its normal walk it will be in x and z axis
        _scaledMovement = _playerSpeed * Time.fixedDeltaTime * new Vector3(
            _movementAmount.x,
            _movementAmount.y,
            0
        );

        if (_scaledMovement != Vector3.zero ) {
            Vector3 heading = _scaledMovement.normalized;
            if (canNotRotate) return;
            transform.rotation = Quaternion.LookRotation(Vector3.forward * -1, heading);
        }

        if (_disableSwimmingMovement) return;
        //Rigidbody moves the player
        _rigidbody.MovePosition(transform.position + _scaledMovement);
    }

    public void Move(Vector2 moveVector)
    {
        _movementAmount = moveVector;
    }

    // Bunlara sonra bakicam

    //public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    //{
    //    activeGrapple = true;

    //    velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
    //    Invoke(nameof(SetVelocity), 0.1f);

    //    Invoke(nameof(ResetRestrictions), 3f);
    //}

    //private void SetVelocity()
    //{
    //    enableMovementOnNextTouch = true;
    //    _rigidbody.velocity = velocityToSet;
    //    Invoke(nameof(EnableMovement), 1f);

    //    //cam.DoFov(grappleFov);
    //}

    //private void EnableMovement()
    //{
    //    enableMovementOnNextTouch = false;
    //    _harpoon.StopGrapple();
    //}

    //public void ResetRestrictions()
    //{
    //    activeGrapple = false;
    //    //cam.DoFov(85f);
    //}

    //public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    //{
    //    float gravity = Physics.gravity.y;
    //    float displacementY = endPoint.y - startPoint.y;
    //    Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

    //    Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
    //    Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
    //        + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

    //    return velocityXZ + velocityY;
    //}
}
