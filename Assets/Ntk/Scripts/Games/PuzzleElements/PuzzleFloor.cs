using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleFloor : MonoBehaviour
{
    [SerializeField] PuzzleFloorButton[] floorButton;

    public UnityEvent onExecute, onDeExecute;
    PhotonView photonview;

    private void Awake()
    {
        photonview = GetComponent<PhotonView>();

        for (int i = 0; i < floorButton.Length; i++)
        {
            floorButton[i].puzzleFloor = this;
        }
    }


    public void CheckAction()
    {
        int t = 0;
        for(int i = 0; i < floorButton.Length; i ++)
        {
            if (floorButton[i].isPlayerEnter)
                t += 1;
        }

        if (t >= floorButton.Length)
        {
            photonview.RPC("ExecuteAction", RpcTarget.All);
        }

        else
        {
            if(isExecuted)
            {
                photonview.RPC("DeExecuteAction", RpcTarget.All);
            }
        }
    }

    bool isExecuted = false;

    [PunRPC]
    void ExecuteAction()
    {
        onExecute.Invoke();
        isExecuted = true;
    }
    [PunRPC]
    void DeExecuteAction()
    {
        onDeExecute.Invoke();
        isExecuted = false;
    }

}
