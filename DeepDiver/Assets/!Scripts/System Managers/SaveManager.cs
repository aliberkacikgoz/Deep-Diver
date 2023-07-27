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
}