using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamageType : MonoBehaviour
{
    public enum SkillType
    {
        RedSkillRight,
        RedSkillLeft,
        RedSkillCrack, 
        FireSkillRight,
        FireSkillLeft,
        FireSkillCrack,
        UltiSlash,
        GreenSlash,
        Phoenix,
        GroundSlash
    }
    [SerializeField] public SkillType _skillType;
}
