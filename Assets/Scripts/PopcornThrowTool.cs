using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class PopcornThrowTool : MonoBehaviour
{
    public float cookTime = 1.5f;
    public float foce = 300f;

    public GameObject Kernel = null;

    public bool IsBusy => cookCoroutine != null || throwCoroutine != null;


    private InputController input;

    private IEnumerator cookCoroutine = null;
    private IEnumerator throwCoroutine = null;

    private bool _triggered = false;
    private Vector2 _direction = Vector2.zero;

    private GameObject cookingKernel = null;

    void OnEnable()
    {
        input = FindFirstObjectByType<InputController>();

        input.Actions.Player.Attack.performed += StartCook;
        input.Actions.Player.Attack.canceled += StartThrow;
    }

    void OnDisable()
    {
        input.Actions.Player.Attack.performed -= StartCook;
        input.Actions.Player.Attack.canceled -= StartThrow;        
    }


    void Update()
    {
        if (input.UsingGamepad)
        {
            _direction = input.Actions.Player.Look.ReadValue<Vector2>();

            if (_direction.magnitude > 0.3f)
            {
                if (!_triggered)
                {
                    _triggered = true;
                    StartCoroutine(cookCoroutine = Cook());
                }
            }
            else if (_triggered && _direction.magnitude < 0.3f)
            {
                _triggered = false;
                StartCoroutine(throwCoroutine = Throw());
            }
        } else {
            _direction = input.Movement;
        }
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
        cookingKernel.GetComponent<Rigidbody2D>().simulated = false;
        cookingKernel.transform.parent = transform;

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

        cookingKernel.GetComponent<Rigidbody2D>().simulated = true;
        cookingKernel.GetComponent<Rigidbody2D>().AddForce(_direction.normalized * foce);

        yield return new WaitForSeconds(0.3f);
        throwCoroutine = null;
        cookingKernel = null;
    }
}