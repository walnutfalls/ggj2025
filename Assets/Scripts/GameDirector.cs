public class GameDirector : SingletonBase<GameDirector>
{
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
    }
}
