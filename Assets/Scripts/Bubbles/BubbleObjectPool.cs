using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bubble Object Pool", menuName = "Bubbles/Bubble Object Pool")]
public class BubbleObjectPool : ScriptableObject {
	private List<Bubble> _allBubbles;
	public List<Bubble> AllBubbles { get => _allBubbles; set { _allBubbles = value; } }
}
