using UnityEngine;

[CreateAssetMenu(fileName = "QuizDatabase", menuName = "Scriptable Objects/QuizDatabase")]
public class QuizDatabase : ScriptableObject
{
    [TextArea]
    public string question;        // Pertanyaan
    public string[] options;       // Pilihan jawaban
    public int correctAnswerIndex; // Index jawaban benar (0-based)
}
