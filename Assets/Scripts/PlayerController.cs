using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    public float runSpeed = 10f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical).normalized * runSpeed;
        body.linearVelocity = movement;
    }
}