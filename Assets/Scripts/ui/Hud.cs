using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hud : MonoBehaviour
{
    private static readonly TimeSpan MaxTimeElapsedToShow = new(1, 39, 59); // 99:59

    [SerializeField]
    private List<ToolTipScriptable> toolTipScriptables;

    [SerializeField]
    private HatLibrary hatLibrary;

    [SerializeField]
    private TextWithIcon timeText;

    [SerializeField]
    private TextWithIcon bubblesAliveText;

    [SerializeField]
    private TextWithIcon bubblesPoppedText;

    [SerializeField]
    private ToolTip toolTipUi;

    private float secondsElapsed = 0.0f;

    private Inventory inventory;

    private GameObject previouslySelectedTool;

    protected void Start()
    {
        StatsTracker.Instance.RegisterHats(this.hatLibrary.HatsList);
        this.Reset();
    }

    protected void Update()
    {
        this.secondsElapsed += Time.deltaTime;
        this.UpdateToolTip();
        this.UpdateTexts();
    }

    private static string FormatBubbleCount(int number)
    {
        return number.ToString("#,0");
    }

    private void Reset()
    {
        this.inventory = FindFirstObjectByType<Inventory>();
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

        if (timeElapsed > MaxTimeElapsedToShow)
        {
            this.timeText.SetText("99:59+");
        }
        else
        {
            var timeElapsedString = string.Format("{0:0}:{1:00}", timeElapsed.TotalMinutes, timeElapsed.Seconds);
            this.timeText.SetText(timeElapsedString);
        }
    }

    private void UpdateToolTip()
    {
        var selectedTool = this.inventory == null ? null : this.inventory.CurrentTool;
        if (selectedTool == null)
        {
            this.previouslySelectedTool = null;
            return;
        }

        if (selectedTool == this.previouslySelectedTool)
        {
            return;
        }

        var toolTipScriptable =
            toolTipScriptables.Where((sc) => sc.GameObjectName == selectedTool.name).FirstOrDefault();

        if (toolTipScriptable == null)
        {
            Debug.LogWarning($"Unable to find a tool-tip scriptable for: {selectedTool.name}.");
        }
        else
        {
            this.toolTipUi.RenderToolTip(toolTipScriptable);
        }

        this.previouslySelectedTool = selectedTool;
    }
}
