using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class PlayerInput : MonoBehaviour
{
    //      ======   获取的组件   ======
    public GameObject model;//获取人物模型


    //      ======   实现角色移动   ======
    //      1.定义角色移动的按键WSAD
    [Header("===   Moving based    ===")]
    public KeyCode KeyUp = KeyCode.W;
    public KeyCode KeyDown = KeyCode.S;
    public KeyCode KeyLeft = KeyCode.A;
    public KeyCode KeyRight = KeyCode.D;

    [Header("===   Function based    ===")]
    public KeyCode KeyA;//是否进入跑步状态
    public KeyCode KeyB;//设定为K,进行跳跃功能

    public bool InputEnable = true;//通过判断InputEnable的值来控制玩家输入

    //      2.获得角色移动的变量
    [Header("===   Speed Variable quantity   ===")]
    public float targetforAndafter;//四边形前后移动的目标输入值
    public float targetleftTorigth;///四边形左右移动的目标输入值
    public float forAndafter;//四边形前后移动输入值
    public float leftTorigth;//四边形左右移动输入值
    public float VelocityforAndafter;// 调用Mathf.SmoothDamp方法时的速度参数，不赋值
    public float VelocityliftTorigth;

    public float forAndafter2;//圆形前后移动输入值
    public float liftTorigth2;//圆形左右移动输入值

    public float lengTh;//输入强度
    public Vector3 direcTion;//向量方向

    //      3.角色跳跃的变量
    [Header("===   Jump Variable quantity   ===")]
    public bool jump;//通过对jump的判断来触发触发器
    private bool Lastjump;//在对jump判断之前，增加Lastjump与newJump的判断来控制跳跃次数
    private bool newJump;

    //      ======   动画触发信号判断   ======
    public bool run;//奔跑控制器（为真，则进入奔跑状态）

    void Update()
    {
        //      $$$$$$   移动部分   $$$$$$
        //方形范围目标得到的坐标（forAndafter,leftTorigth）
        targetforAndafter = ((Input.GetKey(KeyUp) ? 1.0f : 0) - (Input.GetKey(KeyDown) ? 1.0f : 0));
        targetleftTorigth = ((Input.GetKey(KeyRight) ? 1.0f : 0) - (Input.GetKey(KeyLeft) ? 1.0f : 0));

        if (InputEnable == false)//使用InputEnable开关来控制玩家的输入功能
        {
            targetforAndafter = 0;
            targetleftTorigth = 0;

        }

        //方形范围实际的坐标,有一个增长的过程
        forAndafter = Mathf.SmoothDamp(forAndafter, targetforAndafter, ref VelocityforAndafter, 0.1f);
        leftTorigth = Mathf.SmoothDamp(leftTorigth, targetleftTorigth, ref VelocityliftTorigth, 0.1f);

        //将四边形范围改为圆形范围然后得到的坐标
        Vector2 TempVc = SqureToCircle(new Vector2(leftTorigth, forAndafter));
        liftTorigth2 = TempVc.x;
        forAndafter2 = TempVc.y; 
        
        lengTh = Mathf.Sqrt((forAndafter2 * forAndafter2 + liftTorigth2 * liftTorigth2));//输入强度
        direcTion = forAndafter2 * Vector3.forward + liftTorigth2 * Vector3.right;//角色要走的方向

        run = Input.GetKey(KeyA);//奔跑状态控制

        //      $$$$$$   跳跃部分   $$$$$$
        newJump = Input.GetKeyDown(KeyB);
        if (newJump != Lastjump && newJump == true)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
        Lastjump = newJump;
    }



    //      ******   将方形范围改为圆形范围的方法   ******
    public Vector2 SqureToCircle(Vector2 input)//这个方法就是用来将平面的二维坐标转化为圆面的二维坐标
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - input.y * input.y / 2);
        output.y = input.y * Mathf.Sqrt(1 - input.x * input.x / 2);
        return output;
    }
}
