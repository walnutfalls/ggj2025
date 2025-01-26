using UnityEngine;

public class Popcorn : MonoBehaviour
{
    void Start()
    {
        AudioSystem.Instance.PlaySound("Popcorn Bomb");
    }
}
