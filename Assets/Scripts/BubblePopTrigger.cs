using UnityEngine;

public class BubblePopTrigger : MonoBehaviour
{
    private const int POPCORN_LAYER = 6;

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.GetComponent<Popcorn>() != null)
        {
            Destroy(other.gameObject);
            ApplyEffect();
        }
    }

    private void ApplyEffect()
    {
        Debug.Log("Popcorn Eaten!");
        // ...
    }
}