using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private Text _roomNameText;

    [SerializeField]
    private Text _roomPlayers;

    private Text RoomNameText
    {
        get { return _roomNameText; }
    }

    private Text RoomPlayers
    {
        get { return _roomPlayers; }
    }

    public string RoomName { get; private set; }

    public bool Updated { get; set; }


    private void Start()
    {
        GameObject lobbyCanvas = MainLobbyCanvas.Instance.LobbyCanvas.gameObject;
        if (lobbyCanvas == null)
            return;

        LobbyCanvas lobby = lobbyCanvas.GetComponent<LobbyCanvas>();

        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(() => lobby.OnJoinRoomClick(_roomNameText.text));
        btn.onClick.AddListener(() => HideRoomLayout());
    }

    void HideRoomLayout()
    {
        //FindObjectOfType<MainLobbyCanvas>().gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Button btn = GetComponent<Button>();

        btn.onClick.RemoveAllListeners();
    }

    public void SetRoomNameText(string text)
    {
        RoomName = text;
        RoomNameText.text = text;
    }
    public void SetPlayerCount(string playerCount)
    {
        RoomPlayers.text = playerCount;
    }

}
