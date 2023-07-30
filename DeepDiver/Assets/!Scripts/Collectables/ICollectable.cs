using UnityEngine;

public interface ICollectable
{
    GameObject PlayerTarget { get; set; }
    int Value { get; set; }

    void Collect();
    void DestroySelf();
}