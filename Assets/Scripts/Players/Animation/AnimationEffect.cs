using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEffect : StateMachineBehaviour
{
    [SerializeField] private List<GameObject> particle;
    [SerializeField] List<float> delay;
    [SerializeField] List<SkillPositionType> positionType;
    [SerializeField] List<SkillEffectType> skillEffectTypes;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SkillManager._instance.AddEffectList(delay, particle, positionType, skillEffectTypes);
    }
}
