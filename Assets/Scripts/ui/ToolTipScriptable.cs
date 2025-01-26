using UnityEngine;

[CreateAssetMenu(fileName = "ToolTipScriptable", menuName = "UI/ToolTip Scriptable")]
public class ToolTipScriptable : ScriptableObject
{
    [SerializeField]
    private string gameObjectUniqueName;
    public string GameObjectName => this.gameObjectUniqueName;

    [SerializeField]
    private string toolName;
    public string Name => this.toolName;

    [SerializeField]
    private string toolDescription;
    public string Description => this.toolDescription;

    [SerializeField]
    private Sprite toolIcon;
    public Sprite Icon => this.toolIcon;
}
