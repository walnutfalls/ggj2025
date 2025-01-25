using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Stink), typeof(Happiness), typeof(BubbleWater)), RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class Bubble : MonoBehaviour {
    [Tooltip("Scriptable that holds the bubble's stats.")]
    [SerializeField] private BubbleStats _bubbleStats;
    public BubbleStats BubbleStats { get => _bubbleStats; }

    [Tooltip("Hat prefab used for bubble splitting.")]
    [SerializeField] private GameObject _hatPrefab;

    [Tooltip("Prefab used for spawning bubbles when they split.")]
    [SerializeField] private GameObject _bubblePrefab;
    private float _splitCount;
    public float SplitCount { get => _splitCount; set { _splitCount = value; } }

    [Tooltip("Hat game object worn by this bubble (if any)")]
    [SerializeField] private Hat _wornHat;
    public Hat WornHat { get => _wornHat; set { _wornHat = value; } }
    [Tooltip("Transform child of this bubble that acts as the pivot point for any worn hats.")]
    [SerializeField] private Transform _hatPivot;

    private bool _isJammingToMusic = false;
    public bool IsJammingToMusic { get => _isJammingToMusic; set { _isJammingToMusic = value; } }

    private Transform _target;
    public Transform Target { get => _target; set { _target = value; } }


    private Rigidbody2D _rigidbody;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        Target = FindAnyObjectByType<PlayerController>().transform;
    }

    private void Start() {
        SplitCount = BubbleStats.SplitTime;
    }

    private void Update() {
        MoveTowardsTarget();
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

    private void MoveTowardsTarget() {
        if (Target == null) {
            return;
        }

        _rigidbody.AddForce((Target.position - transform.position).normalized * BubbleStats.MoveForce * Time.deltaTime);

        if (_rigidbody.linearVelocity.magnitude > BubbleStats.MaximumVelocity) {
            _rigidbody.linearVelocity = _rigidbody.linearVelocity.normalized * BubbleStats.MaximumVelocity;
        }
    }

    [ContextMenu("Split")]
    public void Split() {
        Bubble newBubble = Instantiate(_bubblePrefab).GetComponent<Bubble>();

        if (WornHat != null) {
            Hat newHat = Instantiate(_hatPrefab).GetComponent<Hat>();
            newHat.SetHatType(WornHat.HatSO); // Replace SetHatType parameter with call to bubble director
            newBubble.EquipHat(newHat);
        }

        SplitCount = BubbleStats.SplitTime;
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
