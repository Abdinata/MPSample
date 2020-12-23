using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLobbyCanvas : MonoBehaviour
{

    public static MainLobbyCanvas Instance;

    [HideInInspector] public LobbyNetwork lobbyNetwork;
    [SerializeField] private LobbyCanvas lobbyCanvas;

    public LobbyCanvas LobbyCanvas
    {
        get { return lobbyCanvas; }
    }

    [SerializeField]
    private RoomCanvas roomCanvas;
    public RoomCanvas RoomCanvas
    {
        get { return roomCanvas; }
    }

    private void Awake()
    {
        Instance = this;
        lobbyNetwork = FindObjectOfType<LobbyNetwork>();
    }
}
