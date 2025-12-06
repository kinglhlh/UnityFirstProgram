using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    //      ======   获取其它组件   ======
    public GameObject model;//抓取要控制的模型
    public PlayerInput pi;//调用PlayerInput脚本
    [SerializeField]
    private Animator anim;//获取组件Animator
    [SerializeField]
    private Rigidbody rigid;//获取刚体

    //      ======   变量声明   ======
    //      1.移动变量
    private Vector3 realLength;//角色移动的最终量
    public float movingSpeed = 3.0f; //基础速度
    public float runMultiplier = 2.0f;//当跑步键按下时，乘以这个速度倍率
    private Vector3 characterTurn;//角色转转向变量
    private float animationTurn; //动画切换变量
    //      2.跳跃变量
    public Vector3 JumpImpulse;//向上跳跃的冲量
    public float JunmpHight = 3.0f;//向上跳跃的高度
    public bool isGround = true;//标记是否在路面
    public bool PlanLock = false;//标记移动锁定
    private Vector3 jumpInertia;//这个是用来保存起跳前的水平速度


    void Awake()
    { 
        //手动获得人物模型后，自动获取的组件
        anim = model.GetComponent<Animator>();
        pi = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //      $$$$$$   移动部分   $$$$$$
        //1.动画播放的平滑切换
        animationTurn = ((pi.run) ? 2.0f : 1.0f);//由它来控制人物跑和走动画的切换                                                                                      
        float targetForward = Mathf.Lerp(anim.GetFloat("forward"), animationTurn, 0.3f);
        anim.SetFloat("forward", pi.lengTh * targetForward);//Mathf.Lerp(线性插值)让动画参数"forward"在走路和跑步之间平滑过渡的，实际上是由1增加到2
       //原理是我们的forward的值达到1就播放走路，达到2就播放跑


        //2.人物转向的切换
        
        if (pi.lengTh > 0.1f && !PlanLock) //添加这个判断，是为了，避免当玩家没有输入时，他的TargetDug和TargetDturn的变为零，导致角色的面朝方向变为0,0
        //这里后续加的!PlanLock是为了避免跳跃时能转向
        {

            characterTurn = Vector3.Slerp(model.transform.forward, pi.direcTion, 0.3f);//Vector3.Slerp（ 球面插值）是用来做人物转向缓冲的
            //原理也是：由模型现在的朝像，以莫个定值转向另一个放像
            model.transform.forward = characterTurn;
        }

        //角色最终的移动，到这里还没完，我们要让角色的刚体移动
        if (pi.lengTh > 0.1f && !PlanLock)
        {
            realLength = pi.lengTh * pi.direcTion * movingSpeed * ((pi.run) ? runMultiplier : 1.0f);
            jumpInertia = realLength; // 保存当前速度作为跳跃惯性
        }
        else if (PlanLock)
        {
            realLength = jumpInertia; // 跳跃中：用起跳前的惯性移动，不响应新输入
        }
        else
        {
            realLength = Vector3.zero; // 输入强度太小，人物不动
        }

        //      $$$$$$   跳跃部分   $$$$$$
        if (pi.jump && isGround)
        {
            anim.SetTrigger("jump");
         
        }
    }

    private void FixedUpdate()
    {
        //      $$$$$$   移动部分   $$$$$$
        //控制刚体移动
        rigid.velocity = new Vector3(realLength.x, rigid.velocity.y, realLength.z ) + JumpImpulse;//后面的JumpImpulse是跳跃部分控制的

        //      $$$$$$   跳跃部分   $$$$$$
        JumpImpulse = Vector3.zero;//清空向上的冲力
    }

    //      ======   外部信息接收区   ======
    //      1.是否在地面
    public void Inground()
    {
        print("is ground");
        isGround = true;
        anim.SetBool("isground", true);

        PlanLock = false;
        pi.InputEnable = true;
        jumpInertia = Vector3.zero; // 落地后清空惯性
    }

    public void NotInground()
    {
        print("not is ground");
        isGround= false;
        anim.SetBool("isground", false);
    }

    //      2.是否在跳跃
    public void OnJumpEnter()
    {
        JumpImpulse = new Vector3(jumpInertia.x , JunmpHight, jumpInertia.z);
        print("跳跃冲量已添加，角色将起跳！");
        PlanLock = true;
        pi.InputEnable = false;
    }

    public void OnJumpExit()
    {
       
    }
}
