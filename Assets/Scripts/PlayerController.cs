using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    public float runSpeed = 10f;

    private PopcornThrowTool popcornThrowTool;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        popcornThrowTool = GetComponent<PopcornThrowTool>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        if (!popcornThrowTool.IsBusy)
        {
            Vector2 movement = new Vector2(horizontal, vertical).normalized * runSpeed;
            body.linearVelocity = movement;
        } else {
            body.linearVelocity = Vector2.zero;
        }
    }
}