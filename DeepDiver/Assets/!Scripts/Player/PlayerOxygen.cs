using UnityEngine;
using UnityEngine.UI;

public class PlayerOxygen : MonoBehaviour, IBreather
{
    [Header("Oxygen Data Scriptable Object")]
    [SerializeField] private PlayerOxygenData _playerOxygenData;

    [Header("Oxygen Sliders")]
    [SerializeField] private Slider _oxygenWheel;
    [SerializeField] private Slider _oxygenUsageWheel;

    [field: Header("Oxygen Sliders")]
    [field: SerializeField] public int OxygenLevel { get; set; }
    [field: SerializeField] public float MaxOxygen { get; set; }
    [field: SerializeField] public float CurrentOxygen { get; set; }
    [field: SerializeField] public float OxygenLoseSpeed { get; set; }
    [field: SerializeField] public float OxygenRecoverySpeed { get; set; }

    public bool isUnderWater { get; set; } = false;

    private void Start()
    {
        OxygenLevel = SaveManager.Instance.LoadPlayerOxygenLevel();
        SetPlayerOxygenParameters();
    }

    private void Update()
    {
        if (isUnderWater)
        {
            if (CurrentOxygen > 0)
            {
                LoseOxygen(10 * Time.deltaTime);
            }
        }
        else
        {
            if (CurrentOxygen < MaxOxygen)
            {
                GainOxygen(30 * Time.deltaTime);
            }
        }
    }

    public void GainOxygen(float gainAmount)
    {
        CurrentOxygen += gainAmount;
        _oxygenUsageWheel.value = CurrentOxygen / MaxOxygen;
        _oxygenWheel.value = CurrentOxygen / MaxOxygen;
    }

    public void LoseOxygen(float loseAmount)
    {
        CurrentOxygen -= loseAmount;
        _oxygenUsageWheel.value = CurrentOxygen / MaxOxygen + 0.05f;
        _oxygenWheel.value = CurrentOxygen / MaxOxygen;

        if (CurrentOxygen <= 0)
        {
            RunOutOfOxygen();
        }
    }

    public void UpgradeMaxOxygen()
    {
        OxygenLevel++;
        SaveManager.Instance.SavePlayerOxygenLevel(OxygenLevel);
        SetPlayerOxygenParameters();
    }

    private void SetPlayerOxygenParameters()
    {
        MaxOxygen = _playerOxygenData.MaxOxygenAmountArray[OxygenLevel];
        OxygenLoseSpeed = _playerOxygenData.OxygenLoseSpeedArray[OxygenLevel];
        OxygenRecoverySpeed = _playerOxygenData.OxygenRecoverySpeedArray[OxygenLevel];
        CurrentOxygen = MaxOxygen;
    }

    public void RunOutOfOxygen()
    {
        Debug.Log("Player Ran Out Of Oxygen!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerSub"))
        {
            isUnderWater = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerSub"))
        {
            isUnderWater = true;
        }
    }
}
