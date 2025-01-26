using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Texture2D cursor;

    private Rigidbody2D body;

    public float runSpeed = 10f;

    private PopcornThrowTool popcornThrowTool;

    private void Start()
    {
       Cursor.SetCursor(this.cursor, new Vector2(2.0f, 2.0f), CursorMode.ForceSoftware);
    }

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