using UnityEngine;

public class SaveManager : MonoSingleton<SaveManager>
{
    #region Money
    public void SavePlayerMoney(int saveMoney)
    {
        PlayerPrefs.SetInt("PlayerMoneySave", saveMoney);
    }

    public int LoadPlayerMoney()
    {
        return PlayerPrefs.GetInt("PlayerMoneySave", 100);
    }
    #endregion

    #region PlayerOxygen
    public void SavePlayerOxygenLevel(int saveLevel)
    {
        PlayerPrefs.SetInt("PlayerOxygenLevelSave", saveLevel);
    }

    public int LoadPlayerOxygenLevel()
    {
        return PlayerPrefs.GetInt("PlayerOxygenLevelSave", 0);
    }
    #endregion

    #region PlayerInventory
    public void SavePlayerInventoryLevel(int saveLevel)
    {
        PlayerPrefs.SetInt("PlayerInventoryLevelSave", saveLevel);
    }

    public int LoadPlayerInventoryLevel()
    {
        return PlayerPrefs.GetInt("PlayerInventoryLevelSave", 0);
    }
    #endregion
}