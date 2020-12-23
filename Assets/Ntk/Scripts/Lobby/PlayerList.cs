using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
public class PlayerList : MonoBehaviour
{
    public Player photonPlayer { get; private set; }

    [SerializeField] Text playerNameText;

    private Text PlayerNameText
    {
        get { return playerNameText; }

    }

    public void ApplyPlayer(Player player)
    {
        photonPlayer = player;

        playerNameText.text = player.NickName;
    }

}
