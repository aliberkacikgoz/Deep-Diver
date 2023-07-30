using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent(typeof(Harpoon))]
[RequireComponent(typeof(CatchFishBar))]
public class PlayerAttack : MonoBehaviour, IAttacker
{
    [SerializeField] private Transform harpoonGun;

    private PlayerSwimmingMovement _playerSwimmingMovement;
    private Harpoon _harpoon;

    public Rigidbody RB { get; set; }

    public Transform RangeTrigger { get; set; }
    public Transform Target { get; set; }
    public List<Transform> PossibleTargetList { get; set; }

    [field: SerializeField] public float AimTime { get; set; } = 3;
    public int CatchPower { get; set; }
    public bool hasTarget { get; set; } = false;
    [field: SerializeField]public bool isMoving { get; set; }

    public WaitForSeconds aimTimeWaitForSeconds { get; set; }
    public Coroutine AimingCoroutine { get; set; }

    public bool catchSuccesfull = false;
    public bool startedCatching = false;

    private Fish _fish;
    private CatchFishBar _catchFishBar;

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
        PossibleTargetList = new List<Transform>();
        aimTimeWaitForSeconds = new WaitForSeconds(AimTime);
        _harpoon = GetComponent<Harpoon>();
        _playerSwimmingMovement = GetComponent<PlayerSwimmingMovement>();
        _catchFishBar = GetComponent<CatchFishBar>();
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();

        ETouch.Touch.onFingerDown += IsMoving;
        ETouch.Touch.onFingerUp += IsStationary;
        //ETouch.Touch.onFingerMove += HandleFingerMove;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= IsMoving;
        ETouch.Touch.onFingerUp -= IsStationary;
        //ETouch.Touch.onFingerMove -= HandleFingerMove;

        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        if (PossibleTargetList.Count <= 0) return;
        if (isMoving) return;

        if (hasTarget)
        {
            harpoonGun.LookAt(Target);
            if (startedCatching) return;
            transform.LookAt(Target);
            return;
        }

        Target = PossibleTargetList[0];
        StartAiming(Target);
    }

    private void LateUpdate()
    {
        //if (startedCatching)
        //{
        //    FishLookAway();
        //}
    }

    public void StartAiming(Transform target)
    {
        hasTarget = true;
        if (AimingCoroutine != null) return;
        //Debug.Log("Started Aiming.");
        AimingCoroutine = StartCoroutine(Aim());
    }

    public void StopAiming()
    {
        hasTarget = false;
        StopCoroutine(AimingCoroutine);
        AimingCoroutine = null;
    }

    public void StartCathcing()
    {
        //Debug.Log("Took a shot!");
        _fish = Target.GetComponent<Fish>();
        _fish.DisableFishMovement = true;
        _fish.RB.velocity = Vector3.zero;
        _fish.transform.forward = Vector3.Lerp(transform.forward, (transform.position - _fish.transform.position).normalized, Time.deltaTime * 20f);
        _playerSwimmingMovement.DisableSwimmingMovement = true;
        _harpoon.StartGrapple();
        startedCatching = true;
        _catchFishBar.GetFishInfo(_fish);
        StartCoroutine(_fish.GettingCaught(this));
    }


    public void CheckIfSuccesfull()
    {
        if (catchSuccesfull)
        {
            CaughtFish();
        }
        else
        {
            StopCathcing(_fish);
        }
        _catchFishBar.catchBar.value = 0;
    }

    public void StopCathcing(Fish target)
    {
        //Debug.Log("Fish escaped!");
        startedCatching = false;
        hasTarget = false;
        target.DisableFishMovement = false;
        _playerSwimmingMovement.DisableSwimmingMovement = false;
        _harpoon.StopGrapple();
    }

    public void CaughtFish()
    {
        //Debug.Log("Got fish!");
        startedCatching = false;
        PossibleTargetList.Remove(Target);
        Target.GetComponent<Fish>().Die();
        hasTarget = false;
        _playerSwimmingMovement.DisableSwimmingMovement = false;
        AimingCoroutine = null;
        _harpoon.StopGrapple();
        InventoryManager.Instance.GainFish();
    }

    public void UpgradePower()
    {

    }

    public IEnumerator Aim()
    {
        //Debug.Log("Aiming...");
        yield return aimTimeWaitForSeconds;
        StartCathcing();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fish"))
        {
            PossibleTargetList.Add(other.gameObject.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fish"))
        {
            PossibleTargetList.Remove(other.gameObject.transform);
            if (Target != other.gameObject.transform) return;
            if (AimingCoroutine == null) return;
            StopAiming();
        }
    }

    public void IsMoving(Finger TouchedFinger)
    {
        isMoving = true;
    }

    public void IsStationary(Finger TouchedFinger)
    {
        isMoving = false;
    }
}