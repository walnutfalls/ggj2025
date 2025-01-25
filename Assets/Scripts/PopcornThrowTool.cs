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

    private float elapsed = 0f;

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
        elapsed = 0f;

        cookingKernel = Instantiate(Kernel, transform.position, Quaternion.identity);
        cookingKernel.transform.position -= new Vector3(0, 0, 1);

        var origScale = cookingKernel.transform.localScale;

        while (elapsed < cookTime)
        {
            var t = elapsed / cookTime;

            cookingKernel.transform.localScale = Vector3.Lerp(origScale * 0.5f, origScale, t);


            elapsed += Time.deltaTime;
            direction = input.Actions.Player.Move.ReadValue<Vector2>();
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
            // Use the existing direction from the input controller for other platforms
        #endif
        
        
        cookingKernel.GetComponent<Rigidbody2D>().AddForce(direction * foce);
        cookingKernel.GetComponent<Kernel>().Run(cookTime - elapsed);

        yield return new WaitForSeconds(0.3f);
        throwCoroutine = null;
        cookingKernel = null;
    }
}