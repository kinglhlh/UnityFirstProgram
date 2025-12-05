using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    //      ======   获取碰撞胶囊的参数   ======
    [Header("parameters of collision capsule")]
    public CapsuleCollider cpC;
    [SerializeField] 
    private Vector3 poinT1; // 定义的胶囊下端点
    [SerializeField] 
    private Vector3 poinT2; // 定义的胶囊上端点
    private float radius;

    //      ======   获取落地状态   ======
    public bool isGrounded;

    private void Awake()
    {
        //获取半径
        radius = cpC.radius;
    }

    void FixedUpdate()
    {
        //      ======   使用Physics.OverlapCapsule，进行落地检测   ======
        poinT1 = transform.position + transform.up * radius;
        poinT2 = transform.position + transform.up * cpC.height - transform.up * radius;
        //外部碰撞体数组
        Collider[] outcolliders = Physics.OverlapCapsule(poinT1, poinT2, radius, LayerMask.GetMask("Ground"));
        if (outcolliders.Length > 0)
        {
            isGrounded = true; 
        }
        else
        {
            isGrounded = false; 
        }

        if (outcolliders.Length != 0)
        {
            foreach (var col in outcolliders)
            {
                print("检测到地面：" + col.name);
            }
        }
    }
}