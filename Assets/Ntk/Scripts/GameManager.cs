using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MyBoardGameMode
{
    Solo, Multi
}

public enum GameType
{
    Runner, Quiz
}

public class GameManager
{
    //--- GLOBAL
    public string selectedLevel = "test";
    public string playerUseName = "";
    public GameType gameType = GameType.Runner;
    //--- QuizGame Variables
    public bool isMyQuizTurn = false;
    public bool myQuizTurnDone = false;

    public int maxQuizPlayers = 2;
    //--- Quiz End

    //Runner Game
    public RunnerSpawner RunnerSpawner;
    public RunnerManager runnerManager;
    public List<GameObject> PlayersObject = new List<GameObject>();

    //--- BoardGame Variables
    public string[] PlayersIDs;
    public bool myTurnDone = false;

    public MyBoardGameMode boardGameMode;
    public bool isMyTurn = false;
    public bool diceShot = false;
    //--- Board End
    //-- Reset All Data
    public void resetAllData()
    {

    }




    //----
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
}
