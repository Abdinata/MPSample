using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class LobbyNetwork : MonoBehaviourPunCallbacks, ILobbyCallbacks, IConnectionCallbacks, IMatchmakingCallbacks
{
    [SerializeField] GameObject uiBlockObject;
    [SerializeField] Text uiblockText;
    [SerializeField] Text playersText, roomsText, serverInfoText;

    [SerializeField] bool isInMaster, isInLobby = false;


    private void Awake()
    {
        Debug.Log("Connecting to services");
        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnected)
        {
            Connect();
        }
        //PhotonNetwork.ConnectToBestCloudServer();
    }




    public override void OnConnected()
    {
        Debug.Log("Connected");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created");
        base.OnCreatedRoom();
    }

    public override void OnJoinedRoom()
    {
        MainLobbyCanvas.Instance.RoomCanvas.PlayerListLayout.OnJoinedRoom();

        GameManager.Instance.chatController.Connect();
        GameManager.Instance.chatController.ChannelsToJoinOnConnect[1] = PhotonNetwork.CurrentRoom.Name; //Channel name
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        MainLobbyCanvas.Instance.RoomCanvas.PlayerListLayout.OnPlayerEnteredRoom(newPlayer);
        base.OnPlayerEnteredRoom(newPlayer);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        uiblockText.text = "Done. Connecting to Lobby..";
        isInMaster = true;

        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.NickName = "Player" + Random.Range(100, 1000);
        Debug.Log(PhotonNetwork.NickName);
        //GameManager.Instance.playerUseName = PhotonNetwork.NickName.ToString();

        PhotonNetwork.JoinLobby(TypedLobby.Default);

        UpdateStats();
    }

    //Update lobby statistic
    void UpdateStats()
    {
        playersText.text = PhotonNetwork.CountOfPlayers.ToString();
        roomsText.text = PhotonNetwork.CountOfRooms.ToString();
        serverInfoText.text = PhotonNetwork.Server.ToString();
    }

    public override void OnCustomAuthenticationFailed(string debugMessage)
    {
        Debug.Log(debugMessage);
    }

    public override void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
        throw new System.NotImplementedException();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected " + cause.ToString());
        StartCoroutine(IEConnect());
    }

    [SerializeField] RoomLayout roomLayout;
    public override void OnJoinedLobby()
    {
        Debug.Log("joining lobby");
        isInLobby = PhotonNetwork.InLobby;
        uiblockText.text = "Heading to lobby";

        StartCoroutine(CloseUIBlock());

        if (!PhotonNetwork.InRoom)
        {
            MainLobbyCanvas.Instance.LobbyCanvas.transform.SetAsLastSibling();
        }

    }

    //Close UI block
    IEnumerator CloseUIBlock()
    {
        yield return new WaitForSeconds(2.5f);
        uiBlockObject.SetActive(false);

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        MainLobbyCanvas.Instance.RoomCanvas.PlayerListLayout.OnPlayerLeftRoom(otherPlayer);
        base.OnPlayerLeftRoom(otherPlayer);
    }

    public override void OnLeftLobby()
    {
    }

    public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        UpdateStats();
    }

    public override void OnRegionListReceived(RegionHandler regionHandler)
    {
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        MainLobbyCanvas.Instance.LobbyCanvas.RoomLayout.OnRoomListUpdate(roomList);
        Debug.Log("there's " + roomList.Count + " room(s)");
        UpdateStats();
    }

    void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Joining Room...");
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
        }
        else
        {

            Debug.Log("Connecting...");
            PhotonNetwork.ConnectUsingSettings();
            // #Critical, we must first and foremost connect to Photon Online Server.
            StartCoroutine(IEConnect());
            //PhotonNetwork.GameVersion = this.gameVersion;
        }
    }

    System.Collections.IEnumerator IEConnect()
    {
        yield return new WaitForSeconds(2);
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("Can't connect, retrying...");
            Connect();
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
        }
        else
        {
            Debug.Log("Connected...");
        }
    }

    public void SetRoomState(bool state)
    {
        PhotonNetwork.CurrentRoom.IsOpen = state;
        PhotonNetwork.CurrentRoom.IsVisible = state;
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        PhotonNetwork.LeaveRoom();
        base.OnMasterClientSwitched(newMasterClient);
    }

}
