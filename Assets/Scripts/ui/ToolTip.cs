using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    private const float TimeToShow = 2.5f;

    private const float TimeToFadeOut = 0.5f;

    private readonly HashSet<string> toolTipsShownCompletely = new();

    [SerializeField]
    private TextMeshProUGUI toolNameText;

    [SerializeField]
    private TextMeshProUGUI toolDescriptionText;

    [SerializeField]
    private Image icon;

    private CanvasGroup canvasGroup;

    private float timeUntilFadedOut = 0.0f;

    protected void Start()
    {
        this.canvasGroup = this.GetComponent<CanvasGroup>();
    }

    protected void Update()
    {
        this.timeUntilFadedOut -= Time.deltaTime;

        if (this.timeUntilFadedOut < TimeToFadeOut)
        {
            this.canvasGroup.alpha =
                Mathf.Lerp(1.0f, 0.0f, (TimeToFadeOut - this.timeUntilFadedOut) / TimeToFadeOut);
        }

        if (this.timeUntilFadedOut <= 0.0f && !string.IsNullOrEmpty(this.toolNameText.text))
        {
            this.toolTipsShownCompletely.Add(this.toolNameText.text);
        }
    }

    public void RenderToolTip(ToolTipScriptable toolTip)
    {
        var nameText = toolTip.Name.ToUpper();

        if (this.toolTipsShownCompletely.Contains(nameText))
        {
            // If we render a new tooltip and that one has already been shown, immediately fate out the current one.
            this.timeUntilFadedOut = Mathf.Min(TimeToFadeOut, this.timeUntilFadedOut);
            return;
        }

        this.timeUntilFadedOut = TimeToShow + TimeToFadeOut;
        this.canvasGroup.alpha = 1.0f;
        this.toolNameText.text = nameText;
        this.toolDescriptionText.text = toolTip.Description;
        this.icon.sprite = toolTip.Icon;
    }
}
