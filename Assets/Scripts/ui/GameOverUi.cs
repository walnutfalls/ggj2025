using System.Security.Cryptography;
using UnityEngine;

public class GameOverUi : MonoBehaviour
{
    [SerializeField]
    private GameObject popup;

    [SerializeField]
    private GameObject victoryContainer;

    [SerializeField]
    private GameObject lossContainer;

    protected void Start()
    {
        this.popup.SetActive(false);
        GameDirector.OnGameOver += this.ShowGameOver;
        GameDirector.OnGameWon += this.ShowGameWon;
    }

    protected void OnDestroy()
    {
        GameDirector.OnGameOver -= this.ShowGameOver;
        GameDirector.OnGameWon -= this.ShowGameWon;
    }

    public void OnClickMainMenu()
    {
        GameDirector.Instance.MainMenu();
    }

    public void OnClickTryAgain()
    {
        GameDirector.Instance.StartNewGame();
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
