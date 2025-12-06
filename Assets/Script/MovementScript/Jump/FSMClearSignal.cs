using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Unity 专门给 Animator 状态机设计的基类，挂载到动画状态上后，能监听该状态的 “进入 / 更新 / 离开” 等生命周期
public class FSMCleanSignal : StateMachineBehaviour
{
    //      ======   定义要清除的信号   ======
    //      1.进入信号
    public string[] clearEnterSignals;
    //      2.完成信号
    public string[] clearExitSignals;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var signal in clearEnterSignals)//遍历进入信号
        {
            animator.ResetTrigger(signal);//清空
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var signal in clearExitSignals)//遍历离开信号
        {
            animator.ResetTrigger(signal);
        }
    }
}
