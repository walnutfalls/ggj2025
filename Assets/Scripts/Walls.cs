using UnityEngine;

public class Walls : MonoBehaviour
{
    public GameObject Top;
    public GameObject Right;
    public GameObject Bottom;
    public GameObject Left;
    
    void Start()
    {
        Camera cam = Camera.main;
        if (cam != null && cam.orthographic)
        {
            float aspect = cam.aspect;
            float halfHeight = cam.orthographicSize;
            float halfWidth = halfHeight * aspect;

            float leftBound = cam.transform.position.x - halfWidth;
            float rightBound = cam.transform.position.x + halfWidth;
            float topBound = cam.transform.position.y + halfHeight;
            float bottomBound = cam.transform.position.y - halfHeight;
            
            Top.transform.position = new Vector3(cam.transform.position.x, topBound, 0);
            Right.transform.position = new Vector3(rightBound, cam.transform.position.y, 0);
            Bottom.transform.position = new Vector3(cam.transform.position.x, bottomBound, 0);
            Left.transform.position = new Vector3(leftBound, cam.transform.position.y, 0);

            Top.GetComponent<BoxCollider2D>().size = new Vector2(halfWidth * 2, 1);
            Right.GetComponent<BoxCollider2D>().size = new Vector2(1, halfHeight * 2);
            Bottom.GetComponent<BoxCollider2D>().size = new Vector2(halfWidth * 2, 1);
            Left.GetComponent<BoxCollider2D>().size = new Vector2(1, halfHeight * 2);
        }        
    }
}