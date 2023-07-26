using System.Collections;
using UnityEngine;

public class Fish : MonoBehaviour, IDamagable, IFishMovable
{
    [field: SerializeField] public int MaxHealth { get; set; } = 1;
    public int CurrentHealth { get; set; }
    public Rigidbody RB { get; set; }

    public FishStateMachine StateMachine { get; set; }
    public FishIdleState IdleState { get; set; }
    public FishRunAwayState RunAwayState { get; set; }
    public FishStruggleState StruggleState { get; set; }

    public float speed = 2f;
    [SerializeField] private float _targetChangeTime = 2f;
    [SerializeField] private WaitForSeconds _changeTargetInterval;
    public Transform areaBoundary;
    public Vector3 targetPosition;

    private void Awake()
    {
        _changeTargetInterval = new WaitForSeconds(_targetChangeTime);

        StateMachine = new FishStateMachine();
        IdleState = new FishIdleState(this, StateMachine);
        RunAwayState = new FishRunAwayState(this, StateMachine);
        StruggleState = new FishStruggleState(this, StateMachine);


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

    public void MoveFish(Vector3 _targetPosition, float speed)
    {
        transform.LookAt(_targetPosition);

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
    }

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentFishState.AnimationTriggerEvent(triggerType);
    }

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlaySound
    }
}
