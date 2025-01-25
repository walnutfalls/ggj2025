using UnityEngine;

public class HowToPlayMenu : MonoBehaviour
{
    [SerializeField] private Canvas MainMenuCanvas;
    [SerializeField] private Canvas HowToPlayCanvas;

    public void Close()
    {
        MainMenuCanvas.enabled = true;
        HowToPlayCanvas.enabled = false;
    }
}
