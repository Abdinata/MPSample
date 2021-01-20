using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntearctableInventoryProp : MonoBehaviour
{
    public UnityEvent onExecuted;
    public InventoryData inventoryData;

    PhotonView photonView, viewPlayer;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    Inventory inventory;
    Collider player;

    [SerializeField] bool destroyAfterExec = true;
    private void OnTriggerEnter(Collider other)
    {
        viewPlayer = other.GetComponent<PhotonView>();

        if(viewPlayer.IsMine)
        {
            if (other.tag == "MPPlayer")
            {
                Debug.Log("Player is here");

                player = other;

                canUseInventory = true;

                GameManager.Instance.inGameUI.ShowControlKey("E");

            }

        }

    }

    private void OnTriggerExit(Collider other)
    {
        viewPlayer = other.GetComponent<PhotonView>();

        if (viewPlayer.IsMine)
        {
            if (other.tag == "MPPlayer")
            {
                canUseInventory = false;
                GameManager.Instance.inGameUI.HideControlKey();
            }

        }
    }

    bool canUseInventory = false;
    bool inventoryUsed = false;
    private void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.K))
        {
            if(canUseInventory && !inventoryUsed)
            {
                canUseInventory = false; inventoryUsed = true;
                Inventory inventory = player.GetComponent<Inventory>();

                Debug.Log("Inventory Pressed");
                for (int i = 0; i < inventory.inventoryDatas.Count; i++)
                {
                    Debug.Log("Looping Inventory");
                    if (inventory.inventoryDatas[i].inventoryID == inventoryData.inventoryID)
                    {
                        Debug.Log("Try Use Inventory");
                        //photonView.RPC("Execute", RpcTarget.All, new object[] { inventoryData.inventoryID, PhotonNetwork.LocalPlayer.ActorNumber });
                        inventory.UseInventory(inventoryData);
                        photonView.RPC("Execute", RpcTarget.All);
                    }
                }
            }
        }
    }

    [PunRPC]
    public void Execute(/*int inventoryData, int playerId*/)
    {
        /**
        for(int i = 0; i< PhotonNetwork.PlayerList.Length; i++)
        {
            if(PhotonNetwork.PlayerList[i].ActorNumber == playerId)
            {
                
            }
        }

        for (int i = 0; i < inventory.inventoryDatas.Count; i++)
        {
            if(inventory.inventoryDatas[i].inventoryID == inventoryData.inventoryID)
            {
                inventory.inventoryDatas.RemoveAt(i);
            }
        }
        */
        
        onExecuted.Invoke();

        if (destroyAfterExec)
            Destroy(this.gameObject);
    }

}
