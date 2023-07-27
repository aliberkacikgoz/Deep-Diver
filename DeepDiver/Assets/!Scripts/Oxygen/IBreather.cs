using UnityEngine;

public interface IBreather
{
    float MaxOxygen { get; set; }
    float CurrentOxygen { get; set; }
    float OxygenLoseSpeed { get; set; }
    float OxygenRecoverySpeed { get; set; }
    int OxygenLevel { get; set; }
    bool isUnderWater { get; set; }

    void GainOxygen(float gainAmount);
    void LoseOxygen(float loseAmount);

    void UpgradeMaxOxygen();

    void RunOutOfOxygen();
}