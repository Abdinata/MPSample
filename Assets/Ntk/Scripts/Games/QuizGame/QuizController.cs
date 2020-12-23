using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

namespace Ntk.Games.Quiz
{
    public class QuizController : MonoBehaviour
    {

        [SerializeField] Button[] answersButton;
        [SerializeField] Text questionText;
        [SerializeField] Text[] answersText;
        [SerializeField] List<Question> questions;
        [SerializeField] Animator quizAnimator;


        private void Awake()
        {
            var qTemp = questions.ToArray();
            for(int i = 0; i <qTemp.Length; i++)
            {

            }
        }

        void AskQuestion()
        {
            var qTemp = questions.ToArray();
            for (int i = 0; i < answersButton.Length; i++)
            {
                //answersButton[i].onClick.AddListener();
            }
        }

        void OnAnswer(int index)
        {

        }

    }
}

[System.Serializable]
public class Question
{
    public int id;
    public string question;
    public Answer[] answer;
}

[System.Serializable]
public class Answer
{
    public bool isCorrect = false;
    public string answer;
}
