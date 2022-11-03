using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quiz : MonoBehaviour
{
    // UI based text
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;

    void Start()
    {
        // using the getter method from the scriptable object
        questionText.text = question.GetQuestion();
    }
}
