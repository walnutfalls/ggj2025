using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HatStatusRow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;

    [SerializeField]
    private Image iconImage;

    [SerializeField]
    private Sprite lockedIconSprite;

    private HatScriptable hat;

    private HatStatus status;

    protected void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void SetHat(HatScriptable hat)
    {
        this.hat = hat;
        this.UpdateUiValues();
    }

    public void SetStatus(HatStatus status)
    {
        this.status = status;
        this.UpdateUiValues();
    }

    private void UpdateUiValues()
    {
        this.nameText.text = this.hat.Name.ToUpper();

        Sprite rowSprite = this.status == HatStatus.Locked
            ? this.lockedIconSprite
            : this.hat.HatSprite;
        float rowOpacity = this.status == HatStatus.UnlockedInCurrent
            ? 1.0f
            : 0.5f;

        this.iconImage.sprite = rowSprite;

        if (!Mathf.Approximately(rowOpacity, this.iconImage.color.a))
        {
            this.iconImage.color = new Color(1.0f, 1.0f, 1.0f, rowOpacity);
        }
    }
}
