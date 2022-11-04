using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    // UI based text
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;
    [SerializeField] GameObject[] answerButtons;
    int correctIndex;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    void Start()
    {
        getNextQuestion();
    }

    private void getNextQuestion(){
        SetButtonState(true);
        SetDefaultButtonSprites();
        DisplayQuestion();
    }

    private void DisplayQuestion(){
        // using the getter method from the scriptable object
        questionText.text = question.GetQuestion();
        // populate answer buttons with the proper text (which is in the scriptable question object)
        for (int i = 0; i < answerButtons.Length; i++){
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>(); 
            buttonText.text = question.GetAnswer(i);
        }        
    }

    private void SetButtonState(bool state){
        for (int i = 0; i < answerButtons.Length; i++){
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    public void OnAnswerSelected(int index){
        // match index of answer chosen with the correct answer. If correct, output text and change
        // current image of the button to a correct answer sprite
        if (index == question.GetCorrectIndex()){
            questionText.text = "Correct";
            Image buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        } else {
            int correctIndex = question.GetCorrectIndex();
            questionText.text = "Incorrect. The correct answer was " + question.GetAnswer(correctIndex);
            Image buttonImage = answerButtons[correctIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;                        
        }
        SetButtonState(false);
    }

    private void SetDefaultButtonSprites(){
        for (int i = 0; i < answerButtons.Length; i++){
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;                 
        }                       
    }
}
