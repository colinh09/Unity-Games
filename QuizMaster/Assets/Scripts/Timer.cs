using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToComplete = 30f;
    [SerializeField] float timetoShowCorrect = 10f;

    public bool isAnswering = true;
    public float fillFraction;
    public bool loadNextQuestion = true;

    float timerValue;

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer(){
        timerValue = 0;
    }

    private void UpdateTimer(){
        timerValue -= Time.deltaTime;

        if (isAnswering){
            if (timerValue > 0){
                fillFraction = timerValue / timeToComplete;
            } else{
                isAnswering = false;
                timerValue = timetoShowCorrect;
            }
        } else {
            if (timerValue > 0){
                fillFraction = timerValue / timetoShowCorrect;
            } else{
                timerValue = timeToComplete;
                isAnswering = true;
                loadNextQuestion = true;
            }
        }
    }
}
