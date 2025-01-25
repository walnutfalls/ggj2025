using System;
using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField]
    private TextWithIcon timeText;

    [SerializeField]
    private TextWithIcon bubblesAliveText;

    [SerializeField]
    private TextWithIcon bubblesPoppedText;

    private float secondsElapsed = 0.0f;

    public void Start()
    {
        this.UpdateTimeText();
        this.bubblesAliveText.SetText("22");
        this.bubblesPoppedText.SetText("333");
    }

    public void Update()
    {
        this.secondsElapsed += Time.deltaTime;
        this.UpdateTimeText();
    }

    private void UpdateTimeText()
    {
        var timeElapsed = TimeSpan.FromSeconds(this.secondsElapsed);
        var timeElapsedString = string.Format("{0:0}:{1:00}", timeElapsed.TotalMinutes, timeElapsed.Seconds);
        this.timeText.SetText(timeElapsedString);
    }
}
