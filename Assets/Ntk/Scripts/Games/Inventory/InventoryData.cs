using UnityEngine;

[CreateAssetMenu(fileName = "iData", menuName = "Ntk/InventoryData", order = 1)]
public class InventoryData : ScriptableObject
{
    public int inventoryID = 0;
    public string inventoryObjectName;
    public GameObject inventoryPrefab;
    public bool isStackable;
}
