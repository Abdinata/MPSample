using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerID : MonoBehaviour
{
    public static PlayerID Instance;

    public GameObject playerRunnerPrefab;

    #region Player Data Variables

    public int avatarIndex = 0;
    public string playerName = "";
    public int playerID = 0;

    #endregion

    #region Backend API Links

    public string backendMainDomain, backendLogin, backendRegister;


    #endregion


    #region Scene Management

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode sceneMode)
    {
        if(GameManager.Instance.selectedLevel == "Run")
        {
            if(PhotonNetwork.IsMasterClient)
            {
                RoomMasterLoad();
            }
            else
            {
                NonRoomMasterLoad();
            }
        }
    }



    #endregion

    PhotonView photonView;

    private void Awake()
    {
        if (PlayerID.Instance != null)
            Destroy(this);

        SceneManager.LoadScene(1);

        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;

        photonView = GetComponent<PhotonView>();

        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;

        if (GameManager.Instance.avatarIndex == 999)
        {
            GameManager.Instance.avatarIndex = Random.Range(0, GameManager.Instance.Avatar.Length);
            ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
            hash.Add("AvatarIndex", GameManager.Instance.avatarIndex);
        }
    }

    void RoomMasterLoad()
    {
        //playersInGame = 1;
        Debug.Log("Calling RPC");
        //SceneLoaded();
        photonView.RPC("RPC_SceneLoaded", RpcTarget.MasterClient);
        photonView.RPC("RPC_LoadLevelOther", RpcTarget.Others, new object[] { (string)GameManager.Instance.selectedLevel });
    }

    void NonRoomMasterLoad()
    {
        Debug.Log("Calling RPC");
        photonView.RPC("RPC_SceneLoaded", RpcTarget.MasterClient);
    }

    [PunRPC]
    public void RPC_LoadLevelOther(string levelString)
    {
        Debug.Log("Level RPC loaded");
        PhotonNetwork.LoadLevel(levelString);
    }

    public int playersInGame = 0;

    [SerializeField] bool testSingle = false;

    [PunRPC]
    public void RPC_SceneLoaded()
    {
        Debug.Log("RPC called, ismaster ? " + PhotonNetwork.IsMasterClient);
        SceneLoaded();
    }

    void SceneLoaded()
    {
        Debug.Log("Loading, ismaster " + PhotonNetwork.IsMasterClient);
        playersInGame++;
        if (playersInGame == PhotonNetwork.PlayerList.Length || testSingle == true)
        {
            Debug.Log("all players are in game");

            if (GameManager.Instance.gameType == GameType.Runner)
            {
                photonView.RPC("SpawnPlayerRunner", RpcTarget.All, new object[] { playersInGame - 1 });
            }
            //if (PhotonNetwork.IsMasterClient)
            //{
            //    StartCoroutine(IEStartGame());
            //}
        }
    }

    [PunRPC]
    public void SpawnPlayerRunner(int spawnIndex)
    {
        Player p = PhotonNetwork.LocalPlayer;
        Debug.Log(spawnIndex +", " + p.ActorNumber);

        List<Player> playerList = new List<Player>(); //Temporary List

        for(int i = 0; i < PhotonNetwork.CountOfPlayers; i++)
        {
            playerList.Add(PhotonNetwork.PlayerList[i]);
        }

        int orderActor = 0;

        // Add Actor order for spawning
        if (playerList.Count > 0)
        {
            playerList.OrderBy(x => x.ActorNumber).ToList();
        }
        //Make sure we have the correct order
        for(int i = 0; i<playerList.Count; i++)
        {
            if(playerList[i].ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                orderActor = i;
            }
        }

        Vector3 t;
        Quaternion q;

        if (playersInGame <= GameManager.Instance.RunnerSpawner.spawnLoc.Length)
        {
            t = new Vector3(GameManager.Instance.RunnerSpawner.spawnLoc[orderActor].position.x, GameManager.Instance.RunnerSpawner.spawnLoc[orderActor].position.y,
                 GameManager.Instance.RunnerSpawner.spawnLoc[orderActor].position.z);

            q = GameManager.Instance.RunnerSpawner.spawnLoc[orderActor].transform.rotation;

            Debug.Log(t + ", " + q);
        }
        else
        {
            //Update : Change the index of spawnlocation to [orderActor], it shouldn't be an issue now on spawning

            t = new Vector3(GameManager.Instance.RunnerSpawner.spawnLoc[orderActor].position.x, GameManager.Instance.RunnerSpawner.spawnLoc[orderActor].position.y,
                GameManager.Instance.RunnerSpawner.spawnLoc[orderActor].position.z + -2.5f);

            q = GameManager.Instance.RunnerSpawner.spawnLoc[orderActor].transform.rotation;
            Debug.Log(t + ", " + q);
        }

        PhotonNetwork.Instantiate(Path.Combine("Prefabs","Runner"), t, q, 0);

    }

}
