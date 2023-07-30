using UnityEngine;

public class UpgradeManager : MonoSingleton<UpgradeManager>
{
    public void UpgradePlayerInventory()
    {
        InventoryManager.Instance.UpgradePlayerInventoryLevel();
    }

    public void UpgradePlayerOxygenLevel()
    {
        FindObjectOfType<PlayerOxygen>().UpgradeMaxOxygen();
    }
}