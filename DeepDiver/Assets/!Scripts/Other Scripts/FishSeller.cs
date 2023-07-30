using UnityEngine;

public class FishSeller : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerSub"))
        {
            InventoryManager.Instance.SellAllFish();
        }
    }
}
