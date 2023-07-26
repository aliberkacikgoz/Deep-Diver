using UnityEngine;

public class FishGrabbedCheck : MonoBehaviour
{
    public GameObject WeaponTarget { get; set; }
    private Fish _fish;

    private void Awake()
    {
        WeaponTarget = GameObject.FindGameObjectWithTag("PlayerBullet");
        _fish = GetComponentInParent<Fish>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == WeaponTarget)
        {
            _fish.SetGrabbedStatus(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == WeaponTarget)
        {
            _fish.SetGrabbedStatus(false);
        }
    }
}