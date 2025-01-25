using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Stink), typeof(Happiness), typeof(Bubble))]
public class BubbleWater : MonoBehaviour {
    private bool _isWatered = false;
    public bool IsWatered { get => _isWatered; set { _isWatered = value; } }

    private float _wateredCount;

    [Tooltip("How long in seconds this bubble should stay watered for.")]
    [SerializeField] private float _wateredSeconds;
    public float WateredSeconds { get => _wateredSeconds; set { _wateredSeconds = value; } }

    [Tooltip("Size in meters this shrinks to when not watered (it's \"normal\" size).")]
    [SerializeField] private float _normalSize;
    [Tooltip("Size in meters this grows to when watered.")]
    [SerializeField] private float _wateredSize;

    private void Start() {
        SetSize(_normalSize);
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
    private void SetToWatered() {
        IsWatered = true;
        SetSize(_wateredSize);
        _wateredCount = WateredSeconds;
    }

    [ContextMenu("Set To Not Watered")]
    private void SetToNotWatered() {
        IsWatered = false;
        SetSize(_normalSize);
    }
}
