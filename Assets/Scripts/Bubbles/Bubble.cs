using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Stink), typeof(Happiness), typeof(BubbleWater)), RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class Bubble : MonoBehaviour
{
    [SerializeField] private float MaxScale = 2.0f;
    [SerializeField] private float MinScale = 1.0f;

    [Tooltip("Bubble object pool scriptable this object registers/unregisters for on spawn/despawn.")]
    [SerializeField] private BubbleObjectPool _bubbleObjectPool;

    [Tooltip("Scriptable that holds the bubble's stats.")]
    [SerializeField] private BubbleStats _bubbleStats;
    public BubbleStats BubbleStats { get => _bubbleStats; }

    [Tooltip("Hat prefab used for bubble splitting.")]
    [SerializeField] private GameObject _hatPrefab;

    [Tooltip("Reference to the hat library for generating RNG hats.")]
    [SerializeField] private HatLibrary _hatLibrary;

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

    private Vector3 _target;
    public Vector3 Target { get => _target; set { _target = value; } }

    private bool _isStunned;
    public bool IsStunned { get => _isStunned; set { _isStunned = value; } }

    private Transform _playerTransform;
    private float _wanderCount;

    private Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody { get => _rigidbody; set => _rigidbody = value; }

    private Happiness _happiness;

    private Vector2 _defaultHatScale = Vector2.one;

    private Vector2? _defaultSelfScale = null;

    private void OnEnable() {
        BubbleDirector.Instance.OnBubbleSpawned();
        _bubbleObjectPool.AllBubbles.Add(this);
    }

    private void OnDisable() {
        _bubbleObjectPool.AllBubbles.Remove(this);
    }

    private void Awake() {
        Rigidbody = GetComponent<Rigidbody2D>();
        _happiness = GetComponent<Happiness>();

        Hat newHat = Instantiate(_hatPrefab).GetComponent<Hat>();
        newHat.SetHatType(_hatLibrary.GetRandomHat());
        this._defaultHatScale = newHat.gameObject.transform.localScale;
        EquipHat(newHat);

        PlayerController[] players = FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
        if (players.Length != 0) {
            _playerTransform = players[0].transform;
        }
    }

    private void Start() {
        SplitCount = BubbleStats.SplitTime;
        _wanderCount = _bubbleStats.WanderSeconds;
    }

    private void Update() {
        if (!IsStunned) {
            AdjustTargetPosition();
            GetComponent<Rigidbody2D>().linearDamping = 1;
            MoveTowardsTarget();
        } else {
            GetComponent<Rigidbody2D>().linearDamping = 0;
        }

        UpdateSize();
        UpdateHatTransform();
        if (_happiness.BubbleHappiness == Happiness.HappinessStatus.Sad) {
            return;
        }

        SplitCount -= Time.deltaTime;
        if (SplitCount <= 0) {
            Split();
        }
    }

    private void UpdateSize()
    {
        float ScaleMultiplier = Mathf.Lerp(MaxScale, MinScale, SplitCount / BubbleStats.SplitTime);
        this.gameObject.transform.localScale = new Vector2(ScaleMultiplier, ScaleMultiplier);

        if (this._defaultSelfScale == null)
        {
            this._defaultSelfScale = this.gameObject.transform.localScale;
        }
    }

    private void AdjustTargetPosition() {
        if (_happiness.BubbleHappiness == Happiness.HappinessStatus.TooHappy) {
            Target = _playerTransform.position;
            return;
        }

        _wanderCount -= Time.deltaTime;
        if (_wanderCount <= 0f) {
            Target = transform.position + (Vector3)Random.insideUnitCircle * _bubbleStats.WanderDistance;
            _wanderCount = _bubbleStats.WanderSeconds;
        }
    }

    private void UpdateHatTransform() {
        if (WornHat == null) {
            return;
        }

        WornHat.transform.position = _hatPivot.position;
        WornHat.transform.localScale =
            this._defaultHatScale
            * this.gameObject.transform.localScale.x
            / this._defaultSelfScale.GetValueOrDefault(Vector2.one).x;
    }

    private void MoveTowardsTarget() {
        Rigidbody.AddForce((Target - transform.position).normalized * BubbleStats.MoveForce * Time.deltaTime);

        if (Rigidbody.linearVelocity.magnitude > BubbleStats.MaximumVelocity) {
            Rigidbody.linearVelocity = Rigidbody.linearVelocity.normalized * BubbleStats.MaximumVelocity;
        }
    }

    [ContextMenu("Split")]
    public void Split() {
        Instantiate(_bubblePrefab).GetComponent<Bubble>();
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
