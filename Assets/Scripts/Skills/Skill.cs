using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skill", order = 1)]
public class Skill : ScriptableObject
{
    public List<GameObject> animation;
    public List<float> cooldown;
    public List<float> spawnDuration;
    public int skillCooldown;

    public SkillPositionType positionType;
}

public enum SkillPositionType
{
    character,
    frontCharacter
}
