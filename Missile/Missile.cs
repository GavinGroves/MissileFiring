using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Missile : MonoBehaviour
{
    private Transform m_Transform;
    private Transform Player_Transform;
    private Vector3 normalForward = Vector3.forward; //导弹默认方向：向前

    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        Player_Transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        //导弹保持向前
        m_Transform.Translate(Vector3.forward);
        //--------------- 实现导弹追踪角色 -----------------------
        //计算方向（导弹与角色之间的方向，角色位置相减导弹位置，得到位置向量
        Vector3 dir = Player_Transform.position - m_Transform.position;
        //插值计算导弹要旋转的角度（以当前导弹前方与上一步得到的向量进行插值,平滑的从当前位置转向dir位置，为旋转做准备
        normalForward = Vector3.Lerp(normalForward, dir,Time.deltaTime);
        //改变当前导弹旋转角度操作
        m_Transform.rotation = Quaternion.LookRotation(normalForward);
    }
}