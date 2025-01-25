using System.Collections;
using UnityEngine;

public class BroomTool : MonoBehaviour
{
    [SerializeField]
    private GameObject broom;

    private Rigidbody2D rb;

    private IEnumerator _waveCoroutine;

    InputController input;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<InputController>();
    }

    private void Update()
    {
        broom.transform.position = transform.position;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DoWave();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            StopWave();
        } else {
            var dir = input.Movement;
            broom.transform.rotation = Quaternion.FromToRotation(Vector2.up, dir);
        }
    }

    private void DoWave()
    {
        StartCoroutine(_waveCoroutine = WaveBroom());
    }

    private void StopWave()
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
            rb.angularVelocity = 90f;
            yield return new WaitForSeconds(0.5f);
            rb.angularVelocity = -90f;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
