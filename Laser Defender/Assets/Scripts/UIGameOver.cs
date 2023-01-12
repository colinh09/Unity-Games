using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    void Awake(){
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        if (scoreKeeper == null){
            Debug.Log("Shit, something went wrong.");
        }
    }

    void Start()
    {
        scoreText.text = "Your Score:\n" + scoreKeeper.GetCurrentScore().ToString();
        scoreKeeper.ResetScore();
    }

}
