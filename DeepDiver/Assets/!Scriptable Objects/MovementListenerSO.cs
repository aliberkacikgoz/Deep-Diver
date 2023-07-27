using System;
using UnityEngine;


[CreateAssetMenu(menuName = "SOs/Movement Listener")]
public class MovementListenerSO : ScriptableObject
{
    public event Action<Vector2> OnMovement;

    public void Move(Vector2 move) => OnMovement?.Invoke(move);
}
