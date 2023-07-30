using UnityEngine;

[CreateAssetMenu(fileName = "NewInventoryData", menuName = "PlayerInventoryData")]
public class InventoryData : ScriptableObject
{
    [Header("Inventory Upgrades")]
    public int[] MaxInventorySlots = new int[3];
}