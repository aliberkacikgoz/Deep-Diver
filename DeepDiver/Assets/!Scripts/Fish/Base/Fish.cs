using System.Collections;
using UnityEngine;

public class Fish : MonoBehaviour, IDamagable, IFishMovable, IFishTriggerCheckable
{
    public int MaxHealth { get; set; } = 1;
    [field: SerializeField] public int CatchTime { get; set; }
    public int CurrentHealth { get; set; }
    public Rigidbody RB { get; set; }

    public FishStateMachine StateMachine { get; set; }
    public FishIdleState IdleState { get; set; }
    public FishScaredState ScaredState { get; set; }
    public FishGrabbedState GrabbedState { get; set; }
    public bool IsScared { get; set; }
    public bool IsGrabbed { get; set; }

    [Header("Idle Movement Variables")]
    [SerializeField] public float speed = 2f;
    public float Speed { get { return speed; } private set { } }
    [SerializeField] protected float rotateSpeed = 5f;
    [SerializeField] protected float _targetChangeTime = 2f;
    [SerializeField] protected Transform areaBoundary;

    [Header("Escaping Movement Variables")]
    [SerializeField] protected float _escapeTime = 2f;
    [SerializeField] protected float speedMult = 4;
    public float SpeedMult { get { return speedMult; } private set { } }
    [SerializeField] protected float positionMult = 6;
    public float PositionMult { get { return positionMult; } private set { } }


    private Vector3 targetPosition;
    public Vector3 TargetPosition { get { return targetPosition; } private set { } }

    private bool _disableFishMovement = false;
    public bool DisableFishMovement
    {
        get
        {
            return _disableFishMovement;
        }
        set
        {
            _disableFishMovement = value;
        }
    }

    private WaitForSeconds _changeTargetInterval;
    private WaitForSeconds _escapeTimeInterval;
    private WaitForSeconds _getCaughtTimeInterval;

    private void Start()
    {
        _changeTargetInterval = new WaitForSeconds(_targetChangeTime);
        _escapeTimeInterval = new WaitForSeconds(_escapeTime);
        _getCaughtTimeInterval = new WaitForSeconds(CatchTime);

        StateMachine = new FishStateMachine();
        IdleState = new FishIdleState(this, StateMachine);
        ScaredState = new FishScaredState(this, StateMachine);
        GrabbedState = new FishGrabbedState(this, StateMachine);


        CurrentHealth = MaxHealth;
        RB = GetComponent<Rigidbody>();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentFishState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentFishState.PhysicsUpdate();
    }

    public void StartChangeTargetPosition()
    {
        StartCoroutine(ChangeTargetPosition());
    }

    public void StartFishIsEscaping()
    {
        StartCoroutine(FishIsEscaping());
    }

    private IEnumerator ChangeTargetPosition()
    {
        while (true)
        {
            Vector3 randomDirection = Random.insideUnitSphere;

            randomDirection.z = 0f;     

            targetPosition = areaBoundary.position + randomDirection * areaBoundary.localScale.x * 0.5f;

            yield return _changeTargetInterval;
        }
    }

    private IEnumerator FishIsEscaping()
    {
        yield return _escapeTimeInterval;
        StateMachine.ChangeState(IdleState);
        ScaredState.startedEscaping = false;
    }

    public void Damage(int damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public IEnumerator GettingCaught(PlayerAttack playerAttack)
    {
        //Debug.Log("Coroutine started.");
        yield return _getCaughtTimeInterval;
        if (!playerAttack.catchSuccesfull)
        {
            playerAttack.CheckIfSuccesfull();
        }
        //Debug.Log("Coroutine finished.");
    }

    public void Die()
    {
        //Debug.Log("Fish Died!");
        Destroy(gameObject);
    }

    public void MoveAndRotateFish(Vector3 _targetPosition, Vector3 _targetDirection, float speed)
    {
        transform.forward = Vector3.Lerp(transform.forward, _targetDirection, Time.deltaTime * rotateSpeed);


        if (_disableFishMovement) return;
        Vector3 desiredVelocity = (_targetPosition - transform.position).normalized * speed;
        Vector3 force = (desiredVelocity - RB.velocity) / Time.fixedDeltaTime;

        RB.AddForce(force);

        //transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
    }

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentFishState.AnimationTriggerEvent(triggerType);
    }

    public void SetScaredStatus(bool isScared)
    {
        IsScared = isScared;
    }

    public void SetGrabbedStatus(bool isGrabbed)
    {
        IsGrabbed = isGrabbed;
    }

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlaySound
    }
}
