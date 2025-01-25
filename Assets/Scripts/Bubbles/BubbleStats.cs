using UnityEngine;

[CreateAssetMenu(fileName = "New Bubble Stats", menuName = "Bubbles/Bubble Stats")]
public class BubbleStats : ScriptableObject {
    [Header("Splitting")]
    [Tooltip("Amount of time in seconds this bubble takes to split.")]
    [SerializeField, Min(0)] private float _splitTime = 10f;
    public float SplitTime { get => _splitTime; }

    [Header("Moving")]
    [Tooltip("Force to apply to this bubble every second towards its target (if there is one).")]
    [SerializeField, Min(0)] private float _moveForce = 100f;
    public float MoveForce { get => _moveForce; }
    [Tooltip("Maximum velocity (meters per second) this bubble can reach.")]
    [SerializeField, Min(0)] private float _maximumVelocity = 1f;
    public float MaximumVelocity { get => _maximumVelocity; }

    [Header("Stink")]
    [Tooltip("Percentage (0.0 to 1.0) between minimum and maximum stink levels that this bubble begins to be stinky at (i.e. if minStink = 0, maxStink = 50, setting this to 0.25 means this bubble will be stinky at 12.5 stink")]
    [SerializeField, Range(0, 1), Min(0)] private float _stinkStartPercentage = 0.5f;
    public float StinkStartPercentage { get => _stinkStartPercentage; }

    [Tooltip("Minimum level of stink this bubble can have (builds 1 stink per second).")]
    [SerializeField, Min(0)] private float _minStink = 0f;
    public float MinStink { get => _minStink; }
    [Tooltip("Maximum level of stink this bubble can have (builds 1 stink per second).")]
    [SerializeField, Min(0)] private float _maxStink = 100f;
    public float MaxStink { get => _maxStink; }
    [Tooltip("Starting stinkiness of this bubble (builds 1 stink per second).")]
    [SerializeField, Min(0)] private float _spawnStinkiness = 20f;
    public float SpawnStinkiness { get => _spawnStinkiness; }

    [Header("Happiness")]
    [Tooltip("Minimum amount of happiness this bubble can have.")]
    [SerializeField, Min(0)] private float _minHappiness = 0f;
    public float MinHappiness { get => _minHappiness; }
    [Tooltip("Maximum amount of happiness this bubble can have.")]
    [SerializeField, Min(0)] private float _maxHappiness = 100f;
    public float MaxHappiness { get => _maxHappiness; }
    [Tooltip("Percentage (between 0.0 and 1.0) between min and max happiness this bubble will start at when spawned.")]
    [SerializeField, Range(0f, 1f), Min(0)] private float _spawnHappinessPercentage = 0.5f;
    public float SpawnHappinessPercentage { get => _spawnHappinessPercentage; }

    [Tooltip("Percentage (between 0.0 and 1.0) between min and max happiness this bubble needs to be at to be considered happy")]
    [SerializeField, Range(0f, 1f), Min(0)] private float _happyPercentage = 0.5f;
    public float HappyPercentage { get => _happyPercentage; }
    [Tooltip("Percentage (between 0.0 and 1.0) between min and max happiness this bubble needs to be at to be considered TOO happy")]
    [SerializeField, Range(0f, 1f), Min(0)] private float _tooHappyPercentage = 0.75f;
    public float TooHappyPercentage { get => _tooHappyPercentage; }

    [Header("Watering")]
    [Tooltip("How long in seconds this bubble should stay watered for.")]
    [SerializeField, Min(0)] private float _wateredSeconds = 10f;
    public float WateredSeconds { get => _wateredSeconds; }

    [Tooltip("Size in meters this shrinks to when not watered (it's \"normal\" size).")]
    [SerializeField, Min(0)] private float _normalSize = 1f;
    public float NormalSize { get => _normalSize; }
    [Tooltip("Size in meters this grows to when watered.")]
    [SerializeField, Min(0)] private float _wateredSize = 1.5f;
    public float WateredSize { get => _wateredSize; }

    [Header("Sprites")]
    [Tooltip("Array of sprites that will be displayed depending on the happiness status of this bubble. In order of sad, happy, too happy.")]
    [SerializeField] private Sprite[] _faceSprites;
    public Sprite[] FaceSprites { get => _faceSprites; set { _faceSprites = value; } }


    private void OnValidate() {
        if (_minStink > _maxStink) {
            _minStink = _maxStink;
        }
        if (_happyPercentage > _tooHappyPercentage) {
            _happyPercentage = _tooHappyPercentage;
        }
    }
}
