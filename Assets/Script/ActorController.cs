using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    //      ======   获取其它组件   ======
    public GameObject model;//抓取要控制的模型
    public PlayerInput pi;//调用PlayerInput脚本
    [SerializeField]
    private Animator anim;//获取组件Animator

    void Awake()
    { 
        anim = model.GetComponent<Animator>();
        pi = GetComponent<PlayerInput>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
