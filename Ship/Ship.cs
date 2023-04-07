using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private Transform m_Transform;
    private bool isLeft = false; //往左旋转
    private bool isRight = false; //往右旋转

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
        //保持飞机一直往前
        m_Transform.Translate(Vector3.forward);
        
        //只需要控制左右方向
        if (isLeft)
        {
            m_Transform.Rotate(Vector3.up * -1);
        }

        if (isRight)
        {
            m_Transform.Rotate(Vector3.up * 1);
        }
    }
}