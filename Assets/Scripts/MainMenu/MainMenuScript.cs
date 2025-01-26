using UnityEditor;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Canvas HowToPlayCanvas;

    [SerializeField] private Canvas MainMenuCanvas;

    //Used by "Play" button
    public void PlayButton()
    {
        this.PlayClickSound();
        GameDirector.Instance.StartNewGame();
    }

    public void QuitGame()
    {
        this.PlayClickSound();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif

        Application.Quit();
    }

    //Only works if attached to MainMenuCanvas!
    public void HowToPlayButton()
    {
        this.PlayClickSound();
        HowToPlayCanvas.enabled = true;
        MainMenuCanvas.enabled = false;
    }

    private void PlayClickSound()
    {
        AudioSystem.Instance.PlaySound("Click");
    }
}
