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

    void Start()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            CustomeValue = new ExitGames.Client.Photon.Hashtable();
            startTime = PhotonNetwork.Time;
            startTimer = true;
            CustomeValue.Add("StartTime", startTime);
            PhotonNetwork.CurrentRoom.SetCustomProperties(CustomeValue);
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

    }

    [SerializeField] Text countText;
    [SerializeField] GameObject countObject, startObject, colliders;
    [SerializeField] Image roundFill;

    void Update()
    {

        if (!startTimer) return;

        timerIncrementValue = PhotonNetwork.Time - startTime;
        countText.text = Mathf.Round((float)timerIncrementValue).ToString();
        roundFill.fillAmount= ((float) startTime / (float)timerIncrementValue);

        if (timerIncrementValue >= timer)
        {
            countObject.SetActive(false);
            startObject.SetActive(true);
            colliders.SetActive(false);
            //Timer Completed
            //Do What Ever You What to Do Here
        }
    }


}
