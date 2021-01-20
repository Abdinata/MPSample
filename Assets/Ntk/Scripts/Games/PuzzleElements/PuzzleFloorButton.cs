using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleFloorButton : MonoBehaviour
{
    public bool isPlayerEnter = false;
    public PuzzleFloor puzzleFloor;

    [SerializeField] Animator animator;
    PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MPPlayer")
        {
            if (isPlayerEnter)
                return;

            photonView.RPC("ExcecuteEnter", RpcTarget.All);
        }
    }

    [SerializeField] string ExcecuteAnim, DeExecuteAnim;
    [PunRPC]
    public void ExcecuteEnter()
    {
        isPlayerEnter = true;
        animator.Play(ExcecuteAnim);
        puzzleFloor.CheckAction();
    }

    [PunRPC]
    public void ExcecuteExit()
    {
        isPlayerEnter = false;
        animator.Play(DeExecuteAnim);
        puzzleFloor.CheckAction();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MPPlayer")
        {
            if (!isPlayerEnter)
                return;
            photonView.RPC("ExcecuteExit", RpcTarget.All);
        }
    }
}
