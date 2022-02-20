using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
    private GameObject previousSkill = null;
    public void SelectSkill(GameObject skill)
    {
        if(previousSkill != null)
        {
            previousSkill.SetActive(false);
        }
        previousSkill = skill;
        skill.SetActive(true);
    }
}
