using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public InputSystem_Actions Actions {get; private set; }

    public bool UsingGamepad {get; private set;}


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