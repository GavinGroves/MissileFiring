using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 导弹管理器
/// </summary>
public class MissileManager : MonoBehaviour
{
    private Transform m_Transform;
    private Transform[] createPoints; //导弹生成点数组
    private GameObject prefab_Missile3;//导弹预制体

    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        createPoints = GameObject.Find("CreatePoints").GetComponent<Transform>().GetComponentsInChildren<Transform>();
        prefab_Missile3 = Resources.Load<GameObject>("Missile_3");
        //调用导弹生成器
        InvokeRepeating("CreateMissile",3,5);
    }

    /// <summary>
    /// 随机位置生成导弹
    /// </summary>
    private void CreateMissile()
    {
        // 0~3(包左不包右，4个随机点
        int index = Random.Range(0, createPoints.Length);

        Instantiate(prefab_Missile3,createPoints[index].position,Quaternion.identity,m_Transform);
    }

    /// <summary>
    /// 停止生成导弹
    /// </summary>
    public void StopCreateMissile()
    {
        CancelInvoke();
    }
}