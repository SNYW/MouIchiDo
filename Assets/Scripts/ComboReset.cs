using UnityEngine;

public class ComboReset : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Samurai.instance.inCombo = false;
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!Samurai.instance.inCombo)
        {
            Samurai.instance.ResetCombo();
        }
    }
}
