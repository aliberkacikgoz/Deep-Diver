using UnityEngine;

[CreateAssetMenu(fileName = "NewOxygenData", menuName = "PlayerOxygenData")]
public class PlayerOxygenData : ScriptableObject
{
    [Header("Oxygen Upgrades")]
    public float[] MaxOxygenAmountArray = new float[3];

    [Header("Oxygen Lose & Recovery Upgrades")]
    public float[] OxygenLoseSpeedArray = new float[3];
    public float[] OxygenRecoverySpeedArray = new float[3];
}