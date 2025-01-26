using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Texture2D cursor;

    [SerializeField] private Sprite fwd;
    [SerializeField] private Sprite back;


    private Rigidbody2D body;

    public float runSpeed = 10f;

    private void Start()
    {
       Cursor.SetCursor(this.cursor, new Vector2(2.0f, 2.0f), CursorMode.ForceSoftware);
    }

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

        var sr = GetComponent<SpriteRenderer>();
        if (vertical > 0)
        {
            sr.sprite = back;
        }
        else if (vertical < 0)
        {
            sr.sprite = fwd;
        }
    }
}