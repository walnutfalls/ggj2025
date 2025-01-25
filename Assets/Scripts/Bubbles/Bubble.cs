using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Stink), typeof(Happiness), typeof(BubbleWater)), RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class Bubble : MonoBehaviour {
    [Tooltip("Bubble object pool scriptable this object registers/unregisters for on spawn/despawn.")]
    [SerializeField] private BubbleObjectPool _bubbleObjectPool;

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

    private bool _isStunned;
    public bool IsStunned { get => _isStunned; set { _isStunned = value; } }

    
    private Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody { get => _rigidbody; set => _rigidbody = value; }

    private void OnEnable() {
        _bubbleObjectPool.AllBubbles.Add(this);
    }

    private void OnDisable() {
        _bubbleObjectPool.AllBubbles.Remove(this);
    }

    private void Awake() {
        Rigidbody = GetComponent<Rigidbody2D>();

        PlayerController[] players = FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
        if (players.Length != 0) {
            Target = players[0].transform;
        }
    }

    private void Start() {
        SplitCount = BubbleStats.SplitTime;
    }

    private void Update() {
        if (!IsStunned) {
            GetComponent<Rigidbody2D>().linearDamping = 1;
            MoveTowardsTarget();
        } else {
            GetComponent<Rigidbody2D>().linearDamping = 0;
        }

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

        Rigidbody.AddForce((Target.position - transform.position).normalized * BubbleStats.MoveForce * Time.deltaTime);

        if (Rigidbody.linearVelocity.magnitude > BubbleStats.MaximumVelocity) {
            Rigidbody.linearVelocity = Rigidbody.linearVelocity.normalized * BubbleStats.MaximumVelocity;
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
