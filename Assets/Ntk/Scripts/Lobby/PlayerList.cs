using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
public class PlayerList : MonoBehaviour
{
    public Player photonPlayer { get; private set; }

    [SerializeField] Text playerNameText;
    [SerializeField] Image avatar;

    private Text PlayerNameText
    {
        get { return playerNameText; }

    }

    public void ApplyPlayer(Player player)
    {
        photonPlayer = player;

        playerNameText.text = player.NickName;

        int avatarIndex = (int)player.CustomProperties["AvatarIndex"];

        avatar.sprite = GameManager.Instance.Avatar[avatarIndex];

        //Wish i had times to implement avatar system instead randomize it

        //ExitGames.Client.Photon.Hashtable
    }

}
