using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skill", order = 1)]
public class Skill : ScriptableObject
{
    public GameObject animation;
    public float duration;
}
