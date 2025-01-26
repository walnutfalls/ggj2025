using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hats Library", menuName = "Hat/Hat Library")]
public class HatLibrary : ScriptableObject {
    [Tooltip("List of all hat scriptables.")]
    [SerializeField] private List<HatScriptable> _hatsList;
    public List<HatScriptable> HatsList { get => _hatsList; }
}
