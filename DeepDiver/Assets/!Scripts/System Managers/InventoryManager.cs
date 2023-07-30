using TMPro;
using UnityEngine;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    [Header("Oxygen Data Scriptable Object")]
    [SerializeField] private InventoryData _inventoryData;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _fishNumberTMP;

    [SerializeField] private int _currentNumberOfFish;
    private int _playerInventoryLevel;
    private int _maxNumberOfFish;

    private void Start()
    {
        _playerInventoryLevel = SaveManager.Instance.LoadPlayerInventoryLevel();
        _maxNumberOfFish = _inventoryData.MaxInventorySlots[_playerInventoryLevel];

        _fishNumberTMP.text = _currentNumberOfFish + "/" + _maxNumberOfFish.ToString();
    }

    public void UpgradePlayerInventoryLevel()
    {
        _playerInventoryLevel++;
        SaveManager.Instance.SavePlayerInventoryLevel(_playerInventoryLevel);
        _maxNumberOfFish = _inventoryData.MaxInventorySlots[_playerInventoryLevel];

        _fishNumberTMP.text = _currentNumberOfFish + "/" + _maxNumberOfFish.ToString();
    }

    public void GainFish()
    {
        if (_currentNumberOfFish >= _maxNumberOfFish) return;
        _currentNumberOfFish++;

        _fishNumberTMP.text = _currentNumberOfFish + "/" + _maxNumberOfFish.ToString();
    }

    public void SellAllFish()
    {
        MoneyManager.Instance.GainMoney(100 * _currentNumberOfFish);
        _currentNumberOfFish = 0;

        _fishNumberTMP.text = _currentNumberOfFish + "/" + _maxNumberOfFish.ToString();
    }
}