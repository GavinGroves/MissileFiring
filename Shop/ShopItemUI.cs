using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 商城item(商品)控制器
/// </summary>
public class ShopItemUI : MonoBehaviour
{
    private Transform m_Transform;
    private Transform m_shipParent; //飞机模型父物体
    private Text text_SpeedNum;
    private Text text_RotateNum;
    private Text text_Price;

    void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_shipParent = m_Transform.Find("Model").GetComponent<Transform>();
        text_SpeedNum = m_Transform.Find("Speed/SpeedNum").GetComponent<Text>();
        text_RotateNum = m_Transform.Find("Rotate/RotateNum").GetComponent<Text>();
        text_Price = m_Transform.Find("BuyButton/PriceIcon/Price").GetComponent<Text>();
    }

    /// <summary>
    /// 单个商品UI赋值
    /// </summary>
    public void SetItemValue(string speed, string rotate, string price,GameObject model)
    {
        //ui元素赋值
        text_SpeedNum.text = speed;
        text_RotateNum.text = rotate;
        text_Price.text = price;
        // 实例化飞机模型，设置相关细节参数
         Instantiate(model, m_shipParent);
    }
}