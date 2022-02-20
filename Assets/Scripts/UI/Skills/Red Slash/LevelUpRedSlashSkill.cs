using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpRedSlashSkill : MonoBehaviour
{
    [SerializeField] private GameObject redSlashSkillButton;
    [SerializeField] private Skill skillLevel1, skillLevel2, skillLevel3;

    private int redSlashLevel = 1;

    public void LevelUpSkill()
    {
        if(redSlashLevel < 3)
            redSlashLevel++;

        //if (redSlashLevel == 2)
        //    redSlashSkillButton.GetComponent<SkillSlot>().skillObject = skillLevel2;
        //else if (redSlashLevel == 3)
        //    redSlashSkillButton.GetComponent<SkillSlot>().skillObject = skillLevel3;
    }
}
