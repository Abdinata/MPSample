using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCanvasManager : MonoBehaviour
{

    public static RoomCanvasManager instance;

    [SerializeField]
    private GameObject _lobbyCanvas;

    public GameObject LobbyCanvas
    {
        get { return _lobbyCanvas; }
    }


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
