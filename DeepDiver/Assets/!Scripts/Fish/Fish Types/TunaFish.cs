using UnityEngine;

public class TunaFish : Fish
{
    [Header("Fish Data Scriptable Object")]
    [SerializeField] private FishData _fishData;

    private void OnEnable()
    {
        MaxHealth= _fishData.MaxHealth;
        speed = _fishData.speed;
        rotateSpeed = _fishData.rotateSpeed;
        _targetChangeTime = _fishData.  _targetChangeTime;
        _escapeTime = _fishData._escapeTime;
        speedMult = _fishData.speedMult;
        positionMult = _fishData.positionMult;
    }
}