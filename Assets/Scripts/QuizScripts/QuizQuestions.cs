using UnityEngine;

[CreateAssetMenu(fileName = "QuizQuestions", menuName = "Scriptable Objects/QuizQuestions")]
public class QuizQuestions : ScriptableObject
{
    public QuizDatabase[] quizzes;
}
