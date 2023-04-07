using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Control;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonControl : MonoBehaviour
{
    private Transform m_Transform;
    private Ship m_Ship;

    private GameObject leftButton;
    private GameObject rightButton;

    private bool isPress = true; //检测是否按下

    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Ship = GameObject.FindGameObjectWithTag("Player").GetComponent<Ship>();
        buttonEventInit();
    }

    #region 左右按键控制方向

    /// <summary>
    /// 初始化绑定左右按键 EventTriggerListener的使用
    /// </summary>
    private void buttonEventInit()
    {
        leftButton = m_Transform.Find("Left").gameObject;
        rightButton = m_Transform.Find("Right").gameObject;
        
        EventTriggerListener.Get(leftButton).onDown += LeftPressDown;
        EventTriggerListener.Get(leftButton).onUp += LeftPressUp;
        EventTriggerListener.Get(rightButton).onDown += RightPressDown;
        EventTriggerListener.Get(rightButton).onUp += RightPressUp;
    }

    private void LeftPressUp(GameObject go)
    {
        m_Ship.IsLeft = false;
        Debug.Log("松开Left");
    }

    private void LeftPressDown(GameObject go)
    {
        m_Ship.IsLeft = true;
        Debug.Log("按下Left");
    }

    private void RightPressUp(GameObject go)
    {
        m_Ship.IsRight = false;
        Debug.Log("松开Right");
    }

    private void RightPressDown(GameObject go)
    {
        m_Ship.IsRight = true;
        Debug.Log("按下Right");
    }

    #endregion
}