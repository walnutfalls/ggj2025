using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Hats Library", menuName = "Hat/Hat Library")]
public class HatLibrary : ScriptableObject {
    [Tooltip("List of all hat scriptables.")]
    [SerializeField] private List<HatScriptable> _hatsList;
    public List<HatScriptable> HatsList { get => _hatsList; }

    [Tooltip("Common hats chance")]
    [SerializeField] private int _commonHatChance;
    public int CommonHatChance { get => _commonHatChance; }

    [Tooltip("Uncommon hats chance")]
    [SerializeField] private int _uncommonHatChance;
    public int UncommonHatChance { get => _uncommonHatChance; }

    [Tooltip("Common hats chance")]
    [SerializeField] private int _rareHatChance;
    public int RareHatChance { get => _rareHatChance; }

    public HatScriptable GetRandomHat() {
        int overallChance = CommonHatChance + UncommonHatChance + RareHatChance;
        int result = Random.Range(0, overallChance);

        if (result <= CommonHatChance) {
            List<HatScriptable> commonHats = HatsList.FindAll(h => h.HatRarity == HatScriptable.Rarity.Common);
            return commonHats[Random.Range(0, commonHats.Count)];
        }
        else if (result <= CommonHatChance + UncommonHatChance) {
            List<HatScriptable> uncommonHats = HatsList.FindAll(h => h.HatRarity == HatScriptable.Rarity.Uncommon);
            return uncommonHats[Random.Range(0, uncommonHats.Count)];
        }
        else {
            List<HatScriptable> rareHats = HatsList.FindAll(h => h.HatRarity == HatScriptable.Rarity.Rare);
            return rareHats[Random.Range(0, rareHats.Count)];
        }
    }
}
