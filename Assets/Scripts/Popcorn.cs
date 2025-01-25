using UnityEngine;

public class Popcorn : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioSource>().Play();
    }
}
