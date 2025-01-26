using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDetector : MonoBehaviour
{
    public bool IsMouseOver { get; private set; } = false;

    private List<RaycastResult> preallocatedRaycastResultList = new();

    public void Update()
    {
        this.IsMouseOver = this.IsMouseOverSelf();
    }

    private bool IsMouseOverSelf()
    {
        PointerEventData eventDataCurrentPosition =
            new(EventSystem.current)
            {
                position = new Vector2(Input.mousePosition.x, Input.mousePosition.y),
            };

        preallocatedRaycastResultList.Clear();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, preallocatedRaycastResultList);

        foreach (var raycastResult in preallocatedRaycastResultList)
        {
            Debug.Log($"casting to {raycastResult.gameObject.name}");
            if (raycastResult.gameObject.Equals(this.gameObject))
            {
                Debug.Log($"true for {this.name}");
                return true;
            }
        }

                Debug.Log($"false for {this.name}");
        return false;
    }
}
