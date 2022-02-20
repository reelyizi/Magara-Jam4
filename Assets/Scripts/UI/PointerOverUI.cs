using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerOverUI : MonoBehaviour
{
    private GameObject clickButton;
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

                clickButton = raycastResults[i].gameObject.transform.GetChild(0).gameObject;
                clickButton.SetActive(true);
                return true;
                
            }
            else
            {
                clickButton.SetActive(false);
            }
        }
        return false;
    }
}
