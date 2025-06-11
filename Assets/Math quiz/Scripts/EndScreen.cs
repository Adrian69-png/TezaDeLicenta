using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {

        scoreKeeper = FindObjectOfType<ScoreKeeper>();

    }

    public void ShowFinalScore()
    {
        finalText.text = "Congratulations! \n Your score: " +
                        scoreKeeper.CalculateScore() + "%";
    }

}
