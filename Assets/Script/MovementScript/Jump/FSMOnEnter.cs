using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMOnEnter : StateMachineBehaviour
{
    //      ======   Ω¯»Î–≈∫≈   ======
    public string[] OnEnterMessages;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var msg in OnEnterMessages)
        {
            animator.gameObject.SendMessageUpwards(msg);
        }
    }
}
