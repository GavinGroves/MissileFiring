using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 飞机管理器，飞机移动，死亡判定
/// </summary>
public class Ship : MonoBehaviour
{
    private Transform m_Transform;
    private MissileManager m_MissileManager;
    private GameUIManager m_GameUIManager;

    private GameObject prefab_Multi; //爆炸特效预制体

    private int rewardNum = 0; //奖励积分
    private bool isLeft = false; //往左旋转
    private bool isRight = false; //往右旋转
    private bool isDeath; //角色是否死亡
    private int speed;
    private int rotate;

    public int Speed
    {
        get => speed;
        set => speed = value;
    }

    public int Rotate
    {
        get => rotate;
        set => rotate = value;
    }

    public bool IsLeft
    {
        get { return isLeft; }
        set { isLeft = value; }
    }

    public bool IsRight
    {
        get { return isRight; }
        set { isRight = value; }
    }

    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_MissileManager = GameObject.Find("MissileManager").GetComponent<MissileManager>();
        m_GameUIManager = GameObject.Find("Canvas").GetComponent<GameUIManager>();
        prefab_Multi = Resources.Load<GameObject>("Multi");
    }

    void Update()
    {
        LeftRightRotateMove();
    }

    /// <summary>
    /// 左右按键旋转方向，向前移动
    /// </summary>
    private void LeftRightRotateMove()
    {
        if (!isDeath)
        {
            //保持飞机一直往前
            m_Transform.Translate(Vector3.forward * speed);

            //只需要控制左右方向
            if (isLeft)
            {
                m_Transform.Rotate(Vector3.up * (-1 * rotate));
            }

            if (isRight)
            {
                m_Transform.Rotate(Vector3.up * (1 * rotate));
            }
        }
    }

    /// <summary>
    /// 触发器，飞机碰撞死亡
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        //飞机与边缘四角相撞
        if (other.CompareTag("Border"))
        {
            //死了不能移动
            isDeath = true;
            m_GameUIManager.ShowOverPanel();
        }

        //飞机与导弹相撞
        if (other.CompareTag("Missile"))
        {
            isDeath = true;
            Instantiate(prefab_Multi, m_Transform.position, Quaternion.identity); //播放爆炸特效
            Destroy(other.gameObject); //销毁碰撞到的导弹
            gameObject.SetActive(false); //隐藏飞机
            m_MissileManager.StopCreateMissile(); //停止生产导弹
            m_GameUIManager.ShowOverPanel();
        }

        //飞机与奖励物品碰撞
        if (other.CompareTag("Reward"))
        {
            rewardNum++;
            m_GameUIManager.UpdateStarNum(rewardNum);
            Destroy(other.gameObject);
        }
    }
}