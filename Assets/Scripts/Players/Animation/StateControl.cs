using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateControl : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SkillManager._instance.SkillReset();
    }
}
