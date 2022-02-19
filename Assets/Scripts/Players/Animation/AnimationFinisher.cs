using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFinisher : MonoBehaviour
{
    public void Finish() => SkillManager._instance.SkillReset();
}
