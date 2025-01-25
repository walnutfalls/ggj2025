using UnityEngine;

public class BubblePopTrigger : MonoBehaviour
{
    private const int POPCORN_LAYER = 6;
    private const int BROOM_LAYER = 8;

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.GetComponent<Popcorn>() != null)
        {
            Destroy(other.gameObject);
            ApplyEffect();
        }

        if (other.gameObject.layer == BROOM_LAYER)
        {
            transform.parent.GetComponent<Bubble>().IsStunned = true;
        }
    }

    private void ApplyEffect()
    {
        Debug.Log("Popcorn Eaten!");
        GetComponent<AudioSource>().Play();
        // ...
    }
}