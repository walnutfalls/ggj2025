using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Stink), typeof(Bubble), typeof(BubbleWater))]
public class Happiness : MonoBehaviour {
    public enum HappinessStatus { Sad, Happy, TooHappy };
    private HappinessStatus _bubbleHappiness;
    public HappinessStatus BubbleHappiness { get => _bubbleHappiness; private set { _bubbleHappiness = value; } }
    private float _currentHappiness;
    public float CurrentHappiness { get => _currentHappiness; set { _currentHappiness = value; } }

    private Bubble _bubble;

    public float CurrentHappinessPercentage {
        get {
            return Mathf.InverseLerp(_bubble.BubbleStats.MinHappiness, _bubble.BubbleStats.MaxHappiness, CurrentHappiness);
        }
    }

    private void Awake() {
        _bubble = GetComponent<Bubble>();
    }

    private void Start() {
        CurrentHappiness = _bubble.BubbleStats.SpawnHappinessPercentage;
    }

    private void Update() {
        LowerHappiness(Time.deltaTime);

        BubbleHappiness = CurrentHappinessPercentage < _bubble.BubbleStats.HappyPercentage ? HappinessStatus.Sad : 
            (CurrentHappinessPercentage < _bubble.BubbleStats.TooHappyPercentage ? HappinessStatus.Happy : HappinessStatus.TooHappy);
    }

    public void AddHappiness(float happinessAmount) {
        CurrentHappiness = Mathf.Min(CurrentHappiness + happinessAmount, _bubble.BubbleStats.MaxHappiness);
    }

    public void LowerHappiness(float happinessAmount) {
        CurrentHappiness = Mathf.Max(CurrentHappiness - happinessAmount, _bubble.BubbleStats.MinHappiness);
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
                CurrentHappiness = _bubble.BubbleStats.MinHappiness;
                break;
            case HappinessStatus.Happy:
                CurrentHappiness = Mathf.Lerp(_bubble.BubbleStats.MinHappiness, _bubble.BubbleStats.MaxHappiness, _bubble.BubbleStats.HappyPercentage);
                break;
            case HappinessStatus.TooHappy:
                CurrentHappiness = Mathf.Lerp(_bubble.BubbleStats.MinHappiness, _bubble.BubbleStats.MaxHappiness, _bubble.BubbleStats.TooHappyPercentage);
                break;
        }
    }
}
