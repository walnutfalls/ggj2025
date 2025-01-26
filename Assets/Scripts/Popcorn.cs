using UnityEngine;

public class Popcorn : MonoBehaviour
{
    [Tooltip("Amount of happiness this will add if colliding with a bubble.")]
    [SerializeField] private float _happinessIncrease;
    public float HappinessIncrease { get => _happinessIncrease; set { _happinessIncrease = value; } }

    void Start()
    {
        if (AudioSystem.Instance != null)
        {
            AudioSystem.Instance.PlaySound("Popcorn Bomb");
        }
    }
}
