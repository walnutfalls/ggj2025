using System.Collections;
using UnityEngine;

public class Kernel : MonoBehaviour
{
    public GameObject popcorn;

    
    public void Run(float timeRemaining)
    {
        StartCoroutine(WaitPop(timeRemaining));
    }

    private IEnumerator WaitPop(float timeRemaining)
    {
        yield return new WaitForSeconds(timeRemaining);
        var go = Instantiate(popcorn, transform.position, Quaternion.identity);
        go.GetComponent<Rigidbody2D>().linearVelocity = GetComponent<Rigidbody2D>().linearVelocity;
        Destroy(gameObject);
    }
}