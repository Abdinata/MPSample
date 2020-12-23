using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class CreateRoom : MonoBehaviour
{

    [SerializeField] InputField roomName;
    private LoadBalancingClient loadBalancingClient;

    private void Awake()
    {
        loadBalancingClient = PhotonNetwork.NetworkingClient;
        //PhotonNetwork.ConnectToMaster(PhotonNetwork.PhotonServerSettings.AppSettings.Server,
        //    PhotonNetwork.PhotonServerSettings.AppSettings.Port,
        //    PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime);
    }

    public void OnCreateRoomClick()
    {
        if (roomName == null)
            return;

        RoomOptions room = new RoomOptions() { IsOpen = true, IsVisible = true, MaxPlayers = (byte)GameManager.Instance.maxPlayers };
        Debug.Log("is in a room " + PhotonNetwork.InRoom);

        PhotonNetwork.CreateRoom(roomName.text, room, TypedLobby.Default);
    }

    
}
