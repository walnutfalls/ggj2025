using System.Collections.Generic;
using UnityEngine;

public class Invenory : MonoBehaviour
{
    private InputController input;

    public List<GameObject> ToolComponents;
    private int currentToolIndex = 0;

    public int CurrentToolIndex
    {
        get { return currentToolIndex; }
        set {
            if (value < 0)
            {
                currentToolIndex = ToolComponents.Count - 1;
            } else if (value >= ToolComponents.Count)
            {
                currentToolIndex = 0;
            } else {
                currentToolIndex = value;
            }

            ToolComponents.ForEach(tool => tool.SetActive(false));
            ToolComponents[currentToolIndex].SetActive(true);
        }
    }

    private void Awake()
    {
        input = GetComponent<InputController>();
    }

    private void Start()
    {
        input.Actions.Player.Next.performed += ToggleInventory;
        input.Actions.Player.Previous.performed += ToggleInventoryPrev;
        CurrentToolIndex = 0;
    }

    private void ToggleInventory(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        CurrentToolIndex++;
    }

    private void ToggleInventoryPrev(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        CurrentToolIndex--;
    }
}