using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSpawner : MonoBehaviour
{

    public Transform[] spawnLoc;

    private void Awake()
    {
        GameManager.Instance.RunnerSpawner = this;
    }

    public void RepositionPlayer()
    {
        for(int i =0; i < GameManager.Instance.PlayersObject.Count; i ++)
        {
            if(i > spawnLoc.Length)
            {

            }
            else
            {
               // GameManager.Instance.PlayersObject[i].transform.position = spawnLoc[i];
            }
        }

    }
}
