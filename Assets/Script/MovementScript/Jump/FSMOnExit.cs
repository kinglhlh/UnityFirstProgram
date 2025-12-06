using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMOnExit : StateMachineBehaviour
{
    //      ======   ÍË³öÐÅºÅ   ======
    public string[] OnExitMessages;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var msg in OnExitMessages)
        {
            animator.gameObject.SendMessageUpwards(msg);
        }
    }

}
