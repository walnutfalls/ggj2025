using TMPro;
using UnityEngine;

public class TextWithIcon : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textBlock;

    public void SetText(string text)
    {
        this.textBlock.text = text;
    }
}
