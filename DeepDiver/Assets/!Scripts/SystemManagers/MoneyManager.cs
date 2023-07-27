using TMPro;
using UnityEngine;

public class MoneyManager : MonoSingleton<MoneyManager>
{
    [SerializeField] private TextMeshProUGUI _moneyTMP;
    [SerializeField] private int _currentPlayerMoney;

    private void Start()
    {
        _currentPlayerMoney = SaveManager.Instance.LoadPlayerMoney();
        _moneyTMP.text = _currentPlayerMoney.ToString();
    }

    public void GainMoney(int gainAmount)
    {
        _currentPlayerMoney += gainAmount;
        SaveManager.Instance.SavePlayerMoney(_currentPlayerMoney);
        _moneyTMP.text = _currentPlayerMoney.ToString();
    }

    public void SpendMoney(int spendAmount)
    {
        _currentPlayerMoney -= spendAmount;
        SaveManager.Instance.SavePlayerMoney(_currentPlayerMoney);
        _moneyTMP.text = _currentPlayerMoney.ToString();
    }
}