using UnityEngine;
using Photon.Pun;

public class RoomCanvas : MonoBehaviour
{
    [SerializeField] PlayerListLayout playerListLayout;

    public PlayerListLayout PlayerListLayout { get { return playerListLayout; } }
    
    public void ChangeRoomStateClick(bool state)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        Debug.Log(state);
        MainLobbyCanvas.Instance.lobbyNetwork.SetRoomState(state);

    }

    public void LeaveRoomClick()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void OnClickLoadLevelSync()
    {
        if (!PhotonNetwork.IsMasterClient) { return; }

        PhotonNetwork.LoadLevel(GameManager.Instance.selectedLevel);
    }

    public void OnClickLoadLevelStart()
    {
        if (!PhotonNetwork.IsMasterClient) { return; }

        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel(GameManager.Instance.selectedLevel);
    }

}
