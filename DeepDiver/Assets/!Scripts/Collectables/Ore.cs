using UnityEngine;

public class Ore : MonoBehaviour, ICollectable
{
    [field: SerializeField] public int Value { get; set; } = 50;
    public GameObject PlayerTarget { get; set; }

    private void Awake()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("PlayerSwimmer");
    }

    public void Collect()
    {
        MoneyManager.Instance.GainMoney(Value);
        DestroySelf();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == PlayerTarget)
        {
            Collect();
        }
    }
}