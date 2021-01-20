using Photon.Pun;
using UnityEngine;

public class ShipPlayerAssign : MonoBehaviour
{

    [SerializeField] Transform shipParent, targetEmbark, targetDisembark;

    bool isEmbarking, isPlayerHere;

    GameObject player;
    PhotonView photonView, playerView;

    private void Awake()
    {
        isEmbarking = !isEmbarking;

        photonView = GetComponent<PhotonView>();
    }
    private void OnTriggerEnter(Collider other)
    {
        playerView = other.GetComponent<PhotonView>();

        if (other.tag == "MPPlayer")
        {
            if (isPlayerHere)
                return;

            if (playerView.IsMine)
                GameManager.Instance.inGameUI.ShowControlKey("E");

            isPlayerHere = true;

            player = other.gameObject;
            //photonView = other.gameObject.GetComponent<PhotonView>();
        }
    }

    private void LateUpdate()
    {
        if (!isPlayerHere)
            return;
        if(Input.GetKeyUp(KeyCode.E))
        {
            if (playerView != null)
                if (playerView.IsMine)
                    photonView.RPC("Embark", RpcTarget.All);
        }
        if(isEmbarking)
        {
            if (player != null)
            {
                //if(photonView.IsMine)
                    player.transform.position = targetEmbark.transform.position;
            }
        }
    }

    [PunRPC]
    public void Embark()
    {
        isEmbarking = !isEmbarking;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MPPlayer")
        {
            GameManager.Instance.inGameUI.HideControlKey();
            isPlayerHere = false;
        }
    }

    //[PunRPC]
    //public void SetPlayerParent(GameObject player)
    //{

    //}

    //[PunRPC]
    //public void SetPlayerParentRoot()
    //{

    //}
}
