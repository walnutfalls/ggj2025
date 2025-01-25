using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteRenderer))]
public class LeafBlower : MonoBehaviour {
    [Tooltip("Scriptable that holds a list of all bubble objects.")]
    [SerializeField] private BubbleObjectPool _bubblePool;

    [Tooltip("Range in meters that the leaf blower can blow bubbles away from.")]
    [SerializeField] private float _range;

    [Tooltip("Angle in degrees of the cone the player blows bubbles away within.")]
    [SerializeField] private float _angle;

    [Tooltip("Amount of force to apply per second to bubbles within the leaf blower cone.")]
    [SerializeField] private float _blowForce;

    private void Update() {
        for (int i = 0; i < _bubblePool.AllBubbles.Count; i++) {
            if (IsWithinCone(_bubblePool.AllBubbles[i].transform.position)) {
                _bubblePool.AllBubbles[i].Rigidbody.AddForce((_bubblePool.AllBubbles[i].transform.position - transform.position).normalized * _blowForce * Time.deltaTime);
            }
        }
    }

    private bool IsWithinCone(Vector3 position) {
        if (Vector3.Distance(position, transform.position) > _range) {
            return false;
        }

        if (Vector3.Angle(transform.up, position - transform.position) > _angle * 0.5f) {
            return false;
        }

        return true;
    }
}
