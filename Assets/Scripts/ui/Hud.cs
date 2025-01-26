using System;
using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField]
    private HatLibrary hatLibrary;

    [SerializeField]
    private TextWithIcon timeText;

    [SerializeField]
    private TextWithIcon bubblesAliveText;

    [SerializeField]
    private TextWithIcon bubblesPoppedText;

    private float secondsElapsed = 0.0f;

    protected void Start()
    {
        StatsTracker.Instance.RegisterHats(this.hatLibrary.HatsList);
        this.Reset();
    }

    protected void Update()
    {
        this.secondsElapsed += Time.deltaTime;
        this.UpdateTexts();
    }

    private static string FormatBubbleCount(int number)
    {
        return number.ToString("#,0");
    }

    private void Reset()
    {
        this.secondsElapsed = 0.0f;
        this.UpdateTexts();
    }

    private void UpdateTexts()
    {
        this.UpdateTimeText();
        this.UpdateBubbleCountTexts();
    }

    private void UpdateBubbleCountTexts()
    {
        var bubblesAlive = BubbleDirector.Instance.BubblesAliveCount;
        var bubblesPopped = BubbleDirector.Instance.BubblesPoppedCount;

        this.bubblesAliveText.SetText(FormatBubbleCount(bubblesAlive));
        this.bubblesPoppedText.SetText(FormatBubbleCount(bubblesPopped));
    }

    private void UpdateTimeText()
    {
        var timeElapsed = TimeSpan.FromSeconds(this.secondsElapsed);
        var timeElapsedString = string.Format("{0:0}:{1:00}", timeElapsed.TotalMinutes, timeElapsed.Seconds);
        this.timeText.SetText(timeElapsedString);
    }
}
