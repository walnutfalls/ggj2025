
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : SingletonBase<GameDirector>
{
    const int SampleSceneIndex = 1;

    public void CollectHat(HatScriptable hat)
    {
        StatsTracker.Instance.OnHatCollected(hat);
    }

    public void GameOver()
    {
    }

    public void GameWon()
    {
    }

    public void StartNewGame()
    {
        StatsTracker.Instance.OnGameStarted();
        SceneManager.LoadScene(SampleSceneIndex);
    }
}
