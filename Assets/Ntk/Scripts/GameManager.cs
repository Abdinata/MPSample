using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO : Add sample board & quiz
public enum MyBoardGameMode
{
    Solo, Multi
}

public enum GameType
{
    Runner, Quiz, Board
}

public class GameManager
{
    //--- GLOBAL
    public string selectedLevel = "test";
    //public string playerUseName = "";
    public GameType gameType = GameType.Runner;

    public ChatController chatController;

    public Sprite[] Avatar;
    public int avatarIndex = 999;
    public int maxPlayers = 4;

    //-- Runner Game
    public RunnerSpawner RunnerSpawner;
    public RunnerManager runnerManager;
    public List<GameObject> PlayersObject = new List<GameObject>();

    //-- Reset All Data
    public void resetAllData()
    {

    }

    private static GameManager instance;

    private GameManager() { }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    public inGameUI inGameUI;
}
