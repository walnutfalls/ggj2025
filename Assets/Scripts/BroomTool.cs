using System.Collections;
using UnityEngine;

public class BroomTool : MonoBehaviour
{
    [SerializeField]
    private GameObject broom;

    private Rigidbody2D rb;

    private IEnumerator _waveCoroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
