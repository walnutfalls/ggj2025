using UnityEngine;

public class GameOverUi : MonoBehaviour
{
    [SerializeField]
    public GameObject popup;

    [SerializeField]
    public GameObject victoryContainer;

    [SerializeField]
    public GameObject lossContainer;

    protected void Start()
    {
        GameDirector.OnGameOver += this.ShowGameOver;
        GameDirector.OnGameWon += this.ShowGameWon;
    }

    public void ShowGameOver()
    {
        this.victoryContainer.SetActive(false);
        this.lossContainer.SetActive(true);
        this.ShowPopup();
    }

    public void ShowGameWon()
    {
        this.victoryContainer.SetActive(true);
        this.lossContainer.SetActive(false);
        this.ShowPopup();
    }

    private void ShowPopup()
    {
        this.popup.SetActive(true);
    }
}
