using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    // adjust text area of of the variables seen in the inspector. 2 params, min and max lines.
    [TextArea(2, 6)]    
    [SerializeField] string question = "Enter new question text here";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctIndex;
    // getter functions to get question, correct index, and answer text in other scripts
    public string GetQuestion(){
        return question;
    }
    
    public int GetCorrectIndex(){
        return correctIndex;
    }

    public string GetAnswer(int index){
        return answers[index];
    }

}
