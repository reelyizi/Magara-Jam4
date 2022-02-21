using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamageManager : MonoBehaviour
{
    public static SkillDamageManager _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public Skill redAttackLevelTwo;
    public Skill redAttackLevelThree;
    public GameObject skillSlot;

    public int redSlashDamage = 40;
    public int redSlashCriticalChance = 10;
    public int redSlashLevel = 1; 
    public int redCrackDamage = 70;
    
    public int RedSlashLevel { 
        get { return redSlashLevel; } 
        set { 
            redSlashLevel += value; 
            if(redSlashLevel == 2)            
                skillSlot.GetComponent<SkillSlot>().skillObject = redAttackLevelTwo;
            else if(redSlashLevel == 3)
                skillSlot.GetComponent<SkillSlot>().skillObject = redAttackLevelThree;
        } 
    }

    public int greenSlashDamage = 20;
    public int greenCriticalChance = 30;
    public int fireSlashDamage = 70;
    public int fireCriticalChance = 5;
    public int fireCrackDamage = 85;
    public int ultiDamage = 200;

    public int phoenixDamage = 150;
    public int phoenixCriticalChance = 10;
}
