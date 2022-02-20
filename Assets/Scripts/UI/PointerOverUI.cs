using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerOverUI : MonoBehaviour
{
    void Update()
    {
        Debug.Log(IsMouseOverUIWithIgnore());

    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private bool IsMouseOverUIWithIgnore()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
        for (int i = 0; i < raycastResults.Count; i++)
        {
            if(raycastResults[i].gameObject.GetComponent<ButtonClick>() != null)
            {
                Debug.Log("Test");
                return true;
            }
        }
        return false;
    }
}
