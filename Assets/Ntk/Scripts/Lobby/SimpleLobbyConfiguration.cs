using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLobbyConfiguration : MonoBehaviour
{
    public GameType gameType = GameType.Quiz;
    public string gameScene = "gamescene";
    private void Awake()
    {
        GameManager.Instance.gameType = gameType;
        GameManager.Instance.selectedLevel = gameScene;
        Debug.Log(GameManager.Instance.selectedLevel);
        if(GameManager.Instance.selectedLevel != gameScene)
            GameManager.Instance.selectedLevel = gameScene;
        Debug.Log(GameManager.Instance.selectedLevel + ", " + gameScene);
    }
}
