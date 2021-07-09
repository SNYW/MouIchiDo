using UnityEngine;

public class AttackChain : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Samurai.instance.CanCombo())
            {
                Samurai.instance.inCombo = true;
                Samurai.instance.Combo("Attack");
            }
        }
    }
}
