using UnityEngine;
using UnityEngine.Video;

public class VideoQuizConnector : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Drag VideoPlayer object here
    public QuizManager quizManager; // Drag QuizManager object here

    void Start()
    {
        // Subscribe ke event selesai video
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Tampilkan quiz ketika video selesai
        quizManager.ShowQuiz();
    }
}
