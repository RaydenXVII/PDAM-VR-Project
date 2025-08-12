using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;

    void Start()
    {
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument not assigned in Inspector!");
            return;
        }

        var root = uiDocument.rootVisualElement;

        var playButton = root.Q<Button>("playButton");
        var quitButton = root.Q<Button>("quitButton");

        if (playButton == null || quitButton == null)
        {
            Debug.LogError("Buttons not found in UXML. Check the names!");
            return;
        }

        playButton.RegisterCallback<ClickEvent>(evt => SceneManager.LoadScene("Scene1"));
        quitButton.RegisterCallback<ClickEvent>(evt => Application.Quit());
    }
}
