using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Stink), typeof(Happiness), typeof(Bubble))]
public class BubbleWater : MonoBehaviour {
    private bool _isWatered = false;
    public bool IsWatered { get => _isWatered; set { _isWatered = value; } }

    private float _wateredCount;

    private Bubble _bubble;

    private void Awake() {
        _bubble = GetComponent<Bubble>();
    }

    private void Start() {
        SetSize(_bubble.BubbleStats.NormalSize);
    }

    private void Update() {
        if (IsWatered) {
            _wateredCount -= Time.deltaTime;

            if (_wateredCount <= 0f) {
                SetToNotWatered();
            }
        }
    }

    private void SetSize(float size) {
        transform.localScale = Vector3.one * size;
    }

    [ContextMenu("Set To Watered")]
    public void SetToWatered() {
        IsWatered = true;
        SetSize(_bubble.BubbleStats.WateredSize);
        _wateredCount = _bubble.BubbleStats.WateredSeconds;
    }

    [ContextMenu("Set To Not Watered")]
    public void SetToNotWatered() {
        IsWatered = false;
        SetSize(_bubble.BubbleStats.NormalSize);
    }
}
