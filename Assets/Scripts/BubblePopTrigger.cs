using UnityEngine;

public class BubblePopTrigger : MonoBehaviour
{
    private const int PLAYER_LAYER = 3;
    private const int POPCORN_LAYER = 6;
    private const int BROOM_LAYER = 8;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.TryGetComponent(out Popcorn popcorn))
        {
            Destroy(other.gameObject);
            ApplyEffect(popcorn);
        }

        if (other.gameObject.layer == BROOM_LAYER)
        {
            transform.parent.GetComponent<Bubble>().IsStunned = true;
        }

        if (other.gameObject.layer == PLAYER_LAYER)
        {
            Destroy(transform.parent.gameObject);
            AudioSystem.Instance.PlaySound("Discontent Chatter 1");
        }
    }

    private void ApplyEffect(Popcorn popcorn)
    {
        AudioSystem.Instance.PlaySound("Pop Scream 1");
        transform.parent.GetComponent<Happiness>().AddHappiness(popcorn.HappinessIncrease);
    }
}
