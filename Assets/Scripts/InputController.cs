using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public InputSystem_Actions Actions {get; private set; }


    private void OnEnable()
    {
        Actions = new InputSystem_Actions();
        Actions.Enable();
    }

    private void OnDisable()
    {
        Actions.Disable();
    }

    private void Start()
    {        
    }
}