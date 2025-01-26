using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Texture2D cursor;

    [SerializeField] private Sprite fwd;
    [SerializeField] private Sprite back;


    private Rigidbody2D body;

    public float runSpeed = 10f;

    private Transform _nextHatPos;

    private HashSet<HatScriptable> WornHats = new HashSet<HatScriptable>();
    

    public void ReceiveHat(HatScriptable hat)
    {
        if (WornHats.Contains(hat)) return;

        var lastHatSr = _nextHatPos.gameObject.GetComponent<SpriteRenderer>();
        

        var hatGo = new GameObject();
        var sr = hatGo.AddComponent<SpriteRenderer>();
        sr.sprite = hat.HatSprite;
        sr.sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1 + WornHats.Count;        
        hatGo.transform.parent = _nextHatPos;
        hatGo.transform.localPosition = Vector2.up * lastHatSr.bounds.extents.y;
        _nextHatPos = hatGo.transform;

        hatGo.transform.localScale *= GetComponent<SpriteRenderer>().bounds.extents.x / hat.HatSprite.bounds.extents.x;
        WornHats.Add(hat);
    }

    private void Start()
    {
        _nextHatPos = transform;
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