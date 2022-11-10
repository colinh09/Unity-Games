using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    // using header to break up variables for better readability in inspector
    [Header("Questions")]
    // UI based text
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questionList = new List<QuestionSO>();
    QuestionSO question;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctIndex;
    bool answeredEarly;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;

    [Header("Timer")]
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scorekeeper;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        scorekeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questionList.Count;
        progressBar.value = 0;
    }

    void Update(){
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion){
            answeredEarly = false;
            getNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!answeredEarly && !timer.isAnswering){
            displayAnswer(-1);
            SetButtonState(false);
        }
    }

    private void getNextQuestion(){
        if (questionList.Count > 0){
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            scorekeeper.IncrementQuestionsSeen();
        }
    }

    private void GetRandomQuestion(){
        int index = Random.Range(0, questionList.Count);
        question = questionList[index];
        if (questionList.Contains(question)){
            questionList.Remove(question);
        }
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

    public void displayAnswer(int index){
        // match index of answer chosen with the correct answer. If correct, output text and change
        // current image of the button to a correct answer sprite
        if (index == question.GetCorrectIndex()){
            questionText.text = "Correct";
            Image buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scorekeeper.IncrementCorrectAnswers();
        } else {
            int correctIndex = question.GetCorrectIndex();
            questionText.text = "Incorrect. The correct answer was " + question.GetAnswer(correctIndex);
            Image buttonImage = answerButtons[correctIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;                        
        }        
    }

    public void OnAnswerSelected(int index){
        answeredEarly = true;
        displayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scorekeeper.CalculateScore() + "%";

        if (progressBar.value == progressBar.maxValue){
            isComplete = true;
        }
    }

    private void SetDefaultButtonSprites(){
        for (int i = 0; i < answerButtons.Length; i++){
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;                 
        }                       
    }
}
