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
        //Debug.Log(IsMouseOverUIWithIgnore());
        IsMouseOverUIWithIgnore();
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void IsMouseOverUIWithIgnore()  
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
                ButtonClick buttonClick = raycastResults[i].gameObject.GetComponent<ButtonClick>();


                moveableHint.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = 
                    (buttonClick.enchanceType == EnchanceType.SkillDamage) ? "Increases the damage of the skill by " + buttonClick.damage.ToString() :
                    (buttonClick.enchanceType == EnchanceType.CriticalChance) ? "Increases the critical change of the skill by " + buttonClick.criticalChance.ToString() :
                    (buttonClick.enchanceType == EnchanceType.LevelUp) ? "Add new combo to skill" : "Something went wrong!";

                Debug.Log(clickButton.GetComponent<RectTransform>().anchoredPosition);
                //clickButton.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                //clickButton = raycastResults[i].gameObject.transform.GetChild(0).gameObject;
                //clickButton.SetActive(true);
                
                break;                
            }
            else
            {
                moveableHint.SetActive(false);
                // clickButton.SetActive(false);
            }
        }
        //return false;
    }
}
