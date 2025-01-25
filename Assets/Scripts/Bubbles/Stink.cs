using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Bubble), typeof(Happiness), typeof(BubbleWater))]
public class Stink : MonoBehaviour {
    private float _stinkiness;
    public float Stinkiness { get => _stinkiness; set { _stinkiness = value; } }
    public bool IsStinky {
        get {
            float stinkage = Mathf.InverseLerp(_bubble.BubbleStats.MinStink, _bubble.BubbleStats.MaxStink, Stinkiness);
            return _bubble.BubbleStats.StinkStartPercentage <= stinkage;
        }
    }
    private Bubble _bubble;

    private void Awake() {
        _bubble = GetComponent<Bubble>();
    }

    private void Start() {
        Stinkiness = _bubble.BubbleStats.SpawnStinkiness;
    }

    private void Update() {
        Stinkiness += Time.deltaTime;

        if (IsStinky) {
            // Do stink stuff
        }
    }

    public void AddStinkiness(float stinkinessAmount) {
        Stinkiness = Mathf.Min(Stinkiness + stinkinessAmount, _bubble.BubbleStats.MaxStink);
    }

    public void LowerStinkiness(float stinkinessAmount) {
        Stinkiness = Mathf.Max(Stinkiness - stinkinessAmount, _bubble.BubbleStats.MinStink);
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
