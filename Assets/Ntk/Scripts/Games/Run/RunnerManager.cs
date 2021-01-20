using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunnerManager : MonoBehaviour
{
    bool startTimer = false;
    double timerIncrementValue;
    double startTime;
    [SerializeField] double timer = 10;
    ExitGames.Client.Photon.Hashtable CustomeValue;

    [SerializeField] double roundTimer = 90;
    ExitGames.Client.Photon.Hashtable RoundCustomeValue;

    double roundTime;

    void Start()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            CustomeValue = new ExitGames.Client.Photon.Hashtable();
            startTime = PhotonNetwork.Time;
            startTimer = true;
            CustomeValue.Add("StartTime", startTime);
            PhotonNetwork.CurrentRoom.SetCustomProperties(CustomeValue);

            RoundCustomeValue = new ExitGames.Client.Photon.Hashtable();
            //startTime = PhotonNetwork.Time;
            //startTimer = true;
            RoundCustomeValue.Add("RoundStartTime", startTime);
            PhotonNetwork.CurrentRoom.SetCustomProperties(RoundCustomeValue);
        }
        else
        {
            StartCoroutine(CheckTime());
        }
    }

    IEnumerator CheckTime()
    {
        yield return new WaitForSeconds(0.2f);
        startTime = double.Parse(PhotonNetwork.CurrentRoom.CustomProperties["StartTime"].ToString());
        startTimer = true;

        roundTime = double.Parse(PhotonNetwork.CurrentRoom.CustomProperties["RoundStartTime"].ToString());
    }

    [SerializeField] Text countText, roundCountText;
    [SerializeField] GameObject countObject, roundCountObject,startObject, colliders;
    [SerializeField] Image roundFill;

    bool isCountingRound = false;

    float roundTimesIncrement, startRoundTime;
    void Update()
    {
        if(!isCountingRound)
        {
            if (!startTimer) return;

            timerIncrementValue = PhotonNetwork.Time - startTime;
            float roundTime = Mathf.Round((float)timer) - Mathf.Round((float)timerIncrementValue);
            countText.text = Mathf.Round(roundTime).ToString();
            roundFill.fillAmount = ((float)timer / (float)timerIncrementValue);

            if (timerIncrementValue >= timer)
            {
                countObject.SetActive(false);
                startObject.SetActive(true);
                colliders.SetActive(false);
                //Timer Completed
                //Do What Ever You What to Do Here

                roundCountObject.SetActive(true);
                startRoundTime = (float) PhotonNetwork.Time;
                isCountingRound = true;

            }
        }
        else
        {
            roundTimesIncrement = (float) PhotonNetwork.Time - startRoundTime;
            float roundTime = Mathf.Round((float)roundTimer) - Mathf.Round((float)roundTimesIncrement);

            roundCountText.text = Mathf.Round(roundTime).ToString();

            if (roundTimesIncrement >= roundTimer)
            {
                countObject.SetActive(false);
                startObject.SetActive(true);
                colliders.SetActive(false);
                //Timer Completed
                //Do What Ever You What to Do Here

                //TODO : GAMEOVER
            }
        }

        
    }

    public void StartRoundTime()
    {

    }

}
