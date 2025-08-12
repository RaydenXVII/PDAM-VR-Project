using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuizManager : MonoBehaviour
{
    public QuizQuestions quizQuestions;
    private List<QuizDatabase> shuffledQuizList;
    private int currentQuizIndex = 0;

    private int correctCount = 0; // jumlah benar
    private int wrongCount = 0;   // jumlah salah

    [Header("Main Quiz UI")]
    public GameObject quizPanel;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI questionNumberText; // <-- label "Question X"
    public Button[] optionButtons;

    [Header("Feedback Panels")]
    public GameObject quizRightPanel;
    public GameObject quizWrongPanel;

    [Header("Result Panel")]
    public GameObject resultPanel;
    public TextMeshProUGUI resultText; // untuk menampilkan hasil benar/salah

    void Start()
    {
        quizPanel.SetActive(false);
        quizRightPanel.SetActive(false);
        quizWrongPanel.SetActive(false);
        resultPanel.SetActive(false);

        shuffledQuizList = new List<QuizDatabase>(quizQuestions.quizzes);
        ShuffleQuiz(shuffledQuizList);
    }

    public void ShowQuiz()
    {
        if (currentQuizIndex < shuffledQuizList.Count)
        {
            quizPanel.SetActive(true);
            quizRightPanel.SetActive(false);
            quizWrongPanel.SetActive(false);
            DisplayQuiz(currentQuizIndex);
        }
        else
        {
            ShowResult();
        }
    }

    void DisplayQuiz(int index)
    {
        QuizDatabase quiz = shuffledQuizList[index];

        // Ubah label nomor pertanyaan
        questionNumberText.text = $"Question {index + 1}";

        questionText.text = quiz.question;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            int capturedIndex = i;
            optionButtons[i].gameObject.SetActive(i < quiz.options.Length);
            optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = quiz.options[i];
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => CheckAnswer(capturedIndex));
        }
    }

    void CheckAnswer(int chosenIndex)
    {
        bool isCorrect = chosenIndex == shuffledQuizList[currentQuizIndex].correctAnswerIndex;

        quizPanel.SetActive(false);

        if (isCorrect)
        {
            correctCount++;
            quizRightPanel.SetActive(true);
        }
        else
        {
            wrongCount++;
            quizWrongPanel.SetActive(true);
        }

        Invoke(nameof(NextStep), 2f);
    }

    void NextStep()
    {
        quizRightPanel.SetActive(false);
        quizWrongPanel.SetActive(false);

        currentQuizIndex++;

        if (currentQuizIndex < shuffledQuizList.Count)
        {
            ShowQuiz();
        }
        else
        {
            ShowResult();
        }
    }

    void ShowResult()
    {
        resultPanel.SetActive(true);
        resultText.text = $"Quiz Selesai!\nBenar: {correctCount}\nSalah: {wrongCount}";
        Debug.Log($"Benar: {correctCount}, Salah: {wrongCount}");
    }

    void ShuffleQuiz(List<QuizDatabase> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            QuizDatabase temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
