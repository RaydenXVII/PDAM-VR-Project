using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class QuizData
    {
        public string question;
        public string[] options;
        public int correctAnswerIndex;
    }

    public QuizData[] quizList;
    private int currentQuizIndex = 0;

    [Header("Main Quiz UI")]
    public GameObject quizPanel;
    public TextMeshProUGUI questionText;
    public Button[] optionButtons;

    [Header("Feedback Panels")]
    public GameObject quizRightPanel;
    public GameObject quizWrongPanel;

    void Start()
    {
        quizPanel.SetActive(false);
        quizRightPanel.SetActive(false);
        quizWrongPanel.SetActive(false);
    }

    public void ShowQuiz()
    {
        if (currentQuizIndex < quizList.Length)
        {
            quizPanel.SetActive(true);
            quizRightPanel.SetActive(false);
            quizWrongPanel.SetActive(false);
            DisplayQuiz(currentQuizIndex);
        }
        else
        {
            Debug.LogWarning("No more quizzes available.");
        }
    }

    void DisplayQuiz(int index)
    {
        QuizData quiz = quizList[index];
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
        bool isCorrect = chosenIndex == quizList[currentQuizIndex].correctAnswerIndex;

        quizPanel.SetActive(false);

        if (isCorrect)
        {
            quizRightPanel.SetActive(true);
        }
        else
        {
            quizWrongPanel.SetActive(true);
        }

        Invoke(nameof(NextStep), 2f); // tunggu 2 detik lalu lanjut
    }

    void NextStep()
    {
        quizRightPanel.SetActive(false);
        quizWrongPanel.SetActive(false);

        currentQuizIndex++;

        // Bisa lanjut video berikutnya atau panggil ShowQuiz lagi
        Debug.Log("Lanjut ke proses berikutnya...");
    }
}
