using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent(typeof(Harpoon))]
public class PlayerAttack : MonoBehaviour, IAttacker
{
    [SerializeField] private Transform harpoonGun;

    private Harpoon _grappling;

    public Rigidbody RB { get; set; }

    public Transform RangeTrigger { get; set; }
    public Transform Target { get; set; }
    public List<Transform> PossibleTargetList { get; set; }

    [field: SerializeField] public int AimTime { get; set; } = 3;
    public int CatchPower { get; set; }
    public bool hasTarget { get; set; } = false;
    [field: SerializeField]public bool isMoving { get; set; }

    public WaitForSeconds aimTimeWaitForSeconds { get; set; }
    public Coroutine AimingCoroutine { get; set; }

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
        PossibleTargetList = new List<Transform>();
        aimTimeWaitForSeconds = new WaitForSeconds(AimTime);
        _grappling = GetComponent<Harpoon>();
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
            transform.LookAt(Target);
            harpoonGun.LookAt(Target);
            return;
        }

        Target = PossibleTargetList[0];
        StartAiming(Target);
    }

    public void StartAiming(Transform target)
    {
        hasTarget = true;
        if (AimingCoroutine != null) return;
        Debug.Log("Started Aiming.");
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
        Debug.Log("Took a shot!");
        _grappling.StartGrapple();
        CaughtFish();
    }

    public void StopCathcing()
    {
        hasTarget = false;
    }

    public void CaughtFish()
    {
        PossibleTargetList.Remove(Target);
        //Target.GetComponent<Fish>().Die();
        hasTarget = false;
        AimingCoroutine = null;
        //_grappling.StopGrapple();
    }

    public void UpgradePower()
    {

    }

    public IEnumerator Aim()
    {
        Debug.Log("Aiming...");
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