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
    private float transitionSpeed;

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

    public void Start()
    {
        this.panelTargetOpacity = this.transform.position.x;

        this.PanelPortalOpacity = 0.0f;
        this.PanelPortalActive = false;
    }

    public void Update()
    {
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
    }
}
