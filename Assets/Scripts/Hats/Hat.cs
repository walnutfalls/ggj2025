using UnityEngine;

[DisallowMultipleComponent]
public class Hat : MonoBehaviour {
    [Tooltip("Hat Scriptable reference for the stats of this hat.")]
    [SerializeField] private HatScriptable _hatSO;

    [Tooltip("Parent bubble that is wearing this hat (none means it is able to be collected by player)")]
    [SerializeField] private Bubble _bubbleParent;
    public Bubble BubbleParent { get => _bubbleParent; set { _bubbleParent = value; } }
}
