using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public InputSystem_Actions Actions { get; private set; }

    public bool UsingGamepad { get; private set; }

    public Vector2 Movement
    {
        get
        {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
            if (UsingGamepad)
            {
                return Actions.Player.Move.ReadValue<Vector2>();
            }
            else
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                return (mousePosition - transform.position).normalized;
            }
#else
            return input.Actions.Player.Move.ReadValue<Vector2>();
#endif
        }
    }

    public Vector2 Look
    {
        get
        {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
            if (UsingGamepad)
            {
                return Actions.Player.Look.ReadValue<Vector2>();
            }
            else
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                return (mousePosition - transform.position).normalized;
            }
#else
            return input.Actions.Player.Look.ReadValue<Vector2>();
#endif
        }
    }


    private void OnEnable()
    {
        Actions = new InputSystem_Actions();
        Actions.Enable();
        InputSystem.onEvent += DetectGamepad;
    }


    private void OnDisable()
    {
        Actions.Disable();
        InputSystem.onEvent -= DetectGamepad;
    }

    private void DetectGamepad(UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, InputDevice device)
    {
        var gamepad = device as Gamepad;
        UsingGamepad = gamepad != null;
    }



}