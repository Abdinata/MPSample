using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class LobbyCanvas : MonoBehaviour
{
    //public EnterPrivateCodeDialogController codeController;

    [SerializeField]
    private RoomLayout _roomLayout;

    public RoomLayout RoomLayout
    {
        get { return _roomLayout; }
    }

    private void Start()
    {
        //codeController = FindObjectOfType<EnterPrivateCodeDialogController>();
    }


    public void OnJoinRoomClick(string name)
    {
        PhotonNetwork.JoinRoom(name);
    }
}
