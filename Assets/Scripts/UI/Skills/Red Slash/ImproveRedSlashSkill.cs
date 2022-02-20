using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImproveRedSlashSkill : MonoBehaviour
{
    public void EnchanceAttackDamage(int damage)
    {
        SkillDamageManager._instance.redSlashDamage += damage;
    }

    public void IncreaseCritChance(int chance)
    {
        SkillDamageManager._instance.redSlashCriticalChance += chance;
    }
}
