using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //      ======   实现角色移动   ======
    //      1.定义角色移动的按键WSAD
    public KeyCode KeyUp = KeyCode.W;
    public KeyCode KeyDown = KeyCode.S;
    public KeyCode KeyLift = KeyCode.A;
    public KeyCode KeyRight = KeyCode.D;

    //      2.获得角色移动的模长
    public float forAndafter;//四边形前后移动距离
    public float liftTorigth;//四边形左右移动距离
    public float forAndafter2;//圆形前后移动距离
    public float liftTorigth2;//圆形左右移动距离
    public float lengTh;//用圆形范围得到的模长
    public Vector2 direction;//向量方向

    void Update()
    {
        //方形范围得到的坐标（forAndafter,liftTorigth）
        forAndafter = (float)((Input.GetKey(KeyUp) ? 1.0 : 0) - (Input.GetKey(KeyDown) ? 1.0 : 0));
        liftTorigth = (float)((Input.GetKey(KeyLift) ? 1.0 : 0) - (Input.GetKey(KeyRight) ? 1.0 : 0));
        //圆形范围得到的坐标
        Vector2 TempVc = SqureToCircle(new Vector2(forAndafter, liftTorigth));
        forAndafter2 = TempVc.x;
        liftTorigth2 = TempVc.y;
        lengTh = Mathf.Sqrt((float)(forAndafter2 * forAndafter2 + liftTorigth2 * liftTorigth2));
        direction = forAndafter2 * transform.forward + liftTorigth2 * transform.right;//角色要走的方向
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
