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
    private Button btn_Buy;//购买ship按钮
    private GameObject itemState;//商品购买状态设置
    public int itemPrice;//商品价格
    public int itemId;//商品ID编号

    void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        //使用m_Transform.Find 而不使用GameObject.Find
        //因为：这个是自动创建，当没有创建的时候GameObject.Find是找不到的
        m_shipParent = m_Transform.Find("Model").GetComponent<Transform>();
        text_SpeedNum = m_Transform.Find("Speed/SpeedNum").GetComponent<Text>();
        text_RotateNum = m_Transform.Find("Rotate/RotateNum").GetComponent<Text>();
        text_Price = m_Transform.Find("BuyButton/PriceIcon/Price").GetComponent<Text>();
        btn_Buy = m_Transform.Find("BuyButton").GetComponent<Button>();
        btn_Buy.onClick.AddListener(BuyButtonClick);
        itemState = m_Transform.Find("BuyButton").gameObject;
    }

    /// <summary>
    /// 购买按钮点击事件
    /// </summary>
    private void BuyButtonClick()
    {
        //向父物体的方法，传递当前脚本的对象
        SendMessageUpwards("CalcItemPrice",this);
    }

    /// <summary>
    /// 购买成功，隐藏UI按钮
    /// </summary>
    public void BuyEnd()
    {
        itemState.SetActive(false);
    }

    /// <summary>
    /// 单个商品UI赋值
    /// state：1已购买，0未购买
    /// </summary>
    public void SetItemValue(string id,string speed, string rotate, string price,GameObject model,int state)
    {
        //ui元素赋值
        text_SpeedNum.text = speed;
        text_RotateNum.text = rotate;
        text_Price.text = price;
        itemPrice = int.Parse(price);
        itemId = int.Parse(id);
        //状态为1：已购买，隐藏UI
         if (state == 1)
         {
             itemState.SetActive(false);
         }
         // 实例化飞机模型，设置相关细节参数
         GameObject ship = Instantiate(model, m_shipParent);
         //UI层
         Transform trans_Ship = ship.GetComponent<Transform>();
         model.layer = 5;
         trans_Ship.Find("Mesh").gameObject.layer = 5;
         //设置飞机模型缩放信息（基于model父物体，使用local）
         //ship模型缩放大小设置
         if (model.name != "Ship_4")
         {
             trans_Ship.localScale= new Vector3(8f, 8f, 8f);
         }
         else
         {
             trans_Ship.localScale = new Vector3(2.5f, 2.5f, 2.5f);
         }
         trans_Ship.localPosition = new Vector3(0, -40, 0);
    }
}