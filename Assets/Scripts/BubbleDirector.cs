public class BubbleDirector : SingletonBase<BubbleDirector>
{
    public int BubblesAliveCount { get; private set; } = 0;

    public int BubblesPoppedCount { get; private set; } = 0;

    public void OnBubblePopped()
    {
        this.BubblesAliveCount--;
        this.BubblesPoppedCount++;

        if (this.BubblesAliveCount == 0)
        {
            GameDirector.Instance.GameOver();
        }
    }

    public void OnBubbleSpawned()
    {
        this.BubblesAliveCount++;
    }

    public void ResetBubbleCounts()
    {
        this.BubblesAliveCount = 0;
        this.BubblesPoppedCount = 0;
    }
}
