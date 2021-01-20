using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ShipController : MonoBehaviour
{
    [SerializeField] GameObject ShipCamera;
    [SerializeField] Transform Ship;

    bool canControl = false;

    Rigidbody rb;
    PhotonView photonView, viewPlayer;
    private void Awake()
    {
        rb = Ship.GetComponent<Rigidbody>();
        photonView = Ship.GetComponent<PhotonView>();
    }

    [SerializeField] float speed = 5;
    void Update()
    {
        if(canControl)
        {
            if(photonView.IsMine)
            {
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");

                Vector3 fVelocity = new Vector3(vertical, 0, horizontal) * speed;
                rb.velocity = fVelocity;

                if (Input.GetKeyUp(KeyCode.B))
                {
                    Debug.Log("Switch Control");
                    AssignPlayer();
                }
            }
        }
        else
        {
            if (photonView.IsMine)
            {
                if (!playerIsHere)
                    return;
                if (Input.GetKeyUp(KeyCode.B))
                {
                    Debug.Log("Switch Control");
                    AssignPlayer();
                }
            }
        }
    }
    bool playerIsHere = false;
    GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        viewPlayer = other.GetComponent<PhotonView>();

        if(viewPlayer.IsMine)
        {
            if (other.tag == "MPPlayer")
            {
                if (isEquipped && playerIsHere)
                    return;


                playerIsHere = true;
                player = other.gameObject;
                GameManager.Instance.inGameUI.ShowControlKey("B");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!viewPlayer.IsMine)
            return;

        if (other.tag == "MPPlayer")
        {
            if (canControl)
                return;
            playerIsHere = false;
            viewPlayer = null;
            GameManager.Instance.inGameUI.HideControlKey();
        }
    }

    public void AssignPlayer()
    {
        Debug.Log("Assign Player");
        canControl = !canControl; shipCamActivated = !shipCamActivated;

        ShipCamera.SetActive(shipCamActivated);

        isEquipped = !isEquipped; /*playerIsHere = !playerIsHere;*/

        player.GetComponent<ThirdPersonUserControl>().enabled = !canControl;
    }
    bool shipCamActivated = false;
    bool isEquipped = false;

}
