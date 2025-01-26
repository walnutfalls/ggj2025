using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteRenderer))]
public class Hat : MonoBehaviour {
    [Tooltip("Hat Scriptable reference for the stats of this hat.")]
    [SerializeField] private HatScriptable _hatSO;
    public HatScriptable HatSO { get => _hatSO; }

    [Tooltip("Parent bubble that is wearing this hat (none means it is able to be collected by player)")]
    [SerializeField] private Bubble _bubbleParent;
    public Bubble BubbleParent { get => _bubbleParent; set { _bubbleParent = value; } }

    private SpriteRenderer _renderer;

    private void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void SetHatType(HatScriptable hatType) {
        _hatSO = hatType;
        _renderer.sprite = _hatSO.HatSprite;
    }

    public void DropHat() {
        BubbleParent = null;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (BubbleParent != null) {
            return;
        }

        if (collision.TryGetComponent(out PlayerController player)) {
            player.ReceiveHat(HatSO);
            GameDirector.Instance.CollectHat(HatSO);
            Destroy(gameObject);
        }
    }
}