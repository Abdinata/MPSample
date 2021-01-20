using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<InventoryData> inventoryDatas = new List<InventoryData>();

    public void CollectInventory(InventoryData inventory)
    {
        inventoryDatas.Add(inventory);
    }

    public void UseInventory(InventoryData inventory)
    {
        for(int i =0; i < inventoryDatas.Count; i ++)
        {
            if(inventoryDatas[i].inventoryID == inventory.inventoryID)
            {
                inventoryDatas.RemoveAt(i);
            }
        }
    }

}
