using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private int requiredXP;

    public GameObject fade;
    public EnchanceType enchanceType;
    public SkillType skillType;

    public int damage;
    public int criticalChance;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        if (GameManager.instance.playerXP >= requiredXP)
        {
            GameManager.instance.playerXP -= requiredXP;
            switch (enchanceType)
            {
                case EnchanceType.SkillDamage:
                    switch (skillType)
                    {
                        case SkillType.RedSlash:
                            SkillDamageManager._instance.redSlashDamage += damage;
                            break;
                        case SkillType.GreenSlash:
                            SkillDamageManager._instance.greenSlashDamage += damage;
                            break;
                        case SkillType.FireSlash:
                            SkillDamageManager._instance.fireSlashDamage += damage;
                            break;
                        case SkillType.Ulti:
                            SkillDamageManager._instance.ultiDamage += damage;
                            break;
                        default:
                            Debug.LogError("Something went wrong!");
                            break;
                    }
                    break;
                case EnchanceType.CriticalChance:
                    switch (skillType)
                    {
                        case SkillType.RedSlash:
                            SkillDamageManager._instance.redSlashCriticalChance += criticalChance;
                            break;
                        case SkillType.GreenSlash:
                            SkillDamageManager._instance.greenCriticalChance += criticalChance;
                            break;
                        case SkillType.FireSlash:
                            SkillDamageManager._instance.fireCriticalChance += criticalChance;
                            break;
                        default:
                            Debug.LogError("Something went wrong!");
                            break;
                    }
                    break;
                case EnchanceType.LevelUp:
                    switch (skillType)
                    {
                        case SkillType.RedSlash:
                            SkillDamageManager._instance.RedSlashLevel = 1;
                            break;
                        default:
                            Debug.LogError("Something went wrong!");
                            break;
                    }
                    break;
            }

            GetComponent<Button>().enabled = false;
            if (fade != null)
                fade.SetActive(false);
        }
        else
            Debug.Log("Not enough xp");
    }
}
public enum EnchanceType
{
    LevelUp,
    SkillDamage,
    CriticalChance
}

public enum SkillType
{
    RedSlash,
    GreenSlash,
    FireSlash,
    Ulti
}