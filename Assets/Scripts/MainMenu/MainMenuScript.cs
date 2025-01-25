using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Canvas HowToPlayCanvas;
    [SerializeField] private Canvas MainMenuCanvas;
    
    //Used by "Play" button
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //Only works if attached to MainMenuCanvas!
    public void HowToPlayButton()
    {
        HowToPlayCanvas.enabled = true;
        MainMenuCanvas.enabled = false;
    }
}
