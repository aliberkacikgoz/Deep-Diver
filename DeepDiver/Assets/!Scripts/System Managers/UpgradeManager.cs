using UnityEngine;

public class UpgradeManager : MonoSingleton<UpgradeManager>
{
    [SerializeField] private PlayerOxygen _playerOxygen;
    public void UpgradePlayerInventory()
    {
        InventoryManager.Instance.UpgradePlayerInventoryLevel();
    }

    public void UpgradePlayerOxygenLevel()
    {
        _playerOxygen.UpgradeMaxOxygen();
    }
}