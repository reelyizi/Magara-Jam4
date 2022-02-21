using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PointerOverUI : MonoBehaviour
{
    private GameObject clickButton;
    [SerializeField] private GameObject moveableHint;
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
                moveableHint.SetActive(true);
                ButtonClick buttonClick = (ButtonClick)raycastResults[i].gameObject.GetComponent<ButtonClick>();

                moveableHint.GetComponent<TextMeshProUGUI>().text = (buttonClick.enchanceType == EnchanceType.SkillDamage) ? "Increases the damage of the skill by " + buttonClick.damage :
                    (buttonClick.enchanceType == EnchanceType.CriticalChance) ? "Increases the critical change of the skill by " + buttonClick.criticalChance :
                    (buttonClick.enchanceType == EnchanceType.LevelUp) ? "Add new combo to skill " + buttonClick.criticalChance : "Something went wrong!";

                clickButton.GetComponent<RectTransform>().position = raycastResults[i].gameObject.GetComponent<RectTransform>().position + Vector3.up * 120;
                //clickButton = raycastResults[i].gameObject.transform.GetChild(0).gameObject;
                //clickButton.SetActive(true);
                return true;
                
            }
            else
            {
                moveableHint.SetActive(false);
                // clickButton.SetActive(false);
            }
        }
        return false;
    }
}
