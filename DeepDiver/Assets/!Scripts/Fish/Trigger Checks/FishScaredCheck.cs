using UnityEngine;

public class FishScaredCheck : MonoBehaviour
{
    public GameObject PlayerTarget { get; set; }
    private Fish _fish;

    private void Awake()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");
        _fish = GetComponentInParent<Fish>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == PlayerTarget)
        {
            _fish.SetScaredStatus(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == PlayerTarget)
        {
            _fish.SetScaredStatus(false);
        }
    }
}