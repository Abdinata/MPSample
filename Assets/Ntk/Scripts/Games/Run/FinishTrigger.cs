using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityStandardAssets.Characters.ThirdPerson;

public class FinishTrigger : MonoBehaviour, IInRoomCallbacks
{
    [SerializeField] GameObject GameFinished;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MPPlayer")
        {
            //other.GetComponent<ThirdPersonUserControl>().enabled = false;
            PhotonNetwork.Destroy(other.GetComponent<PhotonView>());
            if (PhotonNetwork.CountOfPlayersInRooms >= 2)
            {
                GameFinished.SetActive(true);
            }
            else
            {
                GameFinished.SetActive(true);
                StartCoroutine(IEFinishGame());
            }

        }
    }

    IEnumerator IEFinishGame()
    {
        yield return new WaitForSeconds(5);
        FinishGame();
    }

    public void FinishGame()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(1);
    }

    public void OnPlayerEnteredRoom(Player newPlayer)
    {
    }

    public void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.CountOfPlayersInRooms <= 1)
        {
            GameFinished.SetActive(true);
            StartCoroutine(IEFinishGame());
        }
    }

    public void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
    }

    public void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
    }

    public void OnMasterClientSwitched(Player newMasterClient)
    {

    }
}
