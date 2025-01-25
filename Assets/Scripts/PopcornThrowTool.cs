using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class PopcornThrowTool : MonoBehaviour
{    
    public float cookTime = 3f;

    public GameObject Kernel = null;



    private InputController input;
    private IEnumerator cookCoroutine = null;
    private float elapsed = 0f;


    void Start()
    {
        input = GetComponent<InputController>();
        input.Actions.Player.Attack.started += StartCook;
        input.Actions.Player.Attack.performed += StartThrow;
    }
    
    public void StartCook(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.one;
        StartCoroutine(cookCoroutine = Cook());
    }

    private void StartThrow(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        StartCoroutine(Throw());
    }

    private IEnumerator Cook()
    {
        elapsed = 0f;

        while (elapsed < cookTime)
        {

            elapsed += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(Throw());
        cookCoroutine = null;
    }

    private IEnumerator Throw()
    {
        if (cookCoroutine != null)
        {
            StopCoroutine(cookCoroutine);
            cookCoroutine = null;
        }

        Vector2 movementDirection = input.Actions.Player.Move.ReadValue<Vector2>();
        

        Debug.Log("Throwing popcorn");
        yield return null;
    }
 
}