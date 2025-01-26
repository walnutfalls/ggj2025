using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : SingletonBase<GameDirector>
{
    const int MainMenuSceneIndex = 0;

    const int SampleSceneIndex = 1;

    public static event Action OnGameOver;

    public static event Action OnGameWon;

    public void CollectHat(HatScriptable hat)
    {
        StatsTracker.Instance.OnHatCollected(hat);

        if (StatsTracker.Instance.AllUnlocked)
        {
            GameWon();
        }
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

    public void GameWon()
    {
        OnGameWon?.Invoke();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(MainMenuSceneIndex);
    }

    public void StartNewGame()
    {
        StatsTracker.Instance.OnGameStarted();
        SceneManager.LoadScene(SampleSceneIndex);
    }
}
