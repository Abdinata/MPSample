using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using Photon.Pun;

public class RunnerDeadZone : MonoBehaviour
{
    int countDead = 0;

    GameObject playerOnDeadZone;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MPPlayer")
        {
            //other.GetComponent<ThirdPersonUserControl>().enabled = false;
            playerOnDeadZone = other.gameObject;
            other.GetComponent<PhotonView>().RPC("MovePlayer", RpcTarget.All, new object[] { countDead });
        }
    }
}
