using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Fish : MonoBehaviour, IDamagable, IFishMovable, ITriggerCheckable
{
    [field: SerializeField] public int MaxHealth { get; set; } = 1;
    public int CurrentHealth { get; set; }
    public Rigidbody RB { get; set; }

    public FishStateMachine StateMachine { get; set; }
    public FishIdleState IdleState { get; set; }
    public FishScaredState ScaredState { get; set; }
    public FishGrabbedState GrabbedState { get; set; }
    [field: SerializeField] public bool IsScared { get; set; }
    public bool IsGrabbed { get; set; }

    public float speed = 2f;
    public float rotateSpeed = 5f;
    [SerializeField] private float _targetChangeTime = 2f;
    [SerializeField] private WaitForSeconds _changeTargetInterval;
    public Transform areaBoundary;
    public Vector3 targetPosition;

    private void Awake()
    {
        _changeTargetInterval = new WaitForSeconds(_targetChangeTime);

        StateMachine = new FishStateMachine();
        IdleState = new FishIdleState(this, StateMachine);
        ScaredState = new FishScaredState(this, StateMachine);
        GrabbedState = new FishGrabbedState(this, StateMachine);


        CurrentHealth = MaxHealth;
        RB = GetComponent<Rigidbody>();
    }

    private void Start()
    {
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
        yield return _changeTargetInterval;
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

    public void Die()
    {
        Destroy(gameObject);
    }

    public void MoveAndRotateFish(Vector3 _targetPosition, Vector3 _targetDirection, float speed)
    {
        transform.forward = Vector3.Lerp(transform.forward, _targetDirection, Time.deltaTime * rotateSpeed);
        //transform.LookAt(_targetPosition);

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
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
