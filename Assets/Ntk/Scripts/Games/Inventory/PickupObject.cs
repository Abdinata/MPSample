using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    PhotonView photonView;

    public InventoryData inventoryData;
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MPPlayer")
        {
            Inventory inventory = other.GetComponent<Inventory>();

            inventory.CollectInventory(inventoryData);
            Destroy(this.gameObject);
        }
    }
}
