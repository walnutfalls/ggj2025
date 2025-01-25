using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Bubble), typeof(Happiness), typeof(BubbleWater))]
public class Stink : MonoBehaviour {
    private float _stinkiness;
    public float Stinkiness { get => _stinkiness; set { _stinkiness = value; } }
    public bool IsStinky {
        get {
            float stinkage = Mathf.InverseLerp(_minStink, _maxStink, Stinkiness);
            return StinkStartPercentage <= stinkage;
        }
    }
    [Tooltip("Percentage (0.0 to 1.0) between minimum and maximum stink levels that this bubble begins to be stinky at (i.e. if minStink = 0, maxStink = 50, setting this to 0.25 means this bubble will be stinky at 12.5 stink")]
    [SerializeField, Range(0, 1)] private float _stinkStartPercentage;
    public float StinkStartPercentage { get => _stinkStartPercentage; set { _stinkStartPercentage = value; } }

    [Tooltip("Minimum level of stink this bubble can have (builds 1 stink per second).")]
    [SerializeField] private float _minStink;
    [Tooltip("Maximum level of stink this bubble can have (builds 1 stink per second).")]
    [SerializeField] private float _maxStink;
    [Tooltip("Starting stinkiness of this bubble (builds 1 stink per second).")]
    [SerializeField] private float _spawnStinkiness;

    private void OnValidate() {
        if (_minStink > _maxStink) {
            _minStink = _maxStink;
        }
    }

    private void Start() {
        Stinkiness = _spawnStinkiness;
    }

    private void Update() {
        Stinkiness += Time.deltaTime;

        if (IsStinky) {
            Debug.Log("THAT STINKS");
        }
    }

    [ContextMenu("Add Ten Stinkiness")]
    private void AddTenStinkiness() {
        Stinkiness += 10f;
    }

    [ContextMenu("Minus Ten Stinkiness")]
    private void MinusTenStinkiness() {
        Stinkiness -= 10f;
    }
}
