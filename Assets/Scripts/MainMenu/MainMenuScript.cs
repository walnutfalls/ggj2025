using UnityEditor;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Canvas HowToPlayCanvas;

    [SerializeField] private Canvas MainMenuCanvas;

    //Used by "Play" button
    public void PlayButton()
    {
        GameDirector.Instance.StartNewGame();
    }

    public void QuitGame()
    {
        EditorApplication.ExitPlaymode();
        Application.Quit();
    }

    //Only works if attached to MainMenuCanvas!
    public void HowToPlayButton()
    {
        HowToPlayCanvas.enabled = true;
        MainMenuCanvas.enabled = false;
    }
}
