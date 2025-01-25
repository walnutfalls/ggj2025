using UnityEngine;

[CreateAssetMenu(fileName = "New Hat Stats", menuName = "Hat/Hat Stats")]
public class HatScriptable : ScriptableObject {
    public enum Rarity { Common, Uncommon, Rare };
    [Tooltip("Rarity of this hat.")]
    [SerializeField] private Rarity _hatRarity;
    public Rarity HatRarity { get => _hatRarity; set { _hatRarity = value; } }
    [Tooltip("How much this hat is worth")]
    [SerializeField, Min(0)] private float _hatValue;
    public float HatValue { get => _hatValue; set { _hatValue = value; } }

    [Tooltip("Sprite for this type of hat.")]
    [SerializeField] private Sprite _hatSprite;
    public Sprite HatSprite { get => _hatSprite; }
}
