using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Stink), typeof(Happiness), typeof(BubbleWater)), RequireComponent(typeof(SpriteRenderer))]
public class Bubble : MonoBehaviour {
    [Tooltip("Prefab used for spawning bubbles when they split.")]
    [SerializeField] private GameObject _bubblePrefab;

    [Tooltip("Amount of time in seconds this bubble takes to split.")]
    [SerializeField] private float _splitTime;
    public float SplitTime { get => _splitTime; set { _splitTime = value; } }
    private float _splitCount;
    public float SplitCount { get => _splitCount; set { _splitCount = value; } }


    [Tooltip("Hat game object worn by this bubble (if any)")]
    [SerializeField] private Hat _wornHat;
    public Hat WornHat { get => _wornHat; set { _wornHat = value; } }
    [Tooltip("Transform child of this bubble that acts as the pivot point for any worn hats.")]
    [SerializeField] private Transform _hatPivot;

    private bool _isJammingToMusic = false;
    public bool IsJammingToMusic { get => _isJammingToMusic; set { _isJammingToMusic = value; } }

    private void Start() {
        SplitCount = SplitTime;
    }

    private void Update() {
        UpdateHatPosition();
        SplitCount -= Time.deltaTime;
        if (SplitCount <= 0) {
            Split();
        }
    }

    private void UpdateHatPosition() {
        if (WornHat == null) {
            return;
        }

        WornHat.transform.position = _hatPivot.position;
    }

    [ContextMenu("Split")]
    public void Split() {
        Bubble newBubble = Instantiate(_bubblePrefab).GetComponent<Bubble>();
        Hat newHat = new GameObject().AddComponent<Hat>();
        if (WornHat != null) {
            newHat.SetHatType(WornHat.HatSO);
        }
        newBubble.EquipHat(newHat);

        SplitCount = SplitTime;
    }

    public void EquipHat(Hat hat) {
        WornHat = hat;
        WornHat.BubbleParent = this;
    }

    [ContextMenu("Drop Hat")]
    public void DropHat() {
        if (WornHat == null) {
            return;
        }

        WornHat.DropHat();
        WornHat = null;
    }
}
