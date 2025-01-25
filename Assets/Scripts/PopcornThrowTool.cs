using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class PopcornThrowTool : MonoBehaviour
{    
    public float cookTime = 3f;
    public float foce = 300f;

    public GameObject Kernel = null;

    public bool IsBusy => cookCoroutine != null || throwCoroutine != null;


    private InputController input;
    
    private IEnumerator cookCoroutine = null;
    private IEnumerator throwCoroutine = null;


    private Vector2 direction;

    private GameObject cookingKernel = null;



    void Start()
    {
        input = GetComponent<InputController>();
        input.Actions.Player.Attack.performed += StartCook;
        input.Actions.Player.Attack.canceled += StartThrow;
    }
    
    public void StartCook(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        StartCoroutine(cookCoroutine = Cook());
    }

    private void StartThrow(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        StartCoroutine(throwCoroutine = Throw());
    }

    private IEnumerator Cook()
    {
        cookingKernel = Instantiate(Kernel, transform.position, Quaternion.identity);
        cookingKernel.transform.position -= new Vector3(0, 0, 1);
        cookingKernel.GetComponent<Kernel>().Run(cookTime);

        yield return new WaitForSeconds(cookTime);

        StartCoroutine(Throw());
        cookCoroutine = null;
    }

    private IEnumerator Throw()
    {
        if (cookingKernel == null)
        {
            throwCoroutine = null;
            yield break;
        }

        if (cookCoroutine != null)
        {
            StopCoroutine(cookCoroutine);
            cookCoroutine = null;
        }

        #if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
            if (input.UsingGamepad)
            {
                direction = input.Actions.Player.Move.ReadValue<Vector2>();
            }
            else
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                direction = (mousePosition - transform.position).normalized;
            }
        #else
            direction = input.Actions.Player.Move.ReadValue<Vector2>();
        #endif
        
        
        cookingKernel.GetComponent<Rigidbody2D>().AddForce(direction * foce);
        

        yield return new WaitForSeconds(0.3f);
        throwCoroutine = null;
        cookingKernel = null;
    }
}