using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public interface IAttacker
{
    Rigidbody RB { get; set; }

    Transform RangeTrigger { get; set; }
    Transform Target { get; set; }
    List<Transform> PossibleTargetList { get; set; }
    
    float AimTime { get; set; }
    int CatchPower { get; set; }
    bool hasTarget { get; set; }
    bool isMoving { get; set; }

    WaitForSeconds aimTimeWaitForSeconds { get; set; }
    Coroutine AimingCoroutine { get; set; }

    void StartAiming(Transform target);
    void StopAiming();
    void StartCathcing();
    void StopCathcing();

    IEnumerator Aim();

    void UpgradePower();

    void CaughtFish();

    void IsMoving(Finger TouchedFinger);
    void IsStationary(Finger TouchedFinger);
}