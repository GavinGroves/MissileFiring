using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 奖励物品控制
/// </summary>
public class Reward : MonoBehaviour
{
    private Transform m_Transform;
    
    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
    }

    void Update()
    {
        m_Transform.Rotate(Vector3.left);
    }

    /// <summary>
    /// 奖励物品销毁时会执行该方法
    /// </summary>
    private void OnDestroy()
    {
        Debug.Log("我被销毁了");
        //向父级（RewardManager）发送被销毁OnDestroy消息，并且执行父级方法“RewardCountDown”
        SendMessageUpwards("RewardCountDown");
    }
}
