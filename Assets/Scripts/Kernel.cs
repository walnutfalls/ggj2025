using System.Collections;
using UnityEngine;

public class Kernel : MonoBehaviour
{
    public GameObject popcorn;

    private float elapsed = 0f;

    
    public void Run(float timeRemaining)
    {
        StartCoroutine(WaitPop(timeRemaining));
    }

    private IEnumerator WaitPop(float timeRemaining)
    {
        var origScale = transform.localScale;

        while (elapsed < timeRemaining)
        {
            var t = elapsed / timeRemaining;
            transform.localScale = Vector3.Lerp(origScale * 0.5f, origScale, t);
            elapsed += Time.deltaTime;            
            yield return null;
        }
        
        var go = Instantiate(popcorn, transform.position, Quaternion.identity);
        go.GetComponent<Rigidbody2D>().linearVelocity = GetComponent<Rigidbody2D>().linearVelocity;
        Destroy(gameObject);
    }

    
}