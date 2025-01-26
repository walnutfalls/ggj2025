using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsPanel : MonoBehaviour
{
    [SerializeField]
    private MouseDetector triggerDetector;

    [SerializeField]
    private CanvasGroup panelPortalGroup;

    [SerializeField]
    private MouseDetector panelContainer;

    [SerializeField]
    private TextMeshProUGUI gamesCountText;

    [SerializeField]
    private List<HatStatusRow> hatStatusRows;

    [SerializeField]
    private float transitionSpeed;

    private readonly List<Tuple<HatStatus, HatScriptable>> hatStatuses = new();

    private bool isPanelOpen = false;

    private float panelTargetOpacity = 0.0f;

    private bool PanelPortalActive
    {
        set => this.panelPortalGroup.gameObject.SetActive(value);
    }

    private float PanelPortalOpacity
    {
        get => this.panelPortalGroup.alpha;
        set => this.panelPortalGroup.alpha = value;
    }

    protected void Start()
    {
        this.panelTargetOpacity = this.transform.position.x;

        this.PanelPortalOpacity = 0.0f;
        this.PanelPortalActive = false;
    }

    protected void Update()
    {
        if (this.isPanelOpen)
        {
            this.UpdateHatStatuses();
        }

        var shouldPanelBeOpen =
            this.triggerDetector.IsMouseOver
            || (this.isPanelOpen && this.panelContainer.IsMouseOver);

        if (this.isPanelOpen && !shouldPanelBeOpen)
        {
            this.ClosePanel();
        }
        else if (!this.isPanelOpen && shouldPanelBeOpen)
        {
            this.OpenPanel();
        }

        if (!this.isPanelOpen && Mathf.Approximately(this.PanelPortalOpacity, 0.0f))
        {
            this.PanelPortalActive = false;
        }
        else
        {
            var newOpacity = Mathf.MoveTowards(
                this.PanelPortalOpacity,
                this.panelTargetOpacity,
                this.transitionSpeed * Time.deltaTime
            );

            this.PanelPortalOpacity = newOpacity;
        }

        this.gamesCountText.text = StatsTracker.Instance.GamesStarted.ToString();
    }

    private void ClosePanel()
    {
        this.isPanelOpen = false;
        this.panelTargetOpacity = 0.0f;
    }

    private void OpenPanel()
    {
        this.isPanelOpen = true;

        this.PanelPortalActive = true;
        this.panelTargetOpacity = 1.0f;
        AudioSystem.Instance.PlaySound("Rollover");
    }

    private void UpdateHatStatuses()
    {
        StatsTracker.Instance.GetHatStatuses(this.hatStatuses);

        for (var i = 0; i < this.hatStatusRows.Count; i++)
        {
            var row = this.hatStatusRows[i];

            if (i >= this.hatStatuses.Count)
            {
                row.gameObject.SetActive(false);
                continue;
            }

            var status = this.hatStatuses[i];

            row.gameObject.SetActive(true);
            row.SetHat(status.Item2);
            row.SetStatus(status.Item1);
        }
    }
}
