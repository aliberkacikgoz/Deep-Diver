using UnityEngine;

[CreateAssetMenu(fileName = "NewFish", menuName = "Fish")]
public class FishData : ScriptableObject
{
    [Header("Health Settings")]
    public int MaxHealth = 1;

    [Header("Idle Movement Settings")]
    public float speed = 2f;
    public float rotateSpeed = 5f;
    public float _targetChangeTime = 2f;

    [Header("Escaping Movement Settings")]
    public float _escapeTime = 2f;
    public float speedMult = 4;
    public float positionMult = 6;
}