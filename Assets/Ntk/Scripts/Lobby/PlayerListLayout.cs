using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class PlayerListLayout : MonoBehaviour
{
    [SerializeField] GameObject playerListPrefab;
    private GameObject PlayerListPrefab
    {
        get { return playerListPrefab; }
    }

    private List<PlayerList> playerLists = new List<PlayerList>();

    private List<PlayerList> PlayerLists
    {
        get { return playerLists; }
    }

    public void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        Player[] players = PhotonNetwork.PlayerList;
        for(int i = 0; i < players.Length; i++)
        {
            PlayerJoinedRoom(players[i]);
        }
    }

    public void OnPhotonPlayerDisconnected(Player player)
    {
        Debug.Log("X");
        PlayerLeftRoom(player);
    }

    public void PlayerJoinedRoom(Player player)
    {
        MainLobbyCanvas.Instance.RoomCanvas.transform.SetAsLastSibling();

        if (player == null)
            return;

        PlayerLeftRoom(player);

        GameObject playerObject = Instantiate(playerListPrefab, transform);

        PlayerList playerlist = playerObject.GetComponent<PlayerList>();

        playerlist.ApplyPlayer(player);

        playerLists.Add(playerlist);
    }

    public void PlayerLeftRoom(Player player)
    {
        int index = PlayerLists.FindIndex(x => x.photonPlayer == player);

        if(index != -1)
        {
            Destroy(PlayerLists[index].gameObject);
            PlayerLists.RemoveAt(index);
        }

    }

    public void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerJoinedRoom(newPlayer);
    }

    public void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("X");
        PlayerLeftRoom(otherPlayer);
    }
}
