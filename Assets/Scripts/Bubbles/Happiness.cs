using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Stink), typeof(Bubble), typeof(BubbleWater))]
public class Happiness : MonoBehaviour {
    public enum HappinessStatus { Sad, Happy, TooHappy };
    private HappinessStatus _bubbleHappiness;
    public HappinessStatus BubbleHappiness { get => _bubbleHappiness; private set { _bubbleHappiness = value; } }
    private float _currentHappiness;
    public float CurrentHappiness { get => _currentHappiness; set { _currentHappiness = value; } }

    public float CurrentHappinessPercentage {
        get {
            return Mathf.InverseLerp(MinHappiness, MaxHappiness, CurrentHappiness);
        }
    }

    [Tooltip("Minimum amount of happiness this bubble can have.")]
    [SerializeField] private float _minHappiness;
    public float MinHappiness { get => _minHappiness; set { _minHappiness = value; } }
    [Tooltip("Maximum amount of happiness this bubble can have.")]
    [SerializeField] private float _maxHappiness;
    public float MaxHappiness { get => _maxHappiness; set { _maxHappiness = value; } }


    [Tooltip("Percentage (between 0.0 and 1.0) between min and max happiness this bubble will start at when spawned.")]
    [SerializeField, Range(0f, 1f)] private float _spawnHappinessPercentage = 0.5f;
    public float SpawnHappinessPercentage { get => _spawnHappinessPercentage; set { _spawnHappinessPercentage = value; } }

    [Tooltip("Percentage (between 0.0 and 1.0) between min and max happiness this bubble needs to be at to be considered happy")]
    [SerializeField, Range(0f, 1f)] private float _happyPercentage = 0.5f;
    public float HappyPercentage { get => _happyPercentage; set { _happyPercentage = value; } }
    [Tooltip("Percentage (between 0.0 and 1.0) between min and max happiness this bubble needs to be at to be considered TOO happy")]
    [SerializeField, Range(0f, 1f)] private float _tooHappyPercentage = 0.75f;
    public float TooHappyPercentage { get => _tooHappyPercentage; set { _tooHappyPercentage = value; } }

    [Tooltip("Array of sprites that will be displayed depending on the happiness status of this bubble. In order of sad, happy, too happy.")]
    [SerializeField] private Sprite[] _faceSprites;
    public Sprite[] FaceSprites { get => _faceSprites; set { _faceSprites = value; } }

    private void OnValidate() {
        if (_happyPercentage > _tooHappyPercentage) {
            _happyPercentage = _tooHappyPercentage;
        }
    }

    private void Start() {
        CurrentHappiness = SpawnHappinessPercentage;
    }

    private void Update() {
        CurrentHappiness -= Time.deltaTime;

        BubbleHappiness = CurrentHappinessPercentage < HappyPercentage ? HappinessStatus.Sad : 
            (CurrentHappinessPercentage < TooHappyPercentage ? HappinessStatus.Happy : HappinessStatus.TooHappy);
    }

    [ContextMenu("Add Ten Happiness")]
    private void AddTenHappiness() {
        CurrentHappiness += 10f;
    }

    [ContextMenu("Minus Ten Happiness")]
    private void MinusTenHappiness() {
        CurrentHappiness -= 10f;
    }

    [ContextMenu("Set Happiness to Sad")]
    private void SetHappinessToSad() {
        SetHappinessStatus(HappinessStatus.Sad);
    }

    [ContextMenu("Set Happiness to Happy")]
    private void SetHappinessToHappy() {
        SetHappinessStatus(HappinessStatus.Happy);
    }

    [ContextMenu("Set Happiness to Too Happy")]
    private void SetHappinessToTooHappy() {
        SetHappinessStatus(HappinessStatus.TooHappy);
    }

    private void SetHappinessStatus(HappinessStatus status) {
        BubbleHappiness = status;
        switch (status) {
            case HappinessStatus.Sad:
                CurrentHappiness = MinHappiness;
                break;
            case HappinessStatus.Happy:
                CurrentHappiness = Mathf.Lerp(MinHappiness, MaxHappiness, HappyPercentage);
                break;
            case HappinessStatus.TooHappy:
                CurrentHappiness = Mathf.Lerp(MinHappiness, MaxHappiness, TooHappyPercentage);
                break;
        }
    }
}
