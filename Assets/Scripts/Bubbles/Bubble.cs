using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Stink), typeof(Happiness), typeof(BubbleWater))]
public class Bubble : MonoBehaviour {
    private float _splitCount;
    public float SplitCount { get => _splitCount; set { _splitCount = value; } }

    [Tooltip("Hat game object worn by this bubble (if any)")]
    [SerializeField] private Hat _wornHat;
    public Hat WornHat { get => _wornHat; set { _wornHat = value; } }
    [Tooltip("Transform child of this bubble that acts as the pivot point for any worn hats.")]
    [SerializeField] private Transform _hatPivot;

    private bool _isJammingToMusic = false;
    public bool IsJammingToMusic { get => _isJammingToMusic; set { _isJammingToMusic = value; } }

    private void Update() {
        UpdateHatPosition();
    }

    private void UpdateHatPosition() {
        if (WornHat == null) {
            return;
        }

        WornHat.transform.position = _hatPivot.position;
    }
}
