using System.Collections;
using UnityEngine;

public class BroomTool : MonoBehaviour
{

    private Rigidbody2D rb;

    private IEnumerator _waveCoroutine;

    InputController input;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<InputController>();
    }

    private void OnEnable()
    {
        input = FindFirstObjectByType<InputController>();
        input.Actions.Player.Attack.performed += DoWave;
        input.Actions.Player.Attack.canceled += StopWave;
    }

    void OnDisable()
    {
        input.Actions.Player.Attack.performed -= DoWave;
        input.Actions.Player.Attack.canceled -= StopWave;
    }

    private void Update()
    {
        transform.position = transform.parent.position;

        if (_waveCoroutine == null) {
            var dir = input.Look;
            var broomUp = transform.up;
            var broomUpOrth = new Vector2(-broomUp.y, broomUp.x);
            var targetRotation = Quaternion.FromToRotation(Vector2.up, dir);
            var angle = Quaternion.Angle(transform.rotation, targetRotation);

            if (Vector2.Dot(broomUpOrth, dir) < 0)
            {
                rb.angularVelocity = -angle * 10;
            } else {
                rb.angularVelocity = angle * 10;
            }
        }
    }

    public void DoWave(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        StartCoroutine(_waveCoroutine = WaveBroom());
    }

    private void StopWave(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if (_waveCoroutine != null)
        {
            StopCoroutine(_waveCoroutine);
            _waveCoroutine = null;
        }
    }

    private IEnumerator WaveBroom()
    {
        while (true)
        {
            rb.angularVelocity = 360f;
            yield return new WaitForSeconds(0.25f);
            rb.angularVelocity = -360f;
            yield return new WaitForSeconds(0.25f);
            AudioSystem.Instance.PlaySound("Broom Swing");
        }
    }
}
