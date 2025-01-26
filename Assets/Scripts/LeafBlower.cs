using UnityEngine;

[DisallowMultipleComponent]
public class LeafBlower : MonoBehaviour {
    [Tooltip("Scriptable that holds a list of all bubble objects.")]
    [SerializeField] private BubbleObjectPool _bubblePool;

    [Tooltip("Range in meters that the leaf blower can blow bubbles away from.")]
    [SerializeField] private float _range;

    [Tooltip("Angle in degrees of the cone the player blows bubbles away within.")]
    [SerializeField] private float _angle;

    [Tooltip("Amount of force to apply per second to bubbles within the leaf blower cone.")]
    [SerializeField] private float _blowForce;

    private bool _isBlowing;

    private InputController _input;

    private void OnEnable() {
        InputController[] inputs = FindObjectsByType<InputController>(FindObjectsSortMode.None);
        if (inputs.Length != 0) {
            _input = inputs[0];
        }

        _input.Actions.Player.Attack.performed += StartBlowing;
        _input.Actions.Player.Attack.canceled += StopBlowing;
    }

    private void OnDisable() {
        _input.Actions.Player.Attack.performed -= StartBlowing;
        _input.Actions.Player.Attack.canceled -= StopBlowing;
    }

    private void Update() {
        var dir = _input.Look;
        transform.rotation = Quaternion.FromToRotation(Vector2.up, dir);

        if (!_isBlowing) {
            return;
        }

        for (int i = 0; i < _bubblePool.AllBubbles.Count; i++) {
            if (!IsWithinCone(_bubblePool.AllBubbles[i].transform.position)) {
                continue;
            }

            _bubblePool.AllBubbles[i].Rigidbody.AddForce((_bubblePool.AllBubbles[i].transform.position - transform.position).normalized * _blowForce * Time.deltaTime);
        }
    }

    private void StartBlowing(UnityEngine.InputSystem.InputAction.CallbackContext ctx) {
        _isBlowing = true;
        AudioSystem.Instance.PlaySound("Leaf Blower");
    }

    private void StopBlowing(UnityEngine.InputSystem.InputAction.CallbackContext ctx) {
        _isBlowing = false;
        AudioSystem.Instance.StopSound("Leaf Blower");
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
