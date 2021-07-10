using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWalk : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Samurai.instance.canWalk = true;
    }
}
