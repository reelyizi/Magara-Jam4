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

    public int redSlashDamage = 40;
    public int redSlashCriticalChance = 10;
    public int RedSlashCriticalChance
    {
        get { return redSlashCriticalChance; }
        set { redSlashCriticalChance += value; }
    }

    public int redSlashLevel = 1;
    public int RedSlashLevel { 
        get { return redSlashLevel; } 
        set { 
            redSlashLevel += value; 
        } 
    }

    public int greenSlashDamage = 20;
    public int greenCriticalChance = 30;
    public int fireSlashDamage = 70;
    public int fireCriticalChance = 5;
    public int ultiDamage = 200;
}
